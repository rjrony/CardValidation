// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JamesLogEntry.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Logging.Repository
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    ///     The james log entry.
    /// </summary>
    [Table("JamesLogEntry")]
    public partial class JamesLogEntry : ILogEntry
    {
        /// <summary>
        ///     Gets or sets the date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        ///     Gets or sets the exception.
        /// </summary>
        public string Exception { get; set; }

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     Gets or sets the level.
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        ///     Gets or sets the logger.
        /// </summary>
        public string Logger { get; set; }

        /// <summary>
        ///     Gets or sets the machine name.
        /// </summary>
        public string MachineName { get; set; }

        /// <summary>
        ///     Gets or sets the message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     Gets or sets the stack trace.
        /// </summary>
        public string StackTrace { get; set; }

        /// <summary>
        ///     Gets or sets the thread.
        /// </summary>
        public string Thread { get; set; }

        /// <summary>
        ///     Gets or sets the user name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        ///     Gets or sets the work item.
        /// </summary>
        public int? WorkItem { get; set; }

        /// <summary>
        ///     Gets or sets the status.
        /// </summary>
        public short? Status { get; set; }
    }
}