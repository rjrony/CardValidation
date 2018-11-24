// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEndPointParamMapCustom.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.ApiDocumentation.Contracts
{
    /// <summary>
    ///     The EndPointParamMapCustom interface.
    /// </summary>
    public interface IEndPointParamMapCustom : IEndPointParamMap
    {
        /// <summary>
        ///     Gets or sets a value indicating whether is active.
        /// </summary>
        bool IsActive { get; set; }
    }
}