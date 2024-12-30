using Avalonia.Controls;
using Gis.Tool.Apps.Desktop.Abstractions;

namespace Gis.Tool.Apps.Desktop.ViewModels.FeatureItems;

public class FixFeatureItemViewModel(string title, string description, UserControl view)
    : ViewModelBase, IFixFeatureItem
{
    public string Title { get; } = title;

    public string Description { get; } = description;

    public Control View { get; } = view;
}