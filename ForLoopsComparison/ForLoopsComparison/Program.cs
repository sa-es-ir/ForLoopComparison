using BenchmarkDotNet.Running;
using ForLoopsComparison;

Console.WriteLine(BenchmarkRunner.Run<AllForLoopBenchmark>());