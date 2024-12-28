using Ardalis.SmartEnum;

namespace Gis.Convenienter.Libs.Exceptions;

public class ExceptionInfo (string name ,int value, string? detail) : SmartEnum<ExceptionInfo>(name,value)
{
    public string? Detail { get; } = detail;
}