using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Benchmarking.Codewars;

namespace Benchmarking.UnitTestRunner.Benchmarks
{
    //  a = "xyaabbbccccdefww"
    //  b = "xxxxyyyyabklmopq"
    //  longest(a, b) -> "abcdefklmopqwxy"
    //
    //  a = "c"
    //  longest(a, a) -> "abcdefghijklmnopqrstuvwxyz"

    [SimpleJob(RuntimeMoniker.Net80)]
    public class TwoToOneBenchmark
    {
        [Params("xyaabbbccccdefww", "xxxxyyyyabklmopq")]
        public string s1;

        [Params("xxxxyyyyabklmopq", "abcdefghijklmnopqrstuvwxyz")]
        public string s2;

        [Benchmark(Baseline = true)]
        public string Longest_HashSet_SortedSet_NewString()
            => TwoToOne.Longest_HashSet_SortedSet_NewString(s1, s2);

        [Benchmark]
        public string Longest_HashSet_SortedSet_StringBuilderAggregate()
            => TwoToOne.Longest_HashSet_SortedSet_StringBuilderAggregate(s1, s2);

        [Benchmark]
        public string Longest_HashSet_SortMethod_StringBuilderAggregate()
            => TwoToOne.Longest_HashSet_SortMethod_StringBuilderAggregate(s1, s2);

        #region KataSolutions

        [Benchmark]
        public string Longest_Distinct_OrderBy_NewString()
            => TwoToOne.Longest_Distinct_OrderBy_NewString(s1, s2);

        [Benchmark]
        public string Longest_Distinct_OrderBy_StringConcat()
            => TwoToOne.Longest_Distinct_OrderBy_StringConcat(s1, s2);

        #endregion
    }
}
