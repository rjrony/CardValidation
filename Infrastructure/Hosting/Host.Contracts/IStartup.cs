// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStartup.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Web.Http;

namespace Infrastructure.Host.Contracts
{
    /// <summary>
    ///     The Startup interface.
    /// </summary>
    public interface IStartup : IAppStartup
    {
        /// <summary>
        ///     The get injection configuration.
        /// </summary>
        /// <returns>
        ///     The <see cref="HttpConfiguration" />.
        /// </returns>
        HttpConfiguration GetInjectionConfiguration();


        IBootstrapper GetBootstrapperWebApi(HttpConfiguration config);
    }
}