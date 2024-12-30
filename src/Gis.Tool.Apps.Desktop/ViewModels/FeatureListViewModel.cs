using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Gis.Tool.Apps.Desktop.Messages;

namespace Gis.Tool.Apps.Desktop.ViewModels;

/// <summary>
/// 功能列表
/// </summary>
public partial class FeatureListViewModel : ViewModelBase
{
    public FeatureListViewModel()
    {
        WeakReferenceMessenger.Default.Register<FeatureListViewModel,SelectFeatureItemChangedMessage>(this, (list, message) =>
        {
            if (list.FeatureItems.Contains(message.Value)) return;
            list.SelectedFeatureItem = null;
        });
    }

    public required string Id { get; init; }

    public required string Title { get; init; }

    public required string Description { get; init; }

    public required string Icon { get; init; }

    public List<FeatureItemViewModelBase> FeatureItems { get; } = [];

    [ObservableProperty] public partial FeatureItemViewModelBase? SelectedFeatureItem { get; set; }

    partial void OnSelectedFeatureItemChanged(FeatureItemViewModelBase? value)
    {
        if (value != null)
            WeakReferenceMessenger.Default.Send(new SelectFeatureItemChangedMessage(value));
    }
}