using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Benchmarking.Codewars;

namespace Benchmarking.UnitTestRunner.Benchmarks
{
    // XO("ooxx") => true
    // XO("xooxx") => false
    // XO("ooxXm") => true
    // XO("zpzpzpp") => true // when no 'x' and 'o' is present should return true
    // XO("zzoo") => false
    
    [SimpleJob(RuntimeMoniker.Net80)]
    public class ExesAndOhsBenchmarks
    {
        [Params("ooxx", "xooxx", "ooxXm", "zpzpzpp", "zzoo")]
        public string input;

        [Benchmark(Baseline = true)]
        public bool XO_GroupJoin_Distinct() => ExesAndOhs.XO_GroupJoin_Distinct(input);

        #region KataSolutions

        [Benchmark]
        public bool XO_GroupJoin_Any() => ExesAndOhs.XO_GroupJoin_All(input);

        [Benchmark]
        public bool XO_Where_Count() => ExesAndOhs.XO_Where_Count(input);

        [Benchmark]
        public bool XO_Loop() => ExesAndOhs.XO_Loop(input);

        #endregion
    }
}
