using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetDoodle.OAuthServer.Infrastructure.Config
{
    public interface IConfigurationManager
    {
        int AccessTokenTimeoutInSeconds { get; }
    }
}