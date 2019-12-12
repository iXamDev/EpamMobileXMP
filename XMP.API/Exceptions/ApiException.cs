using System;

namespace XMP.API.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException(string code, string message)
            : base(message)
        {
            Code = code;
        }

        public string Code { get; }
    }
}
