// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoggingRepositoryRead.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Logging.Repository
{
    using Infrastructure.ReadModel;
    using Infrastructure.Repository.Contracts;

    /// <summary>
    ///     The logging repository read.
    /// </summary>
    public class LoggingRepositoryRead : ReadOptimizedRepository<Bb_Monitoring>, ILoggingRepositoryRead
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingRepositoryRead"/> class.
        /// </summary>
        /// <param name="dbContextProvider">
        /// The db context provider.
        /// </param>
        public LoggingRepositoryRead(IDbContextFactory<Bb_Monitoring> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}