using System;
using System.Threading.Tasks;
using Realms;

namespace XMP.Core.Services.Abstract
{
    public interface IRealmProvider
    {
        Realm GetRealm();

        bool Initialized { get; }

        Task Init();

        Task Drop();
    }
}
