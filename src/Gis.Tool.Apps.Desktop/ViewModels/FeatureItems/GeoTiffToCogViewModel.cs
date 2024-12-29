using System.Threading;
using System.Threading.Tasks;
using Gis.Tool.Apps.Desktop.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace Gis.Tool.Apps.Desktop.ViewModels.FeatureItems;

[RegisterService(ServiceLifetime.Singleton)]
public partial class GeoTiffToCogViewModel : FeatureItemViewModelBase
{
}