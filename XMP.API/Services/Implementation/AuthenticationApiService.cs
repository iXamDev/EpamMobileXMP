using System;
using System.Security.Authentication;
using System.Threading.Tasks;
using IdentityModel.Client;
using XMP.API.Models;
using XMP.API.Services.Abstract;

namespace XMP.API.Services.Implementation
{
    public class AuthenticationApiService : CommonApiService, IAuthenticationApiService
    {
        public AuthenticationApiService(IWebConnectionService webConnectionService, IApiSettingsService apiSettingService)
            : base(webConnectionService, apiSettingService)
        {
        }

        public async Task<OAuthToken> Login(string login, string password)
        {
            var client = WebConnectionService.Client;

            var request = new DiscoveryDocumentRequest
            {
                Address = ApiSettingsService.AuthorizationHostUrl
            };

            request.Policy.RequireHttps = false;

            request.Policy.ValidateIssuerName = false;

            var identityServer = await client.GetDiscoveryDocumentAsync(request);

            if (identityServer.IsError)
                throw identityServer.Exception;

            var tokenResponce = await client.RequestPasswordTokenAsync(
                new PasswordTokenRequest
                {
                    Address = identityServer.TokenEndpoint,
                    ClientId = ApiSettingsService.OAuthSettings.ClientId,
                    ClientSecret = ApiSettingsService.OAuthSettings.ClientSecret,
                    Password = password,
                    UserName = login,
                    Scope = ApiSettingsService.OAuthSettings.ResourceId
                });

            if (tokenResponce.IsError || tokenResponce.AccessToken == null)
            {
                if (tokenResponce.ErrorType == ResponseErrorType.Protocol &&
                    tokenResponce.Error == ApiSettingsService.OAuthSettings.InvalidCredentialsErrorKey)
                {
                    throw new InvalidCredentialException("Invalid Credentials", tokenResponce.Exception);
                }

                var err = $"{tokenResponce.ErrorType} > {tokenResponce.Error} > {tokenResponce.ErrorDescription}";

                throw new Exception(err, tokenResponce.Exception);
            }

            return new OAuthToken(tokenResponce.AccessToken);
        }
    }
}
