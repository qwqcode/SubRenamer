using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SubRenamer.Helper;

public static class DownloadHelper
{
    public static async Task DownloadFileAsync(string url, string path, Action<int, string, string>? onProgress = null, CancellationToken cancellationToken = default)
    {
        using var httpClient = new HttpClient();
        using var response = await httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
        response.EnsureSuccessStatusCode();

        var total = response.Content.Headers.ContentLength;

        await using var content = await response.Content.ReadAsStreamAsync(cancellationToken);
        var totalRead = 0L;
        var buffer = new byte[8192];
        var isMoreToRead = true;

        await using var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true);
        do
        {
            var bytesRead = await content.ReadAsync(buffer, cancellationToken);
            if (bytesRead == 0)
            {
                isMoreToRead = false;
                continue;
            }

            await fileStream.WriteAsync(buffer.AsMemory(0, bytesRead), cancellationToken);
            totalRead += bytesRead;

            if (total.HasValue)
            {
                string totalSize = HumanReadableSize(total.Value);
                string downloadedSize = HumanReadableSize(totalRead);
                Console.WriteLine($"Downloaded {totalRead * 100 / total.Value}%, {downloadedSize}/{totalSize}");
                onProgress?.Invoke((int)(totalRead * 100 / total.Value), downloadedSize, totalSize);
            }
        } while (isMoreToRead);
    }
    
    public static string HumanReadableSize(long size)
    {
        return size switch
        {
            < 1024 => $"{size} B",
            < 1024 * 1024 => $"{size / 1024} KB",
            < 1024 * 1024 * 1024 => $"{size / 1024 / 1024} MB",
            _ => $"{size / 1024 / 1024 / 1024} GB"
        };
    }
}