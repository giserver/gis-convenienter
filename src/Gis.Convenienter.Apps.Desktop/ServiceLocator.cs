using Avalonia;
using Microsoft.Extensions.DependencyInjection;

namespace Gis.Convenienter.Apps.Desktop;

public static class ServiceLocator
{
    public static T? GetService<T>()
    {
        return (Application.Current as App)!.ServiceProvider.GetService<T>();
    }

    public static T GetRequiredService<T>() where T : notnull
    {
        return (Application.Current as App)!.ServiceProvider.GetRequiredService<T>();
    }
}