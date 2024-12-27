using Gis.Convenienter.Libs.Abstractions;
using Gis.Convenienter.Libs.Implementations;
using Microsoft.Extensions.DependencyInjection;
using OSGeo.GDAL;

namespace Gis.Convenienter.Libs;

public static class Extensions
{
    public static IServiceCollection AddLibs(IServiceCollection services)
    {
        return services.AddSingleton<IGdalWrapper, GdalWrapper>();
    }

    public static void Destory(this Dataset dataset)
    {
        dataset.Close();
        dataset.Dispose();
    }
}
