using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using XMP.API.Exceptions;
using XMP.API.Models;
using XMP.API.Services.Abstract;

namespace XMP.API.Services.Implementation
{
    public abstract class CommonApiService
    {
        protected CommonApiService(IWebConnectionService webConnectionService, IApiSettingsService apiSettingService)
        {
            WebConnectionService = webConnectionService;

            ApiSettingsService = apiSettingService;
        }

        protected IApiSettingsService ApiSettingsService { get; }

        protected IWebConnectionService WebConnectionService { get; }

        protected virtual string GetAbsoluteUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url) || url.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                return url;

            return AbsoluteUrlFromRelative(url);
        }

        protected virtual string AbsoluteUrlFromRelative(string url)
        => ApiSettingsService.ServiceHostUrl + url;

        protected StringContent ToStringContent(object data)
        {
            return new StringContent(
                JsonConvert.SerializeObject(data, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }) ?? string.Empty,
                Encoding.UTF8,
                "application/json");
        }

        protected virtual async Task<RequestResult> ExecuteRequestAsync(
            string url,
            HttpMethod method,
            HttpContent postData = null,
            CancellationToken? cancellationToken = null,
            bool tryToRefreshAccessToken = true)
        {
            var needToTryGetNewAccessToken = tryToRefreshAccessToken;

            bool tryMoreTime;

            RequestResult result = null;

            do
            {
                tryMoreTime = false;

                result = await WebConnectionService.ExecuteRequestAsync(GetAbsoluteUrl(url), method, postData, cancellationToken);

                if (!result.IsSuccess)
                {
                    tryMoreTime = needToTryGetNewAccessToken && await NeedToTryWithNewToken(result);

                    ThrowWebConnectionExceptionIfNeeded(needToTryGetNewAccessToken, tryMoreTime, result);
                }

                needToTryGetNewAccessToken = false;
            }
            while (tryMoreTime);

            return result;
        }

        protected virtual async Task<RequestResult<T>> ExecuteRequestAsync<T>(
            string url,
            HttpMethod method,
            HttpContent postData = null,
            CancellationToken? cancellationToken = null,
            bool tryToRefreshAccessToken = true)
            where T : class
        {
            var needToTryGetNewAccessToken = tryToRefreshAccessToken;

            bool tryMoreTime;

            RequestResult<T> result;

            do
            {
                tryMoreTime = false;

                result = await WebConnectionService.ExecuteRequestAsync<T>(GetAbsoluteUrl(url), method, postData, cancellationToken);

                if (!result.IsSuccess)
                {
                    tryMoreTime = needToTryGetNewAccessToken && await NeedToTryWithNewToken(result);

                    ThrowWebConnectionExceptionIfNeeded(needToTryGetNewAccessToken, tryMoreTime, result);
                }
                else if (!result.IsParsed)
                {
                    throw result.Exception;
                }

                needToTryGetNewAccessToken = false;
            }
            while (tryMoreTime);

            return result;
        }

        protected virtual async Task<bool> NeedToTryWithNewToken(RequestResult result)
        {
            var tryMoreTime =
                result != null
                && !result.IsCancelled
                && result.StatusCode == System.Net.HttpStatusCode.Unauthorized
                && await ApiSettingsService.RefreshTokenUpdater.RefreshToken();

            return tryMoreTime;
        }

        protected virtual Task<bool> NeedToTryWithNewToken<T>(RequestResult<T> result)
            where T : class
        => NeedToTryWithNewToken(result as RequestResult);

        protected virtual void ThrowWebConnectionExceptionIfNeeded(bool needToTryGetNewAccessToken, bool tokenSuccessfullyRefreshed, RequestResult result)
        {
            if (!needToTryGetNewAccessToken || !tokenSuccessfullyRefreshed)
                throw new WebConnectionException(result);
        }

        protected virtual void ThrowApiExceptionIfNeeded<T>(ApiResponceDto<T> apiResponce)
        {
            if (apiResponce.Code != "0")
                throw new ApiException(apiResponce.Code, apiResponce.Message);
        }

        protected virtual async Task<T> Get<T>(string url, CancellationToken? cancellationToken = null, bool tryToRefreshAccessToken = true)
            where T : class
        {
            var result = await ExecuteRequestAsync<T>(url, HttpMethod.Get, null, cancellationToken, tryToRefreshAccessToken: tryToRefreshAccessToken);

            return result.Data;
        }

        protected virtual Task Get(string url, CancellationToken? cancellationToken = null, bool tryToRefreshAccessToken = true)
        => ExecuteRequestAsync(url, HttpMethod.Get, null, cancellationToken, tryToRefreshAccessToken);

        protected virtual async Task<T> Post<T>(string url, HttpContent postData, CancellationToken? cancellationToken = null, bool tryToRefreshAccessToken = true)
            where T : class
        {
            var result = await ExecuteRequestAsync<T>(url, HttpMethod.Post, postData, cancellationToken, tryToRefreshAccessToken: tryToRefreshAccessToken);

            return result.Data;
        }

        protected virtual Task Post(string url, HttpContent postData, CancellationToken? cancellationToken = null, bool tryToRefreshAccessToken = true)
        => ExecuteRequestAsync(url, HttpMethod.Post, postData, cancellationToken, tryToRefreshAccessToken);
    }
}
