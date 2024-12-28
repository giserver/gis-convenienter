using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gis.Convenienter.Apps.Desktop.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace Gis.Convenienter.Apps.Desktop.ViewModels
{
    [RegisterService(ServiceLifetime.Singleton)]
    public partial class MainWindowViewModel : ViewModelBase
    {
        [ObservableProperty] private double _progressValue = 0;
        
        [ObservableProperty] private bool _progressVisible = false;
    }
}