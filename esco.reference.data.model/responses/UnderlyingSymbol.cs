using Newtonsoft.Json;
using System.Collections.Generic;

namespace ESCO.Reference.Data.Model
{
    public class UnderlyingSymbols : List<UnderlyingSymbol> { }

    public class UnderlyingSymbol
    {
        [JsonProperty("underlyingSymbol")]
        public string underlyingSymbol { get; set; }
    }
}
