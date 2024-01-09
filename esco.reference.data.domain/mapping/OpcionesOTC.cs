using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ESCO.Reference.Data.Model
{
    public class OpcionesOTC
    {
        public FuturosOTCList data { get; set; }
        public int? totalCount { get; set; }    
    }

    public class OpcionesOTCList : List<OpcionOTC> { } 

    public class OpcionOTC
    {
        public string name { get; set; }

        public string type { get; set; }

        public bool? active { get; set; }

        public OpcionOTCFields fields { get; set; }

        public DateTime? updated { get; set; }

    }

    public class OpcionOTCFields
    {
        public string cfiCode { get; set; }

        public string symbol { get; set; }

        public string relatedSymbol { get; set; }

        public string contractMultiplier { get; set; }

        public string maturityDate { get; set; }

        public string underlyingSymbol { get; set; }

        public string strikePrice { get; set; }

        public string securityStatus { get; set; }  

        public string currency { get; set; }

        public string marketId { get; set; }    

        public string country { get; set; }

        public string putOrCall { get; set; }

    }
}
