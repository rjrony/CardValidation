// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DependencyResolver.cs" company="">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Infrastructure.Interception.Contract;
using Microsoft.Practices.Unity;

namespace Infrastructure.Interception
{
    /// <summary>
    ///     The instance creater.
    /// </summary>
    public class DependencyResolver : IDependencyResolver
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyResolver"/> class.
        /// </summary>
        /// <param name="unityContainer">
        /// The unity container.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Container musst be not null
        /// </exception>
        public DependencyResolver(IUnityContainer unityContainer)
        {
            if (unityContainer == null)
            {
                throw new ArgumentNullException("unityContainer");
            }

            this.ContainerWeakReference = new WeakReference<IUnityContainer>(unityContainer);
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the i unity container.
        /// </summary>
        private IUnityContainer Container
        {
            get
            {
                IUnityContainer container;
                if (this.ContainerWeakReference.TryGetTarget(out container))
                {
                    return container;
                }

                return null;
            }
        }

        private WeakReference<IUnityContainer> ContainerWeakReference { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The build.
        /// </summary>
        /// <param name="instance">
        /// The instance.
        /// </param>
        public void BuildUp(object instance)
        {
            this.Container.BuildUp(instance.GetType(), instance, null);
        }

        /// <summary>
        /// The create.
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

        /// <summary>
        /// The resolve all.
        /// </summary>
        /// <param name="typeToCreate">
        /// The type to create.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public IEnumerable<object> ResolveAll(Type typeToCreate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     The create.
        /// </summary>
        /// <typeparam name="T">
        ///     type to create. normally it's an interface
        /// </typeparam>
        /// <returns>
        ///     The <see cref="object" />.
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
        public T Resolve<T, Ic>(Ic ic)
        {
            return this.Container.Resolve<T>(new DependencyOverride<Ic>(ic));
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
        /// main type
        /// </typeparam>
        /// <typeparam name="Ic1">
        /// inner constructor type first
        /// </typeparam>
        /// <typeparam name="Ic2">
        /// /// inner constructor type second
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T Resolve<T, Ic1, Ic2>(Ic1 ic1, Ic2 ic2)
        {
            return this.Container.Resolve<T>(new DependencyOverride<Ic1>(ic1), new DependencyOverride<Ic2>(ic2));
        }

        /// <summary>
        /// The register instacnce as unity of work.
        /// </summary>
        /// <param name="instance">
        /// The instance.
        /// </param>
        /// <typeparam name="TInterface">
        /// </typeparam>
        public void RegisterInstacnceAsUnityOfWork<TInterface>(TInterface instance)
        {
            this.Container.RegisterInstance(instance, new HierarchicalLifetimeManager());
        }

        /// <summary>
        /// The get dependency resolver of current container.
        /// </summary>
        /// <returns>
        /// The <see cref="IDependencyResolver"/>.
        /// </returns>
        public IDependencyResolver GetCurrentDependencyResolver()
        {
            if (HttpContext.Current == null)
            {
                return this;
            }

            try
            {
                var context = HttpContext.Current.GetOwinContext();
                var scopedDependencyResolver =
     context.Get<IDependencyResolver>("ScopedDependencyResolver");
                return scopedDependencyResolver.Resolve<IDependencyResolver>();
            }
            catch (Exception)
            {

                return this;
            }



        }

        /// <summary>
        /// The resolve.
        /// </summary>
        /// <param name="paramsObjects">
        /// The params objects.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T Resolve<T>(params object[] paramsObjects)
        {
            var param = paramsObjects.Select(p => new DependencyOverride(p.GetType(), p)).ToArray();
            return this.Container.Resolve<T>(param);
        }
        /// <summary>
        ///     The create.
        /// </summary>
        /// <typeparam name="T">
        ///     type to create. normally it's an interface
        /// </typeparam>
        /// <returns>
        ///     The <see cref="object" />.
        /// </returns>
        public T ResolveWithoutWarning<T>()
        {
            return this.Container.ResolveSafeWithoutWarning<T>();
        }


        #endregion
    }
}