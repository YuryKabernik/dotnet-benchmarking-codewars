using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Benchmarking.Codewars;

namespace Benchmarking.UnitTestRunner.Benchmarks
{
    //  Kata.Maskify("4556364607935616"); // should return "############5616"
    //  Kata.Maskify("64607935616");      // should return "#######5616"
    //  Kata.Maskify("1");                // should return "1"
    //  Kata.Maskify("");                 // should return ""

    //  "What was the name of your first pet?"
    //  Kata.Maskify("Skippy");                                   // should return "##ippy"
    //  Kata.Maskify("Nananananananananananananananana Batman!"); // should return "####################################man!"

    [SimpleJob(RuntimeMoniker.Net80)]
    public class MaskifyBenchmark
    {
        [Params("4556364607935616", "64607935616", "1", "", "Skippy", "Nananananananananananananananana Batman!")]
        public string input;

        [Benchmark(Baseline = true)]
        public string Regex_NegativeLookAhead() => Maskify.Regex_NegativeLookAhead(input);
        
        #region KataSolutions

        [Benchmark]
        public string Regex_PositiveLookAhead() => Maskify.Regex_PositiveLookAhead(input);

        [Benchmark]
        public string Substring_PadLeft() => Maskify.Substring_PadLeft(input);

        [Benchmark]
        public string StringBuilder() => Maskify.StringBuilder(input);

        #endregion
    }
}
