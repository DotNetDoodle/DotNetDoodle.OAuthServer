using System;

namespace DotNetDoodle.OAuth.Domain.Entities
{
    public interface IUpdatesTrackable
    {
        DateTimeOffset LastUpdatedOn { get; }
    }
}