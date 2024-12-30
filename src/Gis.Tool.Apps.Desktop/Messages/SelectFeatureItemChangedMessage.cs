using CommunityToolkit.Mvvm.Messaging.Messages;
using Gis.Tool.Apps.Desktop.Abstractions;
using Gis.Tool.Apps.Desktop.ViewModels;

namespace Gis.Tool.Apps.Desktop.Messages;

public class SelectFeatureItemChangedMessage(IFeatureItem value)
    : ValueChangedMessage<IFeatureItem>(value);