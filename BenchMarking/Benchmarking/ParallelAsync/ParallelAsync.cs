using System.Collections.Concurrent;

namespace Benchmarking.ParallelAsync
{
    /// <summary>
    /// Benchmark different parallel async processing approaches for network operations
    /// </summary>
    public static class ParallelAsyncProcessor
    {
        /// <summary>
        /// Process items using Parallel.ForEachAsync
        /// </summary>
        public static async Task<int[]> ProcessWithParallelForEachAsync(int[] items, int degreeOfParallelism)
        {
            var results = new int[items.Length];
            var options = new ParallelOptions { MaxDegreeOfParallelism = degreeOfParallelism };

            await Parallel.ForEachAsync(items.Select((item, index) => new { item, index }), options, async (pair, cancellationToken) =>
            {
                var result = await SimulateNetworkCallAsync(pair.item);
                results[pair.index] = result;
            });
            
            return results;
        }

        /// <summary>
        /// Process items using Semaphore with Select=>Task and Task.WhenAll
        /// </summary>
        public static async Task<int[]> ProcessWithSemaphoreTaskWhenAll(int[] items, int degreeOfParallelism)
        {
            using var semaphore = new SemaphoreSlim(degreeOfParallelism, degreeOfParallelism);
            
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
        /// Process items using PLINQ with partitioning
        /// </summary>
        public static async Task<int[]> ProcessWithPlinq(int[] items, int degreeOfParallelism)
        {
            var partitions = Partitioner.Create(items, loadBalance: true);
            
            var tasks = partitions.GetPartitions(degreeOfParallelism)
                .AsParallel()
                .Select(ProcessPartitionAsync)
                .ToArray();
            
            var allResults = await Task.WhenAll(tasks);
            return allResults.SelectMany(r => r).ToArray();
        }

        /// <summary>
        /// Process items using Partitioned Task.WhenAll
        /// </summary>
        public static async Task<int[]> ProcessWithPartitionedTaskWhenAll(int[] items, int degreeOfParallelism)
        {
            var partitions = Partitioner.Create(items, loadBalance: true);
            
            var tasks = partitions.GetPartitions(degreeOfParallelism)
                .Select(ProcessPartitionAsync)
                .ToArray();
            
            var allResults = await Task.WhenAll(tasks);
            return allResults.SelectMany(r => r).ToArray();
        }
        
        /// <summary>
        /// Simulates an asynchronous network call
        /// </summary>
        private static async Task<int> SimulateNetworkCallAsync(int id)
        {
            await Task.Delay(TimeSpan.FromMicroseconds(10));
            return id * 2;
        }

        /// <summary>
        /// Processes a partition of items asynchronously
        /// </summary>
        private static async Task<List<int>> ProcessPartitionAsync(IEnumerator<int> partition)
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
        }
    }
}
