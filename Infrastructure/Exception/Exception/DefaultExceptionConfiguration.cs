// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultExceptionConfiguration.cs" company="">
//   Copyright © 2018. All rights reserved.
// </copyright>
// <summary>
//   Defines the DefaultExceptionConfiguration type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------


namespace Infrastructure.Exception
{
    /// <summary>
    /// The default exception configuration.
    /// </summary>
    public class DefaultExceptionConfiguration : IExceptionConfiguration
    {
        /// <summary>
        /// The show nested message.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool ShowNestedMessage()
        {
            return true;
        }
    }
}
