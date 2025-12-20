using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Benchmarking.Codewars;

namespace Benchmarking.UnitTestRunner.Benchmarks
{
    [SimpleJob(RuntimeMoniker.Net80)]
    public class FindOddIntBenchmark
    {
        [Params(
            new[] { 20, 1, -1, 2, -2, 3, 3, 5, 5, 1, 2, 4, 20, 4, -1, -2, 5 },
            new[] { 3, 5, 5, 1, 2, 4, 20, 4, -1, -2, 5, 3, 3, 3, 3, 10, 87, 96, 10, 87, 96, 3434, 3434}
        )]
        public int[] input;

        [Benchmark(Baseline = true)]
        public void Find_GroupBy() => FindOddInt.Find_GroupBy(this.input);

        [Benchmark]
        public void Find_LogicalExclusive() => FindOddInt.Find_LogicalExclusive(this.input);

        [Benchmark]
        public void Find_Aggregate_LogicalExclusive() => FindOddInt.Find_Aggregate_LogicalExclusive(this.input);

        [Benchmark]
        public void Find_Dictionary() => FindOddInt.Find_Dictionary(this.input);
    }
}
