using Ardalis.SmartEnum;
using Gis.Tool.Libs.Abstractions;
using Gis.Tool.Libs.Attributes;

namespace Gis.Tool.Libs.Options;

public sealed class COGCompress(string name, int value, string display)
    : SmartEnum<COGCompress>(name, value)
{
    public static readonly COGCompress LZW = new("LZW", 0, "lzw");
    public static readonly COGCompress JPEG = new("JPEG", 1, "jpeg");
    public static readonly COGCompress DEFLATE = new("DEFLATE", 2, "deflate");
    public static readonly COGCompress WEBP = new("WEBP", 3, "webp");

    public string Display { get; } = display;
}

public record ConvertGeoTiff2COGOptions(
    string DestName,
    string DestDir,
    string[] SrcPaths,
    [GdalParameter("-co", "TARGET_SRS")] string TargetSrs,
    [GdalParameter("-co", "BLOCKSIZE")] uint BlockSize,
    [GdalParameter("-co", "COMPRESS")] COGCompress Compress
) : IOptions;
