using Newtonsoft.Json;
using System.Collections.Generic;

namespace ESCO.Reference.Data.Model
{
    public class FundTypes : List<FundType> { }
    public class FundTypesList
    {
        [JsonProperty("value")]
        public List<FundType> value { get; set; }
    }

    public class FundType
    {
        [JsonProperty("FundTypeId")]
        public string FundTypeId { get; set; }

        [JsonProperty("FundTypeName")]
        public string FundTypeName { get; set; }
    }
}
