using DotNetDoodle.OAuthServer.Infrastructure.Managers;
using DotNetDoodle.OAuthServer.Infrastructure.Objects;
using DotNetDoodle.OAuthServer.Infrastructure.Providers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DotNetDoodle.OAuthServer.Tests
{
    public class DotNetDoodleOAuthAuthorizationServerProviderTests
    {
        [Fact]
        public async Task ShouldSetTheClientToTheContextSuccessfully()
        {
            Mock<IClientManager> clientManagerMock = new Mock<IClientManager>();
            clientManagerMock.Setup(c => c.GetVerfiedClientAsync("1234", "12345678"))
                .Returns<string, string>((clientId, secret) => Task.FromResult(new Client 
                {
                    Id = clientId,
                    Flow = Infrastructure.OAuthFlow.Client,
                    RequireConsent = true,
                    AllowRefreshToken = true
                }));

            Mock<IServiceProvider> serviceProvider = new Mock<IServiceProvider>();
            serviceProvider.Setup(m => m.GetService(typeof(IClientManager))).Returns(clientManagerMock.Object);

            var provider = new DotNetDoodleOAuthAuthorizationServerProvider(null, null);
            provider.ValidateClientAuthentication(null);
        }
    }
}
