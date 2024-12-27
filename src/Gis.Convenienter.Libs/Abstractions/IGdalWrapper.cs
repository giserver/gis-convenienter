using Gis.Convenienter.Libs.Options;

namespace Gis.Convenienter.Libs.Abstractions;

public interface IGdalWrapper
{
    Task ConvertGeoTiff2COGAsync(
        ConvertGeoTiff2COGOptions options,
        IProgress<int>? progressRptr = default,
        CancellationToken cancellationToken = default
    );
}
