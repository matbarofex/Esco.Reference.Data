using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESCO.Reference.Data.Model
{
    public class Custodians : List<Custodian> { }
    public class CustodiansList
    {
        [JsonProperty("value")]
        public List<Custodian> value { get; set; }
    }

    public class Custodian
    {
        [JsonProperty("FundCustodianId")]
        public string FundCustodianId { get; set; }

        [JsonProperty("FundCustodianName")]
        public string FundCustodianName { get; set; }
    }
}
