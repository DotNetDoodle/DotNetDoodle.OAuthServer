using System;

namespace DotNetDoodle.OAuth.Domain.Entities
{
    public interface IDeleteable
    {
        DateTimeOffset? DeletedOn { get; }
        void Delete();
    }
}