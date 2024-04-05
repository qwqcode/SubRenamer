using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.DependencyInjection;
using SubRenamer.ViewModels;
using SubRenamer.Views;
using Microsoft.Extensions.DependencyInjection;
using SubRenamer.Model;
using SubRenamer.Services;

namespace SubRenamer
{
    public partial class App : Application
    {
        public App()
        {
            ConfigureServices();
        }
        
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                // Load user config
                Config.Load();
                
                // load theme
                Config.ApplyThemeMode(Config.ThemeMode);
                
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(),
                };
                desktop.ShutdownRequested += Desktop_ShutdownRequested;
                
                var services = new ServiceCollection();

                services.AddSingleton<IFilesService>(x => new FilesService(desktop.MainWindow));
                services.AddSingleton<IDialogService>(x => new DialogService(desktop.MainWindow));

                Services = services.BuildServiceProvider();
            }

            base.OnFrameworkInitializationCompleted();
        }

        private void ConfigureServices()
        {
            Ioc.Default.ConfigureServices(
                new ServiceCollection()
                    .AddSingleton<IBrowserService, BrowserService>(_ => new BrowserService())
                    .BuildServiceProvider());
        }
        
        private void Desktop_ShutdownRequested(object? sender, ShutdownRequestedEventArgs e)
        {
            _ = Config.SaveAsync();
        }

        public new static App? Current => Application.Current as App;
        
        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
        /// </summary>
        public IServiceProvider? Services { get; private set; }

        private void MenuQuit_OnClick(object? sender, EventArgs e)
        {
            if (Application.Current is { ApplicationLifetime: IClassicDesktopStyleApplicationLifetime lifetime })
            {
                lifetime.TryShutdown();
            }
            else if(Application.Current is {ApplicationLifetime: IControlledApplicationLifetime controlledLifetime})
            {
                controlledLifetime.Shutdown();
            }
        }

        private void MenuSetting_OnClick(object? sender, EventArgs e)
        {
            Current?.Services?.GetService<IDialogService>()!.OpenSettings();
        }
    }
}
