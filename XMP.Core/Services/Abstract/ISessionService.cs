using XMP.Core.Models;
using System.Threading.Tasks;
using XMP.API.Services.Abstract;
using System;

namespace XMP.Core.Services.Abstract
{
    public interface ISessionService : IRefreshTokenUpdater
    {
        bool Active { get; }

        string UserLogin { get; }

        User User { get; }

        Task<bool> Start(UserCredentials credentials);

        Task<bool> Reactivate();

        event EventHandler OnCredentialsFails;
    }
}
