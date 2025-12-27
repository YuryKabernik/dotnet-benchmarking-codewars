using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Benchmarking.Codewars;

namespace Benchmarking.UnitTestRunner.Benchmarks
{
    // Write a function, persistence, that takes in a positive parameter num and returns its multiplicative persistence,
    // which is the number of times you must multiply the digits in num until you reach a single digit.
    
    // For example(Input --> Output):
    
    // 39 --> 3 (because 3*9 = 27, 2*7 = 14, 1*4 = 4 and 4 has only one digit)
    // 999 --> 4 (because 9*9*9 = 729, 7*2*9 = 126, 1*2*6 = 12, and finally 1*2 = 2)
    // 4 --> 0 (because 4 is already a one-digit number)

    [SimpleJob(RuntimeMoniker.Net80)]
    public class PersistentBuggerBenchmark
    {
        [Params(39, 999, 4)]
        public long input;

        [Benchmark(Baseline = true)]
        public int Persistence() => PersistentBugger.Persistence(this.input);

        #region KataSolutions

        [Benchmark]
        public int Persistence_StringParse_Aggregate() => PersistentBugger.Persistence_StringParse_Aggregate(this.input);

        [Benchmark]
        public int Persistence_LongValues() => PersistentBugger.Persistence_LongValues(this.input);

        [Benchmark]
        public int Persistence_Recursive() => PersistentBugger.Persistence_Recursive(this.input);

        #endregion
    }
}
