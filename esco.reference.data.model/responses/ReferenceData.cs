using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESCO.Reference.Data.Model
{
    public class ReferenceDatas : List<ReferenceData> { }


    public  class ReferenceData
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("type")]
        public string type { get; set; }

        [JsonProperty("data")]
        public dynamic data { get; set; }
    }

    public class ODataObject : List<dynamic> { }

    public class ODataList
    {
        [JsonProperty("value")]
        public ODataObject value { get; set; }
    }

    public class ODataReferenceDatas
    {
        [JsonProperty("value")]
        public List<Value> value { get; set;}

    }
    public class Value
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("type")]
        public string type { get; set; }

        [JsonProperty("CFICode")]
        public string CFICode { get; set; }

        [JsonProperty("Currency")]
        public string Currency { get; set; }

        [JsonProperty("Symbol")]
        public string Symbol { get; set; }

        [JsonProperty("ContractMultiplier")]
        public string ContractMultiplier { get; set; }

        [JsonProperty("HighLimitPrice")]
        public string HighLimitPrice { get; set; }

        [JsonProperty("LowLimitPrice")]
        public string LowLimitPrice { get; set; }

        [JsonProperty("MaturityDate")]
        public string MaturityDate { get; set; }

        [JsonProperty("Factor")]
        public string Factor { get; set; }

        [JsonProperty("InstrumentSizePrecision")]
        public string InstrumentSizePrecision { get; set; }

        [JsonProperty("MarketSegmentId")]
        public string MarketSegmentId { get; set; }

        [JsonProperty("TickIncrement")]
        public string TickIncrement { get; set; }

        [JsonProperty("UnderlyingSymbol")]
        public string UnderlyingSymbol { get; set; }

        [JsonProperty("InstrumentPricePrecision")]
        public string InstrumentPricePrecision { get; set; }

        [JsonProperty("MarketId")]
        public string MarketId { get; set; }

        [JsonProperty("MaturityMonthYear")]
        public string MaturityMonthYear { get; set; }

        [JsonProperty("MaxTradeVol")]
        public string MaxTradeVol { get; set; }

        [JsonProperty("MinPriceIncrement")]
        public string MinPriceIncrement { get; set; }

        [JsonProperty("MinTradeVol")]
        public string MinTradeVol { get; set; }

        [JsonProperty("PriceLimitType")]
        public string PriceLimitType { get; set; }

        [JsonProperty("Issuer")]
        public string Issuer { get; set; }

        [JsonProperty("Bloomberg")]
        public string Bloomberg { get; set; }

        [JsonProperty("Isin")]
        public string Isin { get; set; }

        [JsonProperty("SecurityStatus")]
        public string SecurityStatus { get; set; }

        [JsonProperty("UnderlyingSecurityId")]
        public string UnderlyingSecurityId { get; set; }

        [JsonProperty("Text")]
        public string Text { get; set; }

        [JsonProperty("Country")]
        public string Country { get; set; }
    }

    public class ReferenceDataSymbols : List<ReferenceDataSymbol> { }

    public class ReferenceDataSymbolsList
    {
        [JsonProperty("value")]
        public List<ReferenceDataSymbol> value { get; set; }
    }

    public class ReferenceDataSymbol
    {
        [JsonProperty("UnderlyingSymbol")]
        public string UnderlyingSymbol { get; set; }
    }

    public class ReferenceDataTypes : List<ReferenceDataType> { }


    public class ReferenceDataTypesList
    {
        [JsonProperty("value")]
        public List<ReferenceDataType> value { get; set; }
    }

    public class ReferenceDataType
    {
        public string id { get; set; }
        [JsonProperty("type")]
        public string type { get; set; }        
    }
}
