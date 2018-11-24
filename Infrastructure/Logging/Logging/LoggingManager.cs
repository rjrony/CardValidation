// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoggingManager.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Logging
{
    using System;

    using Infrastructure.Logging.Contracts;

    /// <summary>
    ///     The logging manager.
    /// </summary>
    //public static class LoggingManager
    //{
    //    /// <summary>
    //    /// The get logger
    //    /// </summary>
    //    /// <param name="type">
    //    /// Type which is used as name for the logger
    //    /// </param>
    //    /// <returns>
    //    /// The <see cref="ILogger"/>.
    //    /// </returns>
    //    public static ILogger GetLogger(Type type)
    //    {
    //        string loggerName = string.Format("{0}.{1}", type.Namespace, type.Name);
    //        return GetLogger(loggerName);
    //    }

    //    /// <summary>
    //    /// The get logger.
    //    /// </summary>
    //    /// <param name="loggerName">
    //    /// The logger name.
    //    /// </param>
    //    /// <returns>
    //    /// The <see cref="ILogger"/>.
    //    /// </returns>
    //    public static ILogger GetLogger(string loggerName)
    //    {
    //        //return new Logger(loggerName);
    //        return new Logger();
    //    }

    //    /// <summary>
    //    ///     The get logger.
    //    /// </summary>
    //    /// <returns>
    //    ///     The <see cref="ILogger" />.
    //    /// </returns>
    //    public static ILogger GetLogger()
    //    {
    //        return new Logger();
    //    }
    //}
}