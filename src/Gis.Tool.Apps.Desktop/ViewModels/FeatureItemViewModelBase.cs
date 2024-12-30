using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using Gis.Tool.Apps.Desktop.Attributes;
using Gis.Tool.Apps.Desktop.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Gis.Tool.Apps.Desktop.ViewModels;

public abstract class FeatureItemViewModelBase(
    string pId,
    string title,
    string description,
    Type userControlType
) : ViewModelBase
{
    public string PId { get; } = pId;

    public string Title { get; } = title;

    public string Description { get; } = description;
    public Type UserControlType { get; } = userControlType;

    public UserControl Control
    {
        get
        {
            if (field == null)
            {
                if (!typeof(UserControl).IsAssignableFrom(UserControlType))
                    throw new ArgumentException(nameof(UserControlType));
                field = (Activator.CreateInstance(UserControlType) as UserControl)!;
            }
            
            return field;
        }
    }

    protected abstract FeatureTaskItem CreateProcessTask();

    [field: AllowNull, MaybeNull]
    public ICommand RunCommand
    {
        get
        {
            return field ??= new RelayCommand(() => { });
        }
    }
}
