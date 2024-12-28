namespace Gis.Convenienter.Libs.Exceptions;

public class CustomException(ExceptionInfo info) : Exception
{
    public ExceptionInfo Info { get; } = info;
}