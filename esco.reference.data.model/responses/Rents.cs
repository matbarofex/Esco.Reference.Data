using Newtonsoft.Json;
using System.Collections.Generic;

namespace ESCO.Reference.Data.Model
{
    public class Rents : List<Rent> { }
    public class RentsList
    {
        [JsonProperty("value")]
        public List<Rent> value { get; set; }
    }

    public class Rent
    {
        [JsonProperty("RentTypeId")]
        public string RentTypeId { get; set; }

        [JsonProperty("RentTypeName")]
        public string RentTypeName { get; set; }
    }
}
