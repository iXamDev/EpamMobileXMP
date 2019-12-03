using System;
using XMP.API.Services.Abstract;

namespace XMP.API.Services.Implementation
{
    public class OAuthSettings : IOAuthSettings
    {
        public string ResourceId { get; } = "VTS-Server-v2";
        public string ClientId { get; } = "VTS-Mobile-v1";
        public string ClientSecret { get; } = "VTS123456789";
        public string InvalidCredentialsErrorKey { get; } = "invalid_grant";
    }
}
