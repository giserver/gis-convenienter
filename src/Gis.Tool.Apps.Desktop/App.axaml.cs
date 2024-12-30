using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Gis.Tool.Apps.Desktop.ViewModels;
using Gis.Tool.Apps.Desktop.Views;
using Gis.Tool.Libs;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Gis.Tool.Apps.Desktop
{
    public sealed partial class App : Application
    {
        public IServiceProvider ServiceProvider
        {
            get
            {
                if (field == null)
                {
                    var services = new ServiceCollection();
                    services.AddLibs();

                    Log.Logger = new LoggerConfiguration()
                        .WriteTo.Async(c =>
                        {
                            c.File("gis-tool.log");
                        })
                        .CreateLogger();
                    services.AddLogging(builder =>
                    {
                        builder.AddSerilog(Log.Logger, true);
                    });

                    field = services.BuildServiceProvider();
                }

                return field;
            }
        }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            // 如果使用 CommunityToolkit，则需要用下面一行移除 Avalonia 数据验证。
            // 如果没有这一行，数据验证将会在 Avalonia 和 CommunityToolkit 中重复。
            BindingPlugins.DataValidators.RemoveAt(0);

            // var vm = ServiceProvider.GetRequiredService<MainWindowViewModel>();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                // Line below is needed to remove Avalonia data validation.
                // Without this line you will get duplicate validations from both Avalonia and CT
                BindingPlugins.DataValidators.RemoveAt(0);

                var vm = new MainWindowViewModel();
                desktop.MainWindow = new MainWindow { DataContext = vm };

                desktop.Exit += (_, __) =>
                {
                    Log.CloseAndFlush();
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
