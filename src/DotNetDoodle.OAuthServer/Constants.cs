using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetDoodle.OAuthServer
{
    internal static class Constants
    {
        public static class Parameters
        {
            public const string ResponseType = "response_type";
            public const string GrantType = "grant_type";
            public const string ClientId = "client_id";
            public const string ClientSecret = "client_secret";
            public const string RedirectUri = "redirect_uri";
            public const string Scope = "scope";
            public const string State = "state";
            public const string Code = "code";
            public const string RefreshToken = "refresh_token";
            public const string Username = "username";
            public const string Password = "password";
            public const string Error = "error";
            public const string ErrorDescription = "error_description";
            public const string ErrorUri = "error_uri";
            public const string ExpiresIn = "expires_in";
            public const string AccessToken = "access_token";
            public const string TokenType = "token_type";
        }

        public static class ResponseTypes
        {
            public const string Code = "code";
            public const string Token = "token";
        }

        public static class GrantTypes
        {
            public const string AuthorizationCode = "authorization_code";
            public const string ClientCredentials = "client_credentials";
            public const string RefreshToken = "refresh_token";
            public const string Password = "password";
        }

        public static class TokenTypes
        {
            public const string Bearer = "bearer";
        }

        public static class Errors
        {
            /// <summary>
            /// The request is missing a required parameter, includes an
            /// invalid parameter value, includes a parameter more than once, or is otherwise malformed.
            /// </summary>
            public const string InvalidRequest = "invalid_request";

            /// <summary>
            /// Client authentication failed (e.g., unknown client, no
            /// client authentication included, or unsupported
            /// authentication method).  The authorization server MAY
            /// return an HTTP 401 (Unauthorized) status code to indicate
            /// which HTTP authentication schemes are supported.  If the
            /// client attempted to authenticate via the "Authorization"
            /// request header field, the authorization server MUST
            /// respond with an HTTP 401 (Unauthorized) status code and
            /// include the "WWW-Authenticate" response header field
            /// matching the authentication scheme used by the client.
            /// </summary>
            public const string InvalidClient = "invalid_client";

            /// <summary>
            /// The provided authorization grant (e.g., authorization
            /// code, resource owner credentials) or refresh token is invalid, expired, revoked, does not match the redirection
            /// URI used in the authorization request, or was issued to another client.
            /// </summary>
            public const string InvalidGrant = "invalid_grant";
            public const string UnsupportedResponseType = "unsupported_response_type";
            public const string UnsupportedGrantType = "unsupported_grant_type";

            /// <summary>
            /// The client is not authorized to request an authorization code using this method.
            /// </summary>
            public const string UnauthorizedClient = "unauthorized_client";

            /// <summary>
            /// The authorization server encountered an unexpected
            /// condition that prevented it from fulfilling the request.
            /// (This error code is needed because a 500 Internal Server
            /// Error HTTP status code cannot be returned to the client
            /// via an HTTP redirect.)
            /// </summary>
            public const string ServerError = "server_error";

            /// <summary>
            /// The authorization server is currently unable to handle
            /// the request due to a temporary overloading or maintenance
            /// of the server.  (This error code is needed because a 503
            /// Service Unavailable HTTP status code cannot be returned
            /// to the client via an HTTP redirect.)
            /// </summary>
            public const string TemporarilyUnavailable = "temporarily_unavailable";
        }

        public static class Extra
        {
            public const string ClientId = "client_id";
            public const string RedirectUri = "redirect_uri";
        }
    }
}