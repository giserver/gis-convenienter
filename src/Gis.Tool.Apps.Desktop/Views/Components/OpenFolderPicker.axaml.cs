using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Platform.Storage;

namespace Gis.Tool.Apps.Desktop.Views.Components;

public partial class OpenFolderPicker : UserControl
{
    public OpenFolderPicker()
    {
        InitializeComponent();
    }
    
    public static readonly DirectProperty<OpenFolderPicker, string> FolderPathProperty =
        AvaloniaProperty.RegisterDirect<OpenFolderPicker, string>
        (
            nameof(FolderPath),
            o => o.FolderPath,
            (o, v) => o.FolderPath = v
        );

    public string FolderPath
    {
        get;
        set => SetAndRaise(FolderPathProperty, ref field, value);
    }
    
    public static readonly DirectProperty<OpenFolderPicker, FolderPickerOpenOptions> FolderPickerOptionsProperty = 
        AvaloniaProperty.RegisterDirect<OpenFolderPicker, FolderPickerOpenOptions>
        (
            nameof(FolderPickerOptions),
            o => o.FolderPickerOptions,
            (o, v) => o.FolderPickerOptions = v
        );

    public FolderPickerOpenOptions FolderPickerOptions
    {
        get => field ??= new FolderPickerOpenOptions();
        set => SetAndRaise(FolderPickerOptionsProperty, ref field, value);
    }
    
    private async void OpenFolder_OnClick(object? sender, RoutedEventArgs e)
    {
        var result =  await TopLevel.GetTopLevel(this)!.StorageProvider.OpenFolderPickerAsync(FolderPickerOptions);
        if (result.Any())
        {
            FolderPath = result[0].Path.LocalPath;
        }
    }
}