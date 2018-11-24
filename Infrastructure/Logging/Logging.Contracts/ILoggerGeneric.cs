// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILoggerGeneric.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Logging.Contracts
{
    using System;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// The LoggerGeneric interface.
    /// </summary>
    /// <typeparam name="T">
    /// The item used as LogRecord resp. ExtendedItem
    /// </typeparam>
    public interface ILoggerGeneric<in T> : ILogger
        where T : class
    {
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
        void Debug(Func<string> message, T item = null, [CallerMemberName] string memberName = null, [CallerFilePath] string fileName = null);

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
        void Error(Func<string> message, T item = null, [CallerMemberName] string memberName = null, [CallerFilePath] string fileName = null);

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
        void Exception(Exception exception, T item = null, [CallerMemberName] string memberName = null, [CallerFilePath] string fileName = null);

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
        void Fatal(Exception exception, T item = null, [CallerMemberName] string memberName = null, [CallerFilePath] string fileName = null);

        /// <summary>
        /// The info.
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
        void Info(Func<string> message, T item = null, [CallerMemberName] string memberName = null, [CallerFilePath] string fileName = null);

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
        void Warning(Func<string> message, T item = null, [CallerMemberName] string memberName = null, [CallerFilePath] string fileName = null);
    }
}