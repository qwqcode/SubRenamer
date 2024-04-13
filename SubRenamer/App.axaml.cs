using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public static readonly UpdateService Updater = new UpdateService();
        
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
                Config.ApplyThemeMode(Config.Get().ThemeMode);

                var mainWindowStore = new MainViewModel();
                desktop.MainWindow = new MainWindow
                {
                    DataContext = mainWindowStore,
                };
                desktop.ShutdownRequested += Desktop_ShutdownRequested;
                
                var services = new ServiceCollection();

                services.AddSingleton<IFilesService>(x => new FilesService(desktop.MainWindow));
                services.AddSingleton<IDialogService>(x => new DialogService(desktop.MainWindow));
                services.AddSingleton<IClipboardService>(x => new ClipboardService(desktop.MainWindow));
                services.AddSingleton<IImportService>(x => new ImportService(desktop.MainWindow));
                services.AddSingleton<IRenameService>(x => new RenameService(desktop.MainWindow));

                Services = services.BuildServiceProvider();

                _afterInitTasks(mainWindowStore);
            }

            base.OnFrameworkInitializationCompleted();
        }

        private void ConfigureServices()
        {
            Ioc.Default.ConfigureServices(
                new ServiceCollection()
                    // .AddSingleton<IBrowserService, BrowserService>(_ => new BrowserService())
                    .BuildServiceProvider());
        }
        
        private void Desktop_ShutdownRequested(object? sender, ShutdownRequestedEventArgs e)
        {
            Config.Save();
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
            Current?.Services?.GetService<IDialogService>()?.OpenSettings();
        }

        private static void _afterInitTasks(MainViewModel? mainWindowStore)
        {
            Task.Run(async () =>
            {
                if (!Config.Get().UpdateCheck) return;
                
                await Task.Delay(2000);

                try
                {
                    var updateSrc = await Updater.GetUpdatesAsync();
                    if (updateSrc != null && mainWindowStore != null)
                    {
                        mainWindowStore.CurrVersionText += " (有更新)";
                        mainWindowStore.CurrVersionBtnLink = updateSrc;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            });

            Task.Run(async () =>
            {
                await Task.Delay(2000);
                try
                {
                    #if !DEBUG
                    await Updater.VisitorHit();
                    #endif
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            });
        }
    }
}
