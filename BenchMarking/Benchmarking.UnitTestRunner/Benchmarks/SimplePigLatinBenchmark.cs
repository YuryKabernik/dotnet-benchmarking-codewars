using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Benchmarking.Codewars;

namespace Benchmarking.UnitTestRunner.Benchmarks
{
    [SimpleJob(RuntimeMoniker.Net80)]
    public class SimplePigLatinBenchmark
    {
        [Params(
            "Thank you, Joshuah.",
            "A type is Countable if it has a property named Length or Count with an accessible getter and a return type of int. The language can make use of this property to convert an expression of type Index into an int at the point of the expression without the need to use the type Index at all. In case both Length and Count are present, Length will be preferred. For simplicity going forward, the proposal will use the name Length to represent Count or Length."
        )]
        public string _sentence;

        [Benchmark(Baseline = true)]
        public string PigIt_WithMatchEvaluator() =>
            SimplePigLatin.PigIt(_sentence);

        [Benchmark]
        public string PigIt_JoinsSplitsAndLinq() =>
            SimplePigLatin.PigIt_JoinsSplitsAndLinq(_sentence);

        [Benchmark]
        public string PigIt_RegexReplace_CapturedNotWhitespacePosition() =>
            SimplePigLatin.PigIt_RegexReplace_CapturedNotWhitespacePosition(_sentence);

        [Benchmark]
        public string PigIt_RegexReplace_CapturedWordPosition() =>
            SimplePigLatin.PigIt_RegexReplace_CapturedWordPosition(_sentence);
    }
}

