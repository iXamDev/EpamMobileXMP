using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using IdentityModel.Client;
using XMP.API.Services.Abstract;
namespace XMP.API.Services.Implementation
{
    public class WebConnectionService : IWebConnectionService
    {
        public WebConnectionService()
        {
            explicitHttpClient = new Lazy<HttpClient>(() => CreateHttpClient());
        }

        private HttpClient CreateHttpClient(HttpMessageHandler messageHandler)
        {
            return new HttpClient(messageHandler)
            {
                Timeout = TimeSpan.FromSeconds(RequestTimeoutInSeconds)
            };
        }

        private HttpClient CreateHttpClient()
        => CreateHttpClient((HttpHandler = CreateHandlerFunc?.Invoke() ?? CreateHandler()));

        private HttpClientHandler CreateHandler()
        {
            var handler = new HttpClientHandler();

            if (handler.SupportsAutomaticDecompression)
                handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            return handler;
        }

        private bool isDisposed;

        private Lazy<HttpClient> explicitHttpClient;

        public virtual string AcceptHeader { get; set; } = "application/json";

        public HttpClientHandler HttpHandler { get; private set; }

        public Func<HttpClientHandler> CreateHandlerFunc { get; set; }

        public virtual int RequestTimeoutInSeconds { get; set; } = 30;

        public string Bearer { get; set; }

        public HttpClient Client => explicitHttpClient.Value;

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
            if (isDisposed) return;

            if (disposing)
            {
                explicitHttpClient.Value?.Dispose();
            }

            explicitHttpClient = null;

            isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
