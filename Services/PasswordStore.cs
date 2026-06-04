using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using PasswordManagerApp.Models;

namespace PasswordManagerApp.Services;

public sealed class PasswordStore
{
    private readonly string _filePath;
    private byte[] _encryptionKey = Array.Empty<byte>();
    private byte[] _salt = Array.Empty<byte>();
    private readonly MasterPasswordManager _masterPasswordManager;

    public List<PasswordEntry> Entries { get; } = new();

    public PasswordStore(string filePath)
    {
        _filePath = filePath;
        _masterPasswordManager = new MasterPasswordManager();
    }

    /// <summary>
    /// マスターパスワードを使用してストアを初期化
    /// </summary>
    public void Initialize(string masterPassword)
    {
        if (!File.Exists(_filePath))
        {
            // 新規ファイル：新しいSaltを生成して設定
            _salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(_salt);
            }
            _masterPasswordManager.SetMasterPassword(masterPassword);
        }
        else
        {
            // 既存ファイル：保存されたハッシュを検証
            var store = LoadEncryptedStore();
            if (store == null)
            {
                throw new Exception("パスワードストアの読み込みに失敗しました");
            }

            _salt = Convert.FromBase64String(store.Salt);
            _masterPasswordManager.SetMasterPasswordHash(store.MasterPasswordHash);

            if (!_masterPasswordManager.VerifyMasterPassword(masterPassword))
            {
                throw new Exception("マスターパスワードが間違っています");
            }
        }

        // 暗号化キーを生成
        _encryptionKey = MasterPasswordManager.DeriveKeyFromPassword(masterPassword, _salt);
        Load();
    }

    public void Load()
    {
        Entries.Clear();

        if (!File.Exists(_filePath))
        {
            return;
        }

        var store = LoadEncryptedStore();
        if (store?.Entries == null)
        {
            return;
        }

        foreach (var item in store.Entries)
        {
            Entries.Add(new PasswordEntry
            {
                Name = item.Name,
                Account = item.Account,
                Password = Decrypt(item.Password)
            });
        }
    }

    public void Save()
    {
        var encryptedEntries = Entries.Select(entry => new EncryptedPasswordEntry
        {
            Name = entry.Name,
            Account = entry.Account,
            Password = Encrypt(entry.Password)
        }).ToList();

        var store = new EncryptedPasswordStore
        {
            MasterPasswordHash = _masterPasswordManager.GetMasterPasswordHash() ?? string.Empty,
            Salt = Convert.ToBase64String(_salt),
            Entries = encryptedEntries
        };

        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        var json = JsonSerializer.Serialize(store, options);
        Directory.CreateDirectory(Path.GetDirectoryName(_filePath) ?? string.Empty);
        File.WriteAllText(_filePath, json);
    }

    private EncryptedPasswordStore? LoadEncryptedStore()
    {
        if (!File.Exists(_filePath))
        {
            return null;
        }

        var json = File.ReadAllText(_filePath);
        var store = JsonSerializer.Deserialize<EncryptedPasswordStore>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return store;
    }

    public void ImportFromTextFile(string importPath)
    {
        var lines = File.ReadAllLines(importPath);

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            var parts = line.Split(new[] { '\t', ',' }, 3);
            if (parts.Length < 3)
            {
                continue;
            }

            Entries.Add(new PasswordEntry
            {
                Name = parts[0].Trim().Trim('"'),
                Account = parts[1].Trim().Trim('"'),
                Password = parts[2].Trim().Trim('"')
            });
        }

        Save();
    }

    public void ExportToTextFile(string exportPath)
    {
        var lines = Entries.Select(entry => $"{EscapeField(entry.Name)},{EscapeField(entry.Account)},{EscapeField(entry.Password)}");
        Directory.CreateDirectory(Path.GetDirectoryName(exportPath) ?? string.Empty);
        File.WriteAllText(exportPath, string.Join(Environment.NewLine, lines));
    }

    private static string EscapeField(string value)
    {
        if (value.Contains(',') || value.Contains('\t') || value.Contains('"'))
        {
            return $"\"{value.Replace("\"", "\"\"")}\"";
        }

        return value;
    }

    private string Encrypt(string text)
    {
        if (_encryptionKey.Length == 0)
        {
            throw new InvalidOperationException("ストアが初期化されていません");
        }

        var plainBytes = Encoding.UTF8.GetBytes(text);

        using (var aes = Aes.Create())
        {
            aes.Key = _encryptionKey;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (var encryptor = aes.CreateEncryptor())
            {
                var iv = aes.IV;
                var encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

                // IV + 暗号化データを結合
                var result = new byte[iv.Length + encryptedBytes.Length];
                Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                Buffer.BlockCopy(encryptedBytes, 0, result, iv.Length, encryptedBytes.Length);

                return Convert.ToBase64String(result);
            }
        }
    }

    private string Decrypt(string cipherText)
    {
        if (string.IsNullOrEmpty(cipherText) || _encryptionKey.Length == 0)
        {
            return string.Empty;
        }

        var encryptedData = Convert.FromBase64String(cipherText);

        using (var aes = Aes.Create())
        {
            aes.Key = _encryptionKey;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            // IVを抽出（最初の16バイト）
            var iv = new byte[16];
            Buffer.BlockCopy(encryptedData, 0, iv, 0, 16);
            aes.IV = iv;

            // 暗号化されたデータを抽出
            var cipherBytes = new byte[encryptedData.Length - 16];
            Buffer.BlockCopy(encryptedData, 16, cipherBytes, 0, cipherBytes.Length);

            using (var decryptor = aes.CreateDecryptor())
            {
                var plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                return Encoding.UTF8.GetString(plainBytes);
            }
        }
    }
}
