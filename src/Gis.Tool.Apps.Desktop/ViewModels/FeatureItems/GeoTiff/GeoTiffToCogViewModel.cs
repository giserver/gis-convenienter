using System;
using System.IO;
using System.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using Gis.Tool.Apps.Desktop.Models;
using Gis.Tool.Apps.Desktop.Views.FeatureItems.GeoTiff;
using Gis.Tool.Libs.Abstractions;
using Gis.Tool.Libs.Options;

namespace Gis.Tool.Apps.Desktop.ViewModels.FeatureItems.GeoTiff;

public partial class GeoTiffToCogViewModel()
    : RunnerFeatureItemViewModelBase("0", "GeoTiff to Cog", "GeoTiff to Cog", typeof(GeoTiff2Cog))
{
    [ObservableProperty] public partial string DestPath { get; set; }

    [ObservableProperty] public partial string[] SrcPaths { get; set; }

    [ObservableProperty] public partial int BlockSize { get; set; }

    [ObservableProperty] public partial COGCompress COGCompress { get; set; }

    protected override FeatureTaskItem CreateProcessTask()
    {
        var progress = new Progress<int>();
        var cancellationToken = new CancellationTokenSource();

        return new FeatureTaskItem(GetRequiredService<IGdalWrapper>()
                .ConvertGeoTiff2COGAsync(
                    new ConvertGeoTiff2COGOptions(Path.GetDirectoryName(DestPath), Path.GetFileName(DestPath), SrcPaths,
                        "EPSG:3857", (uint)BlockSize, COGCompress),
                    progress,
                    cancellationToken.Token),
            cancellationToken, progress, this);
    }
}