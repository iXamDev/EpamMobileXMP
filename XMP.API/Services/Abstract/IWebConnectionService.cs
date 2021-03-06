﻿using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using XMP.API.Models;

namespace XMP.API.Services.Abstract
{
    public interface IWebConnectionService : IDisposable
    {
        string Bearer { get; set; }

        HttpClient Client { get; }

        int RequestTimeoutInSeconds { get; set; }

        Task<RequestResult> ExecuteRequestAsync(string url, HttpMethod method, HttpContent postData = null, CancellationToken? cancellationToken = null);

        Task<RequestResult<T>> ExecuteRequestAsync<T>(string url, HttpMethod method, HttpContent postData = null, CancellationToken? cancellationToken = null)
            where T : class;
    }
}
