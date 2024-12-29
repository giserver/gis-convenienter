using System;
using Microsoft.Extensions.DependencyInjection;

namespace Gis.Tool.Apps.Desktop.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class RegisterServiceAttribute (ServiceLifetime lifetime, Type? implementationType = null): Attribute
{
    public ServiceLifetime Lifetime { get; } = lifetime;

    public Type? ImplementationType { get; } = implementationType;
}