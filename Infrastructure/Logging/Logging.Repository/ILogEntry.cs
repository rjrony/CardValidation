// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILogEntry.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Logging.Repository
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    ///     The LogEntry interface.
    /// </summary>
    public interface ILogEntry
    {
        /// <summary>
        ///     Gets or sets the date.
        /// </summary>
        DateTime Date { get; set; }

        /// <summary>
        ///     Gets or sets the exception.
        /// </summary>
        string Exception { get; set; }

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        long Id { get; set; }

        /// <summary>
        ///     Gets or sets the level.
        /// </summary>
        [Required]
        [StringLength(50)]
        string Level { get; set; }

        /// <summary>
        ///     Gets or sets the logger.
        /// </summary>
        [StringLength(250)]
        string Logger { get; set; }

        /// <summary>
        ///     Gets or sets the machine name.
        /// </summary>
        [StringLength(250)]
        string MachineName { get; set; }

        /// <summary>
        ///     Gets or sets the message.
        /// </summary>
        string Message { get; set; }

        /// <summary>
        ///     Gets or sets the stack trace.
        /// </summary>
        [StringLength(250)]
        string StackTrace { get; set; }

        /// <summary>
        ///     Gets or sets the thread.
        /// </summary>
        [StringLength(50)]
        string Thread { get; set; }

        /// <summary>
        ///     Gets or sets the user name.
        /// </summary>
        [StringLength(250)]
        string UserName { get; set; }

        /// <summary>
        ///     Gets or sets the work item.
        /// </summary>
        int? WorkItem { get; set; }

        /// <summary>
        ///     Gets or sets the status.
        /// </summary>
        short? Status { get; set; }
    }
}