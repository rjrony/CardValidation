// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HeaderParameterBase.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.ApiDocumentation.Contracts
{
    /// <summary>
    ///     The header parameter base.
    /// </summary>
    public abstract class HeaderParameterBase : IParameter
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="HeaderParameterBase" /> class.
        /// </summary>
        protected HeaderParameterBase()
        {
            this.Name = "HeaderParameter";
            this.In = "header";
            this.Type = "string";
            this.Required = true;
            this.Description = "Header Parameter";
            this.IsDefault = false;
        }

        /// <summary>
        ///     Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Gets or sets the in.
        /// </summary>
        public string In { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether is default.
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether required.
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        ///     Gets or sets the type.
        /// </summary>
        public string Type { get; set; }
    }
}