using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ESCO.Reference.Data.Model
{
    public class Securities : List<Securitie> { }
    public class Securitie
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("source")]
        public string source { get; set; }

        [JsonProperty("type")]
        public string type { get; set; }

        [JsonProperty("created")]
        public DateTime created { get; set; }

        [JsonProperty("updated")]
        public DateTime updated { get; set; }

        [JsonProperty("active")]
        public bool active { get; set; }

        [JsonProperty("fields")]
        public List<SecuritieField> fields { get; set; }
    }

    public class SecuritieField
    {
        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("value")]
        public string value { get; set; }       
    }
}
