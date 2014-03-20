using System;
using System.ComponentModel.DataAnnotations;

namespace DotNetDoodle.OAuthServer.Data.Entities
{
    public class StoredGrantResourceOwnerClaimEntity : IEntity<int>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string StoredGrantId { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Value { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        // References

        public StoredGrantEntity StoredGrant { get; set; }
    }
}
