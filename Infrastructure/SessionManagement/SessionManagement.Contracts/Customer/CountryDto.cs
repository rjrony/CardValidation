// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CountryDto.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.SessionManagement.Contracts.Customer
{
    /// <summary>
    ///     The country dto.
    /// </summary>
    public class CountryDto
    {
        /// <summary>
        ///     Gets or sets the iso code.
        /// </summary>
        public string IsoCode { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
    }
}