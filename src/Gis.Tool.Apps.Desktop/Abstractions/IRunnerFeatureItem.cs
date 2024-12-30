namespace Gis.Tool.Apps.Desktop.Abstractions;

public interface IRunnerFeatureItem : IFeatureItem
{
    /// <summary>
    /// IFeatureList 中的Id
    /// </summary>
    public string PId { get; }
}