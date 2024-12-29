namespace Gis.Tool.Libs.Exceptions;

public class CustomException(ExceptionInfo info) : Exception
{
    public ExceptionInfo Info { get; } = info;
}