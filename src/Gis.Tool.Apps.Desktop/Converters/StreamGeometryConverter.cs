using System;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace Gis.Tool.Apps.Desktop.Converters;

public class StreamGeometryConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not string || targetType != typeof(Geometry) || Application.Current is null) return null;
        
        return Application.Current.Styles.TryGetResource(value , Application.Current!.ActualThemeVariant,out var geomety) ? geomety : null;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}