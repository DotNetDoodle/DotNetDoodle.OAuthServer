using System;

namespace DotNetDoodle.OAuthServer.Data.Entities
{
    public interface IEntity<TId> : IEntity where TId : IComparable
    {
        TId Id { get; set; }
    }
}
