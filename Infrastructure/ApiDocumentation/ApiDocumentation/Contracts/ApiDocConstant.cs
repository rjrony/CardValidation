// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApiDocConstant.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.ApiDocumentation.Contracts
{
    /// <summary>
    ///     The api doc constant.
    /// </summary>
    public class ApiDocConstant
    {
        /// <summary>
        /// The query string separator for url.
        /// </summary>
        public const char QueryStringSeparatorForUrl = '?';

        private readonly string resourceFileGenericName = "Content.swagger-customization";

        /// <summary>
        ///     The css path.
        /// </summary>
        public string CssPath => string.Concat(this.GetType().Assembly.GetName().Name, ".", this.resourceFileGenericName, ".", "css");

        /// <summary>
        ///     The js path.
        /// </summary>
        public string JsPath => string.Concat(this.GetType().Assembly.GetName().Name, ".", this.resourceFileGenericName, ".", "js");
    }
}