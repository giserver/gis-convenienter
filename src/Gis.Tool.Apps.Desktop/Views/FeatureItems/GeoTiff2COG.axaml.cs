using Avalonia.Controls;
using Gis.Tool.Apps.Desktop.Attributes;
using Gis.Tool.Apps.Desktop.ViewModels.FeatureItems;

namespace Gis.Tool.Apps.Desktop.Views.FeatureItems;

[FeatureItem("0", "GeoTiff 转 COG", "", "", typeof(GeoTiffToCogViewModel))]
public partial class GeoTiff2Cog : UserControl
{
    public GeoTiff2Cog()
    {
        InitializeComponent();
    }
}