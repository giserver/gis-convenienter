using System;
using Gis.Tool.Apps.Desktop.Attributes;
using Gis.Tool.Apps.Desktop.Views.FeatureItems;
using Microsoft.Extensions.DependencyInjection;

namespace Gis.Tool.Apps.Desktop.ViewModels.FeatureItems;

public partial class GeoTiffToCogViewModel() : FeatureItemViewModelBase("0", "GeoTiff to Cog", "GeoTiff to Cog",  typeof(GeoTiff2Cog))
{
}