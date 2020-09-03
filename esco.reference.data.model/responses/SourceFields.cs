using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESCO.Reference.Data.Model
{
    public class SourceFields : List<SourceField> { }
    public class SourceField
    {
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("schemaId")]
        public string schemaId { get; set; }
        [JsonProperty("active")]
        public bool active { get; set; }
        [JsonProperty("source")]
        public int source { get; set; }
        [JsonProperty("fieldName")]
        public string fieldName { get; set; }
    }
}
