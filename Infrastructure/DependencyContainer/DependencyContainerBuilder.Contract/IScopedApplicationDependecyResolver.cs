// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IScopedApplicationDependecyResolver.cs" company="">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.DependencyContainerBuilder.Contract
{
    using System;

    using Infrastructure.Interception.Contract;

    /// <summary>
    /// The ScopedApplicationDependecyResolver interface.
    /// </summary>
    public interface IScopedApplicationDependecyResolver : IDependencyResolver, IDisposable
    {
    }
}