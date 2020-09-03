using Newtonsoft.Json;
using System.Collections.Generic;

namespace ESCO.Reference.Data.Model
{
    public class Regions : List<Region> { }

    public class RegionsList
    {
        [JsonProperty("value")]
        public List<Region> value { get; set; }
    }

    public class Region
    {
        [JsonProperty("RegionId")]
        public string RegionId { get; set; }

        [JsonProperty("RegionName")]
        public string RegionName { get; set; }
    }
}
