using Newtonsoft.Json;
using System.Collections.Generic;

namespace ESCO.Reference.Data.Model
{
    public class Benchmarks : List<Benchmark> { }
    public class BenchmarksList
    {
        [JsonProperty("value")]
        public List<Benchmark> value { get; set; }
    }

    public class Benchmark
    {
        [JsonProperty("FundBenchmarkId")]
        public string FundBenchmarkId { get; set; }

        [JsonProperty("FundBenchmarkName")]
        public string FundBenchmarkName { get; set; }
    }
}
