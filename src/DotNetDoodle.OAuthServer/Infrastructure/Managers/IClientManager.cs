using DotNetDoodle.OAuthServer.Infrastructure.Objects;
using System.Threading.Tasks;

namespace DotNetDoodle.OAuthServer.Infrastructure.Managers
{
    public interface IClientManager
    {
       Task<Client> GetVerfiedClientAsync(string clientId, string clientSecret);
    }
}