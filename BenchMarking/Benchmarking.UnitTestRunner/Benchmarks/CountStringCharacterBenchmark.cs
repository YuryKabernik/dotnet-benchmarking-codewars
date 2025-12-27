using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Benchmarking.Codewars;

namespace Benchmarking.UnitTestRunner.Benchmarks
{
    [SimpleJob(RuntimeMoniker.Net80)]
    public class CountStringCharacterBenchmark
    {
        [Params("aaaa", "aabb", "asddfsdfdgwerqweqwasfsdf", "")]
        public string text;

        [Benchmark(Baseline = true)]
        public void Count() => CountStringCharacter.Count(this.text);

        #region KataSolutions

        [Benchmark]
        public void Count_GroupBy() => CountStringCharacter.Count_GroupBy(this.text);

        #endregion
    }
}
