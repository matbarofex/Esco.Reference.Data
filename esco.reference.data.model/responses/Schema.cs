using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESCO.Reference.Data.Model
{
    public class Schemas : List<Schema> { }
    public class Schema 
    {
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("effectiveDateUtc")]
        public string effectiveDateUtc { get; set; }
        [JsonProperty("active")]
        public bool active { get; set; }
    }

    public class PromoteSchema
    {
        [JsonProperty("isPromoteSchemaRunning")]
        public bool isPromoteSchemaRunning { get; set; }
    }
}
