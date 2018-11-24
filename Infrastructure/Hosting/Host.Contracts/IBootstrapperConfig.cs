// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBootstrapperConfig.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Host.Contracts
{
    using System.Web.Http;
    using Infrastructure.Interception.Contract;
    using Microsoft.Practices.Unity;

    /// <summary>
    ///     The BootstrapperConfig interface.
    /// </summary>
    public interface IBootstrapperConfig
    {
        /// <summary>
        ///     Gets the bootstrapper.
        /// </summary>
        IBootstrapper Bootstrapper { get; }

        /// <summary>
        ///     Gets or sets the configuration.
        /// </summary>
        HttpConfiguration Configuration { get; set; }

        /// <summary>
        ///     Gets the container.
        /// </summary>
        IUnityContainer Container { get; }

        /// <summary>
        ///     Gets or sets a value indicating whether is auto register unity of work.
        /// </summary>
        bool IsAutoRegisterUnityOfWork { get; set; }

        /// <summary>
        ///     Gets a value indicating whether is logging enabled.
        /// </summary>
        bool IsLoggingEnabled { get; }

        /// <summary>
        ///     Gets or sets the type registrar.
        /// </summary>
        ITypeRegistrarService TypeRegistrarService { get; set; }
    }
}