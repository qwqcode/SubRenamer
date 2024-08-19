using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using SubRenamer.Helper;

namespace SubRenamer.ViewModels;

public partial class ConflictViewModel : ViewModelBase
{
    /**
     * Mapping from user-friendly options to their actual values
     */
    private readonly Dictionary<string, string> _label2Key;
    
    /**
     * Options for the user to choose from
     */
    [ObservableProperty] private ObservableCollection<string> _labels;

    /**
     * The currently selected option
     */
    [ObservableProperty] private string _selectedLabel = "";
    public string? GetResult()
    {
        // Get the actual value from the user-friendly option
        _label2Key.TryGetValue(SelectedLabel, out var result);
        return result;
    }

    public ConflictViewModel(List<string> options)
    {
        _label2Key = GetFriendlyOptionsDictionary(options);
        _labels = new ObservableCollection<string>(_label2Key.Keys.OrderBy((x) => !x.Contains('(')).ThenBy(i => i));
    }
    
    public static Dictionary<string, string> GetFriendlyOptionsDictionary(IEnumerable<string> options)
    {
        // var lang2Info = CultureInfo.GetCultures(CultureTypes.NeutralCultures).ToDictionary(c => c.Name, c => c);

        var dict = new Dictionary<string, string>();
        foreach (var k in options)
        {
            var splitParts = k.Split([".", "_", " "], StringSplitOptions.RemoveEmptyEntries & StringSplitOptions.TrimEntries);
            
            var nativeName = splitParts
                .Select(LocalizationHelper.GetNativeName)
                .FirstOrDefault(x => x != null);

            dict[!string.IsNullOrEmpty(nativeName) ? $"{nativeName} ({k})" : k] = k;
        }
        
        return dict;
    }
}