using System;
using System.Net.Http;
using System.Threading;

namespace XMP.API.Services.Abstract
{
    public interface IWebConnectionService : IDisposable
    {
        string Bearer { get; set; }

        HttpClient Client { get; }
        //HttpClientHandler HttpHandler { get; }

        int RequestTimeoutInSeconds { get; set; }

        //Task<RequestResult> ExecuteRequestAsync(string url, HttpMethod method, HttpContent postData = null, CancellationToken? cancellationToken = null, Action<HttpClient> tuneClient = null, Action<HttpRequestMessage> tuneHttpRequestMessage = null, HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseContentRead);
        //Task<RequestResult<T>> ExecuteRequestAsync<T>(string url, HttpMethod method, HttpContent postData = null, CancellationToken? cancellationToken = null, Action<HttpClient> tuneClient = null, Action<HttpRequestMessage> tuneHttpRequestMessage = null, HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseContentRead) where T : class;
    }
}