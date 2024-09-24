using System;
using System.Linq;
using Avalonia;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Platform;

namespace SubRenamer.Helper;

public static class I18NHelper
{
    public static readonly string DefaultLanguage = "zh-Hans";
    public static string[] Languages { get; } = ["en-US:English", "zh-Hans:简体中文"];
    public static string[] LanguageNames { get; } = Languages.Select(x => x.Split(":")[0]).ToArray();
    public static string[] LanguageTitles { get; } = Languages.Select(x => x.Split(":")[1]).ToArray();

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