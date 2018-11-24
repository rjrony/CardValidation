// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExceptionErrorMassageType.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Exception
{
    /// <summary>
    ///     The exception error massage type.
    /// </summary>
    public enum ExceptionErrorMassageType
    {
        /// <summary>
        ///     The root.
        /// </summary>
        Root = 1,

        /// <summary>
        ///     The external.
        /// </summary>
        External,

        /// <summary>
        ///     The nested.
        /// </summary>
        Nested
    }
}