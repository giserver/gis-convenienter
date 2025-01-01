using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Platform.Storage;

namespace Gis.Tool.Apps.Desktop.Views.Components;

public partial class OpenFilePicker : UserControl
{
    public OpenFilePicker()
    {
        InitializeComponent();
    }
    
    public static readonly DirectProperty<OpenFilePicker, string> FilePathProperty =
        AvaloniaProperty.RegisterDirect<OpenFilePicker, string>
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

    public static readonly DirectProperty<OpenFilePicker, FilePickerOpenOptions> FilePickerOptionsProperty = 
        AvaloniaProperty.RegisterDirect<OpenFilePicker, FilePickerOpenOptions>
        (
            nameof(FilePickerOptions),
            o => o.FilePickerOptions,
            (o, v) => o.FilePickerOptions = v
        );
    
    public FilePickerOpenOptions FilePickerOptions
    {
        get => field ??= new FilePickerOpenOptions();
        set => SetAndRaise(FilePickerOptionsProperty, ref field, value);
    }

    private async void PickFile_OnClick(object? sender, RoutedEventArgs e)
    {
        var result =  await TopLevel.GetTopLevel(this)!.StorageProvider.OpenFilePickerAsync(FilePickerOptions);
        if (result.Any())
        {
            FilePath = result[0].Path.LocalPath;
        }
    }
}