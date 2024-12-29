using System;
using System.Linq;
using System.Reflection;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Gis.Tool.Apps.Desktop.Attributes;
using Gis.Tool.Apps.Desktop.Models;
using Gis.Tool.Apps.Desktop.ViewModels;
using Gis.Tool.Apps.Desktop.Views;
using Gis.Tool.Libs;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Gis.Tool.Apps.Desktop
{
    public sealed partial class App : Application
    {
        private IServiceProvider? _serviceProvider;

        public IServiceProvider ServiceProvider
        {
            get
            {
                if (_serviceProvider == null)
                {
                    var services = new ServiceCollection();
                    services.AddLibs();
                    
                    var types = GetType().Assembly.GetTypes().ToArray();
                    foreach (var type in types)
                    {
                        var attr = type.GetCustomAttribute<RegisterServiceAttribute>();
                        if (attr != null)
                        {
                            if (attr.ImplementationType == null)
                            {
                                _ = attr.Lifetime switch
                                {
                                    ServiceLifetime.Scoped => services.AddScoped(type),
                                    ServiceLifetime.Singleton => services.AddSingleton(type),
                                    ServiceLifetime.Transient => services.AddTransient(type),
                                    _ => throw new ArgumentOutOfRangeException()
                                };
                            }
                            else
                            {
                                _ = attr.Lifetime switch
                                {
                                    ServiceLifetime.Scoped => services.AddScoped(type,attr.ImplementationType),
                                    ServiceLifetime.Singleton => services.AddSingleton(type, attr.ImplementationType),
                                    ServiceLifetime.Transient => services.AddTransient(type, attr.ImplementationType),
                                    _ => throw new ArgumentOutOfRangeException()
                                };
                            }
                        }
                    }
                    
                    Log.Logger = new LoggerConfiguration().WriteTo.Async(c =>
                    {
                        c.File("gis-tool.log");
                    }).CreateLogger();
                    services.AddLogging(builder =>
                    {
                        builder.AddSerilog(Log.Logger,  true);
                    });
                    
                    _serviceProvider = services.BuildServiceProvider();
                }

                return _serviceProvider;
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

            var vm = ServiceProvider.GetRequiredService<MainWindowViewModel>();
            
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                // Line below is needed to remove Avalonia data validation.
                // Without this line you will get duplicate validations from both Avalonia and CT
                BindingPlugins.DataValidators.RemoveAt(0);
                desktop.MainWindow = new MainWindow
                {
                    DataContext = vm,
                };
                LoadFeatureItems(vm);

                desktop.Exit += (_, __) =>
                {
                    Log.CloseAndFlush();
                };
            }

            base.OnFrameworkInitializationCompleted();
        }

        private void LoadFeatureItems(MainWindowViewModel vm)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes().ToArray();

            foreach (var type in types)
            {
                var featureItemAttr = type.GetCustomAttribute<FeatureItemAttribute>();
                if (featureItemAttr != null && typeof(UserControl).IsAssignableFrom(type))
                {
                    var control = (Activator.CreateInstance(type) as UserControl)!;
                    control.DataContext = ServiceProvider.GetRequiredService(featureItemAttr.ViewModel);

                    var featureItem = new FeatureItem(
                        featureItemAttr.PId,
                        featureItemAttr.Title,
                        featureItemAttr.Description,
                        featureItemAttr.Icon,
                        control
                    );
                
                    vm.FeatureLists.FirstOrDefault(x=>x.Id == featureItem.PId)?.FeatureItems.Add(featureItem);
                }
            }
        }
    }
}