using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gis.Tool.Apps.Desktop.Attributes;
using Gis.Tool.Apps.Desktop.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Gis.Tool.Apps.Desktop.ViewModels
{
    [RegisterService(ServiceLifetime.Singleton)]
    public partial class MainWindowViewModel : ViewModelBase
    {
        [ObservableProperty] private bool _splitViewOpen = true;

        [ObservableProperty] private List<FeatureList> _featureLists =
        [
            new FeatureList("0", "GeoTiff", "GeoTiff 数据处理", "FeatureListGrid", []),
            new FeatureList("-1", "插件" , "",  "FeatureListPlugin",[])
        ];

        [ObservableProperty] private FeatureItemViewModelBase _selectedFeatureItem = null;

        [RelayCommand]
        private void ToggleSplitView()
        {
            SplitViewOpen = !SplitViewOpen;
        }
    }
}