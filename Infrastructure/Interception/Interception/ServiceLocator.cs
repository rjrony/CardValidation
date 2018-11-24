// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceLocator.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Runtime.Remoting.Messaging;

namespace Infrastructure.Interception
{
    using System;

    using Infrastructure.Interception.Contract;

    using Microsoft.Practices.Unity;

    /// <summary>
    ///     The service locator.
    /// </summary>
    public class ServiceLocator : IServiceLocator, IUnitOfWorkContainerSetter, IServiceLocatorInitializer
    {
        #region Static Fields

        private static IServiceLocator serviceLocatorInternal;

        #endregion

        #region Constructors and Destructors

        private ServiceLocator()
        {
        }

        #endregion

        //void IUnitOfWorkContainerSetter.SetUnitOfWorkContainerThreadStatic(IUnityContainer container)
        //{
        //    unityOfWorkContainerThreadStatic = container;
        //}
        void IUnitOfWorkContainerSetter.SetUnitOfWorkContainer(IUnityContainer container)
        {
            CallContext.LogicalSetData("CurrentContainer", new ContainerWrapper { Container = container });
        }

        #region Fields

        //[ThreadStatic]
        //private static IUnityContainer unityOfWorkContainerThreadStatic;

        private IHostUnityContainer hostContainerInternal;

        private IUnityContainer unityOfWorkContainerThreadIndependend;

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the instance.
        /// </summary>
        public static IServiceLocator Instance
        {
            get
            {
                return serviceLocatorInternal ?? (serviceLocatorInternal = new ServiceLocator());
            }
        }

        /// <summary>
        ///     Gets the current.
        /// </summary>
        public IUnityContainer Current
        {
            get
            {
                var unitiOfWorkContainer = this.GetUnityOfWorkContainer();
                if (unitiOfWorkContainer != null)
                {
                    return unitiOfWorkContainer;
                }

                return this.hostContainerInternal;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The get root container.
        /// </summary>
        /// <returns>
        ///     The <see cref="IUnityContainer" />.
        /// </returns>
        public IUnityContainer GetRootContainer()
        {
            return this.hostContainerInternal.GetRootContainer();
        }

        /// <summary>
        ///     The get unity of work container.
        /// </summary>
        /// <returns>
        ///     The <see cref="IUnityContainer" />.
        /// </returns>
        //public IUnityContainer GetUnityOfWorkContainer()
        //{
        //    return unityOfWorkContainerThreadStatic ?? this.unityOfWorkContainerThreadIndependend;
        //}

        public IUnityContainer GetUnityOfWorkContainer()
        {
            var ret = CallContext.LogicalGetData("CurrentContainer") as ContainerWrapper;
            return ret?.Container;
        }

        public void CleanUpUnityOfWorkContainer()
        {
            CallContext.FreeNamedDataSlot("CurrentContainer");
        }

        #endregion

        #region Explicit Interface Methods

        void IServiceLocatorInitializer.Initialize(IHostUnityContainer hostUnityContainer)
        {
            if (hostUnityContainer == null)
            {
                throw new ArgumentNullException("hostUnityContainer");
            }

            if (this.hostContainerInternal != null)
            {
                throw new Exception("ServiceLocator is already initialized");
            }

            this.hostContainerInternal = hostUnityContainer;
        }

        void IUnitOfWorkContainerSetter.SetUnitOfWorkContainerThreadIndepended(IUnityContainer container)
        {
            this.unityOfWorkContainerThreadIndependend = container;
        }

        #endregion

        private sealed class ContainerWrapper : MarshalByRefObject
        {
            public IUnityContainer Container { get; set; }
        }
    }


}