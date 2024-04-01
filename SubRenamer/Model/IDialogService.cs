using System.Threading.Tasks;

namespace SubRenamer.Model;

public interface IDialogService
{
    public Task OpenSettings();
    public Task OpenRules();
    public Task OpenItemEdit(MatchItem item);
}