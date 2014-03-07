using DotNetDoodle.OAuthServer.Common;
using System;
using System.Collections.Generic;

namespace DotNetDoodle.OAuthServer.Data.Entities
{
    /// <summary>
    /// Represents the resource server in OAuth language. The resource server is the server hosting the protected 
    /// resources, capable of accepting and responding to protected resource requests using access tokens.
    /// </summary>
    public class ApplicationEntity : IEntity<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string LogoUrl { get; set; }
        public string Namespace { get; set; }
        public string Audience { get; set; }

        // OAuth Settings
        public int TokenLifetime { get; set; }
        public bool AllowRefreshToken { get; set; }
        public bool RequireConsent { get; set; }
        public bool AllowRememberConsentDecision { get; set; }

        // Status
        public ApplicationStatus Status { get; set; }

        // IEntity
        public DateTimeOffset CreatedOn { get; set; }

        // References
        public ICollection<ScopeEntity> Scopes { get; set; }
    }
}
