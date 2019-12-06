﻿using System;
namespace XMP.API.Exceptions
{
    public class ApiException : Exception
    {
        public string Code { get; }

        public ApiException(string code, string message) : base(message)
        {
            Code = code;
        }
    }
}
