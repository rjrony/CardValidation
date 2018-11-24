// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestInfo.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Repository.Contracts
{
    /// <summary>
    ///     The test info.
    /// </summary>
    public class TestInfo : ITestInfo
    {
        /// <summary>
        ///     Gets or sets a value indicating whether is test enable.
        /// </summary>
        public bool IsTestEnabled { get; set; } = false;
    }
}