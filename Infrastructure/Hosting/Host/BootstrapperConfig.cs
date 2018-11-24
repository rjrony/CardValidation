// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BootstrapperConfig.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Host
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;

    using  global::Infrastructure.Interception.Contract;

    using Infrastructure.Host.Contracts;

    using Microsoft.Practices.Unity;

    /// <summary>
    ///     The bootstrapper config.
    /// </summary>
    public class BootstrapperConfig : IBootstrapperConfig
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BootstrapperConfig" /> class.
        /// </summary>
        public BootstrapperConfig()
        {
            this.UnitOfWorkTypesToRegisterDynamic = new Dictionary<Type, Type>();
        }

        /// <summary>
        ///     Gets the bootstrapper.
        /// </summary>
        public IBootstrapper Bootstrapper { get; internal set; }

        /// <summary>
        ///     Gets or sets the configuration.
        /// </summary>
        public HttpConfiguration Configuration { get; set; }

        /// <summary>
        ///     Gets the container.
        /// </summary>
        public IUnityContainer Container { get; internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether is auto register unity of work.
        /// </summary>
        public bool IsAutoRegisterUnityOfWork { get; set; }

        /// <summary>
        ///     Gets a value indicating whether is logging enabled.
        /// </summary>
        public bool IsLoggingEnabled { get; internal set; }

        /// <summary>
        ///     Gets a value indicating whether [is unity container tenant aware].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [is unity container tenant aware]; otherwise, <c>false</c>.
        /// </value>
        public bool IsTenantAwarenessEnabled { get; internal set; } = false;

        /// <summary>
        ///     Gets a value indicating whether transaction is enabled
        /// </summary>
        public bool IsTransactionEnabled { get; internal set; }

        /// <summary>
        ///     Gets the type registrar.
        /// </summary>
        public ITypeRegistrarService TypeRegistrarService { get; set; }

        /// <summary>
        ///     Gets or sets the unit of work types to register dynamic.
        /// </summary>
        public Dictionary<Type, Type> UnitOfWorkTypesToRegisterDynamic { get; set; }
    }
}