using Newtonsoft.Json;
using System.Collections.Generic;

namespace ESCO.Reference.Data.Model
{
    public class Currencys : List<CurrencyValue> { }
    public class CurrencysList
    {
        [JsonProperty("value")]
        public List<CurrencyValue> value { get; set; }
    }

    public class CurrencyValue
    {
        [JsonProperty("Currency")]
        public string Currency { get; set; }
    }
}
