using System;
using Gis.Tool.Apps.Desktop.ViewModels;

namespace Gis.Tool.Apps.Desktop.Attributes;

public class FeatureItemAttribute(string pId, string title, string description, string icon , Type viewModel) : Attribute
{
    public string PId => pId;
    
    public string Title => title;
    
    public string Description => description;

    public string Icon => icon;
    
    public Type ViewModel => viewModel;
    
}