using BenchmarkDotNet.Attributes;
using VoxReader.Interfaces;
using VoxReader.UnitTests;

namespace VoxReader.Benchmarks;

[MemoryDiagnoser]
public class ReadBenchmarks
{
    private readonly byte[] voxFile1X1 = File.ReadAllBytes(Zip.UnzipFilesFromZipArchive(GeneralTests.TestFile1X1).First());
    private readonly byte[] voxFile256X256 = File.ReadAllBytes(Zip.UnzipFilesFromZipArchive(GeneralTests.TestFile256X256).First());
    private readonly byte[] voxFileMultipleModels = File.ReadAllBytes(Zip.UnzipFilesFromZipArchive(GeneralTests.TestFileMultipleModels).First());

    [Benchmark(Baseline = true)]
    public IVoxFile Read1X1()
    {
        return VoxReader.Read(voxFile1X1);
    }

    [Benchmark]
    public IVoxFile Read256X256()
    {
        return VoxReader.Read(voxFile256X256);
    }
    
    [Benchmark]
    public IVoxFile ReadMultipleModels()
    {
        return VoxReader.Read(voxFileMultipleModels);
    }
}