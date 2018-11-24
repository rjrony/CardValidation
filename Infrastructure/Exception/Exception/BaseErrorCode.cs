// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseErrorCode.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Infrastructure.Exception
{
    /// <summary>
    /// The base error code.
    /// </summary>
    public enum BaseErrorCode
    {
        /// <summary>
        ///     The un authorized.
        /// </summary>
        UnAuthorized = 1001, 

        /// <summary>
        ///     The un authorized security token expired exception.
        /// </summary>
        UnAuthorizedSecurityTokenExpiredException = 10011, 

        /// <summary>
        ///     The un authorized security token invalid audience exception.
        /// </summary>
        UnAuthorizedSecurityTokenInvalidAudienceException = 10012, 

        /// <summary>
        ///     The un authorized security token invalid issuer exception.
        /// </summary>
        UnAuthorizedSecurityTokenInvalidIssuerException = 10013, 

        /// <summary>
        ///     The un authorized security token invalid lifetime exception.
        /// </summary>
        UnAuthorizedSecurityTokenInvalidLifetimeException = 10014, 

        /// <summary>
        ///     The un authorized security token invalid signature exception.
        /// </summary>
        UnAuthorizedSecurityTokenInvalidSignatureException = 10015, 

        /// <summary>
        ///     The un authorized security token invalid signing key exception.
        /// </summary>
        UnAuthorizedSecurityTokenInvalidSigningKeyException = 10016, 

        /// <summary>
        ///     The un authorized security token no expiration exception.
        /// </summary>
        UnAuthorizedSecurityTokenNoExpirationException = 10017, 

        /// <summary>
        ///     The un authorized security token not yet valid exception.
        /// </summary>
        UnAuthorizedSecurityTokenNotYetValidException = 10018, 

        /// <summary>
        ///     The un authorized security token replay add failed exception.
        /// </summary>
        UnAuthorizedSecurityTokenReplayAddFailedException = 10019, 

        /// <summary>
        ///     The un authorized security token replay detected exception.
        /// </summary>
        UnAuthorizedSecurityTokenReplayDetectedException = 100110, 

        /// <summary>
        ///     The un authorized security token validation exception.
        /// </summary>
        UnAuthorizedSecurityTokenValidationException = 100111, 

        /// <summary>
        ///     The un authorized security token signature key not found exception.
        /// </summary>
        UnAuthorizedSecurityTokenSignatureKeyNotFoundException = 100112, 

        /// <summary>
        ///     The un authorized security token validation exception wrong authorization header.
        /// </summary>
        UnAuthorizedSecurityTokenValidationExceptionWrongAuthorizationHeader = 100113, 

        /// <summary>
        ///     The external.
        /// </summary>
        External = 1002, 

        /// <summary>
        ///     The user conflict.
        /// </summary>
        Conflict = 1003, 

        /// <summary>
        ///     The account access denied.
        /// </summary>
        AccountAccessDenied = 1004, 

        /// <summary>
        ///     The invalid input.
        /// </summary>
        InvalidInput = 1005, 

        /// <summary>
        ///     The command validation.
        /// </summary>
        CommandValidation = 1006, 

        /// <summary>
        ///     The command data validation.
        /// </summary>
        CommandDataValidation = 1007, 

        /// <summary>
        ///     The authentication.
        /// </summary>
        Authentication = 1008, 

        /// <summary>
        ///     The authentication invalid client.
        /// </summary>
        AuthenticationInvalidClient = 10081, 

        /// <summary>
        ///     The authentication invalid user.
        /// </summary>
        AuthenticationInvalidUser = 10082, 

        /// <summary>
        ///     The authentication invalid audience.
        /// </summary>
        AuthenticationInvalidAudience = 10083,

        /// <summary>
        /// The account is locked
        /// </summary>
        AuthenticationInvalidAccountLocked = 10084,

        /// <summary>
        /// The authentication invalid account temporary locked
        /// </summary>
        AuthenticationInvalidAccountTemporaryLocked = 10085,

        /// <summary>
        /// The authentication invalid account permanent locked
        /// </summary>
        AuthenticationInvalidAccountPermanentLocked = 10086,

        /// <summary>
        ///     The invalid refresh token request.
        /// </summary>
        InvalidRefreshTokenRequest = 100114, 

        /// <summary>
        ///     The unhandled exception.
        /// </summary>
        UnhandledException = 1009, 

        /// <summary>
        /// The item not found.
        /// </summary>
        ItemNotFound = 10010,

        /// <summary>
        /// The technical configuration error.
        /// </summary>
        TechnicalConfigurationError = 3001,

        /// <summary>
        /// The file size exceeded
        /// </summary>
        FileSizeExceeded = 10050001,

        /// <summary>
        /// The server busy.
        /// </summary>
        ServerBusy = 50053,

        /// <summary>
        /// The HTTP header missing
        /// </summary>
        HttpHeaderMissing = 10050002,

        /// <summary>
        /// The content header missing
        /// </summary>
        ContentHeaderMissing = 10050003
    }
}