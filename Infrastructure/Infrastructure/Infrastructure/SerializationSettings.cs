// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SerializationSettings.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure
{
    using System.Runtime.Serialization.Formatters;

    using Newtonsoft.Json;

    /// <summary>
    ///     The serialization settings.
    /// </summary>
    public class SerializationSettings
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="SerializationSettings" /> class.
        /// </summary>
        public SerializationSettings()
        {
            this.FormatterAssemblyStyle = FormatterAssemblyStyle.Simple;
            this.TypeNameHandling = TypeNameHandling.Auto;
            this.ReferenceLoopHandling = ReferenceLoopHandling.Error;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the formatter assembly style.
        /// </summary>
        public FormatterAssemblyStyle FormatterAssemblyStyle { get; set; }

        /// <summary>
        ///     Gets or sets the preserve references handling.
        /// </summary>
        public PreserveReferencesHandling PreserveReferencesHandling { get; set; }

        /// <summary>
        ///     Gets or sets the reference loop handling.
        /// </summary>
        public ReferenceLoopHandling ReferenceLoopHandling { get; set; }

        /// <summary>
        ///     Gets or sets the type name handling.
        /// </summary>
        public TypeNameHandling TypeNameHandling { get; set; }

        #endregion
    }
}