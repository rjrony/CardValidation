// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoggerTraceListener.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Logging
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Text;

    using Infrastructure.Logging.Contracts;

    /// <summary>
    ///     The logger trace listener.
    /// </summary>
    //public class LoggerTraceListener : TraceListener
    //{
    //    private readonly ILogger logger;

    //    /// <summary>
    //    ///     Initializes a new instance of the <see cref="LoggerTraceListener" /> class.
    //    /// </summary>
    //    public LoggerTraceListener()
    //    {
    //        this.logger = LoggingManager.GetLogger();
    //    }

    //    /// <summary>
    //    /// The trace data.
    //    /// </summary>
    //    /// <param name="eventCache">
    //    /// The event cache.
    //    /// </param>
    //    /// <param name="source">
    //    /// The source.
    //    /// </param>
    //    /// <param name="eventType">
    //    /// The event type.
    //    /// </param>
    //    /// <param name="id">
    //    /// The id.
    //    /// </param>
    //    /// <param name="data">
    //    /// The data.
    //    /// </param>
    //    public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
    //    {
    //        this.LogTraceDataAndEvent(source, eventType, data.ToString(), true);
    //        base.TraceData(eventCache, source, eventType, id, data);
    //    }

    //    /// <summary>
    //    /// The trace data.
    //    /// </summary>
    //    /// <param name="eventCache">
    //    /// The event cache.
    //    /// </param>
    //    /// <param name="source">
    //    /// The source.
    //    /// </param>
    //    /// <param name="eventType">
    //    /// The event type.
    //    /// </param>
    //    /// <param name="id">
    //    /// The id.
    //    /// </param>
    //    /// <param name="data">
    //    /// The data.
    //    /// </param>
    //    public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
    //    {
    //        if (data == null)
    //        {
    //            return;
    //        }

    //        var sb = new StringBuilder();
    //        foreach (var tempData in data)
    //        {
    //            if (tempData != null)
    //            {
    //                sb.AppendLine(tempData.ToString());
    //            }
    //        }

    //        this.LogTraceDataAndEvent(source, eventType, sb.ToString(), true);
    //        base.TraceData(eventCache, source, eventType, id, data);
    //    }

    //    /// <summary>
    //    /// The trace event.
    //    /// </summary>
    //    /// <param name="eventCache">
    //    /// The event cache.
    //    /// </param>
    //    /// <param name="source">
    //    /// The source.
    //    /// </param>
    //    /// <param name="eventType">
    //    /// The event type.
    //    /// </param>
    //    /// <param name="id">
    //    /// The id.
    //    /// </param>
    //    /// <param name="message">
    //    /// The message.
    //    /// </param>
    //    public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
    //    {
    //        this.LogTraceDataAndEvent(source, eventType, message, false);
    //        base.TraceEvent(eventCache, source, eventType, id, message);
    //    }

    //    /// <summary>
    //    /// The trace event.
    //    /// </summary>
    //    /// <param name="eventCache">
    //    /// The event cache.
    //    /// </param>
    //    /// <param name="source">
    //    /// The source.
    //    /// </param>
    //    /// <param name="eventType">
    //    /// The event type.
    //    /// </param>
    //    /// <param name="id">
    //    /// The id.
    //    /// </param>
    //    /// <param name="format">
    //    /// The format.
    //    /// </param>
    //    /// <param name="args">
    //    /// The args.
    //    /// </param>
    //    public override void TraceEvent(
    //        TraceEventCache eventCache,
    //        string source,
    //        TraceEventType eventType,
    //        int id,
    //        string format,
    //        params object[] args)
    //    {
    //        string message = string.Format(CultureInfo.CurrentCulture, format, args);
    //        this.LogTraceDataAndEvent(source, eventType, message, false);
    //        base.TraceEvent(eventCache, source, eventType, id, format, args);
    //    }

    //    /// <summary>
    //    /// The write.
    //    /// </summary>
    //    /// <param name="message">
    //    /// The message.
    //    /// </param>
    //    public override void Write(string message)
    //    {
    //    }

    //    /// <summary>
    //    /// The write line.
    //    /// </summary>
    //    /// <param name="message">
    //    /// The message.
    //    /// </param>
    //    public override void WriteLine(string message)
    //    {
    //    }

    //    private void LogTraceDataAndEvent(string source, TraceEventType eventType, string message, bool isTraceData)
    //    {
    //        if (string.IsNullOrEmpty(message))
    //        {
    //            return;
    //        }

    //        switch (eventType)
    //        {
    //            case TraceEventType.Critical:
    //            case TraceEventType.Error:
    //                this.logger.Error(() => message, source);
    //                break;
    //            case TraceEventType.Warning:
    //                this.logger.Warning(() => message, source);
    //                break;
    //            case TraceEventType.Information:
    //                this.logger.Info(() => message, source);
    //                break;
    //            case TraceEventType.Verbose:
    //                this.logger.Debug(() => message, source);
    //                break;
    //            case TraceEventType.Start:
    //                break;
    //            case TraceEventType.Stop:
    //                break;
    //            case TraceEventType.Suspend:
    //                break;
    //            case TraceEventType.Resume:
    //                break;
    //            case TraceEventType.Transfer:
    //                break;
    //            default:
    //                throw new ArgumentOutOfRangeException("eventType");
    //        }
    //    }
    //}
}