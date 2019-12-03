using System;
using System.Threading.Tasks;

namespace XMP.API.Services.Abstract
{
    public interface IRefreshTokenProvider
    {
        Task<bool> RefreshToken();
    }
}
