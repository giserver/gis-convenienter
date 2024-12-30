using Avalonia.Controls;

namespace Gis.Tool.Apps.Desktop.Abstractions;

public interface IFeatureItem : ITabItem
{
    Control View { get; }
}