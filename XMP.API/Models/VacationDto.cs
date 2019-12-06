using System;
using Newtonsoft.Json;

namespace XMP.API.Models
{
    public class VacationDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("start")]
        public DateTime Start { get; set; }

        [JsonProperty("end")]
        public DateTime End { get; set; }

        [JsonProperty("vacationType")]
        public VacationType VacationType { get; set; }

        [JsonProperty("vacationStatus")]
        public VacationStatus VacationStatus { get; set; }

        [JsonProperty("createdBy")]
        public string CreatedBy { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }
    }
}
