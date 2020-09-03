using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESCO.Reference.Data.Model
{

    public class Types : List<TypeField> { }
    public class TypeField
    {
        [JsonProperty("code")]
        public int code { get; set; }
        [JsonProperty("description")]
        public string description { get; set; }
    }
}
