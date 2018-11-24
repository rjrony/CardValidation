// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthorizationException.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Exception
{
    using System;

    //using Infrastructure.Localization.Contracts;

    /// <summary>
    ///     The authorization exception.
    /// </summary>
    public class AuthorizationException : Exception
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationException"/> class.
        /// </summary>
        /// <param name="resourceCode">
        /// The resource code.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        /// <param name="arguments">
        /// The arguments.
        /// </param>
        /*public AuthorizationException(ILocalizationResource resourceCode, Exception innerException, params object[] arguments)
            : base(resourceCode.GetString(arguments), innerException)
        {
        }
        */

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationException"/> class.
        /// </summary>
        /// <param name="msg">
        /// The msg.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        public AuthorizationException(string msg, Exception innerException)
            : base(msg, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationException"/> class.
        /// </summary>
        /// <param name="msg">
        /// The msg.
        /// </param>
        public AuthorizationException(string msg)
            : base(msg)
        {
        }

        #endregion
    }
}