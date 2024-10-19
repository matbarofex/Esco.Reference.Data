using System.Collections.Generic;

namespace ESCO.Reference.Data.Model
{
    public class Currencies
    {
        public List<CurrencyData> Value { get; set; }
    }

    public class CurrenciesToResponse
    {
        public List<CurrencyInfo> Value { get; set; }
    }

    public class CurrencyData
    {
        public string CurrencyDescription { get; set; }
        public string Currency { get; set; }
        public string CurrencySymbol { get; set; }
        public bool IsCurrencyBase { get; set; }
        public string FiscalCode { get; set; }
        public string CurrencyCode { get; set; }
    }

    public class CurrencyInfo
    {
        public string CurrencyDescription { get; set; }
        public string Currency { get; set; }
        public string MarketDataCurrency { get; set; }
        public string ReferenceDataCurrency { get; set; }
        public Dictionary<string, string> Market { get; set; }
    }
}