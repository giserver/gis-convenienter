using System.Collections.Generic;
using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;

namespace Gis.Tool.Apps.Desktop.Models;

public record FeatureList(string Id, string Title, string Description, string Icon, List<FeatureItem> FeatureItems);