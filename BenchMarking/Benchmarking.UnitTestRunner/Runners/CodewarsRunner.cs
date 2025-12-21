using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Running;
using Benchmarking.UnitTestRunner.Benchmarks;

namespace Benchmarking.UnitTestRunner.Runners
{
    public class BenchmarkTests : BenchmarkBaseTest
    {
        public BenchmarkTests(BenchmarkSetupFixture benchmarkSetup) : base(benchmarkSetup)
        {
        }

        [Fact]
        public void CreatePhoneNumberBenchmark()
        {
            BenchmarkRunner.Run<CreatePhoneNumberBenchmark>(this.config);
        }

        [Fact]
        public void ExesAndOhsBenchmarks()
        {
            BenchmarkRunner.Run<ExesAndOhsBenchmarks>(this.config);
        }

        [Fact]
        public void TwoToOneBenchmark()
        {
            BenchmarkRunner.Run<TwoToOneBenchmark>(this.config);
        }

        [Fact]
        public void MaskifyBenchmark()
        {
            BenchmarkRunner.Run<MaskifyBenchmark>(this.config);
        }

        [Fact]
        public void AlphabetPositionBenchmark()
        {
            BenchmarkRunner.Run<AlphabetPositionBenchmark>(this.config);
        }

        [Fact]
        public void PersistentBuggerBenchmark()
        {
            BenchmarkRunner.Run<PersistentBuggerBenchmark>(this.config);
        }

        [Fact]
        public void CountStringCharacterBenchmark()
        {
            BenchmarkRunner.Run<CountStringCharacterBenchmark>(this.config);
        }

        [Fact]
        public void FindOddIntBenchmark()
        {
            BenchmarkRunner.Run<FindOddIntBenchmark>(this.config);
        }

        [Fact]
        public void LongestConsecutivesBenchmark()
        {
            BenchmarkRunner.Run<LongestConsecutivesBenchmark>(this.config);
        }

        [Fact]
        public void PigIt()
        {
            BenchmarkRunner.Run<SimplePigLatinBenchmark>(this.config);
        }

        [Fact]
        public void ParallelAsyncBenchmark()
        {
            BenchmarkRunner.Run<ParallelAsyncBenchmark>(
                this.config.AddDiagnoser(ThreadingDiagnoser.Default)
            );
        }
    }
}