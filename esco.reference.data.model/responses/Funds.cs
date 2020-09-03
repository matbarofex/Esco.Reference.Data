using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESCO.Reference.Data.Model
{
    public class Funds : List<Fund> { }
    public class Fund
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("source")]
        public string source { get; set; }

        [JsonProperty("type")]
        public string type { get; set; }

        [JsonProperty("created")]
        public DateTime created { get; set; }

        [JsonProperty("updated")]
        public DateTime updated { get; set; }

        [JsonProperty("active")]
        public bool active { get; set; }

        [JsonProperty("fields")]
        public FundsFields fields { get; set; }
    }

    public class FundsFields
    {
        [JsonProperty("fundName")]
        public string fundName { get; set; }

        [JsonProperty("cnvCode")]
        public string cnvCode { get; set; }

        [JsonProperty("settlementDays")]
        public string settlementDays { get; set; }

        [JsonProperty("despositarySocietyId")]
        public string despositarySocietyId { get; set; }

        [JsonProperty("despositarySocietyName")]
        public string despositarySocietyName { get; set; }

        [JsonProperty("managementSocietyId")]
        public string managementSocietyId { get; set; }

        [JsonProperty("managementSocietyName")]
        public string managementSocietyName { get; set; }

        [JsonProperty("rentTypeId")]
        public string rentTypeId { get; set; }

        [JsonProperty("rentTypeName")]
        public string rentTypeName { get; set; }

        [JsonProperty("regionId")]
        public string regionId { get; set; }

        [JsonProperty("regionName")]
        public string regionName { get; set; }

        [JsonProperty("classId")]
        public string classId { get; set; }

        [JsonProperty("instrumentName")]
        public string instrumentName { get; set; }

        [JsonProperty("bloombergTicker")]
        public string bloombergTicker { get; set; }

        [JsonProperty("currencyId")]
        public string currencyId { get; set; }

        [JsonProperty("currencyCode")]
        public string currencyCode { get; set; }

        [JsonProperty("horizonId")]
        public string horizonId { get; set; }

        [JsonProperty("horizonCnvCode")]
        public string horizonCnvCode { get; set; }

        [JsonProperty("minimumInvestment")]
        public string minimumInvestment { get; set; }

        [JsonProperty("maximumInvestment")]
        public string maximumInvestment { get; set; }

        [JsonProperty("shareFractionDecimals")]
        public string shareFractionDecimals { get; set; }

        [JsonProperty("quoteDate")]
        public string quoteDate { get; set; }

        [JsonProperty("equity")]
        public string equity { get; set; }

        [JsonProperty("shareValue")]
        public string shareValue { get; set; }

        [JsonProperty("unitShareValue")]
        public string unitShareValue { get; set; }
    }
}
