using System.Threading.Tasks;

namespace SubRenamer.Model;

public interface IBrowserService
{
    Task OpenBrowserAsync(System.Uri uri, bool external = false);
}