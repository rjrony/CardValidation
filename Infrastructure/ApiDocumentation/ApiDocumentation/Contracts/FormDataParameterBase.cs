// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FormDataParameterBase.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.ApiDocumentation.Contracts
{
    /// <summary>
    ///     The form data parameter base.
    /// </summary>
    public abstract class FormDataParameterBase : IParameter
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="FormDataParameterBase" /> class.
        /// </summary>
        protected FormDataParameterBase()
        {
            this.Name = "file";
            this.In = "formData";
            this.Type = "file";
            this.Required = true;
            this.Description = "Upload a file";
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

        ///// <summary>
        ///// Gets or sets a value indicating whether this instance is active.
        ///// </summary>
        ///// <value>
        /////   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        ///// </value>
        //public bool IsActive { get; set; }

        ///// <summary>
        ///// Gets or sets a value indicating whether this instance is optional.
        ///// </summary>
        ///// <value>
        /////   <c>true</c> if this instance is optional; otherwise, <c>false</c>.
        ///// </value>
        //public bool IsOptional { get; set; }
    }
}