using System.Diagnostics.CodeAnalysis;
using Xamarin.Essentials;
using XMP.API.Services.Abstract;

namespace XMP.API.Services.Implementation
{
    public class ApiSettingsService : IApiSettingsService
    {
        private const string AndroidHost = "10.0.2.2";

        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1303:ConstFieldNamesMustBeginWithUpperCaseLetter", Justification = "Reviewed.")]
        private const string iOSHost = "localhost";

        public ApiSettingsService(IWebConnectionService webConnectionService)
        {
            WebConnectionService = webConnectionService;

            Host = DeviceInfo.Platform == DevicePlatform.iOS ? iOSHost : AndroidHost;
        }

        public IOAuthSettings OAuthSettings { get; } = new OAuthSettings();

        public string ServiceHostUrl => $"http://{Host}:5000/api/vts/";

        public string AuthorizationHostUrl => $"http://{Host}:5001/";

        public IRefreshTokenUpdater RefreshTokenUpdater { get; set; }

        protected string Host { get; }

        protected IWebConnectionService WebConnectionService { get; }

        public void SetAuthorizationToken(string token)
        {
            WebConnectionService.Bearer = token;
        }
    }
}
