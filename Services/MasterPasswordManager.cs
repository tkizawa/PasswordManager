using System.Security.Cryptography;
using System.Text;

namespace PasswordManagerApp.Services;

public sealed class MasterPasswordManager
{
    private string? _masterPasswordHash;

    /// <summary>
    /// マスターパスワードのハッシュ値を設定します
    /// </summary>
    public void SetMasterPassword(string password)
    {
        _masterPasswordHash = HashPassword(password);
    }

    /// <summary>
    /// マスターパスワードを検証します
    /// </summary>
    public bool VerifyMasterPassword(string password)
    {
        if (_masterPasswordHash == null)
        {
            return false;
        }

        var hash = HashPassword(password);
        return hash == _masterPasswordHash;
    }

    /// <summary>
    /// マスターパスワードが設定されているか確認します
    /// </summary>
    public bool IsMasterPasswordSet => _masterPasswordHash != null;

    /// <summary>
    /// マスターパスワードハッシュを取得します
    /// </summary>
    public string? GetMasterPasswordHash()
    {
        return _masterPasswordHash;
    }

    /// <summary>
    /// マスターパスワードハッシュを設定します（ファイル読込用）
    /// </summary>
    public void SetMasterPasswordHash(string hash)
    {
        _masterPasswordHash = hash;
    }

    /// <summary>
    /// パスワードをハッシュ化します（SHA-256）
    /// </summary>
    private static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashedBytes);
    }

    /// <summary>
    /// マスターパスワードから暗号化キーを生成します
    /// </summary>
    public static byte[] DeriveKeyFromPassword(string password, byte[] salt)
    {
        return Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            10000,
            HashAlgorithmName.SHA256,
            32); // AES-256用の32バイト
    }
}
