using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Exception.ErrorCodes
{
    public static class BaseErrorCodes
    {
        public static UnAuthorized UnAuthorized => UnAuthorized.GetInstance();
        public static UnAuthorizedSecurityTokenExpiredException UnAuthorizedSecurityTokenExpiredException => UnAuthorizedSecurityTokenExpiredException.GetInstance();
        public static UnAuthorizedSecurityTokenInvalidAudienceException UnAuthorizedSecurityTokenInvalidAudienceException => UnAuthorizedSecurityTokenInvalidAudienceException.GetInstance();
        public static UnAuthorizedSecurityTokenInvalidIssuerException UnAuthorizedSecurityTokenInvalidIssuerException => UnAuthorizedSecurityTokenInvalidIssuerException.GetInstance();
        public static UnAuthorizedSecurityTokenInvalidLifetimeException UnAuthorizedSecurityTokenInvalidLifetimeException => UnAuthorizedSecurityTokenInvalidLifetimeException.GetInstance();
        public static UnAuthorizedSecurityTokenInvalidSignatureException UnAuthorizedSecurityTokenInvalidSignatureException => UnAuthorizedSecurityTokenInvalidSignatureException.GetInstance();
        public static UnAuthorizedSecurityTokenInvalidSigningKeyException UnAuthorizedSecurityTokenInvalidSigningKeyException => UnAuthorizedSecurityTokenInvalidSigningKeyException.GetInstance();
        public static UnAuthorizedSecurityTokenNoExpirationException UnAuthorizedSecurityTokenNoExpirationException => UnAuthorizedSecurityTokenNoExpirationException.GetInstance();
        public static UnAuthorizedSecurityTokenNotYetValidException UnAuthorizedSecurityTokenNotYetValidException => UnAuthorizedSecurityTokenNotYetValidException.GetInstance();
        public static UnAuthorizedSecurityTokenReplayAddFailedException UnAuthorizedSecurityTokenReplayAddFailedException => UnAuthorizedSecurityTokenReplayAddFailedException.GetInstance();
        public static UnAuthorizedSecurityTokenReplayDetectedException UnAuthorizedSecurityTokenReplayDetectedException => UnAuthorizedSecurityTokenReplayDetectedException.GetInstance();
        public static UnAuthorizedSecurityTokenValidationException UnAuthorizedSecurityTokenValidationException => UnAuthorizedSecurityTokenValidationException.GetInstance();
        public static UnAuthorizedSecurityTokenSignatureKeyNotFoundException UnAuthorizedSecurityTokenSignatureKeyNotFoundException => UnAuthorizedSecurityTokenSignatureKeyNotFoundException.GetInstance();
        public static UnAuthorizedSecurityTokenValidationExceptionWrongAuthorizationHeader UnAuthorizedSecurityTokenValidationExceptionWrongAuthorizationHeader => UnAuthorizedSecurityTokenValidationExceptionWrongAuthorizationHeader.GetInstance();
        public static External External => External.GetInstance();
        public static Conflict Conflict => Conflict.GetInstance();
        public static AccountAccessDenied AccountAccessDenied => AccountAccessDenied.GetInstance();
        public static InvalidInput InvalidInput => InvalidInput.GetInstance();
        public static Config Config => Config.GetInstance();
        public static CommandValidation CommandValidation => CommandValidation.GetInstance();
        public static CommandDataValidation CommandDataValidation => CommandDataValidation.GetInstance();
        public static Authentication Authentication => Authentication.GetInstance();
        public static AuthenticationInvalidClient AuthenticationInvalidClient => AuthenticationInvalidClient.GetInstance();
        public static AuthenticationInvalidUser AuthenticationInvalidUser => AuthenticationInvalidUser.GetInstance();
        public static AuthenticationInvalidAudience AuthenticationInvalidAudience => AuthenticationInvalidAudience.GetInstance();
        public static AuthenticationInvalidAccountLocked AuthenticationInvalidAccountLocked => AuthenticationInvalidAccountLocked.GetInstance();
        public static AuthenticationInvalidAccountTemporaryLocked AuthenticationInvalidAccountTemporaryLocked => AuthenticationInvalidAccountTemporaryLocked.GetInstance();
        public static AuthenticationInvalidAccountPermanentLocked AuthenticationInvalidAccountPermanentLocked => AuthenticationInvalidAccountPermanentLocked.GetInstance();
        public static InvalidRefreshTokenRequest InvalidRefreshTokenRequest => InvalidRefreshTokenRequest.GetInstance();
        public static UnhandledException UnhandledException => UnhandledException.GetInstance();
        public static ItemNotFound ItemNotFound => ItemNotFound.GetInstance();
        public static TechnicalConfigurationError TechnicalConfigurationError => TechnicalConfigurationError.GetInstance();
        public static FileSizeExceeded FileSizeExceeded => FileSizeExceeded.GetInstance();
        public static ServerBusy ServerBusy => ServerBusy.GetInstance();
        public static HttpHeaderMissing HttpHeaderMissing => HttpHeaderMissing.GetInstance();
        public static ContentHeaderMissing ContentHeaderMissing => ContentHeaderMissing.GetInstance();


    }

    #region Error Code Classes

    public class UnAuthorized : EnumerationSingleton<int, UnAuthorized>
    {
        public UnAuthorized()
            : base(1001)
        {
        }
    }
    public class UnAuthorizedSecurityTokenExpiredException : EnumerationSingleton<int, UnAuthorizedSecurityTokenExpiredException>
    {
        public UnAuthorizedSecurityTokenExpiredException()
            : base(10011)
        {
        }
    }
    public class UnAuthorizedSecurityTokenInvalidAudienceException : EnumerationSingleton<int, UnAuthorizedSecurityTokenInvalidAudienceException>
    {
        public UnAuthorizedSecurityTokenInvalidAudienceException()
            : base(10012)
        {
        }
    }
    public class UnAuthorizedSecurityTokenInvalidIssuerException : EnumerationSingleton<int, UnAuthorizedSecurityTokenInvalidIssuerException>
    {
        public UnAuthorizedSecurityTokenInvalidIssuerException()
            : base(10013)
        {
        }
    }
    public class UnAuthorizedSecurityTokenInvalidLifetimeException : EnumerationSingleton<int, UnAuthorizedSecurityTokenInvalidLifetimeException>
    {
        public UnAuthorizedSecurityTokenInvalidLifetimeException()
            : base(10014)
        {
        }
    }
    public class UnAuthorizedSecurityTokenInvalidSignatureException : EnumerationSingleton<int, UnAuthorizedSecurityTokenInvalidSignatureException>
    {
        public UnAuthorizedSecurityTokenInvalidSignatureException()
            : base(10015)
        {
        }
    }
    public class UnAuthorizedSecurityTokenInvalidSigningKeyException : EnumerationSingleton<int, UnAuthorizedSecurityTokenInvalidSigningKeyException>
    {
        public UnAuthorizedSecurityTokenInvalidSigningKeyException()
            : base(10016)
        {
        }
    }
    public class UnAuthorizedSecurityTokenNoExpirationException : EnumerationSingleton<int, UnAuthorizedSecurityTokenNoExpirationException>
    {
        public UnAuthorizedSecurityTokenNoExpirationException()
            : base(10017)
        {
        }
    }
    public class UnAuthorizedSecurityTokenNotYetValidException : EnumerationSingleton<int, UnAuthorizedSecurityTokenNotYetValidException>
    {
        public UnAuthorizedSecurityTokenNotYetValidException()
            : base(10018)
        {
        }
    }
    public class UnAuthorizedSecurityTokenReplayAddFailedException : EnumerationSingleton<int, UnAuthorizedSecurityTokenReplayAddFailedException>
    {
        public UnAuthorizedSecurityTokenReplayAddFailedException()
            : base(10019)
        {
        }
    }
    public class Config : EnumerationSingleton<int, Config>
    {
        public Config()
            : base(10020)
        {
        }
    }
    public class UnAuthorizedSecurityTokenReplayDetectedException : EnumerationSingleton<int, UnAuthorizedSecurityTokenReplayDetectedException>
    {
        public UnAuthorizedSecurityTokenReplayDetectedException()
            : base(100110)
        {
        }
    }
    public class UnAuthorizedSecurityTokenValidationException : EnumerationSingleton<int, UnAuthorizedSecurityTokenValidationException>
    {
        public UnAuthorizedSecurityTokenValidationException()
            : base(100111)
        {
        }
    }
    public class UnAuthorizedSecurityTokenSignatureKeyNotFoundException : EnumerationSingleton<int, UnAuthorizedSecurityTokenSignatureKeyNotFoundException>
    {
        public UnAuthorizedSecurityTokenSignatureKeyNotFoundException()
            : base(100112)
        {
        }
    }
    public class UnAuthorizedSecurityTokenValidationExceptionWrongAuthorizationHeader : EnumerationSingleton<int, UnAuthorizedSecurityTokenValidationExceptionWrongAuthorizationHeader>
    {
        public UnAuthorizedSecurityTokenValidationExceptionWrongAuthorizationHeader()
            : base(100113)
        {
        }
    }
    public class External : EnumerationSingleton<int, External>
    {
        public External()
            : base(1002)
        {
        }
    }
    public class Conflict : EnumerationSingleton<int, Conflict>
    {
        public Conflict()
            : base(1003)
        {
        }
    }
    public class AccountAccessDenied : EnumerationSingleton<int, AccountAccessDenied>
    {
        public AccountAccessDenied()
            : base(1004)
        {
        }
    }
    public class InvalidInput : EnumerationSingleton<int, InvalidInput>
    {
        public InvalidInput()
            : base(1005)
        {
        }
    }
    public class CommandValidation : EnumerationSingleton<int, CommandValidation>
    {
        public CommandValidation()
            : base(1006)
        {
        }
    }
    public class CommandDataValidation : EnumerationSingleton<int, CommandDataValidation>
    {
        public CommandDataValidation()
            : base(1007)
        {
        }
    }
    public class Authentication : EnumerationSingleton<int, Authentication>
    {
        public Authentication()
            : base(1008)
        {
        }
    }
    public class AuthenticationInvalidClient : EnumerationSingleton<int, AuthenticationInvalidClient>
    {
        public AuthenticationInvalidClient()
            : base(10081)
        {
        }
    }
    public class AuthenticationInvalidUser : EnumerationSingleton<int, AuthenticationInvalidUser>
    {
        public AuthenticationInvalidUser()
            : base(10082)
        {
        }
    }

    public class AuthenticationInvalidAudience : EnumerationSingleton<int, AuthenticationInvalidAudience>
    {
        public AuthenticationInvalidAudience()
            : base(10083)
        {
        }
    }
    public class AuthenticationInvalidAccountLocked : EnumerationSingleton<int, AuthenticationInvalidAccountLocked>
    {
        public AuthenticationInvalidAccountLocked()
            : base(10084)
        {
        }
    }
    public class AuthenticationInvalidAccountTemporaryLocked : EnumerationSingleton<int, AuthenticationInvalidAccountTemporaryLocked>
    {
        public AuthenticationInvalidAccountTemporaryLocked()
            : base(10085)
        {
        }
    }

    public class AuthenticationInvalidAccountPermanentLocked : EnumerationSingleton<int, AuthenticationInvalidAccountPermanentLocked>
    {
        public AuthenticationInvalidAccountPermanentLocked()
            : base(10086)
        {
        }
    }
    public class InvalidRefreshTokenRequest : EnumerationSingleton<int, InvalidRefreshTokenRequest>
    {
        public InvalidRefreshTokenRequest()
            : base(100114)
        {
        }
    }
    public class UnhandledException : EnumerationSingleton<int, UnhandledException>
    {
        public UnhandledException()
            : base(1009)
        {
        }
    }
    public class ItemNotFound : EnumerationSingleton<int, ItemNotFound>
    {
        public ItemNotFound()
            : base(10010)
        {
        }
    }
    public class TechnicalConfigurationError : EnumerationSingleton<int, TechnicalConfigurationError>
    {
        public TechnicalConfigurationError()
            : base(3001)
        {
        }
    }
    public class FileSizeExceeded : EnumerationSingleton<int, FileSizeExceeded>
    {
        public FileSizeExceeded()
            : base(10050001)
        {
        }
    }
    public class ServerBusy : EnumerationSingleton<int, ServerBusy>
    {
        public ServerBusy()
            : base(50053)
        {
        }
    }
    public class HttpHeaderMissing : EnumerationSingleton<int, HttpHeaderMissing>
    {
        public HttpHeaderMissing()
            : base(10050002)
        {
        }
    }
    public class ContentHeaderMissing : EnumerationSingleton<int, ContentHeaderMissing>
    {
        public ContentHeaderMissing()
            : base(10050003)
        {
        }
    }

    #endregion Error Code Classes
}
