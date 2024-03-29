﻿using System.Collections.Generic;
using System;

namespace ESCO.Reference.Data.Model
{
    public class FuturosOTC
    {
        public FuturosOTCList data { get; set; }
        public int? totalCount { get; set; }
    }
    public class FuturosOTCList : List<FuturoOTC> { }

    public class FuturoOTC
    {
        public string name { get; set; }
        public string type { get; set; }
        public bool? active { get; set; }
        public FuturoOTCFields fields { get; set; }
        public DateTime? updated { get; set; }
    }

    public class FuturoOTCFields
    {
        public string cfiCode { get; set; }
        public string maturityDate { get; set; }
        public string symbol { get; set; }
        public string relatedSymbol { get; set; }
        public string contractMultiplier { get; set; }
        public string underlyingSymbol { get; set; }
        public string strikePrice { get; set; }
        public string securityStatus { get; set; }
        public string currency { get; set; }
        public string marketId { get; set; }
        public string country { get; set; } 
        public string marketSegmentId { get; set; }
    }
}
