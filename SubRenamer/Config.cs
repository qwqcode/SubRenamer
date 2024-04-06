using Avalonia;
using Avalonia.Controls;
using Avalonia.Styling;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using SubRenamer.Common;
using SubRenamer.Helper;

namespace SubRenamer;

public class Config
{
    public static ThemeMode ThemeMode { get; set; } = ThemeMode.Default;
    public static bool BackupEnabled { get; set; } = true;
    public static string VideoExtAppend { get; set; } = "";
    public static string SubtitleExtAppend { get; set; } = "";
    public static MatchMode MatchMode { get; set; } = MatchMode.Diff;
    public static string VideoRegex { get; set; } = "";
    public static string SubtitleRegex { get; set; } = "";
    
    // Manual Match Mode Configs
    public static string ManualVideoRegex { get; set; } = "";
    public static string ManualSubtitleRegex { get; set; } = "";
    public static string ManualVideo { get; set; } = "";
    public static string ManualSubtitle { get; set; } = "";
    public static string ManualVideoRaw { get; set; } = "";
    public static string ManualSubtitleRaw { get; set; } = "";
    
    /// <summary>
    /// Loads user configs from file.
    /// </summary>
    public static void Load()
    {
        Directory.CreateDirectory(ConfigDir);
        if (LoadUserConfigs() is not IConfiguration items) return;

        ThemeMode = items.GetValue(nameof(ThemeMode), ThemeMode);
        
        BackupEnabled = items.GetValue(nameof(BackupEnabled), BackupEnabled);
        VideoExtAppend = items.GetValue(nameof(VideoExtAppend), VideoExtAppend);
        SubtitleExtAppend = items.GetValue(nameof(SubtitleExtAppend), SubtitleExtAppend);
        
        MatchMode = items.GetValue(nameof(MatchMode), MatchMode);
        
        VideoRegex = items.GetValue(nameof(VideoRegex), VideoRegex);
        SubtitleRegex = items.GetValue(nameof(SubtitleRegex), SubtitleRegex);

        ManualVideoRegex = items.GetValue(nameof(ManualVideoRegex), ManualVideoRegex);
        ManualSubtitleRegex = items.GetValue(nameof(ManualSubtitleRegex), ManualSubtitleRegex);
        ManualVideo = items.GetValue(nameof(ManualVideo), ManualVideo);
        ManualSubtitle = items.GetValue(nameof(ManualSubtitle), ManualSubtitle);
        ManualVideoRaw = items.GetValue(nameof(ManualVideoRaw), ManualVideoRaw);
        ManualSubtitleRaw = items.GetValue(nameof(ManualSubtitleRaw), ManualSubtitleRaw);   
    }

    /// <summary>
    /// Write user configs to file.
    /// </summary>
    public static async Task SaveAsync()
    {
        Directory.CreateDirectory(ConfigDir);

        // metadata
        var metadata = new ExpandoObject();
        _ = metadata.TryAdd("Description", "SubRenamer configuration file");
        _ = metadata.TryAdd("Version", "1.0");

        var settings = new ExpandoObject();
        _ = settings.TryAdd("_Metadata", metadata);
        
        // user configs
        _ = settings.TryAdd(nameof(ThemeMode), ThemeMode);
        
        _ = settings.TryAdd(nameof(BackupEnabled), BackupEnabled);
        _ = settings.TryAdd(nameof(VideoExtAppend), VideoExtAppend);
        _ = settings.TryAdd(nameof(SubtitleExtAppend), SubtitleExtAppend);
        
        _ = settings.TryAdd(nameof(MatchMode), MatchMode);
        _ = settings.TryAdd(nameof(VideoRegex), VideoRegex);
        _ = settings.TryAdd(nameof(SubtitleRegex), SubtitleRegex);

        _ = settings.TryAdd(nameof(ManualVideoRegex), ManualVideoRegex);
        _ = settings.TryAdd(nameof(ManualSubtitleRegex), ManualSubtitleRegex);
        _ = settings.TryAdd(nameof(ManualVideo), ManualVideo);
        _ = settings.TryAdd(nameof(ManualSubtitle), ManualSubtitle);
        _ = settings.TryAdd(nameof(ManualVideoRaw), ManualVideoRaw);
        _ = settings.TryAdd(nameof(ManualSubtitleRaw), ManualSubtitleRaw);
        
        await JsonHelper.WriteJsonAsync(ConfigFilePath, settings);
    }
    
    private static string ConfigFileName => "SubRenamer.config.json";
    // @link https://learn.microsoft.com/en-us/dotnet/api/system.environment.specialfolder?view=net-8.0
    // @link https://gist.github.com/DamianSuess/c143ed869e02e002d252056656aeb9bf
    public static string ConfigDir => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), AppName);
    private static string ConfigFilePath => Path.Combine(ConfigDir, ConfigFileName);
    public static string AppName => System.Reflection.Assembly.GetExecutingAssembly().GetName().Name ?? "SubRenamer";
    public static Version AppVersion => new(System.Reflection.Assembly.GetExecutingAssembly().GetName().Version?.ToString(3) ?? "1.0.0");
    
    private static IConfigurationRoot? LoadUserConfigs()
    {
        return new ConfigurationBuilder()
            .SetBasePath(ConfigDir)
            .AddJsonFile(ConfigFileName, optional: true)
            .Build();
    }

    /// <summary>
    /// Applies theme mode to the app.
    /// </summary>
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
