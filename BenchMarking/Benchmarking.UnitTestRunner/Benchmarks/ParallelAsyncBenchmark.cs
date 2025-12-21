using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Benchmarking.ParallelAsync;

namespace Benchmarking.UnitTestRunner.Benchmarks
{
    /// <summary>
    /// Benchmarks for comparing different parallel async processing approaches
    /// for asynchronous network operations (simulated with Task.Delay)
    /// </summary>
    [SimpleJob(RuntimeMoniker.Net80)]
    public class ParallelAsyncBenchmark
    {
        private int[] _items = null!;

        [Params(25, 100)]
        public int ItemCount;

        [Params(8, 16)]
        public int DegreeOfParallelism;

        [GlobalSetup]
        public void Setup()
        {
            _items = Enumerable.Range(1, ItemCount).ToArray();
        }

        [Benchmark(Baseline = true)]
        public async Task<int[]> SemaphoreTaskWhenAll()
        {
            return await ParallelAsyncProcessor.ProcessWithSemaphoreTaskWhenAll(_items, DegreeOfParallelism);
        }

        [Benchmark]
        public async Task<int[]> ParallelForEachAsync()
        {
            return await ParallelAsyncProcessor.ProcessWithParallelForEachAsync(_items, DegreeOfParallelism);
        }

        [Benchmark]
        public async Task<int[]> Plinq()
        {
            return await ParallelAsyncProcessor.ProcessWithPlinq(_items, DegreeOfParallelism);
        }

        [Benchmark]
        public async Task<int[]> PartitionedTaskWhenAll()
        {
            return await ParallelAsyncProcessor.ProcessWithPartitionedTaskWhenAll(_items, DegreeOfParallelism);
        }
    }
}
