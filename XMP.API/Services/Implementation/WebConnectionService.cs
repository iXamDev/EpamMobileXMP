using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using IdentityModel.Client;
using XMP.API.Models;
using XMP.API.Services.Abstract;

namespace XMP.API.Services.Implementation
{
    public class WebConnectionService : IWebConnectionService
    {
        private bool _isDisposed;

        private Lazy<HttpClient> _explicitHttpClient;

        public WebConnectionService()
        {
            _explicitHttpClient = new Lazy<HttpClient>(() => CreateHttpClient());
        }

        public virtual string AcceptHeader { get; set; } = "application/json";

        public HttpClientHandler HttpHandler { get; private set; }

        public Func<HttpClientHandler> CreateHandlerFunc { get; set; }

        public virtual int RequestTimeoutInSeconds { get; set; } = 30;

        public string Bearer { get; set; }

        public HttpClient Client => _explicitHttpClient.Value;

        private HttpClient CreateHttpClient(HttpMessageHandler messageHandler)
        {
            return new HttpClient(messageHandler)
            {
                Timeout = TimeSpan.FromSeconds(RequestTimeoutInSeconds)
            };
        }

        private HttpClient CreateHttpClient()
        => CreateHttpClient(HttpHandler = CreateHandlerFunc?.Invoke() ?? CreateHandler());

        private HttpClientHandler CreateHandler()
        {
            var handler = new HttpClientHandler();

            if (handler.SupportsAutomaticDecompression)
                handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            return handler;
        }

        private void SetupRequestHeaders(HttpRequestMessage message)
        {
            message.Headers?.Clear();

            if (message.Headers.Accept.All(x => x.MediaType != AcceptHeader))
                message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(AcceptHeader));

            if (message.Headers.AcceptEncoding.All(x => x.Value != "gzip"))
                message.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

            message.SetBearerToken(Bearer);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
                return;

            if (disposing)
            {
                _explicitHttpClient.Value?.Dispose();
            }

            _explicitHttpClient = null;

            _isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public async Task<RequestResult> ExecuteRequestAsync(string url, HttpMethod method, HttpContent postData = null, CancellationToken? cancellationToken = null)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));

            var httpClient = Client;

            var notNullCancellationToken = cancellationToken ?? default;

            var requestResult = new RequestResult
            {
                Url = url
            };

            var httpRequestMessage = new HttpRequestMessage(method, url);

            if (method == HttpMethod.Post || method == HttpMethod.Put)
            {
                if (postData != null)
                    httpRequestMessage.Content = postData;
            }

            try
            {
                SetupRequestHeaders(httpRequestMessage);

                requestResult.Bearer = httpRequestMessage.Headers?.Authorization?.Parameter;

                var result = await httpClient.SendAsync(httpRequestMessage, notNullCancellationToken).ConfigureAwait(false);

                requestResult.IsSuccess = result.IsSuccessStatusCode;

                requestResult.StatusCode = result.StatusCode;

                requestResult.Content = result.Content;
            }
            catch (TaskCanceledException tcEx)
            {
                requestResult.IsSuccess = false;
                requestResult.IsCancelled = true;
                requestResult.Exception = tcEx;
            }
            catch (Exception ex)
            {
                requestResult.IsSuccess = false;
                requestResult.IsCancelled = false;
                requestResult.Exception = ex;
            }

            return requestResult;
        }

        public async Task<RequestResult<T>> ExecuteRequestAsync<T>(string url, HttpMethod method, HttpContent postData = null, CancellationToken? cancellationToken = null)
            where T : class
        {
            var requestResult = await ExecuteRequestAsync(url, method, postData, cancellationToken);
            var jsonResult = new RequestResult<T>(requestResult);
            await jsonResult.ParseContent();
            return jsonResult;
        }
    }
}
