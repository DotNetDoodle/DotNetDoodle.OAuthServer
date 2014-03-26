using DotNetDoodle.OAuth.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DotNetDoodle.OAuth.Domain.Tests
{
    public class ClientTests
    {
        [Fact]
        public void Client_Should_Throw_Null_Reference_Exception_If_The_Name_Is_Null()
        {
            Assert.Throws<ArgumentNullException>(() => new Client(null, OAuthGrant.Client));
        }
    }
}
