using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetDoodle.OAuthServer.Data.Entities
{
    /// <summary>
    /// This entity represents the scope which an application holds. For example; READ, USER_EMAIL, USER_GENDER, etc.
    /// It also has references to allowed clients for the particular scope.
    /// </summary>
    public class ScopeEntity : IEntity<string>
    {
        /// <summary>
        /// The Id of the scope entity. This also acts as the name of the scope.
        /// </summary>
        [StringLength(150)]
        public string Id { get; set; }
        public int ApplicationId { get; set; }

        [Required]
        [StringLength(200)]
        public string DisplayName { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }
        public bool IsEmphasized { get; set; }

        // IEntity

        public DateTimeOffset CreatedOn { get; set; }

        // References

        public ApplicationEntity Application { get; set; }
        public ICollection<ClientEntity> AllowedInClients { get; set; }
        public ICollection<StoredGrantEntity> AllowedInStoredGrants { get; set; }
    }
}
