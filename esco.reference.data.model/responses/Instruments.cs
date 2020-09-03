using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESCO.Reference.Data.Model
{
    public class Instruments : List<Instrument> { }    
    public class InstrumentsReport : List<InstrumentReport> { }
    public  class Instrument
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("schemaId", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string schemaId { get; set; }

        [JsonProperty("active", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool active { get; set; }

        [JsonProperty("source", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int source { get; set; }

        [JsonProperty("type", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int type { get; set; }

        [JsonProperty("control", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int control { get; set; }

        [JsonProperty("properties", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<Fields> properties { get; set; }    
    }

    public class InstrumentReport
    {
        [JsonProperty("id")]
        public string id { get; set; }     

        [JsonProperty("source", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string source { get; set; }

        [JsonProperty("properties", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public dynamic properties { get; set; }
    }

    public class Fields
    {
        [JsonProperty("fieldId", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string fieldId { get; set; }

        [JsonProperty("value", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string value { get; set; }

        [JsonProperty("sourceValue", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string sourceValue { get; set; }       

        [JsonProperty("origin", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int origin { get; set; }

        [JsonProperty("control", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int control { get; set; }
    }
}
