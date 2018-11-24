// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApiDocConfig.cs" company="SS">
//   Copyright © SS. All rights reserved.
// </copyright>
// <summary>
//   The api doc config.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Configuration;
using Infrastructure.ApiDocumentation.Contracts;

namespace CardValidation.Api.ApiDocumentation
{
    /// <summary>
    ///     The api doc config.
    /// </summary>
    public class ApiDocConfig : IApiDocConfig
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ApiDocConfig" /> class.
        /// </summary>
        public ApiDocConfig()
        {
            this.AppName = "CardValidation";
            this.AppVirtualPath = string.IsNullOrEmpty(ConfigurationManager.AppSettings["AppVirtualPath"])
                                      ? string.Empty
                                      : "/" + ConfigurationManager.AppSettings["AppVirtualPath"];
            this.AssemblyPrefix = "CardValidation";
            this.DocumentHeader = "API listing for using CardValidation features";
            this.Version1Name = "v1";
            this.XmlCommentFileApi = "CardValidation.Api.xml";
            this.XmlCommentFileDto = "CardValidation.Api.Dtos.xml";
            //this.XmlCommentFilePathBase = "ApiDocumentation";
            this.AssemblyPrefixDetailed = "CardValidation.Api";
        }

        /// <summary>
        ///     Gets or sets the app name.
        /// </summary>
        public string AppName { get; set; }

        /// <summary>
        ///     Gets or sets the app virtual path.
        /// </summary>
        public string AppVirtualPath { get; set; }

        /// <summary>
        ///     Gets or sets the assembly prefix.
        /// </summary>
        public string AssemblyPrefix { get; set; }

        /// <summary>
        ///     Gets or sets the assembly prefix detailed.
        /// </summary>
        public string AssemblyPrefixDetailed { get; set; }

        /// <summary>
        ///     Gets or sets the document header.
        /// </summary>
        public string DocumentHeader { get; set; }

        /// <summary>
        ///     Gets or sets the version 1 name.
        /// </summary>
        public string Version1Name { get; set; }

        /// <summary>
        ///     Gets or sets the xml comment file api.
        /// </summary>
        public string XmlCommentFileApi { get; set; }

        /// <summary>
        ///     Gets or sets the xml comment file dto.
        /// </summary>
        public string XmlCommentFileDto { get; set; }

        /// <summary>
        ///     Gets or sets the xml comment file path base.
        /// </summary>
        public string XmlCommentFilePathBase { get; set; }
    }
}