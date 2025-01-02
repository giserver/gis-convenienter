using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using Gis.Tool.Apps.Desktop.Models;
using Gis.Tool.Apps.Desktop.Views.FeatureItems.GeoTiff;
using Gis.Tool.Libs.Abstractions;
using Gis.Tool.Libs.Options;

namespace Gis.Tool.Apps.Desktop.ViewModels.FeatureItems.GeoTiff;

public partial class GeoTiffToCogViewModel()
    : RunnerFeatureItemViewModelBase(
        "0", 
        "GeoTiff to Cog", 
        "gdal关于cog文档 [链接](https://gdal.org/en/stable/drivers/raster/cog.html)", 
        typeof(GeoTiff2Cog))
{
    [ObservableProperty] public partial string DestPath { get; set; }

    [ObservableProperty] public partial string SrcPath { get; set; }

    [ObservableProperty] public partial int BlockSize { get; set; } = 256;
    
    [ObservableProperty] public partial IReadOnlyList<int> BlockSizeOptions { get; set; } = [256,512];

    [ObservableProperty] public partial string CogCompress { get; set; } = COGCompress.WEBP.Name;
    
    [ObservableProperty]
    public partial IReadOnlyList<string> CogCompressOptions { get; set; } = COGCompress.List.Select(x => x.Name).ToList();

    protected override FeatureTaskItem CreateProcessTask()
    {
        var progress = new Progress<int>();
        var cancellationToken = new CancellationTokenSource();

        return new FeatureTaskItem(GetRequiredService<IGdalWrapper>()
                .ConvertGeoTiff2COGAsync(
                    new ConvertGeoTiff2COGOptions(Path.GetDirectoryName(DestPath), Path.GetFileName(DestPath), [SrcPath],
                        "EPSG:3857", (uint)BlockSize, COGCompress.FromName(CogCompress)),
                    progress,
                    cancellationToken.Token),
            cancellationToken, progress, this);
    }
}