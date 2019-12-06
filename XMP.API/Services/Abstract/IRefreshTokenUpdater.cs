using System;
using System.Threading.Tasks;

namespace XMP.API.Services.Abstract
{
    public interface IRefreshTokenUpdater
    {
        Task<bool> RefreshToken();
    }
}
