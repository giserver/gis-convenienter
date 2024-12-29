using Gis.Tool.Libs.Options;

namespace Gis.Tool.Libs.Abstractions;

public interface IGdalWrapper
{
    Task ConvertGeoTiff2COGAsync(
        ConvertGeoTiff2COGOptions options,
        IProgress<int>? progressRptr = default,
        CancellationToken cancellationToken = default
    );
}
