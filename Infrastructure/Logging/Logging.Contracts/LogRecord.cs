// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LogRecord.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Logging.Contracts
{
    using System;

    /// <summary>
    /// The log record.
    /// </summary>
    /// <typeparam name="TItem">
    /// The type of the item used as log record resp. ExtendedItem
    /// </typeparam>
    public class LogRecord<TItem>
        where TItem : class
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="LogRecord{TItem}" /> class.
        /// </summary>
        public LogRecord()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogRecord{TItem}"/> class.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <param name="loggerName">
        /// The logger name.
        /// </param>
        /// <param name="level">
        /// The level.
        /// </param>
        public LogRecord(TItem item, string loggerName, LogLevel level)
        {
            this.ExtendedItem = item;
            this.LoggerName = loggerName;
            this.Level = level;
        }

        /// <summary>
        ///     Gets or sets the exception.
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        ///     Gets or sets the extended item.
        /// </summary>
        public TItem ExtendedItem { get; set; }

        /// <summary>
        ///     Gets or sets the level.
        /// </summary>
        public LogLevel Level { get; set; }

        /// <summary>
        ///     Gets or sets the logger name.
        /// </summary>
        public string LoggerName { get; set; }

        /// <summary>
        ///     Gets or sets the message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     Gets or sets the time stamp.
        /// </summary>
        public DateTime TimeStamp { get; set; }
    }
}