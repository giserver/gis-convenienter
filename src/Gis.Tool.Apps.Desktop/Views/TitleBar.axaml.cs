using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Gis.Tool.Apps.Desktop.ViewModels;

namespace Gis.Tool.Apps.Desktop.Views;

public partial class TitleBar : UserControl
{
    public TitleBar()
    {
        InitializeComponent();
    }

    private void InputElement_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        (DataContext as MainWindowViewModel)!.BackToHomePageCommand.Execute(sender);
    }
}