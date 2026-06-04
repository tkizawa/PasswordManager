namespace PasswordManagerApp.Models;

public sealed class PasswordEntry
{
    public string Name { get; set; } = string.Empty;
    public string Account { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
