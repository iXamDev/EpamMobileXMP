using System.Threading.Tasks;
using Realms;

namespace XMP.Core.Services.Abstract
{
    public interface IRealmProvider
    {
        bool Initialized { get; }

        Realm GetRealm();

        Task Init();

        Task Drop();
    }
}
