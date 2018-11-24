// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IExceptionConfiguration.cs" company="">
//   Copyright © 2018. All rights reserved.
// </copyright>
// <summary>
//   Defines the IExceptionConfiguration type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Exception
{
    /// <summary>
    /// The ExceptionConfiguration interface.
    /// </summary>
    public interface IExceptionConfiguration
    {
        /// <summary>
        /// The show nested message.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool ShowNestedMessage();
    }
}
