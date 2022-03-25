using System.Collections.Generic;

namespace ESCO.Reference.Data.Model
{
    public class Currencys : List<string> { }
    public class CurrencysList
    {
        public List<CurrencyFields> data { get; set; }
    }

    public class CurrencyFields
    {
        public Currency fields { get; set; }
    }

    public class Currency
    {
        public string currency { get; set; }
    }
}
