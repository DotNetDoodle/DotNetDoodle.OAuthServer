using DotNetDoodle.OAuthServer.Identity.Data.Entities;
using DotNetDoodle.OAuthServer.Infrastructure.Config;
using DotNetDoodle.OAuthServer.Infrastructure.Managers;
using DotNetDoodle.OAuthServer.Infrastructure.Objects;
using DotNetDoodle.Owin.Dependencies;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Logging;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DotNetDoodle.OAuthServer.Infrastructure.Providers
{
    public class DotNetDoodleOAuthAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
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
                        context.OwinContext.Set<Client>(Constants.Owin.ClientObjectEnvironmentKey, client);
                        context.Validated(clientId);
                    }
                    else
                    {
                        _logger.WriteInformation(string.Format("Client could not be validated. clientId: {0}", clientId));
                        context.SetError(Constants.Errors.InvalidClient, "Client credentials are invalid.");
                        context.Rejected();
                    }
                }
                catch (Exception ex)
                {
                    _logger.WriteError("Could not get the client through the IClientManager implementation.", ex);
                    context.SetError(Constants.Errors.ServerError);
                    context.Rejected();
                }
            }
            else
            {
                _logger.WriteInformation(string.Format("The client credentials could not be retrieved. Headers: {0}", string.Join("; ", context.Request.Headers.Select(header => string.Concat(header.Key, ": ", header.Value)).ToArray())));
                context.SetError(Constants.Errors.InvalidClient, "Client credentials could not be retrieved through the Authorization header.");
                context.Rejected();
            }
        }

        // Actual grants

        public override Task GrantClientCredentials(OAuthGrantClientCredentialsContext context)
        {
            return base.GrantClientCredentials(context);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            _logger.WriteVerbose("BoM: GrantResourceOwnerCredentials");

            if (string.IsNullOrEmpty(context.ClientId) == false)
            {
                Client client = context.OwinContext.Get<Client>(Constants.Owin.ClientObjectEnvironmentKey);
                if (client.Flow == OAuthFlow.ResourceOwner) 
                {
                    _logger.WriteVerbose(string.Format("Client flow matches the requested flow. flow: {0}", Enum.GetName(typeof(OAuthFlow), client.Flow)));
                    IServiceProvider requestContainer = context.OwinContext.Environment.GetRequestContainer();
                    UserManager<UserEntity> userManager = requestContainer.GetService<UserManager<UserEntity>>();

                    UserEntity user;
                    try
                    {
                        user = await userManager.FindAsync(context.UserName, context.Password);
                    }
                    catch (Exception ex)
                    {
                        _logger.WriteError(string.Format("Could not retrieve the user. username: {0}", context.UserName), ex);
                        context.SetError(Constants.Errors.ServerError);
                        context.Rejected();

                        // Return here so that we don't process further. Not ideal but needed to be done here.
                        return;
                    }

                    if (user != null)
                    {
                        try
                        {
                            _logger.WriteVerbose(string.Format("User is found. userId: {0}, clientId: {1}", user.Id, client.Id));
                            ClaimsIdentity identity = await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ExternalBearer);
                            context.Validated(identity);
                        }
                        catch (Exception ex)
                        {
                            _logger.WriteError(string.Format("The ClaimsIdentity could not be created by the UserManager. userId: {0}, username: {1}", user.Id, user.UserName), ex);
                            context.SetError(Constants.Errors.ServerError);
                            context.Rejected();
                        }
                    }
                    else
                    {
                        _logger.WriteInformation(string.Format("The resource owner credentials are invalid or resource owner does not exist. clientId: {0}, flow: {1}, username: {2}", client.Id, Enum.GetName(typeof(OAuthFlow), client.Flow), context.UserName));
                        context.SetError(Constants.Errors.InvalidGrant, "The resource owner credentials are invalid or resource owner does not exist.");
                        context.Rejected();
                    }
                }
                else
                {
                    _logger.WriteInformation(string.Format("Client is not allowed for the 'Resource Owner Password Credentials Grant'. clientId: {0}, allowedFlow: {1}", client.Id, Enum.GetName(typeof(OAuthFlow), client.Flow)));
                    context.SetError(Constants.Errors.UnauthorizedClient, "Client is not allowed for the 'Resource Owner Password Credentials Grant'");
                    context.Rejected();
                }
            }
            else
            {
                _logger.WriteInformation(string.Format("The clientId is not present inside the context. Headers: {0}", string.Join("; ", context.Request.Headers.Select(header => string.Concat(header.Key, ": ", header.Value)).ToArray())));
                context.SetError(Constants.Errors.InvalidClient, "ClientId is not present inside the request.");
                context.Rejected();
            }
        }
    }
}