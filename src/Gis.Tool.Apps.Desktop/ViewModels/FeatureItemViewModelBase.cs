using System;
using Avalonia.Controls;
using Gis.Tool.Apps.Desktop.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace Gis.Tool.Apps.Desktop.ViewModels;

[RegisterService(ServiceLifetime.Singleton)]
public abstract class FeatureItemViewModelBase(string pId, string title, string description, Type userControlType ) : ViewModelBase
{
    public string PId { get; } = pId;
    
    public  string Title { get; } = title;
    
    public  string Description { get; } = description;
    public  Type UserControlType { get; } = userControlType;
    
    public UserControl Control {
        get
        {
            if(!typeof(UserControl).IsAssignableFrom(UserControlType)) 
                throw new ArgumentException(nameof(UserControlType));
            
            return (Activator.CreateInstance(userControlType) as UserControl)!;
        }
    }
}