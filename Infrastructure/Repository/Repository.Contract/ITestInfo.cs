// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITestInfo.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Repository.Contracts
{
    /// <summary>
    ///     The TestInfo interface.
    /// </summary>
    public interface ITestInfo
    {
        /// <summary>
        ///     Gets or sets a value indicating whether is test enable.
        /// </summary>
        bool IsTestEnabled { get; set; }
    }
}