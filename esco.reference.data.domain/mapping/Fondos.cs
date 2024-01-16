using System.Collections.Generic;
using System;

namespace ESCO.Reference.Data.Model
{
    public class Fondos
    {
        public FundsList data { get; set; }
        public int? totalCount { get; set; }
    }
    public class FundsList : List<Fund> { }

    public class Fund
    {
        public string name { get; set; }
        public string type { get; set; }
        public bool? active { get; set; }
        public Fields fields { get; set; }
        public DateTime? updated { get; set; }
    }

    public class FundsFields
    {
        public string currency { get; set; }
        public string issuer { get; set; }
        public string isinTicker { get; set; }
        public string bloombergTicker { get; set; }
        public string underlyingSymbol { get; set; }
        public string cnvCode { get; set; }
        public string daysToSettlement { get; set; }
        public string fundCustodianId { get; set; }
        public string fundCustodianName { get; set; }
        public string fundManagerId { get; set; }
        public string fundManagerName { get; set; }
        public string rentTypeId { get; set; }
        public string rentTypeName { get; set; }
        public string regionId { get; set; }
        public string regionName { get; set; }
        public string classId { get; set; }
        public string horizonId { get; set; }
        public string horizonName { get; set; }
        public string minimumInvestment { get; set; }
        public DateTime date { get; set; }
        public string fundAum { get; set; }
        public string price { get; set; }
        public string unitPrice { get; set; }
        public string shareCurrency { get; set; }
        public string fundTypeId { get; set; }
        public string fundTypeName { get; set; }
        public string fundBenchmarkId { get; set; }
        public string fundBenchmarkName { get; set; }
        public string suscriptionFee { get; set; }
        public string managerAdministrationFee { get; set; }
        public string custodianAdministrationFee { get; set; }
        public string managementExpenses { get; set; }
        public string rescueFee { get; set; }
        public string transferFee { get; set; }
        public string successFee { get; set; }
        public string fundPortfolioDate { get; set; }
        public string fundPortfolio { get; set; }
        public string performanceDay { get; set; }
        public string performanceMtd { get; set; }
        public string performanceYtd { get; set; }
        public string performanceYear { get; set; }
        public string qualityRatingFirm { get; set; }
        public string qualityRating { get; set; }
        public string qualityRatingDate { get; set; }
        public string shareCurrencyId { get; set; }
        public string classTypeId { get; set; }
        public string startDate { get; set; }
        public string performanceMonthYear { get; set; }
        public string country { get; set; }
        public string securityStatus { get; set; }
        public string marketId { get; set; }
        public string priceDecimals { get; set; }
        public string shareMinimumFraction { get; set; }
        public string maximumInvestment { get; set; }
        public string cfiCode { get; set; }
        public string text { get; set; }
        public string shareCurrencyName { get; set; }
        public string classTypeName { get; set; }
        public string symbol { get; set; }
        public string collateralHaircut { get; set; }
        public string collateralElegible { get; set; }
        public string collateralQuota { get; set; }
        public string personTypeId { get; set; }
        public string personTypeName { get; set; }
        public string speciesCode { get; set; }
        public string denomination { get; set; }

    }
}
