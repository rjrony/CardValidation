// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IApiDocConfig.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.ApiDocumentation.Contracts
{
    /// <summary>
    ///     The ApiDocConfig interface.
    /// </summary>
    public interface IApiDocConfig
    {
        /// <summary>
        ///     Gets or sets the app name.
        /// </summary>
        string AppName { get; set; }

        /// <summary>
        ///     Gets or sets the app virtual path.
        /// </summary>
        string AppVirtualPath { get; set; }

        /// <summary>
        ///     Gets or sets the assembly prefix.
        /// </summary>
        string AssemblyPrefix { get; set; }

        /// <summary>
        ///     Gets or sets the assembly prefix detailed.
        /// </summary>
        string AssemblyPrefixDetailed { get; set; }

        /// <summary>
        ///     Gets or sets the document header.
        /// </summary>
        string DocumentHeader { get; set; }

        /// <summary>
        ///     Gets or sets the version 1 name.
        /// </summary>
        string Version1Name { get; set; }

        /// <summary>
        ///     Gets or sets the xml comment file api.
        /// </summary>
        string XmlCommentFileApi { get; set; }

        /// <summary>
        ///     Gets or sets the xml comment file dto.
        /// </summary>
        string XmlCommentFileDto { get; set; }

        /// <summary>
        ///     Gets or sets the xml comment file path base.
        /// </summary>
        string XmlCommentFilePathBase { get; set; }
    }
}