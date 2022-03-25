using System.Collections.Generic;

namespace ESCO.Reference.Data.Model
{
    public class Custodians : List<Custodian> { }

    public class CustodiansList
    {
        public List<CustodianFields> data { get; set; }
    }

    public class CustodianFields
    {
        public Custodian fields { get; set; }
    }

    public class Custodian
    {
        public string fundCustodianId { get; set; }
        public string fundCustodianName { get; set; }
    }
}
