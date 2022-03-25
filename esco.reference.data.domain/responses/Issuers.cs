using System.Collections.Generic;

namespace ESCO.Reference.Data.Model
{
    public class Issuers : List<string> { }
    public class IssuersList
    {
        public List<IssuerFields> data { get; set; }
    }

    public class IssuerFields
    {
        public Issuer fields { get; set; }
    }

    public class Issuer
    {
        public string issuer { get; set; }
    }
}
