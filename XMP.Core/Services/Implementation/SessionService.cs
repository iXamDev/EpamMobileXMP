using System;
using System.Security.Authentication;
using System.Threading.Tasks;
using Xamarin.Essentials;
using XMP.API.Services.Abstract;
using XMP.Core.Constants;
using XMP.Core.Models;
using XMP.Core.Services.Abstract;

namespace XMP.Core.Services.Implementation
{
    public class SessionService : ISessionService
    {
        private object _syncRoot = new object();

        private TaskCompletionSource<bool> _refreshTokenTCS;

        private bool _isRefreshingToken;

        private string _authorizationToken;

        public SessionService(IApiSettingsService apiSettingsService, IAuthenticationApiService authenticationApiService)
        {
            ApiSettingsService = apiSettingsService;

            AuthenticationApiService = authenticationApiService;
        }

        public event EventHandler OnCredentialsFails;

        public string UserLogin { get; private set; }

        public User User { get; } = new User { FullName = "Arkadiy Dobkin" };

        public bool Active => !string.IsNullOrEmpty(_authorizationToken);

        protected IApiSettingsService ApiSettingsService { get; }

        protected IAuthenticationApiService AuthenticationApiService { get; }

        private UserCredentials Credentials { get; set; }

        public Task<bool> RefreshToken()
        {
            lock (_syncRoot)
            {
                if (Credentials == null)
                    return Task.FromResult(false);

                if (!_isRefreshingToken)
                {
                    _isRefreshingToken = true;
                    _refreshTokenTCS = new TaskCompletionSource<bool>();
                    Task.Run(() => ExecuteTokenRefresh(Credentials));
                }

                return _refreshTokenTCS.Task;
            }
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
                _authorizationToken = accessToken;

                return Launch(new UserCredentials { Login = login, Password = password }, accessToken);
            }

            return false;
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
            catch (InvalidCredentialException)
            {
                wrongCredentials = true;
            }
            catch
            {
            }

            lock (_syncRoot)
            {
                if (success)
                {
                    SetAuthorizationToken(token);
                }
                else
                if (wrongCredentials)
                {
                    Logout();

                    RaiseCredentialsFails();
                }

                _refreshTokenTCS.TrySetResult(success);
            }

            if (success)
                await SaveAuthorizationToken(token);

            return success;
        }

        private void SetAuthorizationToken(string token)
        {
            _authorizationToken = token;

            ApiSettingsService.SetAuthorizationToken(token);
        }

        private Task SaveAuthorizationToken(string token)
        => SecureStorage.SetAsync(SecureStorageConstants.AccessTokenKey, token);

        private void RaiseCredentialsFails()
        => OnCredentialsFails?.Invoke(this, EventArgs.Empty);

        private void Logout()
        {
            Credentials = null;

            _authorizationToken = null;

            ApiSettingsService.SetAuthorizationToken(null);

            UserLogin = null;

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

            UserLogin = credentials.Login;

            SetAuthorizationToken(token);

            return true;
        }
    }
}
