using System;
namespace XMP.Core.Models
{
    public class VacantionRequest
    {
        public string Id { get; set; }

        public VacationType VacationType { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public VacationState State { get; set; }
    }
}
