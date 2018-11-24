// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NestedMessage.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// <summary>
//   The nested message.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Exception
{
    /// <summary>
    ///     The nested message.
    /// </summary>
    public class NestedMessage : ExceptionErrorMessageBase
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="NestedMessage" /> class.
        /// </summary>
        public NestedMessage()
            : base(ExceptionErrorMassageType.Nested)
        {
        }
    }
}