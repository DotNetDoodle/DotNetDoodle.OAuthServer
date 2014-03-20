using System;

namespace DotNetDoodle.OAuthServer.Data.Entities
{
    public interface IEntity
    {
        DateTimeOffset CreatedOn { get; set; }
    }
}
