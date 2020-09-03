using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ESCO.Reference.Data.Model
{
    public class Derivatives : List<Derivative> { }

    public class DerivativesList
    {
        [JsonProperty("value")]
        public List<Derivative> value { get; set; }
    }

    public class Derivative
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("marketId")]
        public string marketId { get; set; }

        [JsonProperty("marketSegmentId")]
        public string marketSegmentId { get; set; }

        [JsonProperty("securityDesc")]
        public string securityDesc { get; set; }

        [JsonProperty("factor")]
        public string factor { get; set; }

        [JsonProperty("cfiCode")]
        public string cfiCode { get; set; }

        [JsonProperty("contractMultiplier")]
        public string contractMultiplier { get; set; }

        [JsonProperty("maturityMonthYear")]
        public string maturityMonthYear { get; set; }

        [JsonProperty("maturityDate")]
        public string maturityDate { get; set; }

        [JsonProperty("strikePrice")]
        public string strikePrice { get; set; }

        [JsonProperty("strikeCurrency")]
        public string strikeCurrency { get; set; }

        [JsonProperty("minPriceIncrement")]
        public string minPriceIncrement { get; set; }

        [JsonProperty("tickSize")]
        public string tickSize { get; set; }

        [JsonProperty("instrumentPricePrecision")]
        public string instrumentPricePrecision { get; set; }

        [JsonProperty("instrumentSizePrecision")]
        public string instrumentSizePrecision { get; set; }

        [JsonProperty("currency")]
        public string currency { get; set; }

        [JsonProperty("endDate")]
        public string endDate { get; set; }

        [JsonProperty("maxTradeVol")]
        public string maxTradeVol { get; set; }

        [JsonProperty("minTradeVol")]
        public string minTradeVol { get; set; }

        [JsonProperty("lowLimitPrice")]
        public string lowLimitPrice { get; set; }

        [JsonProperty("highLimitPrice")]
        public string highLimitPrice { get; set; }

        [JsonProperty("symbol")]
        public string symbol { get; set; }

        [JsonProperty("securityExchange")]
        public string securityExchange { get; set; }

        [JsonProperty("underlyingSymbol")]
        public string underlyingSymbol { get; set; }
    }
}
