// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDependencyResolver.cs" company="">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Infrastructure.Interception.Contract
{
    /// <summary>
    ///     The instance creater.
    /// </summary>
    public interface IDependencyResolver
    {
        #region Public Methods and Operators

        /// <summary>
        /// The build.
        /// </summary>
        /// <param name="instance">
        /// The instance.
        /// </param>
        void BuildUp(object instance);

        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="typeToCreate">
        /// The type to create.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        object Resolve(Type typeToCreate);

        /// <summary>
        /// The resolve all.
        /// </summary>
        /// <param name="typeToCreate">
        /// The type to create.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        IEnumerable<object> ResolveAll(Type typeToCreate);

        /// <summary>
        ///     The create.
        /// </summary>
        /// <typeparam name="T">
        ///     type to create. normally it's an interface
        /// </typeparam>
        /// <returns>
        ///     The <see cref="object" />.
        /// </returns>
        T Resolve<T>();

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
        T Resolve<T, Ic>(Ic ic);

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
        T Resolve<T, Ic1, Ic2>(Ic1 ic1, Ic2 ic2);


        /// <summary>
        /// The register instacnce.
        /// </summary>
        /// <param name="instance">
        /// The instance.
        /// </param>
        /// <typeparam name="TInterface">
        /// </typeparam>
        void RegisterInstacnceAsUnityOfWork<TInterface>(TInterface instance);

        /// <summary>
        ///     The get dependency resolver of current container.
        /// </summary>
        /// <returns>
        ///     The <see cref="IDependencyResolver" />.
        /// </returns>
        IDependencyResolver GetCurrentDependencyResolver();


        /// <summary>
        ///     The create.
        /// </summary>
        /// <typeparam name="T">
        ///     type to create. normally it's an interface
        /// </typeparam>
        /// <returns>
        ///     The <see cref="object" />.
        /// </returns>
        T ResolveWithoutWarning<T>();

        
        #endregion
    }
}