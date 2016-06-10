using System;

namespace WebApi.Log4net
{
    [Flags]
    public enum LoggerEventType
    {
        OnFatalEvent = 0x05,
        OnErrorEvent = 0x04,
        OnWarnEvent = 0x03,
        OnInfoEvent = 0x02,
        OnDebugEvent = 0x01,
        OnAllEvents = OnFatalEvent | OnErrorEvent | OnWarnEvent | OnInfoEvent | OnDebugEvent
    }

    public interface ILogger
    {
        void OnFatalEventLogger(string message);
        void OnErrorEventLogger(string message);
        void OnWarnEventLogger(string message);
        void OnInfoEventLogger(string message);
        void OnDebugEventLogger(string message);
    }

    public interface ILoggerEx
    {
        void OnFatalEventLogger(string message, Exception ex);
        void OnErrorEventLogger(string message, Exception ex);
        void OnWarnEventLogger(string message, Exception ex);
        void OnInfoEventLogger(string message, Exception ex);
        void OnDebugEventLogger(string message, Exception ex);
    }
}
