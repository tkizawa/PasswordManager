using System.Text.Json;
using System.Text.Json.Serialization;

namespace PasswordManagerApp.Services;

public sealed class UserSettings
{
    public int WindowLeft { get; set; }
    public int WindowTop { get; set; }
    public int WindowWidth { get; set; }
    public int WindowHeight { get; set; }
    public FormWindowState WindowState { get; set; } = FormWindowState.Normal;
    public List<int> ColumnWidths { get; set; } = new List<int> { 180, 180, 160 };
}

public static class UserSettingsManager
{
    private static string GetSettingsFilePath()
    {
        var appFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WoodStream PasswordManager");
        var legacyFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PasswordManagerApp");

        var newPath = Path.Combine(appFolder, "settings.json");
        var legacyPath = Path.Combine(legacyFolder, "settings.json");

        if (!File.Exists(newPath) && File.Exists(legacyPath))
        {
            Directory.CreateDirectory(appFolder);
            File.Copy(legacyPath, newPath, true);
        }

        Directory.CreateDirectory(appFolder);
        return newPath;
    }

    public static UserSettings Load()
    {
        var filePath = GetSettingsFilePath();
        if (!File.Exists(filePath))
        {
            return new UserSettings();
        }

        try
        {
            var json = File.ReadAllText(filePath);
            var settings = JsonSerializer.Deserialize<UserSettings>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
            });
            return settings ?? new UserSettings();
        }
        catch
        {
            return new UserSettings();
        }
    }

    public static void Save(UserSettings settings)
    {
        var filePath = GetSettingsFilePath();
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
        };

        var json = JsonSerializer.Serialize(settings, options);
        File.WriteAllText(filePath, json);
    }
}
