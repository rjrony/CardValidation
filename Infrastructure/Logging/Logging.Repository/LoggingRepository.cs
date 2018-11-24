// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoggingRepository.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Logging.Repository
{
    using Infrastructure.ReadModel;
    using Infrastructure.Repository.Contracts;

    /// <summary>
    ///     The logging repository.
    /// </summary>
    public class LoggingRepository : Repository<Bb_Monitoring>, ILoggingRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingRepository"/> class.
        /// </summary>
        /// <param name="dbContextProvider">
        /// The db context provider.
        /// </param>
        public LoggingRepository(IDbContextFactory<Bb_Monitoring> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}