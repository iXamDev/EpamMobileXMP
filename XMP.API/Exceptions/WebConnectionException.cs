using System;
using XMP.API.Models;

namespace XMP.API.Exceptions
{
    public class WebConnectionException : Exception
    {
        public WebConnectionException(RequestResult result)
        {
            RequestResult = result;
        }

        public WebConnectionException(string message, RequestResult result)
                : base(message)
        {
            RequestResult = result;
        }

        public RequestResult RequestResult { get; private set; }

        public override Exception GetBaseException()
        {
            return RequestResult?.Exception;
        }
    }
}
