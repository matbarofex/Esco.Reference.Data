using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ESCO.Reference.Data.Model
{
    public class SuggestedFields : List<SuggestedField> { }
    public  class SuggestedField
    {
        [JsonProperty("source")]
        public string source { get; set; }

        [JsonProperty("fieldId")]
        public string fieldId { get; set; }

        [JsonProperty("label")]
        public string label { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }
    }
}
