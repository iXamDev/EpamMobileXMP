using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
namespace XMP.API.Models
{
    public class ApiResponceDto<T>
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("result")]
        public T Result { get; set; }
    }
}
