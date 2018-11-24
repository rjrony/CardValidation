// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DbMigrationException.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Repository
{
    using System;

    /// <summary>
    ///     The db migration exception.
    /// </summary>
    public class DbMigrationException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DbMigrationException" /> class.
        /// </summary>
        public DbMigrationException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbMigrationException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public DbMigrationException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbMigrationException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        public DbMigrationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}