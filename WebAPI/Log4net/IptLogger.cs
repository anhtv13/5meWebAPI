using System;
using System.Collections.Generic;
using System.Text;
using log4net;
using log4net.Config;

namespace WebApi.Log4net
{
    public delegate void LoggerEventHandler(string message);
    public delegate void LoggerEventHandlerEx(string message, Exception ex);

    public class IptLogger
    {
        private static event LoggerEventHandler m_notifyDebug;
        private static event LoggerEventHandler m_notifyInfo;
        private static event LoggerEventHandler m_notifyWarn;
        private static event LoggerEventHandler m_notifyError;
        private static event LoggerEventHandler m_notifyFatal;

        private static event LoggerEventHandlerEx m_notifyDebugEx;
        private static event LoggerEventHandlerEx m_notifyInfoEx;
        private static event LoggerEventHandlerEx m_notifyWarnEx;
        private static event LoggerEventHandlerEx m_notifyErrorEx;
        private static event LoggerEventHandlerEx m_notifyFatalEx;

        log4net.ILog log;
        public IptLogger(Type loggerType)
        {
            log = log4net.LogManager.GetLogger(loggerType);
        }

        public IptLogger(string loggerName)
        {
            log = log4net.LogManager.GetLogger(loggerName);
        }

        public static void RegisterEvent(ILogger eventLoggerAdapter, LoggerEventType eventType)
        {
            m_notifyFatal -= new LoggerEventHandler(eventLoggerAdapter.OnFatalEventLogger);
            if (LoggerEventType.OnFatalEvent >= eventType || eventType == LoggerEventType.OnAllEvents)
                m_notifyFatal += new LoggerEventHandler(eventLoggerAdapter.OnFatalEventLogger);
            m_notifyError -= new LoggerEventHandler(eventLoggerAdapter.OnErrorEventLogger);
            if (LoggerEventType.OnErrorEvent >= eventType || eventType == LoggerEventType.OnAllEvents)
                m_notifyError += new LoggerEventHandler(eventLoggerAdapter.OnErrorEventLogger);
            m_notifyWarn -= new LoggerEventHandler(eventLoggerAdapter.OnWarnEventLogger);
            if (LoggerEventType.OnWarnEvent >= eventType || eventType == LoggerEventType.OnAllEvents)
                m_notifyWarn += new LoggerEventHandler(eventLoggerAdapter.OnWarnEventLogger);
            m_notifyInfo -= new LoggerEventHandler(eventLoggerAdapter.OnInfoEventLogger);
            if (LoggerEventType.OnInfoEvent >= eventType || eventType == LoggerEventType.OnAllEvents)
                m_notifyInfo += new LoggerEventHandler(eventLoggerAdapter.OnInfoEventLogger);
            m_notifyDebug -= new LoggerEventHandler(eventLoggerAdapter.OnDebugEventLogger);
            if (LoggerEventType.OnDebugEvent >= eventType || eventType == LoggerEventType.OnAllEvents)
                m_notifyDebug += new LoggerEventHandler(eventLoggerAdapter.OnDebugEventLogger);
        }

        public static void RegisterEvent(ILoggerEx eventLoggerAdapter, LoggerEventType eventType)
        {
            m_notifyFatalEx -= new LoggerEventHandlerEx(eventLoggerAdapter.OnFatalEventLogger);
            if (LoggerEventType.OnFatalEvent >= eventType || eventType == LoggerEventType.OnAllEvents)
                m_notifyFatalEx += new LoggerEventHandlerEx(eventLoggerAdapter.OnFatalEventLogger);
            m_notifyErrorEx -= new LoggerEventHandlerEx(eventLoggerAdapter.OnErrorEventLogger);
            if (LoggerEventType.OnErrorEvent >= eventType || eventType == LoggerEventType.OnAllEvents)
                m_notifyErrorEx += new LoggerEventHandlerEx(eventLoggerAdapter.OnErrorEventLogger);
            m_notifyWarnEx -= new LoggerEventHandlerEx(eventLoggerAdapter.OnWarnEventLogger);
            if (LoggerEventType.OnWarnEvent >= eventType || eventType == LoggerEventType.OnAllEvents)
                m_notifyWarnEx += new LoggerEventHandlerEx(eventLoggerAdapter.OnWarnEventLogger);
            m_notifyInfoEx -= new LoggerEventHandlerEx(eventLoggerAdapter.OnInfoEventLogger);
            if (LoggerEventType.OnInfoEvent >= eventType || eventType == LoggerEventType.OnAllEvents)
                m_notifyInfoEx += new LoggerEventHandlerEx(eventLoggerAdapter.OnInfoEventLogger);
            m_notifyDebugEx -= new LoggerEventHandlerEx(eventLoggerAdapter.OnDebugEventLogger);
            if (LoggerEventType.OnDebugEvent >= eventType || eventType == LoggerEventType.OnAllEvents)
                m_notifyDebugEx += new LoggerEventHandlerEx(eventLoggerAdapter.OnDebugEventLogger);
        }

        public void Debug(string message, Exception exception = null)
        {
            if (this.log.IsDebugEnabled)
            {
                this.log.Debug(message);
                EventHelper.FireEventAsync(m_notifyDebug, message);
                EventHelper.FireEventAsync(m_notifyDebugEx, message, exception);
            }
        }

        public void Info(string message, Exception exception = null)
        {
            if (this.log.IsInfoEnabled)
            {
                this.log.Info(message);
                EventHelper.FireEventAsync(m_notifyInfo, message);
                EventHelper.FireEventAsync(m_notifyInfoEx, message, exception);
            }
        }

        public void Warn(string message, Exception exception = null)
        {
            if (this.log.IsErrorEnabled)
            {
                this.log.Warn(message);
                EventHelper.FireEventAsync(m_notifyWarn, message);
                EventHelper.FireEventAsync(m_notifyWarnEx, message, exception);
            }
        }

        public void Error(string message, Exception exception = null)
        {
            if (this.log.IsErrorEnabled)
            {
                this.log.Error(message);
                EventHelper.FireEventAsync(m_notifyError, message);
                EventHelper.FireEventAsync(m_notifyErrorEx, message, exception);
            }
        }

        public void Fatal(string message, Exception exception = null)
        {
            if (log.IsFatalEnabled)
            {
                log.Fatal(message);
                EventHelper.FireEventAsync(m_notifyFatal, message);
                EventHelper.FireEventAsync(m_notifyFatalEx, message, exception);
            }
        }
    }
}
