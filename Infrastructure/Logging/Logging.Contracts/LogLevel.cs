// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LogLevel.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Logging.Contracts
{
    /// <summary>
    ///     The log level.
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        ///     The trace.
        /// </summary>
        Trace,

        /// <summary>
        ///     The debug.
        /// </summary>
        Debug,

        /// <summary>
        ///     The info.
        /// </summary>
        Info,

        /// <summary>
        ///     The warn.
        /// </summary>
        Warn,

        /// <summary>
        ///     The error.
        /// </summary>
        Error,

        /// <summary>
        ///     The fatal.
        /// </summary>
        Fatal
    }
}