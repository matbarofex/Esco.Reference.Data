using System.Collections.Generic;

namespace ESCO.Reference.Data.Model
{
    public class Benchmarks : List<Benchmark> { }

    public class BenchmarksList
    {
        public List<BenchmarkFields> data { get; set; }
    }

    public class BenchmarkFields
    {
        public Benchmark fields { get; set; }
    }

    public class Benchmark
    {
        public string fundBenchmarkId { get; set; }
        public string fundBenchmarkName { get; set; }
    }
}
