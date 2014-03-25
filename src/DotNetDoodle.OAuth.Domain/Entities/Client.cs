using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetDoodle.OAuth.Domain.Entities
{
    public class Client
    {
        public Client(string name)
        {
            Id = Guid.NewGuid().ToString("N");
        }

        public string Id { get; protected set; }
        public string Name { get; protected set; }
    }
}