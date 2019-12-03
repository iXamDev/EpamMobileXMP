using System;
using XMP.API.Services.Abstract;
using XMP.Core.Models;
using System.Threading.Tasks;

namespace XMP.Core.Services.Abstract
{
    public interface ISessionService : IRefreshTokenProvider
    {
        bool Active { get; }

        User User { get; }

        Task<bool> Start(UserCredentials credentials);
    }
}
