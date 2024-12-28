using System;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;

namespace Gis.Convenienter.Apps.Desktop.ViewModels;

public abstract partial class RunnerViewModelBase : ViewModelBase
{
    private readonly MainWindowViewModel
        _mainWindowViewModel = ServiceLocator.GetRequiredService<MainWindowViewModel>();

    private readonly Progress<double> _progress =
        (ServiceLocator.GetRequiredService<IProgress<double>>() as Progress<double>)!;

    protected RunnerViewModelBase()
    {
        _progress.ProgressChanged += HandleProgressChanged;
    }

    ~RunnerViewModelBase()
    {
        _progress.ProgressChanged -= HandleProgressChanged;
    }

    [RelayCommand]
    private async Task RunAsync(CancellationToken cancellationToken)
    {
        _mainWindowViewModel.ProgressValue = 0;
        _mainWindowViewModel.ProgressVisible = true;

        try
        {
            await OnRunAsync(cancellationToken);
        }
        // 异常通知给用户界面
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            _mainWindowViewModel.ProgressValue = 0;
            _mainWindowViewModel.ProgressVisible = false;
        }
    }

    [RelayCommand]
    private void Cancel()
    {
        if (RunCommand.CanBeCanceled)
            RunCommand.Cancel();

        _mainWindowViewModel.ProgressVisible = false;
        _mainWindowViewModel.ProgressValue = 0;
    }

    protected abstract Task OnRunAsync(CancellationToken cancellationToken);

    private void HandleProgressChanged(object? sender, double value)
    {
        _mainWindowViewModel.ProgressValue = value;
    }
}