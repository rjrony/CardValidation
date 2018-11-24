// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AssignableExtensions.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure
{
    using System;
    using System.Linq;

    /// <summary>
    ///     The assignable extensions.
    /// </summary>
    public static class AssignableExtensions
    {
        /// <summary>
        /// Determines whether the <paramref name="genericType"/> is assignable from
        ///     <paramref name="givenType"/> taking into account generic definitions
        /// </summary>
        /// <param name="givenType">
        /// The given Type.
        /// </param>
        /// <param name="genericType">
        /// The generic Type.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsAssignableToGenericType(this Type givenType, Type genericType)
        {
            if (givenType == null || genericType == null)
            {
                return false;
            }

            return givenType == genericType || givenType.MapsToGenericTypeDefinition(genericType)
                   || givenType.HasInterfaceThatMapsToGenericTypeDefinition(genericType)
                   || givenType.BaseType.IsAssignableToGenericType(genericType);
        }

        private static bool HasInterfaceThatMapsToGenericTypeDefinition(this Type givenType, Type genericType)
        {
            return givenType.GetInterfaces().Where(it => it.IsGenericType).Any(it => it.GetGenericTypeDefinition() == genericType);
        }

        private static bool MapsToGenericTypeDefinition(this Type givenType, Type genericType)
        {
            return genericType.IsGenericTypeDefinition && givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType;
        }
    }
}