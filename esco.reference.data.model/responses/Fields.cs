using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESCO.Reference.Data.Model
{
    public class FieldsList : List<Field> { }
    public class Field
    {
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("schemaId")]
        public string schemaId { get; set; }
        [JsonProperty("active")]
        public bool active { get; set; }
        [JsonProperty("label")]
        public string label { get; set; }
        [JsonProperty("type")]
        public string type { get; set; }
        [JsonProperty("format")]
        public string format { get; set; }
        [JsonProperty("readOnly")]
        public bool readOnly { get; set; }
        [JsonProperty("mandatory")]
        public bool mandatory { get; set; }
        [JsonProperty("structural")]
        public bool structural { get; set; }
    }
}
