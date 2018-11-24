// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomDbFormatter.cs" company="">
//   Copyright © 2018. All rights reserved.
// </copyright>
// <summary>
//   The custom db formatter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Repository
{
    using System;
    using System.Data.Common;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure.Interception;

    /// <summary>
    /// The custom db formatter.
    /// </summary>
    public class CustomDbFormatter : DatabaseLogFormatter
    {
        private readonly bool isSqlLogDisabled;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomDbFormatter"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <param name="writeAction">
        /// The write action.
        /// </param>
        /// <param name="isSqlLogEnabled">
        /// The is sql log enabled.
        /// </param>
        public CustomDbFormatter(DbContext context, Action<string> writeAction, bool isSqlLogDisabled = false)
            : base(context, writeAction)
        {
            this.isSqlLogDisabled = isSqlLogDisabled;
        }

        /// <summary>
        /// The log command.
        /// </summary>
        /// <param name="command">
        /// The command.
        /// </param>
        /// <param name="interceptionContext">
        /// The interception context.
        /// </param>
        /// <typeparam name="TResult">
        /// </typeparam>
        public override void LogCommand<TResult>(
            DbCommand command,
            DbCommandInterceptionContext<TResult> interceptionContext)
        {
            if (!this.isSqlLogDisabled)
            {
                base.LogCommand(command, interceptionContext);
            }
        }

        public override void LogResult<TResult>(
            DbCommand command,
            DbCommandInterceptionContext<TResult> interceptionContext)
        {
            if (!this.isSqlLogDisabled)
            {
                base.LogResult(command, interceptionContext);
            }
        }
    }
}
