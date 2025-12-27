using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Benchmarking.Codewars;

namespace Benchmarking.UnitTestRunner.Benchmarks
{
    [SimpleJob(RuntimeMoniker.Net80)]
    public class LongestConsecutivesBenchmark
    {
        [Params(
            new[] { "zone", "abigail", "theta", "form", "libe", "zas", "theta", "abigail" },
            new[] { "ejjjjmmtthh", "zxxuueeg", "aanlljrrrxx", "dqqqaaabbb", "oocccffuucccjjjkkkjyyyeehh" },
            new[] { "it", "wkppv", "ixoyx", "3452", "zzzzzzzzzzzz", "it", "wkppv", "ixoyx", "3452", "zzzzzzzzzzzz" }
        )]
        public string[] strings;

        [Params(3, 2)]
        public int k;

        [Benchmark(Baseline = true)]
        public string LongestConsec() =>
            LongestConsecutives.LongestConsec(this.strings, this.k);

        [Benchmark]
        public string LongestConsec_ConcatStringsEachIteration() =>
            LongestConsecutives.LongestConsec_ConcatStringsEachIteration(this.strings, this.k);

        [Benchmark]
        public string LongestConsec_LinqSkipTake() =>
            LongestConsecutives.LongestConsec_LinqSkipTake(this.strings, this.k);
    }
}
