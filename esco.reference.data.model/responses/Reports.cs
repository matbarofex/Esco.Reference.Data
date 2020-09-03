using Newtonsoft.Json;
using System.Collections.Generic;

namespace ESCO.Reference.Data.Model
{
    public class Reports : List<Report> { }
    public class Report 
    {
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("schemaId")]
        public string schemaId { get; set; }
        [JsonProperty("active")]
        public bool active { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("filter")]
        public string filter { get; set; }
        [JsonProperty("fields")]
        public string fields { get; set; } 
    }
}
