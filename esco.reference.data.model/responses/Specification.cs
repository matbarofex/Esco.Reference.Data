using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESCO.Reference.Data.Model
{
    public  class Specification
    {
        [JsonProperty("instrumentTypes")]
        public dynamic instrumentTypes { get; set; }

        [JsonProperty("fieldTypes")]
        public dynamic fieldTypes { get; set; }

        [JsonProperty("fieldTypesByInstrumentTypes")]
        public dynamic fieldTypesByInstrumentTypes { get; set; }
    }
}
