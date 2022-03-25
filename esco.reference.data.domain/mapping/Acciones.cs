﻿using System.Collections.Generic;
using System;

namespace ESCO.Reference.Data.Model
{
    public class Acciones
    {
        public AccionesList data { get; set; }
        public int? totalCount { get; set; }
    }
    public class AccionesList : List<Accion> { }

    public class Accion
    {
        public string name { get; set; }
        public string type { get; set; }
        public bool? active { get; set; }
        public AccionesFields fields { get; set; }
        public DateTime? updated { get; set; }
    }

    public class AccionesFields
    {
        public string cfiCode { get; set; }
        public string contractMultiplier { get; set; }
        public string currency { get; set; }
        public string factor { get; set; }
        public string highLimitPrice { get; set; }
        public string lowLimitPrice { get; set; }
        public string maturityDate { get; set; }
        public string symbol { get; set; }
        public string issuer { get; set; }
        public string marketId { get; set; }
        public string priceLimitType { get; set; }
        public string securityStatus { get; set; }
        public string text { get; set; }
        public string underlyingSecurityId { get; set; }
        public string country { get; set; }
        public string isinTicker { get; set; }
        public string bloombergTicker { get; set; }
        public string speciesCode { get; set; }
        public string denomination { get; set; }
        public string collateralHaircut { get; set; }
        public string collateralElegible { get; set; }
        public string collateralQuota { get; set; }
        public string isShortable { get; set; }
    }
}
