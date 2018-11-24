// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOperationContext.cs" company="">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Infrastructure
{
    /// <summary>
    /// The OperationContext interface.
    /// </summary>
    public interface IOperationContext
    {
        /// <summary>
        ///     Gets or sets the correlation id.
        /// </summary>
        string CorrelationId { get; set; }
    }
}