using System.Collections.Concurrent;

namespace Benchmarking.ParallelAsync
{
    /// <summary>
    /// Benchmark different parallel async processing approaches for network operations
    /// </summary>
    public static class ParallelAsyncProcessor
    {
        /// <summary>
        /// Simulates an asynchronous network call
        /// </summary>
        private static async Task<int> SimulateNetworkCallAsync(int id)
        {
            await Task.Delay(100);
            return id * 2;
        }

        /// <summary>
        /// Process items using Parallel.ForEachAsync
        /// </summary>
        public static async Task<int[]> ProcessWithParallelForEachAsync(int[] items)
        {
            var results = new int[items.Length];
            
            await Parallel.ForEachAsync(items.Select((item, index) => new { item, index }), async (pair, cancellationToken) =>
            {
                var result = await SimulateNetworkCallAsync(pair.item);
                results[pair.index] = result;
            });
            
            return results;
        }

        /// <summary>
        /// Process items using Semaphore with Select=>Task and Task.WhenAll
        /// </summary>
        public static async Task<int[]> ProcessWithSemaphoreTaskWhenAll(int[] items)
        {
            using var semaphore = new SemaphoreSlim(Environment.ProcessorCount, Environment.ProcessorCount);
            
            var tasks = items.Select(async item =>
            {
                await semaphore.WaitAsync();
                try
                {
                    return await SimulateNetworkCallAsync(item);
                }
                finally
                {
                    semaphore.Release();
                }
            }).ToArray();
            
            return await Task.WhenAll(tasks);
        }

        /// <summary>
        /// Process items using PLINQ
        /// </summary>
        public static async Task<int[]> ProcessWithPlinq(int[] items)
        {
            var tasks = items
                .AsParallel()
                .Select(item => SimulateNetworkCallAsync(item))
                .ToArray();
            
            return await Task.WhenAll(tasks);
        }

        /// <summary>
        /// Process items using Partitioned Task.WhenAll
        /// </summary>
        public static async Task<int[]> ProcessWithPartitionedTaskWhenAll(int[] items)
        {
            var partitions = Partitioner.Create(items, loadBalance: true);
            
            var tasks = partitions.GetPartitions(Environment.ProcessorCount)
                .Select(async partition =>
                {
                    var partitionResults = new List<int>();
                    using (partition)
                    {
                        while (partition.MoveNext())
                        {
                            var result = await SimulateNetworkCallAsync(partition.Current);
                            partitionResults.Add(result);
                        }
                    }
                    return partitionResults;
                })
                .ToArray();
            
            var allResults = await Task.WhenAll(tasks);
            return allResults.SelectMany(r => r).ToArray();
        }
    }
}
