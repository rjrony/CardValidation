// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomParameterBase.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.ApiDocumentation.Contracts
{
    /// <summary>
    ///     The custom parameter base.
    /// </summary>
    public abstract class CustomParameterBase : IParameter
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CustomParameterBase" /> class.
        /// </summary>
        protected CustomParameterBase()
            : this("CustomParameter", "Custom Parameter", "formData", "string", true, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomParameterBase"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        protected CustomParameterBase(string name)
            : this(name, "Custom Parameter", "formData", "string", true, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomParameterBase"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        protected CustomParameterBase(string name, string description)
            : this(name, description, "formData", "string", true, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomParameterBase"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <param name="in">
        /// The in.
        /// </param>
        protected CustomParameterBase(string name, string description, string @in)
            : this(name, description, @in, "string", true, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomParameterBase"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <param name="in">
        /// The in.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        protected CustomParameterBase(string name, string description, string @in, string type)
            : this(name, description, @in, type, true, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomParameterBase"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <param name="in">
        /// The in.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="required">
        /// The required.
        /// </param>
        protected CustomParameterBase(string name, string description, string @in, string type, bool required)
            : this(name, description, @in, type, required, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomParameterBase"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <param name="in">
        /// The in.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="required">
        /// The required.
        /// </param>
        /// <param name="isDefault">
        /// The is default.
        /// </param>
        protected CustomParameterBase(string name, string description, string @in, string type, bool required, bool isDefault)
        {
            this.Name = name;
            this.Description = description;
            this.In = @in;
            this.Type = type;
            this.Required = required;
            this.IsDefault = isDefault;
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