using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Markup.Xaml;
using Gis.Tool.Apps.Desktop.ViewModels.FeatureItems;

namespace Gis.Tool.Apps.Desktop.Views.FeatureItems;

public partial class RunnerFeatureItemWrapper : UserControl
{
    public RunnerFeatureItemWrapper()
    {
        InitializeComponent();
    }
    
    // public static readonly DirectProperty<RunnerFeatrueItemWrapper, RunnerFeatureItemViewModelBase> RunnerFeatureItemProperty =
    //     AvaloniaProperty.RegisterDirect<RunnerFeatrueItemWrapper, RunnerFeatureItemViewModelBase>(
    //         nameof(Thickness),
    //         o => o.RunnerFeatureItem,
    //         (o, v) => o.RunnerFeatureItem = v,
    //         defaultBindingMode: BindingMode.OneWay);
    //
    // private RunnerFeatureItemViewModelBase runnerFeatureItem;
    //
    // public RunnerFeatureItemViewModelBase RunnerFeatureItem
    // {
    //     get => runnerFeatureItem;
    //     set => SetAndRaise(RunnerFeatureItemProperty, ref runnerFeatureItem, value);
    // }
}