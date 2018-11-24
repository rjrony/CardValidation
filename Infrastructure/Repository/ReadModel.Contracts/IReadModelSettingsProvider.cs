// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IReadModelSettingsProvider.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.ReadModel.Contracts
{
    /// <summary>
    /// Provides settings for read model.
    /// </summary>
    /// <typeparam name="TContext">
    /// The type of the context.
    /// </typeparam>
    public interface IReadModelSettingsProvider<TContext>
        where TContext : class
    {
        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <param name="settingsPrefix">
        /// The settingsPrefix is used for write model settings lookup.
        /// </param>
        /// <returns>
        /// The ReadModelSettings.
        /// </returns>
        IReadModelSettings GetSettings(string settingsPrefix = null);
    }
}