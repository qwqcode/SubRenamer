using System.Threading.Tasks;

namespace SubRenamer.Model;

public interface IClipboardService
{
    public Task CopyToClipboard(string content);
}