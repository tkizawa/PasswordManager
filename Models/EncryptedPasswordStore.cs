using System.Text.Json.Serialization;

namespace PasswordManagerApp.Models;

/// <summary>
/// 暗号化されたパスワード保存データ
/// </summary>
public sealed class EncryptedPasswordStore
{
    [JsonPropertyName("masterPasswordHash")]
    public string MasterPasswordHash { get; set; } = string.Empty;

    [JsonPropertyName("salt")]
    public string Salt { get; set; } = string.Empty;

    [JsonPropertyName("entries")]
    public List<EncryptedPasswordEntry> Entries { get; set; } = new();
}

/// <summary>
/// 暗号化されたパスワードエントリ
/// </summary>
public sealed class EncryptedPasswordEntry
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("account")]
    public string Account { get; set; } = string.Empty;

    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;
}
