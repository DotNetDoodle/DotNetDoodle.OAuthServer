using System;

namespace DotNetDoodle.OAuthServer.Data.Entities
{
    public class ClientRedirectUriEntity : IEntity<int>
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public string Uri { get; set; }
        public string Description { get; set; }

        // IEntity

        public DateTimeOffset CreatedOn { get; set; }

        // References

        public ClientEntity Client { get; set; }
    }
}
