using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESCO.Reference.Data.Model
{
    public class Mappings : List<Mapping> { }
    public class Mapping 
    {
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("schemaId")]
        public string schemaId { get; set; }
        [JsonProperty("active")]
        public bool active { get; set; }
        [JsonProperty("source")]
        public int source { get; set; }
        [JsonProperty("instrumentType")]
        public int instrumentType { get; set; }
        [JsonProperty("sourceFieldName")]
        public string sourceFieldName { get; set; }
        [JsonProperty("fieldId")]
        public string fieldId { get; set; }
        [JsonProperty("sourceFieldType")]
        public int sourceFieldType { get; set; }
    }
}
