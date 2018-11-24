// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScopedApplicationDependecyResolver.cs" company="">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Infrastructure.Interception;

namespace Infrastructure.DependencyContainerBuilder
{
    using System;
    using System.Collections.Generic;

    using Infrastructure.DependencyContainerBuilder.Contract;
    using Infrastructure.Interception.Contract;

    using Microsoft.Practices.Unity;
    //  using Microsoft.Owin.Logging;
    //  using Microsoft.Practices.ServiceLocation;

    public class ScopedApplicationDependecyResolver : IScopedApplicationDependecyResolver
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScopedApplicationDependecyResolver"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public ScopedApplicationDependecyResolver(IUnityContainer container)
        {
            this.Container = container;
        }

        protected IUnityContainer Container { get; set; }



        /// <summary>
        /// The build up.
        /// </summary>
        /// <param name="instance">
        /// The instance.
        /// </param>
        public void BuildUp(object instance)
        {
            this.Container.BuildUp(instance.GetType(), instance, null);
        }


        /// <summary>
        /// The resolve.
        /// </summary>
        /// <param name="typeToCreate">
        /// The type to create.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public object Resolve(Type typeToCreate)
        {
            return this.Container.Resolve(typeToCreate);
        }

        public IEnumerable<object> ResolveAll(Type typeToCreate)
        {
            return this.Container.ResolveAll(typeToCreate);
        }

        /// <summary>
        ///     The resolve.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        ///     The <see cref="T" />.
        /// </returns>
        public T Resolve<T>()
        {
            return this.Container.Resolve<T>();
        }

        /// <summary>
        /// The resolve.
        /// </summary>
        /// <param name="ic">
        /// The ic.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <typeparam name="Ic">
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public T Resolve<T, Ic>(Ic ic)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The resolve.
        /// </summary>
        /// <param name="ic1">
        /// The ic 1.
        /// </param>
        /// <param name="ic2">
        /// The ic 2.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <typeparam name="Ic1">
        /// </typeparam>
        /// <typeparam name="Ic2">
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public T Resolve<T, Ic1, Ic2>(Ic1 ic1, Ic2 ic2)
        {
            throw new NotImplementedException();
        }

        public void RegisterInstacnceAsUnityOfWork<TInterface>(TInterface instance)
        {
            throw new NotImplementedException();
        }

        public IDependencyResolver GetCurrentDependencyResolver()
        {
            throw new NotImplementedException();
        }

        public T ResolveWithoutWarning<T>()
        {
            return this.Container.ResolveSafeWithoutWarning<T>();
        }

        public void Dispose()
        {
            ServiceLocator.Instance.CleanUpUnityOfWorkContainer();
            this.Container?.Dispose();
        }
    }
}