using System;
namespace XMP.API.Models
{
    public class OAuthToken
    {
        public string AccessToken { get; }

        public OAuthToken(string accessToken)
        {
            AccessToken = accessToken;
        }
    }
}
