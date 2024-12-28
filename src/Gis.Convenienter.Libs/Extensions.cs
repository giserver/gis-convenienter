using Gis.Convenienter.Libs.Abstractions;
using Gis.Convenienter.Libs.Implementations;
using Microsoft.Extensions.DependencyInjection;
using OSGeo.GDAL;

namespace Gis.Convenienter.Libs;

public static class Extensions
{
    public static IServiceCollection AddLibs(this IServiceCollection services)
    {
        return services
            .AddSingleton<IGdalWrapper, GdalWrapper>()
            .AddSingleton<IProgress<double>>(new Progress<double>());
    }

    public static void Destroy(this Dataset dataset)
    {
        dataset.Close();
        dataset.Dispose();
    }
}
