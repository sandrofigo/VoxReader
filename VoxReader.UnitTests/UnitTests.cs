using System.Linq;
using FluentAssertions;
using VoxReader.Interfaces;
using Xunit;

namespace VoxReader.UnitTests
{
    public class UnitTests
    {
        private const string TestFile = "data/3x3.vox";

        [Theory]
        [InlineData(TestFile, 1)]
        public void VoxReader_Read_ModelCountIsCorrect(string file, int expectedCount)
        {
            IVoxFile voxFile = FileReader.ReadVoxFile(file);

            voxFile.Models.Should().HaveCount(expectedCount);
        }

        [Theory]
        [InlineData(TestFile, 4)]
        public void VoxReader_Read_VoxelsCountIsCorrect(string file, int expectedCount)
        {
            IVoxFile voxFile = FileReader.ReadVoxFile(file);

            voxFile.Models.First().Voxels.Should().HaveCount(expectedCount);
        }
    }
}