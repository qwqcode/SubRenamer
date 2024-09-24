using System;
using System.Globalization;
using System.Linq;
using Avalonia;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Platform;

namespace SubRenamer.Helper;

public static class I18NHelper
{
    public static readonly string DefaultLanguage = "zh-Hans";
    public static string[] Languages { get; } = ["en-US:English", "zh-Hans:简体中文 (Simplified Chinese)", "zh-Hant:繁體中文 (Traditional Chinese)", "ja-JP:日本語 (Japanese)"];
    public static string[] LanguageNames { get; } = Languages.Select(x => x.Split(":")[0]).ToArray();
    public static string[] LanguageTitles { get; } = Languages.Select(x => x.Split(":")[1]).ToArray();

    public static string GetLanguageNameFromOs()
    {
        string tz = TimeZoneInfo.Local.Id.ToLower();
        
        // FIXME: `CultureInfo.CurrentCulture.Name` always return `null` in AvaloniaUI
        var name = CultureInfo.CurrentCulture.Name;
        if (string.IsNullOrWhiteSpace(name)) name = Environment.GetEnvironmentVariable("LANG");

        name = name?.Trim().ToLower().Replace("_", "-") ?? "";
        if (name.Contains("en")) return "en-US";

        if (tz == "est" || tz == "edt" || tz.Contains("america")) return "en-US"; // on macos and linux
        if (tz == "pacific standard time" || tz == "mountain standard time"
            || tz == "central standard time" || tz == "eastern standard time") return "en-US";  // on windows

        if (name.Contains("zh-hans") || name.Contains("zh-cn")) return "zh-Hans";
        if (tz == "prc" || tz == "cst" || tz == "asia/shanghai" || tz.Contains("china")) return "zh-Hans";

        if (name.Contains("zh-hant") || name.Contains("zh-tw") || name.Contains("zh-hk")) return "zh-Hant";
        if (tz == "roc" || tz == "hkt" || tz == "asia/hong_kong" || tz == "asia/taipei" || tz.Contains("taiwan") || tz.Contains("hong kong"))
            return "zh-Hant";

        if (name.Contains("zh")) return "zh-Hans";
        if (name.Contains("ja") || name.Contains("jp") || tz == "JST" || tz == "asia/tokyo" || tz.Contains("japan")) return "ja-JP";

        return DefaultLanguage;
    }

    public static void Translate(
        this Application? app,
        string targetLanguage)
    {
        if (!LanguageNames.Contains(targetLanguage))
        {
            Console.WriteLine($"[I18NHelper.Translate] language \"{targetLanguage}\" not support");
            return;
        }

        var translations = App.Current?.Resources.MergedDictionaries.OfType<ResourceInclude>()
            .FirstOrDefault(x => x.Source?.OriginalString?.Contains("/Lang/") ?? false);
        if (translations != null)
            App.Current?.Resources.MergedDictionaries.Remove(translations);

        var uri = new Uri($"avares://SubRenamer/Assets/Lang/{targetLanguage}.axaml");
        App.Current?.Resources.MergedDictionaries.Add(new ResourceInclude(uri)
        {
            Source = uri
        });
    }
}