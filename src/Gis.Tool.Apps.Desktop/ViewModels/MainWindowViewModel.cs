using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Gis.Tool.Apps.Desktop.Messages;
using Gis.Tool.Apps.Desktop.Views.FeatureItems;

namespace Gis.Tool.Apps.Desktop.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private static readonly UserControl HomePage = new HomePage();

    public MainWindowViewModel()
    {
        LoadFeatureItems();
        
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

    [ObservableProperty] public partial UserControl SelectedFeatureItemControl { get; set; } = HomePage;

    [RelayCommand]
    private void ToggleSplitView()
    {
        SplitViewOpen = !SplitViewOpen;
    }

    [RelayCommand]
    private void BackToHomePage()
    {
        SelectedFeatureItemControl = HomePage;
        FeatureLists.ForEach(x=>x.SelectedFeatureItem = null);
    }
    
    private void LoadFeatureItems()
    {
        var types = Assembly.GetExecutingAssembly().GetTypes().ToArray();
        var baseType = typeof(FeatureItemViewModelBase);

        foreach (var type in types)
        {
            if (!baseType.IsAssignableFrom(type) || type == baseType)
                continue;
                
            var featureItemViewModel =(Activator.CreateInstance(type) as FeatureItemViewModelBase)!;
            FeatureLists.FirstOrDefault(x => x.Id == featureItemViewModel.PId)
                ?.FeatureItems.Add(featureItemViewModel);
        }
    }
}