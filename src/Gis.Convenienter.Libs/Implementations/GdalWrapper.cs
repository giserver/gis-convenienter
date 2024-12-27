using Gis.Convenienter.Libs.Abstractions;
using Gis.Convenienter.Libs.Options;
using Microsoft.Extensions.Logging;
using OSGeo.GDAL;

namespace Gis.Convenienter.Libs.Implementations;

internal class GdalWrapper(ILogger<GdalWrapper> logger) : IGdalWrapper
{
    public Task ConvertGeoTiff2COGAsync(
        ConvertGeoTiff2COGOptions options,
        IProgress<int>? progressRptr = default,
        CancellationToken cancellationToken = default
    )
    {
        var (destName, destDir, srcPaths, _, _, _) = options;

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
                    dataset.Close();
                    dataset.Dispose();
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
                            progressRptr?.Report((int)(processing * 10000));
                            return cancellationToken.IsCancellationRequested ? 0 : 1;
                        },
                        null
                    );

                    destDataSet?.Close();
                    destDataSet?.Dispose();
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
                        dataset.Close();
                        dataset.Dispose();
                    }
                }
            },
            cancellationToken
        );
    }
}
