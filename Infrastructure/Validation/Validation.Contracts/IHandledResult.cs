// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHandledResult.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Validation.Contracts
{
    /// <summary>
    ///     The HandledResult interface.
    /// </summary>
    public interface IHandledResult
    {
        /// <summary>
        ///     Gets or sets a value indicating whether is handled.
        /// </summary>
        bool IsHandled { get; set; }
    }
}