using System;

namespace XMP.API.Models
{
    public class OAuthToken
    {
        public OAuthToken(string accessToken)
        {
            AccessToken = accessToken;
        }

        public string AccessToken { get; }
    }
}
