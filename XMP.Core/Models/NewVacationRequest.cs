using System;

namespace XMP.Core.Models
{
    public class NewVacationRequest
    {
        public VacationType VacationType { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public VacationState State { get; set; }

        public string CreatedBy { get; set; }
    }
}
