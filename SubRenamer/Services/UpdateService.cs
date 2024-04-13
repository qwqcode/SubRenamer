using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Avalonia.Controls;
using SubRenamer.Helper;

namespace SubRenamer.Services;

public class UpdateService
{
    public async Task<bool> VisitorHit()
    {
        const string url = "https://hits.dwyl.com/qwqcode/SubRenamer.json";

        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue
        {
            NoCache = true,
        };
        httpClient.DefaultRequestHeaders.Add("User-Agent", $"SubRenamer/{Config.AppVersion}");
        
        var response = await httpClient.GetAsync(url);
        return response.IsSuccessStatusCode;
    }
    
    public async Task<string?> GetUpdatesAsync()
    {
        const string url = "https://api.github.com/repos/qwqcode/SubRenamer/releases/latest";

        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue
        {
            NoCache = true,
        };
        httpClient.DefaultRequestHeaders.Add("User-Agent", $"SubRenamer/{Config.AppVersion}");
        var response = await httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode) return null;

        var jsonResponse = await response.Content.ReadAsStreamAsync();
        var latestRelease = await JsonHelper.ParseJsonAsync<GitHubRelease>(jsonResponse);
        if (latestRelease == null) return null;

        var newVersion = new Version(latestRelease.TagName.TrimStart('v'));
        
        var currentVersion = Config.AppVersion;
        if (newVersion > currentVersion)
        {
            var downloadLink = ExtractPlatformAssetItem(latestRelease.Assets);
            return downloadLink?.BrowserDownloadUrl ?? null;
        }

        return null;
    }

    private GitHubReleaseAsset? ExtractPlatformAssetItem(IEnumerable<GitHubReleaseAsset> assets)
    {
        string os = GetOSName();
        string arch = GetArchName();
        
        string targetAssetName = $"{os}_{arch}";
        
        var targetAsset = assets.FirstOrDefault(asset => asset.Name.Contains(targetAssetName));
        return targetAsset ?? null;
    }

    private static string GetOSName()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return "windows";
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) return "macos";
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) return "linux";
        else return "unknown";
    }

    private static string GetArchName()
    {
        return RuntimeInformation.OSArchitecture switch
        {
            Architecture.X64 => "amd64",
            Architecture.Arm64 => "arm64",
            _ => "unknown"
        };
    }
}

public class GitHubRelease
{
    [JsonPropertyName("tag_name")]
    public string TagName { get; set; } = "";
    
    [JsonPropertyName("name")]
    public string Name { get; set; } = "";
    
    [JsonPropertyName("html_url")]
    public string HtmlUrl { get; set; } = "";

    [JsonPropertyName("assets")]
    public List<GitHubReleaseAsset> Assets { get; set; } = new ();
}

public class GitHubReleaseAsset
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("browser_download_url")]
    public string BrowserDownloadUrl { get; set; } = "";
}