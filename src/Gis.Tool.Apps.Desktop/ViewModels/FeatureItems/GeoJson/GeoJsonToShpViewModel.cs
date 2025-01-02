using Gis.Tool.Apps.Desktop.Models;
using Gis.Tool.Apps.Desktop.Views.FeatureItems.GeoJson;

namespace Gis.Tool.Apps.Desktop.ViewModels.FeatureItems.GeoJson;

public class GeoJsonToShpViewModel() : RunnerFeatureItemViewModelBase("1", "geojson 转 shp", "", typeof(GeoJsonToShp))
{
    protected override FeatureTaskItem CreateProcessTask()
    {
        throw new System.NotImplementedException();
    }
}