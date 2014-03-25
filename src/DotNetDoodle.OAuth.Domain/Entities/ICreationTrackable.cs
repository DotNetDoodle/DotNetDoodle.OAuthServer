using System;

namespace DotNetDoodle.OAuth.Domain.Entities
{
    public interface ICreationTrackable
    {
        DateTimeOffset CreatedOn { get; }
    }
}