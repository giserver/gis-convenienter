using System.Reflection;
using Gis.Convenienter.Libs.Abstractions;
using Gis.Convenienter.Libs.Attributes;

namespace Gis.Convenienter.Libs;

public static class Utils
{
    public static string[] ComposeGdalParameters(
        IOptions options,
        IEnumerable<string>? extra = default
    )
    {
        var results = new List<string>();

        foreach (var prop in options.GetType().GetProperties())
        {
            var attr = prop.GetCustomAttribute<GdalParameterAttribute>();
            if (attr != null)
            {
                var value = prop.GetValue(options);
                if (value is bool vb)
                {
                    if (vb)
                        results.Add(attr.Key);
                }
                else if (value is not null)
                {
                    var valStr = value.ToString()!;
                    if (attr.Name is not null)
                        valStr = attr.Name + "=" + valStr;

                    results.Add(attr.Key);
                    results.Add(valStr);
                }
            }
        }

        if (extra != null)
        {
            results.AddRange(extra);
        }

        return [.. results];
    }
}
