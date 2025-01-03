﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Gis.Tool.Apps.Desktop.Abstractions;
using Gis.Tool.Apps.Desktop.Messages;
using Gis.Tool.Apps.Desktop.ViewModels.FeatureItems;
using Gis.Tool.Apps.Desktop.ViewModels.FeatureItems.GeoJson;
using Gis.Tool.Apps.Desktop.ViewModels.FeatureItems.GeoTiff;
using Gis.Tool.Apps.Desktop.Views.FeatureItems;

namespace Gis.Tool.Apps.Desktop.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        SelectedFixFeatureItem = FixFeatureItems[0];
        SelectedFeatureItemControl = FixFeatureItems[0].View;
        
        WeakReferenceMessenger.Default.Register<MainWindowViewModel, SelectFeatureItemChangedMessage>(this,
            (r, m) =>
            {
                r.SelectedFeatureItemControl = m.Value.View;
                if (!FixFeatureItems.Contains(m.Value))
                    SelectedFixFeatureItem = null;
            });
    }
    
    public List<FixFeatureItemViewModel> FixFeatureItems { get; set; } = [
        new ("主页", "", new HomePage(), "Home"),
        new ("运行", "", new FeatureTaskScheduler(), "Run")
    ];

    public List<FeatureListViewModel> FeatureLists { get; set; } =
    [
        new()
        {
            Id = "0",
            Title = "GeoTiff",
            Description = "GeoTiff 数据处理",
            Icon = "FeatureListGeoTiff",
            FeatureItems =
            {
                new GeoTiffToCogViewModel()
            }
        },
        new()
        {
            Id = "1",
            Title = "GeoJson",
            Description = "",
            Icon = "FeatureListGeoJson",
            FeatureItems =
            {
                new GeoJsonToShpViewModel()
            }
        },
        new()
        {
            Id = "-1",
            Title = "插件",
            Description = "",
            Icon = "FeatureListPlugin",
        },
    ];
   
    [ObservableProperty] public partial Control SelectedFeatureItemControl { get; set; }
    
    [ObservableProperty] public partial FixFeatureItemViewModel? SelectedFixFeatureItem { get; set; }

    partial void OnSelectedFixFeatureItemChanged(FixFeatureItemViewModel? value)
    {
        if(SelectedFixFeatureItem != null)
            WeakReferenceMessenger.Default.Send(new SelectFeatureItemChangedMessage(SelectedFixFeatureItem));
    }
    
    [ObservableProperty] public partial bool SplitViewOpen { get; set; } = true;

    [RelayCommand]
    private void ToggleSplitView()
    {
        SplitViewOpen = !SplitViewOpen;
    }
}