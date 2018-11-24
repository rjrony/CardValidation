// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserProvider.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.SessionManagement.Contracts.User
{
    using System.Threading.Tasks;

    /// <summary>
    ///     The UserProvider interface.
    /// </summary>
    public interface IUserProvider
    {
        /// <summary>
        ///     The get user.
        /// </summary>
        /// <returns>
        ///     The <see cref="UserDto" />.
        /// </returns>
        Task<UserDto> GetUserAsync();
    }
}