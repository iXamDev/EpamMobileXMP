using System;
namespace XMP.Core.Models
{
    public class VacantionRequest : IEquatable<VacantionRequest>
    {
        public string Id { get; set; }

        public DateTimeOffset Start { get; set; }

        public DateTimeOffset End { get; set; }

        public string CreatedBy { get; set; }

        public DateTimeOffset Created { get; set; }

        public VacationType VacationType { get; set; }

        public VacationState State { get; set; }

        public string LocalId { get; set; }

        public SynchronizationState SyncState { get; set; }

        public bool Equals(VacantionRequest other)
        =>
            Id == other.Id
            && Start == other.Start
            && End == other.End
            && Created == other.Created
            && CreatedBy == other.CreatedBy
            && VacationType == other.VacationType
            && State == other.State;
    }
}
