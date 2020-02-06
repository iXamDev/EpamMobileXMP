using System;
using Realms;

namespace XMP.Core.Database.Implementation.RealmDatabase.VacationRequests
{
    public class VacationRequestRealmObject : RealmObject
    {
        [PrimaryKey]
        public string LocalId { get; set; }

        public string Id { get; set; }

        public short VacationType { get; set; }

        public DateTimeOffset Start { get; set; }

        public DateTimeOffset End { get; set; }

        public string CreatedBy { get; set; }

        public DateTimeOffset Created { get; set; }

        public short State { get; set; }

        public short SyncState { get; set; }
    }
}
