using System.Collections.Generic;
using Gis.Tool.Apps.Desktop.ViewModels;

namespace Gis.Tool.Apps.Desktop.Models;

public record FeatureList(string Id, string Title, string Description, string Icon, List<FeatureItemViewModelBase> FeatureItems);