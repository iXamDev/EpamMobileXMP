using System;
using System.Threading.Tasks;
using XMP.API.Models;

namespace XMP.API.Services.Abstract
{
    public interface IAuthenticationApiService
    {
        Task<OAuthToken> Login(string login, string password);
    }
}
