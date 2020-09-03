using Newtonsoft.Json;
using System.Collections.Generic;

namespace ESCO.Reference.Data.Model
{
    public class MarketSegments : List<MarketSegment> { }

    public class MarketSegment
    {
        [JsonProperty("marketSegmentId")]
        public string marketSegmentId { get; set; }
    }
}
