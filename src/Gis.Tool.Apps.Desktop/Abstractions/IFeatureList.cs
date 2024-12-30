using System.Collections.Generic;

namespace Gis.Tool.Apps.Desktop.Abstractions;

public interface IFeatureList<T> : ITabItem where T : IFeatureItem
{
    string Id { get; }
    
    string Icon { get; }
    
    List<T> FeatureItems { get; }
}