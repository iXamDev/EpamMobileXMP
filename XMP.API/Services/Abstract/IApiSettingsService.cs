using System;
namespace XMP.API.Services.Abstract
{
    public interface IApiSettingsService
    {
        IOAuthSettings OAuthSettings { get; }

        string HostUrl { get; }

        string AuthorizationHostUrl { get; }

        IRefreshTokenProvider RefreshTokenProvider { get; set; }

        void SetAuthorizationToken(string token);
    }
}
