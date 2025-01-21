using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using VoxReader.Benchmarks;

Summary summary = BenchmarkRunner.Run<ReadBenchmarks>();