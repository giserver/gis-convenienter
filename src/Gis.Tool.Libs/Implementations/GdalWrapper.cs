using Gis.Tool.Libs.Abstractions;
using Gis.Tool.Libs.Options;
using Microsoft.Extensions.Logging;
using OSGeo.GDAL;

namespace Gis.Tool.Libs.Implementations;

internal class GdalWrapper(ILogger<GdalWrapper> logger) : IGdalWrapper
{
    public Task ConvertGeoTiff2COGAsync(
        ConvertGeoTiff2COGOptions options,
        IProgress<int>? progressRptr = null,
        CancellationToken cancellationToken = default
    )
    {
        var (destName, destDir, srcPaths, _, _, _) = options ?? throw new ArgumentNullException(nameof(options));

        if (!Directory.Exists(destDir))
            throw new DirectoryNotFoundException($"结果文件夹不存在: {destDir}");

        var srcDatasets = new Dataset[srcPaths.Length];
        for (var i = 0; i < srcPaths.Length; i++)
        {
            var srcPath = srcPaths[i];
            if (!File.Exists(srcPath))
            {
                foreach (var dataset in srcDatasets)
                {
                    dataset.Destroy();
                }
                throw new FileNotFoundException("输入文件不存在", srcPath);
            }

            srcDatasets[i] = Gdal.Open(srcPath, Access.GA_ReadOnly);
        }

        return Task.Run(
            () =>
            {
                try
                {
                    var destDataSet = Gdal.Warp(
                        Path.Combine(destDir, destName),
                        srcDatasets,
                        new GDALWarpAppOptions(
                            Utils.ComposeGdalParameters(
                                options,
                                [
                                    "-of",
                                    "COG",
                                    "-co",
                                    "TILING_SCHEME=GoogleMapsCompatible",
                                    "-overwrite",
                                ]
                            )
                        ),
                        (double processing, nint msg, nint data) =>
                        {
                            progressRptr?.Report((int)(processing * 100));
                            return cancellationToken.IsCancellationRequested ? 0 : 1;
                        },
                        null
                    );

                    destDataSet?.Destroy();
                }
                catch (Exception ex)
                {
                    Log.LogProcessErrorMessage(logger, "cog 转换", ex);
                    throw;
                }
                finally
                {
                    foreach (var dataset in srcDatasets)
                    {
                        dataset.Destroy();
                    }
                }
            },
            cancellationToken
        );
    }
}
