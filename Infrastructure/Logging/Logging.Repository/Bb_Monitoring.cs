// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Bb_Monitoring.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Logging.Repository
{
    using System.Data.Common;
    using System.Data.Entity;
    using Infrastructure.Repository;

    /// <summary>
    ///     The bb_Monitoring.
    /// </summary>
    public partial class Bb_Monitoring : ContextBase
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Bb_Monitoring" /> class.
        /// </summary>
        public Bb_Monitoring()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Bb_Monitoring"/> class.
        /// </summary>
        /// <param name="nameOrConnectionString">
        /// The name or connection string.
        /// </param>
        public Bb_Monitoring(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Bb_Monitoring"/> class.
        /// </summary>
        /// <param name="existingConnection">
        /// The existing connection.
        /// </param>
        public Bb_Monitoring(DbConnection existingConnection)
            : base(existingConnection)
        {
        }

        /// <summary>
        ///     Gets or sets the is log entries.
        /// </summary>
        public virtual DbSet<IsLogEntry> IsLogEntries { get; set; }

        /// <summary>
        ///     Gets or sets the log entries.
        /// </summary>
        public virtual DbSet<LogEntry> LogEntries { get; set; }

        /// <summary>
        ///     Gets or sets the access log entries.
        /// </summary>
        public virtual DbSet<AccessLogEntry> AccessLogEntries { get; set; }

        /// <summary>
        /// Gets or sets the james log entries.
        /// </summary>
        public virtual DbSet<JamesLogEntry> JamesLogEntries { get; set; }

        /// <summary>
        /// Gets or sets the anx log entries.
        /// </summary>
        public virtual DbSet<AnxLogEntry> AnxLogEntries { get; set; }

        /// <summary>
        /// Gets or sets the transaction log entries.
        /// </summary>
        public virtual DbSet<TransactionLogEntry> TransactionLogEntries { get; set; }

        /// <summary>
        /// Gets or sets the transaction queue log entries.
        /// </summary>
        public virtual DbSet<TransactionQueueLogEntry> TransactionQueueLogEntries { get; set; }

        /// <summary>
        /// The on model creating.
        /// </summary>
        /// <param name="modelBuilder">
        /// The model builder.
        /// </param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}