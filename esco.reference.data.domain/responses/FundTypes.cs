using System.Collections.Generic;

namespace ESCO.Reference.Data.Model
{
    public class FundTypes : List<FundType> { }

    public class FundTypesList
    {
        public List<FundFields> data { get; set; }
    }

    public class FundFields
    {
        public FundType fields { get; set; }
    }

    public class FundType
    {
        public string fundTypeId { get; set; }
        public string fundTypeName { get; set; }
    }
}
