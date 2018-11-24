// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoggerBase.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Infrastructure.Interception;
using Microsoft.Practices.Unity;

namespace Infrastructure.Logging
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Reflection;
    using System.Text;
    using System.Transactions;

    using Infrastructure.Interception.Contract;
    using Infrastructure.Logging.Contracts;

    using NLog;

    using LogLevel = Infrastructure.Logging.Contracts.LogLevel;

    /// <summary>
    /// The logger base.
    /// </summary>
    /// <typeparam name="TItem">
    /// The type of the item used as log record resp. ExtendedItem
    /// </typeparam>
    public abstract class LoggerBase<TItem> : ILoggerGeneric<TItem>
        where TItem : class
    {
        private readonly IDependencyResolver _dependencyResolver;

        private readonly bool isLoggerNameOverride = false;

        private string rootLoggerName = string.Empty;

        /// <summary>
        ///     Initializes a new instance of the <see cref="LoggerBase{TItem}" /> class.
        /// </summary>
        protected LoggerBase(IDependencyResolver dependencyResolver)
        {
            this._dependencyResolver = dependencyResolver;
            this.DefaultLogger = LogManager.GetLogger(this.GetType().FullName);
            this.rootLoggerName = System.Configuration.ConfigurationManager.AppSettings["RootLoggerName"];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerBase{TItem}"/> class.
        /// </summary>
        /// <param name="defaultLoggerName">
        /// The default logger name.
        /// </param>
        protected LoggerBase(string defaultLoggerName, IDependencyResolver dependencyResolver)
            : this(dependencyResolver)
        {
            this.DefaultLogger = LogManager.GetLogger(defaultLoggerName);
            this.isLoggerNameOverride = true;
        }

        /// <summary>
        ///     Gets or sets the root logger name.
        /// </summary>
        public string RootLoggerName
        {
            get
            {
                return this.rootLoggerName;
            }

            set
            {
                this.rootLoggerName = value;
            }
        }

        /// <summary>
        ///     Gets or sets the default logger.
        /// </summary>
        private NLog.Logger DefaultLogger { get; set; }

        /// <summary>
        /// The debug.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <param name="memberName">
        /// The member Name.
        /// </param>
        /// <param name="fileName">
        /// The file Name.
        /// </param>
        public void Debug(Func<string> message, TItem item, [CallerMemberName] string memberName = null, [CallerFilePath] string fileName = null)
        {
            this.Log(LogLevel.Debug, message, item, memberName, fileName);
        }

        /// <summary>
        /// The debug.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="memberName">
        /// The member Name.
        /// </param>
        /// <param name="fileName">
        /// The file Name.
        /// </param>
        public void Debug(Func<string> message, [CallerMemberName] string memberName = null, [CallerFilePath] string fileName = null)
        {
            this.Log(LogLevel.Debug, message, null, memberName, fileName);
        }

        /// <summary>
        /// The debug.
        /// </summary>
        /// <param name="messageStr">
        /// The message str.
        /// </param>
        /// <param name="memberName">
        /// The member Name.
        /// </param>
        /// <param name="fileName">
        /// The file Name.
        /// </param>
        public void Debug(string messageStr, [CallerMemberName] string memberName = null, [CallerFilePath] string fileName = null)
        {
            this.Log(LogLevel.Debug, messageStr, null, memberName, fileName);
        }

        /// <summary>
        /// The error.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <param name="memberName">
        /// The member Name.
        /// </param>
        /// <param name="fileName">
        /// The file Name.
        /// </param>
        public void Error(Func<string> message, TItem item, [CallerMemberName] string memberName = null, [CallerFilePath] string fileName = null)
        {
            this.Log(LogLevel.Error, message, item, memberName, fileName);
        }

        /// <summary>
        /// The error.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="memberName">
        /// The member Name.
        /// </param>
        /// <param name="fileName">
        /// The file Name.
        /// </param>
        public void Error(Func<string> message, [CallerMemberName] string memberName = null, [CallerFilePath] string fileName = null)
        {
            this.Log(LogLevel.Error, message, null, memberName, fileName);
        }

        /// <summary>
        /// Log an exception.
        /// </summary>
        /// <param name="exception">
        /// The exception.
        /// </param>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <param name="memberName">
        /// The member Name.
        /// </param>
        /// <param name="fileName">
        /// The file Name.
        /// </param>
        public void Exception(Exception exception, TItem item, [CallerMemberName] string memberName = null, [CallerFilePath] string fileName = null)
        {
            this.Log(LogLevel.Error, () => string.Empty, null, memberName, fileName, exception);
        }

        /// <summary>
        /// Log an exception.
        /// </summary>
        /// <param name="exception">
        /// The exception.
        /// </param>
        /// <param name="memberName">
        /// The member Name.
        /// </param>
        /// <param name="fileName">
        /// The file Name.
        /// </param>
        public void Exception(Exception exception, [CallerMemberName] string memberName = null, [CallerFilePath] string fileName = null)
        {
            this.Log(LogLevel.Error, () => string.Empty, null, memberName, fileName, exception: exception);
        }

        /// <summary>
        /// The fatal.
        /// </summary>
        /// <param name="exception">
        /// The exception.
        /// </param>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <param name="memberName">
        /// The member Name.
        /// </param>
        /// <param name="fileName">
        /// The file Name.
        /// </param>
        public void Fatal(Exception exception, TItem item = null, [CallerMemberName] string memberName = null, [CallerFilePath] string fileName = null)
        {
            this.Log(LogLevel.Fatal, () => string.Empty, null, memberName, fileName, exception);
        }

        /// <summary>
        /// The fatal.
        /// </summary>
        /// <param name="exception">
        /// The exception.
        /// </param>
        /// <param name="memberName">
        /// The member Name.
        /// </param>
        /// <param name="fileName">
        /// The file Name.
        /// </param>
        public void Fatal(Exception exception, [CallerMemberName] string memberName = null, [CallerFilePath] string fileName = null)
        {
            this.Log(LogLevel.Fatal, () => string.Empty, null, memberName, fileName, exception);
        }

        /// <summary>
        /// The fatal.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="memberName">
        /// The member Name.
        /// </param>
        /// <param name="fileName">
        /// The file Name.
        /// </param>
        public void Fatal(Func<string> message, [CallerMemberName] string memberName = null, [CallerFilePath] string fileName = null)
        {
            this.Log(LogLevel.Fatal, message, null, memberName, fileName);
        }

        /// <summary>
        /// The info.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <param name="loggerName">
        /// The logger name.
        /// </param>
        public void Info(Func<string> message, TItem item, [CallerMemberName] string memberName = null, [CallerFilePath] string fileName = null)
        {
            this.Log(LogLevel.Info, message, item, memberName, fileName);
        }

        /// <summary>
        /// The info.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="memberName">
        /// The member Name.
        /// </param>
        /// <param name="fileName">
        /// The file Name.
        /// </param>
        public void Info(Func<string> message, [CallerMemberName] string memberName = null, [CallerFilePath] string fileName = null)
        {
            this.Log(LogLevel.Info, message, null, memberName, fileName);
        }

        /// <summary>
        ///     The is logging enabled.
        /// </summary>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public bool IsLoggingEnabled()
        {
            var logger = this.DefaultLogger;
            return logger.IsDebugEnabled || logger.IsErrorEnabled || logger.IsFatalEnabled || logger.IsInfoEnabled || logger.IsTraceEnabled
                   || logger.IsWarnEnabled;
        }

        /// <summary>
        /// The log.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="logLevel">
        /// The log level.
        /// </param>
        /// <param name="priority">
        /// The priority.
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        /// <param name="memberName">
        /// The member Name.
        /// </param>
        /// <param name="fileName">
        /// The file Name.
        /// </param>
        /// <param name="loggerName">
        /// The logger name.
        /// </param>
        public void Log(
            Func<string> message,
            LogLevel logLevel,
            Priority priority = Priority.None,
            Exception exception = null,
            [CallerMemberName] string memberName = null,
            [CallerFilePath] string fileName = null,
            string loggerName = null)
        {
            if (string.IsNullOrEmpty(fileName) && string.IsNullOrEmpty(memberName))
            {
            }
            else if (!string.IsNullOrEmpty(memberName))
            {
                loggerName = memberName;
            }
            else if (!string.IsNullOrEmpty(fileName))
            {
                loggerName = fileName;
            }
            else
            {
                loggerName = $"{fileName}:{memberName}";
            }
            this.Log(logLevel, message, null, loggerName, null, exception, priority);
        }

        /// <summary>
        /// The log.
        /// </summary>
        /// <param name="logEvent">
        /// The log event.
        /// </param>
        public void Log(LogEvent logEvent)
        {
            this.Log(() => logEvent.Message, logEvent.Level, loggerName: logEvent.LoggerName, exception: logEvent.Exception);
        }

        /// <summary>
        /// The set root logger name.
        /// </summary>
        /// <param name="loggerName">
        /// The logger name.
        /// </param>
        public virtual void SetRootLoggerName(string loggerName)
        {
            this.rootLoggerName = loggerName;
        }

        /// <summary>
        /// The warning.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <param name="memberName">
        /// The member Name.
        /// </param>
        /// <param name="fileName">
        /// The file Name.
        /// </param>
        public void Warning(Func<string> message, TItem item, [CallerMemberName] string memberName = null, [CallerFilePath] string fileName = null)
        {
            this.Log(LogLevel.Warn, message, item, memberName, fileName);
        }

        /// <summary>
        /// The warning.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="memberName">
        /// The member name.
        /// </param>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        public void Warning(Func<string> message, [CallerMemberName] string memberName = null, [CallerFilePath] string fileName = null)
        {
            this.Log(LogLevel.Warn, message, null, memberName, fileName);
        }

        /// <summary>
        /// The get item log text.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <returns>
        /// The <see cref="StringBuilder"/>.
        /// </returns>
        protected abstract StringBuilder GetItemLogText(TItem item);

        /// <summary>
        /// The log.
        /// </summary>
        /// <param name="logLevel">
        /// The log level.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <param name="memberName">
        /// The member Name.
        /// </param>
        /// <param name="fileName">
        /// The file Name.
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        /// <param name="priority">
        /// The priority.
        /// </param>
        protected void Log(
            LogLevel logLevel,
            Func<string> message,
            TItem item = null,
            string memberName = null,
            string fileName = null,
            Exception exception = null,
            Priority priority = Priority.None)
        {
            if (!this.IsLoggingEnabled())
            {
                return;
            }

            if (!this.DefaultLogger.IsEnabled(this.GetNLogLogLevel(logLevel)))
            {
                return;
            }

            string loggerName = "";
            if (!string.IsNullOrEmpty(fileName) && !string.IsNullOrEmpty(memberName))
            {
                loggerName = $"{fileName}:{memberName}";
            }
            else if (!string.IsNullOrEmpty(memberName))
            {
                loggerName = memberName;
            }
            else
            {
                loggerName = fileName;
            }
            var record = this.GetLogRecord(logLevel, message, item, loggerName, exception);
            this.LogUsingNLog(record);
        }

        /// <summary>
        /// The log.
        /// </summary>
        /// <param name="logLevel">
        /// The log level.
        /// </param>
        /// <param name="messageStr">
        /// The message str.
        /// </param>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <param name="memberName">
        /// The member Name.
        /// </param>
        /// <param name="fileName">
        /// The file Name.
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        /// <param name="priority">
        /// The priority.
        /// </param>
        protected void Log(
            LogLevel logLevel,
            string messageStr,
            TItem item = null,
            string memberName = null,
            string fileName = null,
            Exception exception = null,
            Priority priority = Priority.None)
        {
            if (!this.IsLoggingEnabled())
            {
                return;
            }

            if (!this.DefaultLogger.IsEnabled(this.GetNLogLogLevel(logLevel)))
            {
                return;
            }

            string loggerName = "";
            if (!string.IsNullOrEmpty(fileName) && !string.IsNullOrEmpty(memberName))
            {
                loggerName = $"{fileName}:{memberName}";
            }
            else if (!string.IsNullOrEmpty(memberName))
            {
                loggerName = memberName;
            }
            else
            {
                loggerName = fileName;
            }
            var record = this.GetLogRecord(logLevel, messageStr, item, loggerName, exception);
            this.LogUsingNLog(record);
        }

        /// <summary>
        /// The get log record.
        /// </summary>
        /// <param name="logLevel">
        /// The log level.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <param name="loggerName">
        /// The logger name.
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        /// <returns>
        /// The <see cref="LogRecord"/>.
        /// </returns>
        private LogRecord<TItem> GetLogRecord(LogLevel logLevel, Func<string> message, TItem item, string loggerName, Exception exception)
        {
            var logRecord = new LogRecord<TItem>(item, loggerName, logLevel) { Message = message(), Exception = exception, };
            return logRecord;
        }

        private LogRecord<TItem> GetLogRecord(LogLevel logLevel, string messageStr, TItem item, string loggerName, Exception exception)
        {
            return new LogRecord<TItem>(item, loggerName, logLevel) { Message = messageStr, Exception = exception, };
        }

        /// <summary>
        /// The get message.
        /// </summary>
        /// <param name="record">
        /// The record.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetMessage(LogRecord<TItem> record)
        {
            var message = new StringBuilder();

            if (record.ExtendedItem != null)
            {
                var itemLog = this.GetItemLogText(record.ExtendedItem);
                if (itemLog != null)
                {
                    message.Append(itemLog);
                }
            }

            if (!string.IsNullOrWhiteSpace(record.Message))
            {
                message.Append(message.Length > 0 ? " " : string.Empty).Append(record.Message);
            }

            if (record.Exception != null && !string.IsNullOrWhiteSpace(record.Exception.GetBaseException().Message))
            {
                message.Append(message.Length > 0 ? " " : string.Empty).Append(record.Exception.GetBaseException().Message);
            }

            return message.ToString();
        }

        /// <summary>
        ///     The get new logger name.
        /// </summary>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        private string GetNewLoggerNameTemp()
        {
            var stackFrame = new StackFrame(4, false);

            if (stackFrame.GetMethod() != null)
            {
                var declaringType = stackFrame.GetMethod().DeclaringType;
                if (declaringType != null)
                {
                    return declaringType.FullName;
                }
            }

            return this.GetType().Name;
        }



        /// <summary>
        /// The get n log log level.
        /// </summary>
        /// <param name="logLevel">
        /// The log level.
        /// </param>
        /// <returns>
        /// The <see cref="LogLevel"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The passed level is out or range
        /// </exception>
        private NLog.LogLevel GetNLogLogLevel(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Trace:
                    return NLog.LogLevel.Trace;
                case LogLevel.Debug:
                    return NLog.LogLevel.Debug;
                case LogLevel.Info:
                    return NLog.LogLevel.Info;
                case LogLevel.Warn:
                    return NLog.LogLevel.Warn;
                case LogLevel.Error:
                    return NLog.LogLevel.Error;
                case LogLevel.Fatal:
                    return NLog.LogLevel.Fatal;
                default:
                    throw new ArgumentOutOfRangeException("logLevel");
            }
        }

        private void HandleAggregateException(NLog.Logger logger, Exception exception)
        {
            var aggregateException = exception as AggregateException;
            if (aggregateException == null)
            {
                return;
            }

            aggregateException.Handle(
                e =>
                {
                    logger.Log(NLog.LogLevel.Error, e, $"CorrelationId:{this.GetCorrelationId()}, {e.Message}");
                    this.HandleReflectionTypeLoadException(logger, e);
                    return false;
                });
        }

        private void HandleReflectionTypeLoadException(NLog.Logger logger, Exception exception)
        {
            var reflectionTypeLoadException = exception as ReflectionTypeLoadException;
            if (reflectionTypeLoadException == null)
            {
                return;
            }

            var loadExceptions = reflectionTypeLoadException.LoaderExceptions;

            if (loadExceptions != null)
            {
                foreach (var loadException in loadExceptions)
                {
                    logger.Log(NLog.LogLevel.Error, loadException, $"CorrelationId:{this.GetCorrelationId()}, {loadException.Message}");
                }
            }
        }

        /// <summary>
        /// The log using n log.
        /// </summary>
        /// <param name="record">
        /// The record.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// the record must not be null
        /// </exception>
        private void LogUsingNLog(LogRecord<TItem> record)
        {
            var correlationId = this.GetCorrelationId();

            record.Message = $"CorrelationId:{correlationId}, {record.Message}";
            if (record == null)
            {
                throw new ArgumentNullException("record");
            }

            using (var scope = new TransactionScope(TransactionScopeOption.Suppress))
            {
                var logger = this.DefaultLogger;
                if (!this.isLoggerNameOverride)
                {
                    string newLoggerName = record.LoggerName ?? "";
                    if (!string.IsNullOrEmpty(this.rootLoggerName))
                    {
                        newLoggerName = $"{this.rootLoggerName}:{newLoggerName}";
                    }

                    logger = LogManager.GetLogger(newLoggerName);
                }

                if (record.Exception == null)
                {
                    var message = this.GetMessage(record);
                    logger.Log(this.GetNLogLogLevel(record.Level), message);
                }
                else
                {
                    // log exception and innerexception
                    //logger.Log(NLog.LogLevel.Error, record.Exception, record.Exception.Message);
                    logger.Log(NLog.LogLevel.Error, record.Exception, $"CorrelationId:{correlationId}, {record.Exception.Message}");
                    if (record.Exception.InnerException != null)
                    {
                        logger.Log(NLog.LogLevel.Error, record.Exception.InnerException, $"CorrelationId:{correlationId}, {record.Exception.InnerException.Message}");
                    }

                    this.HandleAggregateException(logger, record.Exception);

                    this.HandleReflectionTypeLoadException(logger, record.Exception);
                }

                scope.Complete();
            }
        }


        private string GetCorrelationId()
        {
           // var currentDependencyResolver = this._dependencyResolver.GetCurrentDependencyResolver();
            var operationContext = ServiceLocator.Instance.Current.Resolve<IOperationContext>();
            return operationContext.CorrelationId;
        }
    }
}