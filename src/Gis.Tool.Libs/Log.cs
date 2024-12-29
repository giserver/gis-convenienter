using Microsoft.Extensions.Logging;

namespace Gis.Tool.Libs;

internal partial class Log
{
    [LoggerMessage(Level = LogLevel.Error, Message = "处理程序：{ProcessName} 出错")]
    public static partial void LogProcessErrorMessage(
        ILogger logger,
        string processName,
        Exception exception
    );
}
