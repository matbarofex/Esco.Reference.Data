using Newtonsoft.Json;
using System.Collections.Generic;

namespace ESCO.Reference.Data.Model
{
    public class Countrys : List<CountryValue> { }
    public class CountrysList
    {
        [JsonProperty("value")]
        public List<CountryValue> value { get; set; }
    }

    public class CountryValue
    {
        [JsonProperty("Country")]
        public string Country { get; set; }
    }
}
