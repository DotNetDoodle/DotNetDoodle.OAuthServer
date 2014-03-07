using DotNetDoodle.OAuthServer.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetDoodle.OAuthServer.Data.Entities
{
    /// <summary>
    /// This storage space represents the OAuth request handles such as Authorization Code, Refresh Token Request, etc.
    /// </summary>
    public class StoredGrantEntity : IEntity<string>
    {
        /// <summary>
        /// The identifier of the grant. E.g. The authorization code for the Authorization Code Grant.
        /// </summary>
        [StringLength(150)]
        public string Id { get; set; }

        [StringLength(150)]
        public string ClientId { get; set; }
        public int ApplicationId { get; set; }

        [Required]
        [StringLength(200)]
        public string Subject { get; set; }

        public StoredGrantType Type { get; set; }
        public bool CreateRefreshToken { get; set; }

        /// <summary>
        /// The Redirect URI to be used on the authorization code flow. This needs to be
        /// checked if redirect URI from authorize and token request match.
        /// </summary>
        [StringLength(1000)]
        public string RedirectUri { get; set; }

        public DateTimeOffset ExpiresOn { get; set; }
        public DateTimeOffset? RefreshTokenExpiresOn { get; set; }

        // IEntity

        public DateTimeOffset CreatedOn { get; set; }

        // References

        public ClientEntity Client { get; set; }
        public ApplicationEntity Application { get; set; }
        public ICollection<StoredGrantResourceOwnerClaimEntity> ResourceOwnerClaims { get; set; }
        public ICollection<ScopeEntity> AllowedScopes { get; set; }
    }
}
