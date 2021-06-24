using System.Linq;
using FluentAssertions;
using VoxReader.Interfaces;
using Xunit;

namespace VoxReader.UnitTests
{
    public class UnitTests
    {
        private const string TestFile1 = "data/3x3.vox";
        private const string TestFile2 = "data/3x3_2.vox";
        private const string TestFile3 = "data/3x3_3.vox";

        [Theory]
        [InlineData(TestFile1, 1)]
        [InlineData(TestFile2, 1)]
        [InlineData(TestFile3, 4)]
        public void VoxReader_Read_ModelCountIsCorrect(string file, int expectedCount)
        {
            IVoxFile voxFile = VoxReader.ReadVoxFile(file);

            voxFile.Models.Should().HaveCount(expectedCount);
        }

        [Theory]
        [InlineData(TestFile1, 4)]
        [InlineData(TestFile2, 3)]
        [InlineData(TestFile3, 1, 1, 1, 1)]
        public void VoxReader_Read_VoxelCountIsCorrect(string file, params int[] expectedCount)
        {
            IVoxFile voxFile = VoxReader.ReadVoxFile(file);

            var models = voxFile.Models;

            models.Should().HaveCount(expectedCount.Length);

            for (int i = 0; i < expectedCount.Length; i++)
            {
                models[i].Voxels.Should().HaveCount(expectedCount[i]);
            }
        }

        [Fact]
        public void VoxReader_Read_VoxelColorIsCorrect()
        {
            IVoxFile voxFile = VoxReader.ReadVoxFile(TestFile1);

            IModel model = voxFile.Models.First();

            model.Voxels.First(voxel => voxel.Position == new Vector3(0, 0, 0)).Color.Should().Be(new Color(255, 177, 27, 255));
            model.Voxels.First(voxel => voxel.Position == new Vector3(2, 0, 0)).Color.Should().Be(new Color(203, 64, 66, 255));
            model.Voxels.First(voxel => voxel.Position == new Vector3(0, 2, 0)).Color.Should().Be(new Color(27, 129, 62, 255));
            model.Voxels.First(voxel => voxel.Position == new Vector3(0, 0, 2)).Color.Should().Be(new Color(0, 92, 175, 255));
        }
    }
}