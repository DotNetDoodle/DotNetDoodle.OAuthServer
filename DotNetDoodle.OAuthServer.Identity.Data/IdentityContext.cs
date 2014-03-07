using DotNetDoodle.OAuthServer.Identity.Data.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DotNetDoodle.OAuthServer.Identity.Data
{
    public class IdentityContext : IdentityDbContext<User>
    {
    }
}
