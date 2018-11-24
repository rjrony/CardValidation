// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnityTypeMapping.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Host.Contracts
{
    using System;

    using Microsoft.Practices.Unity;

    /// <summary>
    ///     Defines a Unity Type Mapping.
    /// </summary>
    public class UnityTypeMapping
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnityTypeMapping"/> class.
        /// </summary>
        /// <param name="typeFrom">
        /// The type from.
        /// </param>
        /// <param name="typeTo">
        /// The type to.
        /// </param>
        public UnityTypeMapping(Type typeFrom, Type typeTo)
        {
            this.TypeFrom = typeFrom;
            this.TypeTo = typeTo;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityTypeMapping"/> class.
        /// </summary>
        /// <param name="typeFrom">
        /// The type from.
        /// </param>
        /// <param name="typeTo">
        /// The type to.
        /// </param>
        /// <param name="injectionMembers">
        /// The injection members.
        /// </param>
        public UnityTypeMapping(Type typeFrom, Type typeTo, params InjectionMember[] injectionMembers)
        {
            this.TypeFrom = typeFrom;
            this.TypeTo = typeTo;
            this.InjectionMembers = injectionMembers;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityTypeMapping"/> class.
        /// </summary>
        /// <param name="typeFrom">
        /// The type from.
        /// </param>
        /// <param name="typeTo">
        /// The type to.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="injectionMembers">
        /// The injection members.
        /// </param>
        public UnityTypeMapping(Type typeFrom, Type typeTo, string name, params InjectionMember[] injectionMembers)
        {
            this.TypeFrom = typeFrom;
            this.TypeTo = typeTo;
            this.Name = name;
            this.InjectionMembers = injectionMembers;
        }

        /// <summary>
        ///     Gets the injection members.
        /// </summary>
        /// <value>
        ///     The injection members.
        /// </value>
        public InjectionMember[] InjectionMembers { get; private set; }

        /// <summary>
        ///     Gets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public string Name { get; private set; }

        /// <summary>
        ///     Gets the type from.
        /// </summary>
        /// <value>
        ///     The type from.
        /// </value>
        public Type TypeFrom { get; private set; }

        /// <summary>
        ///     Gets the type to.
        /// </summary>
        /// <value>
        ///     The type to.
        /// </value>
        public Type TypeTo { get; private set; }
    }
}