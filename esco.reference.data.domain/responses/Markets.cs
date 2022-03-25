using System.Collections.Generic;

namespace ESCO.Reference.Data.Model
{
    public class Markets : List<string> { }
    public class MarketsList
    {
        public List<MarketFields> data { get; set; }
    }

    public class MarketFields
    {
        public Market fields { get; set; }
    }

    public class Market
    {
        public string marketId { get; set; }
    }
}
