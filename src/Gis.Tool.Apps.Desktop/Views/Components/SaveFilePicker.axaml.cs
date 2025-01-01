using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Platform.Storage;

namespace Gis.Tool.Apps.Desktop.Views.Components;

public partial class SaveFilePicker : UserControl
{
    public SaveFilePicker()
    {
        InitializeComponent();
    }
    
    public static readonly DirectProperty<SaveFilePicker, string> FilePathProperty =
        AvaloniaProperty.RegisterDirect<SaveFilePicker, string>
        (
            nameof(FilePath),
            o => o.FilePath,
            (o, v) => o.FilePath = v
        );

    public string FilePath
    {
        get;
        set => SetAndRaise(FilePathProperty, ref field, value);
    }
    
    public static readonly DirectProperty<SaveFilePicker, FilePickerSaveOptions> FilePickerOptionsProperty = 
        AvaloniaProperty.RegisterDirect<SaveFilePicker, FilePickerSaveOptions>
        (
            nameof(FilePickerOptions),
            o => o.FilePickerOptions,
            (o, v) => o.FilePickerOptions = v
        );

    public FilePickerSaveOptions FilePickerOptions
    {
        get => field ??= new FilePickerSaveOptions();
        set => SetAndRaise(FilePickerOptionsProperty, ref field, value);
    }
    
    private async void SaveFile_OnClick(object? sender, RoutedEventArgs e)
    {
        var result =  await TopLevel.GetTopLevel(this)!.StorageProvider.SaveFilePickerAsync(FilePickerOptions);
        if (result != null)
        {
            FilePath = result.Path.LocalPath;
        }
    }
}