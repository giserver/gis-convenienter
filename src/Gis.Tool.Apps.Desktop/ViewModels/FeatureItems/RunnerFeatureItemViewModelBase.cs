using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using Gis.Tool.Apps.Desktop.Abstractions;
using Gis.Tool.Apps.Desktop.Models;

namespace Gis.Tool.Apps.Desktop.ViewModels.FeatureItems;

public abstract class RunnerFeatureItemViewModelBase(
    string pId,
    string title,
    string description,
    Type userControlType
) : ViewModelBase, IRunnerFeatureItem
{
    public string PId { get; } = pId;

    public string Title { get; } = title;

    public string Description { get; } = description;
    public Type UserControlType { get; } = userControlType;

    [field: AllowNull, MaybeNull]
    public UserControl View
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
