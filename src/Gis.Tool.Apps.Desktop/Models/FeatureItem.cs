using Avalonia.Controls;
using Avalonia.Media.Imaging;

namespace Gis.Tool.Apps.Desktop.Models;

public record FeatureItem(string PId, string Title, string Description, string Icon, UserControl Control);