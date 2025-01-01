using Avalonia.Platform.Storage;

namespace Gis.Tool.Apps.Desktop.Contracts;

public class StorageModalOptions
{
    public static FilePickerOpenOptions GeoTiffPickerOpenOptions { get; } = new FilePickerOpenOptions()
    {
        Title = "请选择GeoTiff文件",
        FileTypeFilter = [new FilePickerFileType("GeoTiff")
        { 
            Patterns = ["*.tif"]
        }],
        AllowMultiple = false
    };

    public static FilePickerSaveOptions GeoTiffPickerSaveOptions { get; } = new FilePickerSaveOptions()
    {
        Title = "保存GeoTiff文件到",
        FileTypeChoices =
        [
            new FilePickerFileType("GeoTiff")
            {
                Patterns = ["*.tif"]
            }
        ],
        SuggestedFileName = "result.tif",
        DefaultExtension = ".tif"
    };
}