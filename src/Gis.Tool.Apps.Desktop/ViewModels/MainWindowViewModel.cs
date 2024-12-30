using System.Collections.Generic;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Gis.Tool.Apps.Desktop.Attributes;
using Gis.Tool.Apps.Desktop.Messages;
using Gis.Tool.Apps.Desktop.Views.FeatureItems;
using Microsoft.Extensions.DependencyInjection;

namespace Gis.Tool.Apps.Desktop.ViewModels;

[RegisterService(ServiceLifetime.Singleton)]
public partial class MainWindowViewModel : ViewModelBase
{
    private  static readonly UserControl _homePage = new HomePage();

    public MainWindowViewModel()
    {
        WeakReferenceMessenger.Default.Register<MainWindowViewModel, SelectFeatureItemChangedMessage>(this,
            (r, m) => { r.SelectedFeatureItemControl = m.Value.Control; });
    }

    [ObservableProperty] public partial bool SplitViewOpen { get; set; } = true;

    [ObservableProperty]
    public partial List<FeatureListViewModel> FeatureLists { get; set; } =
    [
        new()
        {
            Id = "0",
            Title = "GeoTiff",
            Description = "GeoTiff 数据处理",
            Icon = "FeatureListGeoTiff",
        },
        new()
        {
            Id = "1",
            Title = "GeoJson",
            Description = "",
            Icon = "FeatureListGeoJson",
        },
        new()
        {
            Id = "-1",
            Title = "插件",
            Description = "",
            Icon = "FeatureListPlugin",
        },
    ];

    [ObservableProperty] public partial UserControl SelectedFeatureItemControl { get; set; } = _homePage;

    [RelayCommand]
    private void ToggleSplitView()
    {
        SplitViewOpen = !SplitViewOpen;
    }

    [RelayCommand]
    private void BackToHomePage()
    {
        SelectedFeatureItemControl = _homePage;
        FeatureLists.ForEach(x=>x.SelectedFeatureItem = null);
    }
}