namespace Gis.Tool.Apps.Desktop.Abstractions;

public interface ITabItem
{
    /// <summary>
    /// 名字，标题
    /// </summary>
    string Title { get; }
    
    /// <summary>
    /// 描述
    /// </summary>
    string Description { get; }
}