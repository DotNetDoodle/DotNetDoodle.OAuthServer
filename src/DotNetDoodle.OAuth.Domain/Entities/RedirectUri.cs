using System;

namespace DotNetDoodle.OAuth.Domain.Entities
{
    public class RedirectUri : IObject, ICreationTrackable, IUpdatesTrackable, IDeleteable
    {
        public RedirectUri(string clientId, string redirectUri, string description)
        {
            if (clientId == null) throw new ArgumentNullException("clientId");
            if (redirectUri == null) throw new ArgumentNullException("redirectUri");
            if (description == null) throw new ArgumentNullException("description");

            Id = GenerateKey(clientId, redirectUri);
            ClientId = clientId;
            Uri = redirectUri;
            Description = description;
            CreatedOn = DateTimeOffset.UtcNow;
        }

        public string Id { get; protected set; }
        public string ClientId { get; protected set; }
        public string Uri { get; protected set; }
        public string Description { get; protected set; }

        public DateTimeOffset CreatedOn { get; protected set; }
        public DateTimeOffset LastUpdatedOn { get; protected set; }

        public DateTimeOffset? DeletedOn { get; private set; }

        public virtual void AlterDescription(string description)
        {
            Description = description;
            Updated();
        }

        public virtual void Delete()
        {
            if (DeletedOn == null)
            {
                DeletedOn = DateTimeOffset.UtcNow;
                Updated();
            }
        }

        // statics

        public static string GenerateKey(string clientId, string redirectUri)
        {
            return string.Format(Constants.ClientRedirectUriKeyTemplate, clientId, redirectUri);
        }

        // privates

        private void Updated()
        {
            LastUpdatedOn = DateTimeOffset.UtcNow;
        }
    }
}