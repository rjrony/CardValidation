// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PrivateSetterContractResolver.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure
{
    using System.Reflection;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    ///     The private setter conctract resolver.
    /// </summary>
    public class PrivateSetterContractResolver : DefaultContractResolver
    {
        #region Methods

        /// <summary>
        /// The create property.
        /// </summary>
        /// <param name="member">
        /// The member.
        /// </param>
        /// <param name="memberSerialization">
        /// The member serialization.
        /// </param>
        /// <returns>
        /// The <see cref="JsonProperty"/>.
        /// </returns>
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var prop = base.CreateProperty(member, memberSerialization);
            if (!prop.Writable)
            {
                var property = member as PropertyInfo;
                if (property != null)
                {
                    prop.Writable = property.CanWrite;
                }
            }

            return prop;
        }

        #endregion
    }
}