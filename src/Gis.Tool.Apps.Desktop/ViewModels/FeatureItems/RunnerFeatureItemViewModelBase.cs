﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using Gis.Tool.Apps.Desktop.Abstractions;
using Gis.Tool.Apps.Desktop.Models;
using Gis.Tool.Apps.Desktop.Views.FeatureItems;

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

    [field: AllowNull, MaybeNull]
    public Control View
    {
        get
        {
            if (field == null)
            {
                field = new RunnerFeatureItemWrapper()
                {
                    DataContext = this 
                };
            }
            
            return field;
        }
    }

    [field: AllowNull, MaybeNull]
    public Control BusinessView
    {
        get
        {
            if (field == null)
            {
                if (!typeof(UserControl).IsAssignableFrom(userControlType))
                    throw new ArgumentException(nameof(userControlType));
                field = (Activator.CreateInstance(userControlType) as UserControl)!;
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
            return field ??= new RelayCommand(() =>
            {
                Console.WriteLine(this);
            });
        }
    }
}
