using Avalonia;
using Avalonia.Styling;
using System;
using System.IO;
using SubRenamer.Common;
using SubRenamer.Helper;

namespace SubRenamer;

public partial class Config
{
    public ThemeMode ThemeMode { get; set; } = ThemeMode.Default;
    public bool Backup { get; set; } = true;
    public bool UpdateCheck { get; set; } = true;
    public bool KeepLangExt { get; set; } = false;
    public string VideoExtAppend { get; set; } = "";
    public string SubtitleExtAppend { get; set; } = "";
    
    public MatchMode MatchMode { get; set; } = MatchMode.Diff;
    
    public string VideoRegex { get; set; } = "";
    public string SubtitleRegex { get; set; } = "";
    
    // Manual Match Mode Configs
    public string ManualVideoRegex { get; set; } = "";
    public string ManualSubtitleRegex { get; set; } = "";
    public string ManualVideo { get; set; } = "";
    public string ManualSubtitle { get; set; } = "";
    public string ManualVideoRaw { get; set; } = "";
    public string ManualSubtitleRaw { get; set; } = "";
}

public partial class Config
{
    private static Config _instance = new ();
    public static Config Get() => _instance;
    
    private static string ConfigFileName => "SubRenamer.config.json";
    // @link https://learn.microsoft.com/en-us/dotnet/api/system.environment.specialfolder?view=net-8.0
    // @link https://gist.github.com/DamianSuess/c143ed869e02e002d252056656aeb9bf
    public static string ConfigDir => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), AppName);
    private static string ConfigFilePath => Path.Combine(ConfigDir, ConfigFileName);
    public static string AppName => System.Reflection.Assembly.GetExecutingAssembly().GetName().Name ?? "SubRenamer";
    public static Version AppVersion => new(System.Reflection.Assembly.GetExecutingAssembly().GetName().Version?.ToString(3) ?? "1.0.0");
    
    /**
     * Loads user configs from file
     */
    public static void Load()
    {
        Directory.CreateDirectory(ConfigDir);
        
        if (!File.Exists(ConfigFilePath)) return;

        using (var fs = new FileStream(ConfigFilePath, FileMode.Open))
        {
            var conf = JsonHelper.ParseJsonSync<Config>(fs);
            if (conf != null) _instance = conf;
        }
    }

    /**
     * Write user configs to file
     */
    public static async void Save()
    {
        Directory.CreateDirectory(ConfigDir);
        await JsonHelper.WriteJsonAsync(ConfigFilePath, _instance);
    }
    
    /**
     * Applies theme mode to the app
     */
    public static void ApplyThemeMode(ThemeMode mode)
    {
        if (Application.Current == null) return;

        var themeVariant = mode switch
        {
            ThemeMode.Dark => ThemeVariant.Dark,
            ThemeMode.Light => ThemeVariant.Light,
            _ => ThemeVariant.Default
        };

        Application.Current.RequestedThemeVariant = themeVariant;
    }
}
