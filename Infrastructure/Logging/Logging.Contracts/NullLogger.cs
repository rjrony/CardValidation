// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NullLogger.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Logging.Contracts
{
    using System;
    using System.Runtime.CompilerServices;

    /// <summary>
    ///     The null logger.
    /// </summary>
    public class NullLogger : ILogger
    {
        /// <summary>
        ///     Gets or sets the root logger name.
        /// </summary>
        public string RootLoggerName { get; set; }

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
        }

        /// <summary>
        ///     The is logging enabled.
        /// </summary>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public bool IsLoggingEnabled()
        {
            return true;
        }

        /// <summary>
        /// The log.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="category">
        /// The category.
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
        public void Log(Func<string> message, LogLevel category, Priority priority, Exception exception = null,
            [CallerMemberName] string memberName = null, [CallerFilePath] string fileName = null, string loggerName = null)
        {
        }

        /// <summary>
        /// The log.
        /// </summary>
        /// <param name="logEvent">
        /// The log event.
        /// </param>
        public void Log(LogEvent logEvent)
        {
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
        }
    }
}