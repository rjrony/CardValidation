// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserDto.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.SessionManagement.Contracts.User
{
    using System;

    /// <summary>
    ///     The user dto.
    /// </summary>
    public class UserDto
    {
        /// <summary>
        ///     Gets or sets the display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        ///     Gets or sets the email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        ///     Gets or sets the phone number.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        ///     Gets or sets the user guid.
        /// </summary>
        public Guid UserGuid { get; set; }

        /// <summary>
        ///     Gets or sets the user id.
        /// </summary>
        public long UserId { get; set; }
    }
}