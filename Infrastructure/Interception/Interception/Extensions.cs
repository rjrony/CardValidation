// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Interception
{
    using System;

    using Infrastructure.Logging.Contracts;

    using Microsoft.Practices.Unity;

    /// <summary>
    ///     The extensions.
    /// </summary>
    public static class Extensions
    {
        #region Public Methods and Operators

        /// <summary>
        /// The resolve safe.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <param name="logger">
        /// The logger.
        /// </param>
        /// <typeparam name="T">
        /// The type to resolve
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T ResolveSafe<T>(this IUnityContainer container, ILogger logger)
        {
            T resolved;
            try
            {
                resolved = container.Resolve<T>();
            }
            catch (Exception)
            {
                if (logger != null)
                {
                    logger.Warning(() => string.Format("No custom implementation for {0}", typeof(T)));
                }

                resolved = default(T);
            }

            return resolved;
        }

        /// <summary>
        /// The add logging extension.
        /// </summary>
        /// <param name="unityContainer">
        /// The unity container.
        /// </param>
        /// <param name="shouldRegister">
        /// The should register.
        /// </param>
        public static void AddLoggingExtension(this IUnityContainer unityContainer, Func<RegisterEventArgs, bool> shouldRegister)
        {
            unityContainer.AddExtension(
                new UnityInterfaceInterceptionRegisterer(new[] { new InterceptConfig(new LoggingInterceptionBehavior(), shouldRegister) }));
        }

        /// <summary>
        /// The resolve or throw.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <typeparam name="T">
        /// type to resolve
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// container must not be null
        /// </exception>
        /// <exception cref="Exception">
        /// if not able to resolve the type
        /// </exception>
        public static T ResolveOrThrow<T>(this IUnityContainer container)
        {
            return (T)container.ResolveOrThrow(typeof(T));
        }

        /// <summary>
        /// The resolve or throw.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <param name="typeToResolve">
        /// The type to resolve.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// parameter must be not null
        /// </exception>
        /// <exception cref="Exception">
        /// if not able to resolve
        /// </exception>
        public static object ResolveOrThrow(this IUnityContainer container, Type typeToResolve)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            if (typeToResolve == null)
            {
                throw new ArgumentNullException("typeToResolve");
            }

            var instance = container.Resolve(typeToResolve);
            if (object.Equals(instance, null))
            {
                throw new Exception(string.Format("Not able to resolve instance of type {0}", typeToResolve.FullName));
            }

            return instance;
        }
        /// <summary>
        /// The resolve safe.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <typeparam name="T">
        /// The type to resolve
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T ResolveSafeWithoutWarning<T>(this IUnityContainer container)
        {
            T resolved;
            try
            {
                resolved = container.Resolve<T>();
            }
            catch (Exception)
            {
                resolved = default(T);
            }

            return resolved;
        }
        #endregion
    }
}