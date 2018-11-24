// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScopeHostUnityContainer.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Host
{
    using System.Net.Http;
    using System.Web;

    using global::Infrastructure.Interception;
    using  global::Infrastructure.Interception.Contract;

    using Microsoft.Practices.Unity;

    /// <summary>
    ///     The scope host unity container.
    /// </summary>
    public class ScopeHostUnityContainer : HostUnityContainer
    {
        #region Methods

        /// <summary>
        ///     The get container for resolve. This class hass effect in the case where WebApi and bus are used like client command
        ///     Outgoing message are not creating a child container.
        ///     ServiceLocator is here not used because of multithreaded environment. Context ist present in multiple threads
        /// </summary>
        /// <returns>
        ///     The <see cref="IUnityContainer" />.
        /// </returns>
        protected override IUnityContainer GetContainerForResolve()
        {
            if (HttpContext.Current == null)
            {
                return base.GetContainerForResolve();
            }

            var message = HttpContext.Current.Items["MS_HttpRequestMessage"] as HttpRequestMessage;
            if (message == null)
            {
                return base.GetContainerForResolve();
            }

            var childContainer = (IChildContainer)message.GetDependencyScope().GetService(typeof(IChildContainer));
            return childContainer.Container;
        }

        #endregion
    }
}