using Benchmarking.ParallelAsync;

namespace Benchmarking.UnitTests.ParallelAsync;

public class ParallelAsyncTests
{
    private const int DegreeOfParallelism = 5;
    private static readonly int[] Collection = Enumerable.Range(1, 500).ToArray();

    [Test]
    public async Task Concurrent_ProcessWithPartitionedTaskWhenAll()
    {
        await ParallelAsyncProcessor.ProcessWithPartitionedTaskWhenAll(Collection, DegreeOfParallelism);
    }

    [Test]
    public async Task Concurrent_ForEachWithPartitionedTaskWhenAll()
    {
        await ParallelAsyncProcessor.ProcessWithParallelForEachAsync(Collection, DegreeOfParallelism);
    }
}