using CommunityToolkit.Mvvm.Messaging.Messages;
using Gis.Tool.Apps.Desktop.ViewModels;

namespace Gis.Tool.Apps.Desktop.Messages;

public class SelectFeatureItemChangedMessage(FeatureItemViewModelBase value)
    : ValueChangedMessage<FeatureItemViewModelBase>(value);