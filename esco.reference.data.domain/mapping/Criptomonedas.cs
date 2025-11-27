using System.Collections.Generic;
using System;

namespace ESCO.Reference.Data.Model
{
    public class Criptomonedas
    {
        public CriptomonedasList data { get; set; }
        public int? totalCount { get; set; }
    }
    public class CriptomonedasList : List<Criptomoneda> { }

    public class Criptomoneda
    {
        public string name { get; set; }
        public string type { get; set; }
        public bool? active { get; set; }
        public CriptomonedasFields fields { get; set; }
        public DateTime? updated { get; set; }
    }

    public class CriptomonedasFields
    {
        public string cfiCode { get; set; }
        public string symbol { get; set; }
        public string baseAsset { get; set; }
        public string quoteAsset { get; set; }
        public string baseAssetPrecision { get; set; }
        public string quoteAssetPrecision { get; set; }
        public string denomination2 { get; set; }
        public string marketId { get; set; }
    }
}
