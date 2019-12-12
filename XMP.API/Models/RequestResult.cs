using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace XMP.API.Models
{
    public class RequestResult
    {
        public string Url { get; internal set; }

        public string Bearer { get; internal set; }

        public bool IsSuccess { get; internal set; }

        public bool IsCancelled { get; internal set; }

        public HttpContent Content { get; internal set; }

        public HttpStatusCode StatusCode { get; internal set; }

        public Exception Exception { get; internal set; }
    }

    public class RequestResult<T> : RequestResult
    {
        public RequestResult()
        {
        }

        public RequestResult(RequestResult requestResult)
        {
            Url = requestResult.Url;
            Bearer = requestResult.Bearer;
            IsSuccess = requestResult.IsSuccess;
            IsCancelled = requestResult.IsCancelled;
            Content = requestResult.Content;
            StatusCode = requestResult.StatusCode;
            Exception = requestResult.Exception;
        }

        public T Data { get; protected set; }

        public bool IsParsed { get; protected set; }

        public async Task<bool> ParseContent()
        {
            IsParsed = false;

            if (Content != null && IsSuccess)
            {
                try
                {
                    var content = await Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<T>(content);

                    Data = data;

                    IsParsed = true;
                }
                catch (Exception ex)
                {
                    Exception = ex;
                }
            }

            return false;
        }
    }
}
