using System;
using System.Threading;
using System.Threading.Tasks;
using Gis.Tool.Apps.Desktop.ViewModels;

namespace Gis.Tool.Apps.Desktop.Models;

public record FeatureTaskItem(Task Task, CancellationTokenSource CancellationTokenSource, Progress<int> Progress, FeatureItemViewModelBase? ViewModel);