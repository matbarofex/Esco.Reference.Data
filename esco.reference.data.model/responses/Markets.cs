using Newtonsoft.Json;
using System.Collections.Generic;

namespace ESCO.Reference.Data.Model
{
    public class Markets : List<Market> { }
    public class MarketsList
    {
        [JsonProperty("value")]
        public List<Market> value { get; set; }
    }

    public class Market
    {
        [JsonProperty("MarketId")]
        public string MarketId { get; set; }
    }
}
