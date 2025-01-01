using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Gis.Tool.Apps.Desktop.ViewModels.FeatureItems;

namespace Gis.Tool.Apps.Desktop.Views.FeatureItems;

public partial class RunnerFeatureItemWrapper : UserControl
{
    public RunnerFeatureItemWrapper()
    {
        InitializeComponent();
    }

    private void ToggleDocument_OnClick(object? sender, RoutedEventArgs e)
    {
        CtrlSplitView.IsPaneOpen = !CtrlSplitView.IsPaneOpen;
    }
}