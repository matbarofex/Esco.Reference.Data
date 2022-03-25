﻿using System.Collections.Generic;
using System;

namespace ESCO.Reference.Data.Model
{
    public class Pases
    {
        public BuySellList data { get; set; }
        public int? totalCount { get; set; }
    }
    public class BuySellList : List<BuySellEntity> { }

    public class BuySellEntity
    {
        public string name { get; set; }
        public string type { get; set; }
        public bool? active { get; set; }
        public BuySellFields fields { get; set; }
        public DateTime? updated { get; set; }
    }

    public class BuySellFields
    {        
        public string cfiCode { get; set; }        
        public string currency { get; set; }        
        public string symbol { get; set; }        
        public string contractMultiplier { get; set; }        
        public string highLimitPrice { get; set; }        
        public string lowLimitPrice { get; set; }        
        public string maturityDate { get; set; }        
        public string factor { get; set; }        
        public string instrumentSizePrecision { get; set; }        
        public string marketSegmentId { get; set; }        
        public string tickIncrement { get; set; }        
        public string underlyingSymbol { get; set; }        
        public string instrumentPricePrecision { get; set; }        
        public string marketId { get; set; }        
        public string maturityMonthYear { get; set; }        
        public string qumaxTradeVolalityRating { get; set; }        
        public string minPriceIncrement { get; set; }        
        public string minTradeVol { get; set; }        
        public string underlyingSecurityId { get; set; }        
        public string text { get; set; }        
        public string country { get; set; }        
        public string priceLimitType { get; set; }        
        public string issuer { get; set; }        
        public string bloombergTicker { get; set; }        
        public string isinTicker { get; set; }        
        public string securityStatus { get; set; } 
    }
}
