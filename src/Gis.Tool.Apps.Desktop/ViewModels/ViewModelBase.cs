using System;
using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;

namespace Gis.Tool.Apps.Desktop.ViewModels
{
    public class ViewModelBase : ObservableObject
    {
        public static T? GetService<T>()
        {
            return (Application.Current as App)!.ServiceProvider.GetService<T>();
        }
        
        public static object? GetService(Type serviceType)
        {
            return (Application.Current as App)!.ServiceProvider.GetService(serviceType);
        }

        public static T GetRequiredService<T>() where T : notnull
        {
            return (Application.Current as App)!.ServiceProvider.GetRequiredService<T>();
        }

        public static object GetRequiredService(Type serviceType)
        {
            return (Application.Current as App)!.ServiceProvider.GetRequiredService(serviceType);
        }
    }
}
