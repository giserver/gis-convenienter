namespace Gis.Convenienter.Libs.Attributes;

[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property)]
internal class GdalParameterAttribute(string key, string? name = default) : Attribute
{
    public string? Name { get; set; } = name;

    public string Key { get; set; } = key;
}
