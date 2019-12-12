using XMP.Core.Database.Abstract;
using XMP.Core.Database.Implementation.RealmDatabase.Common;
using XMP.Core.Database.Implementation.RealmDatabase.VacationRequests;
using XMP.Core.Models;
using XMP.Core.Services.Abstract;

namespace XMP.Core.Database.Implementation.RealmDatabase
{
    public class VacationRequestsRepository : CommonRealmStringPrimaryKeyRepository<VacantionRequest, VacationRequestRealmObject>, IVacationRequestsRepository
    {
        public VacationRequestsRepository(IRealmRepositoryEntriesMapper<VacantionRequest, VacationRequestRealmObject> realmRepositoryEntriesMapper, IRealmProvider realmProvider)
            : base(item => item.LocalId, realmRepositoryEntriesMapper, realmProvider)
        {
        }
    }
}
