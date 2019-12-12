using System;

namespace XMP.API.Services.Abstract
{
    public interface IApiSettingsService
    {
        IOAuthSettings OAuthSettings { get; }

        string ServiceHostUrl { get; }

        string AuthorizationHostUrl { get; }

        IRefreshTokenUpdater RefreshTokenUpdater { get; set; }

        void SetAuthorizationToken(string token);
    }
}
