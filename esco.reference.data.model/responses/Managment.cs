using Newtonsoft.Json;
using System.Collections.Generic;

namespace ESCO.Reference.Data.Model
{
    public class Managments : List<Managment> { }
    public class ManagmentsList
    {
        [JsonProperty("value")]
        public List<Managment> value { get; set; }
    }

    public class Managment
    {
        [JsonProperty("FundManagerId")]
        public string FundManagerId { get; set; }

        [JsonProperty("FundManagerName")]
        public string FundManagerName { get; set; }
    }
}
