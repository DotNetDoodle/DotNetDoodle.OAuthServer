using DotNetDoodle.OAuthServer.Infrastructure.Config;
using DotNetDoodle.OAuthServer.Infrastructure.Managers;
using DotNetDoodle.OAuthServer.Infrastructure.Objects;
using DotNetDoodle.Owin.Dependencies;
using Microsoft.Owin.Logging;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetDoodle.OAuthServer.Infrastructure.Providers
{
    public class DotNetDoodleOAuthAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private const string ClientContextKey = "OAuthServer:oauth:client";
        private readonly IConfigurationManager _configManager;
        private readonly ILogger _logger;

        public DotNetDoodleOAuthAuthorizationServerProvider(IAppBuilder app, IConfigurationManager configManager)
        {
            _configManager = configManager;
            _logger = app.CreateLogger<DotNetDoodleOAuthAuthorizationServerProvider>();
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId;
            string clientSecret;

            if (context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                _logger.WriteVerbose(string.Format("Client credentials has been retrieved from the Authorization header. clientId: {0}", clientId));
                IServiceProvider requestContainer = context.OwinContext.Environment.GetRequestContainer();
                IClientManager clientManager = requestContainer.GetService<IClientManager>();

                try
                {
                    Client client = await clientManager.GetVerfiedClientAsync(clientId, clientSecret);
                    if (client != null)
                    {
                        _logger.WriteVerbose(string.Format("Client has been verified. clientId: {0}", clientId));
                        context.OwinContext.Set<Client>(ClientContextKey, client);
                        context.Validated(clientId);
                        return;
                    }
                    else
                    {
                        _logger.WriteInformation(string.Format("Client could not be validated. clientId: {0}", clientId));
                        context.SetError(Constants.Errors.InvalidClient, "Client credentials are invalid.");
                    }
                }
                catch (Exception ex)
                {
                    _logger.WriteError("Could not get the client through the IClientManager implementation.", ex);
                    context.SetError(Constants.Errors.ServerError);
                }
            }
            else
            {
                _logger.WriteInformation(string.Format("The client credentials could not be retrieved. Headers: {0}", string.Join("; ", context.Request.Headers.Select(header => string.Concat(header.Key, ": ", header.Value)).ToArray())));
                context.SetError(Constants.Errors.InvalidClient, "Client credentials could not be retrieved through the Authorization header.");
            }

            context.Rejected();
        }

        public override Task GrantClientCredentials(OAuthGrantClientCredentialsContext context)
        {
            return base.GrantClientCredentials(context);
        }
    }
}