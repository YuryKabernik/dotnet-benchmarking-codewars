using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Benchmarking.Codewars;

namespace Benchmarking.UnitTestRunner.Benchmarks
{
    //In this kata you are required to, given a string, replace every letter with its position in the alphabet.
    //If anything in the text isn't a letter, ignore it and don't return it.
    //"a" = 1, "b" = 2, etc.

    // "a" is ANSCII 65
    // "z" is ANSCII 90

    [SimpleJob(RuntimeMoniker.Net80)]
    public class AlphabetPositionBenchmark
    {
        [Params(
            "The sunset sets at twelve o' clock.",
            "The narwhal bacons at midnight.",
            "-.-'"
         )]
        public string text;

        [Benchmark(Baseline = true)]
        public string AlphabetPosition_CharValueMinus64() =>
            AlphabetPosition.AlphabetPosition_CharValueMinus64(text);

        #region KataSolutions

        [Benchmark]
        public string AlphabetPosition_StringOfLowerCaseLetters() =>
            AlphabetPosition.AlphabetPosition_StringOfLowerCaseLetters(text);

        [Benchmark]
        public string AlphabetPosition_SelectWithUmpersant() =>
            AlphabetPosition.AlphabetPosition_SelectWithUmpersant(text);

        [Benchmark]
        public string AlphabetPosition_StringBuilder() =>
            AlphabetPosition.AlphabetPosition_StringBuilder(text);

        [Benchmark]
        public string AlphabetPosition_IndexInLowerCasedAlphabet() =>
            AlphabetPosition.AlphabetPosition_IndexInLowerCasedAlphabet(text);

        #endregion
    }
}
