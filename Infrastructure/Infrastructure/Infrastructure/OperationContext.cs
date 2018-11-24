// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OperationContext.cs" company="">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Infrastructure
{
    /// <summary>
    /// The operation context.
    /// </summary>
    public class OperationContext : IOperationContext
    {
        /// <summary>
        ///     Gets or sets the correlation id.
        /// </summary>
        public string CorrelationId { get; set; }
    }
}