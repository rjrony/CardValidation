// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomerDto.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.SessionManagement.Contracts.Customer
{
    using System;

    /// <summary>
    ///     The customer dto.
    /// </summary>
    public class CustomerDto
    {
        /// <summary>
        ///     Gets or sets the country id.
        /// </summary>
        public short CountryId { get; set; }

        /// <summary>
        ///     Gets or sets the customer guid.
        /// </summary>
        public Guid CustomerGuid { get; set; }

        /// <summary>
        ///     Gets or sets the customer id.
        /// </summary>
        public long CustomerId { get; set; }

        /// <summary>
        ///     Gets or sets the default currency id.
        /// </summary>
        public short DefaultCurrencyId { get; set; }

        /// <summary>
        ///     Gets or sets the email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     Gets or sets the first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether is verified.
        /// </summary>
        public bool IsVerified { get; set; }

        /// <summary>
        ///     Gets or sets the last login.
        /// </summary>
        public DateTime? LastLogin { get; set; }

        /// <summary>
        ///     Gets or sets the last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        ///     Gets or sets the owner user id.
        /// </summary>
        public long OwnerUserId { get; set; }
    }
}