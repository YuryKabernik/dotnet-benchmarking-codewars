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
            
            await Parallel.ForEachAsync(items, async (item, cancellationToken) =>
            {
                var index = Array.IndexOf(items, item);
                var result = await SimulateNetworkCallAsync(item);
                results[index] = result;
            });
            
            return results;
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
            var partitionSize = Math.Max(1, items.Length / Environment.ProcessorCount);
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
