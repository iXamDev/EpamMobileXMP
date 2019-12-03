using System;
using XMP.API.Services.Abstract;

namespace XMP.API.Services.Implementation
{
    public class ApiSettingsService : IApiSettingsService
    {
        public ApiSettingsService(IWebConnectionService webConnectionService)
        {
            WebConnectionService = webConnectionService;
        }

        protected IWebConnectionService WebConnectionService { get; }

        public IOAuthSettings OAuthSettings { get; } = new OAuthSettings();

        public string HostUrl => "http://localhost:5000";

        public string AuthorizationHostUrl =>
        //"http://10.0.2.2:5001";
        "http://localhost:5001";

        public IRefreshTokenProvider RefreshTokenProvider { get; set; }

        public void SetAuthorizationToken(string token)
        {
            WebConnectionService.Bearer = token;
        }
    }
}
