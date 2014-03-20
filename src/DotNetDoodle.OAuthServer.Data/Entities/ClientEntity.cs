using DotNetDoodle.OAuthServer.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DotNetDoodle.OAuthServer.Data.Entities
{
    public class ClientEntity : IEntity<string>
    {
        [StringLength(150)]
        public string Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        public string ClientSecretHash { get; set; }
        public OAuthFlow AllowedFlow { get; set; }

        // Flags

        /// <summary>
        /// Indicates whether the refresh token should be issued for this client or not.
        /// </summary>
        public bool AllowRefreshToken { get; set; }

        /// <remarks>
        /// Only checked if (Flow == Code || Flow == Implicit)
        /// </remarks>
        public bool RequireConsent { get; set; }

        /// <summary>
        /// Indicates whether the client is enabled or not.
        /// </summary>
        public bool IsEnabled { get; set; }

        // IEntity

        public DateTimeOffset CreatedOn { get; set; }

        // References

        /// <summary>
        /// Represents the allowed scopes for the client. This implicitly maps the clients
        /// to an Application as a scope is tied to an application.
        /// </summary>
        public ICollection<ScopeEntity> Scopes { get; set; }
        public ICollection<ClientRedirectUriEntity> RedirectUris { get; set; }
    }
}
