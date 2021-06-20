using System.Linq;
using FluentAssertions;
using VoxReader.Interfaces;
using Xunit;

namespace VoxReader.UnitTests
{
    public class UnitTests
    {
        private const string TestFile = "data/3x3.vox";
        private const string TestFile2 = "data/3x3_2.vox";

        [Theory]
        [InlineData(TestFile, 1)]
        [InlineData(TestFile2, 1)]
        public void VoxReader_Read_ModelCountIsCorrect(string file, int expectedCount)
        {
            IVoxFile voxFile = VoxReader.ReadVoxFile(file);

            voxFile.Models.Should().HaveCount(expectedCount);
        }

        [Theory]
        [InlineData(TestFile, 4)]
        [InlineData(TestFile2, 3)]
        public void VoxReader_Read_VoxelsCountIsCorrect(string file, int expectedCount)
        {
            IVoxFile voxFile = VoxReader.ReadVoxFile(file);

            voxFile.Models.First().Voxels.Should().HaveCount(expectedCount);
        }
    }
}