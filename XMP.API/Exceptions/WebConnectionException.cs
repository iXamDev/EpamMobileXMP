using System;
using XMP.API.Models;

namespace XMP.API.Exceptions
{
    public class WebConnectionException : Exception
    {
        public RequestResult RequestResult { get; private set; }

        public WebConnectionException(RequestResult result)
        {
            RequestResult = result;
        }

        public WebConnectionException(string message, RequestResult result)
                : base(message)
        {
            RequestResult = result;
        }

        public override Exception GetBaseException()
        {
            return RequestResult?.Exception;
        }
    }
}
