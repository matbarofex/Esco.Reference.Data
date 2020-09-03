using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESCO.Reference.Data.Model
{
    public class Status : List<StatusField> { }
    public class StatusField
    {
        [JsonProperty("processName")]
        public string processName { get; set; }
        [JsonProperty("executionUtcTime")]
        public DateTime executionUtcTime { get; set; }
        [JsonProperty("status")]
        public int status { get; set; }
        [JsonProperty("details")]
        public string[] details { get; set; }       
    }
}
