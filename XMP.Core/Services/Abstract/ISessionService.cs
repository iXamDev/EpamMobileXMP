using System;
using System.Threading.Tasks;
using XMP.API.Services.Abstract;
using XMP.Core.Models;

namespace XMP.Core.Services.Abstract
{
    public interface ISessionService : IRefreshTokenUpdater
    {
        event EventHandler OnCredentialsFails;

        bool Active { get; }

        string UserLogin { get; }

        User User { get; }

        Task<bool> Start(UserCredentials credentials);

        Task<bool> Reactivate();
    }
}
