using System;
using System.Threading.Tasks;
using XMP.Core.Models;
using XMP.Core.Services.Abstract;
using XMP.API.Services.Abstract;
using XMP.Core.Constants;
using System.Security.Authentication;
using Xamarin.Essentials;

namespace XMP.Core.Services.Implementation
{
    public class SessionService : ISessionService
    {
        public SessionService(IApiSettingsService apiSettingsService, IAuthenticationApiService authenticationApiService)
        {
            ApiSettingsService = apiSettingsService;

            AuthenticationApiService = authenticationApiService;
        }

        private object syncRoot = new object();

        private UserCredentials Credentials { get; set; }

        private TaskCompletionSource<bool> refreshTokenTCS;

        private bool isRefreshingToken;

        private string authorizationToken;

        protected IApiSettingsService ApiSettingsService { get; }

        protected IAuthenticationApiService AuthenticationApiService { get; }

        public bool Active => !string.IsNullOrEmpty(authorizationToken);

        public User User { get; } = new User { FullName = "Arkadiy Dobkin" };

        public Task<bool> RefreshToken()
        {
            lock (syncRoot)
            {
                if (Credentials == null)
                    return Task.FromResult(false);

                if (!isRefreshingToken)
                {
                    isRefreshingToken = true;
                    refreshTokenTCS = new TaskCompletionSource<bool>();
                    Task.Run(() => ExecuteTokenRefresh(Credentials));
                }

                return refreshTokenTCS.Task;
            }
        }

        private async Task<bool> ExecuteTokenRefresh(UserCredentials credentials)
        {
            var success = false;

            var wrongCredentials = false;

            string token = null;

            try
            {
                token = await GetAccessToken(credentials);

                success = true;
            }
            catch (AuthenticationException _)
            {
                wrongCredentials = true;
            }
            catch
            {
            }

            lock (syncRoot)
            {
                if (success)
                    SetAuthorizationToken(token);

                if (wrongCredentials)
                    HandleWrongCredentials();

                refreshTokenTCS.TrySetResult(success);
            }

            if (success)
                await SaveAuthorizationToken(token);

            return success;
        }

        private void SetAuthorizationToken(string token)
        {
            authorizationToken = token;

            ApiSettingsService.SetAuthorizationToken(token);
        }

        private Task SaveAuthorizationToken(string token)
        => SecureStorage.SetAsync(SecureStorageConstants.AccessTokenKey, token);

        private void HandleWrongCredentials()
        {
            Credentials = null;

            authorizationToken = null;

            ApiSettingsService.SetAuthorizationToken(null);

            SecureStorage.Remove(SecureStorageConstants.LoginKey);

            SecureStorage.Remove(SecureStorageConstants.PasswordKey);

            SecureStorage.Remove(SecureStorageConstants.AccessTokenKey);
        }

        private async Task<string> GetAccessToken(UserCredentials credentials)
        {
            var token = await AuthenticationApiService.Login(credentials.Login, credentials.Password);

            return token.AccessToken;
        }

        private bool Launch(UserCredentials credentials, string token)
        {
            Credentials = credentials;

            SetAuthorizationToken(token);

            return true;
        }

        public async Task<bool> Start(UserCredentials credentials)
        {
            var token = await GetAccessToken(credentials);

            if (!string.IsNullOrEmpty(token))
            {
                Launch(credentials, token);

                await SaveAuthorizationToken(token);

                await SecureStorage.SetAsync(SecureStorageConstants.LoginKey, credentials.Login);

                await SecureStorage.SetAsync(SecureStorageConstants.PasswordKey, credentials.Password);

                return true;
            }

            return false;
        }

        public async Task<bool> Reactivate()
        {
            var login = await SecureStorage.GetAsync(SecureStorageConstants.LoginKey);

            var password = await SecureStorage.GetAsync(SecureStorageConstants.PasswordKey);

            var accessToken = await SecureStorage.GetAsync(SecureStorageConstants.AccessTokenKey);

            bool Valid(string s) => !string.IsNullOrWhiteSpace(s);

            if (Valid(login) && Valid(password) && Valid(accessToken))
            {
                this.authorizationToken = accessToken;

                return Launch(new UserCredentials { Login = login, Password = password }, accessToken);
            }

            return false;
        }
    }
}
