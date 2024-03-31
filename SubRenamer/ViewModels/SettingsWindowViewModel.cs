using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using SubRenamer.Services;
using Microsoft.Extensions.DependencyInjection;
using SubRenamer.Model;

namespace SubRenamer.ViewModels;

public partial class SettingsWindowViewModel : ViewModelBase
{
    [RelayCommand]
    private async Task OpenLink(string url)
    {
        var service = Ioc.Default.GetService<IBrowserService>();
        if (service is null) throw new NullReferenceException("Missing Browser Service instance.");
        await service.OpenBrowserAsync(new Uri(url));
    }
}