// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IParameter.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.ApiDocumentation.Contracts
{
    /// <summary>
    ///     The HeaderParameter interface.
    /// </summary>
    public interface IParameter
    {
        /// <summary>
        ///     Gets or sets the description.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        ///     Gets or sets the in.
        /// </summary>
        string In { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether is default.
        /// </summary>
        bool IsDefault { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether required.
        /// </summary>
        bool Required { get; set; }

        /// <summary>
        ///     Gets or sets the type.
        /// </summary>
        string Type { get; set; }
    }
}