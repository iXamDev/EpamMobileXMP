using System;
namespace XMP.API.Services.Abstract
{
    public interface IOAuthSettings
    {
        string ResourceId { get; }
        string ClientId { get; }
        string ClientSecret { get; }
        string InvalidCredentialsErrorKey { get; }
    }
}
