using System.Linq;
using FluentAssertions;
using VoxReader.Interfaces;
using Xunit;

namespace VoxReader.UnitTests
{
    public class UnitTests
    {
        private const string TestFile_3x3 = "data/3x3.zip";
        private const string TestFile_3x3_2 = "data/3x3_2.zip";
        private const string TestFile_3x3_3 = "data/3x3_3.zip";
        private const string TestFile_1x1 = "data/1x1.zip";
        private const string TestFile_256x256 = "data/256x256.zip";

        [Theory]
        [InlineData(TestFile_3x3, 1)]
        [InlineData(TestFile_3x3_2, 1)]
        [InlineData(TestFile_3x3_3, 4)]
        public void VoxReader_Read_ModelCountIsCorrect(string file, int expectedCount)
        {
            file = Zip.UnzipFilesFromSevenZipArchive(file).First();

            IVoxFile voxFile = VoxReader.Read(file);

            voxFile.Models.Should().HaveCount(expectedCount);
        }

        [Theory]
        [InlineData(TestFile_3x3, 4)]
        [InlineData(TestFile_3x3_2, 3)]
        [InlineData(TestFile_3x3_3, 1, 1, 1, 1)]
        public void VoxReader_Read_VoxelCountIsCorrect(string file, params int[] expectedCount)
        {
            file = Zip.UnzipFilesFromSevenZipArchive(file).First();

            IVoxFile voxFile = VoxReader.Read(file);

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
            string file = Zip.UnzipFilesFromSevenZipArchive(TestFile_3x3).First();

            IVoxFile voxFile = VoxReader.Read(file);

            IModel model = voxFile.Models.First();

            model.Voxels.First(voxel => voxel.Position == new Vector3(0, 0, 0)).Color.Should().Be(new Color(255, 177, 27, 255));
            model.Voxels.First(voxel => voxel.Position == new Vector3(2, 0, 0)).Color.Should().Be(new Color(203, 64, 66, 255));
            model.Voxels.First(voxel => voxel.Position == new Vector3(0, 2, 0)).Color.Should().Be(new Color(27, 129, 62, 255));
            model.Voxels.First(voxel => voxel.Position == new Vector3(0, 0, 2)).Color.Should().Be(new Color(0, 92, 175, 255));
        }

        [Fact]
        public void VoxReader_Read_VoxelColorIsCorrectForSmallestModel()
        {
            string file = Zip.UnzipFilesFromSevenZipArchive(TestFile_1x1).First();

            IVoxFile voxFile = VoxReader.Read(file);

            IModel model = voxFile.Models.First();

            model.Voxels.First(voxel => voxel.Position == new Vector3(0, 0, 0)).Color.Should().Be(new Color(123, 162, 63, 255));
        }

        [Fact]
        public void VoxReader_Read_VoxelCountIsCorrectForSmallestModel()
        {
            string file = Zip.UnzipFilesFromSevenZipArchive(TestFile_1x1).First();

            IVoxFile voxFile = VoxReader.Read(file);

            IModel model = voxFile.Models.First();

            model.Voxels.Should().HaveCount(1);
        }

        [Fact]
        public void VoxReader_Read_VoxelColorIsCorrectForLargestModel()
        {
            string file = Zip.UnzipFilesFromSevenZipArchive(TestFile_256x256).First();

            IVoxFile voxFile = VoxReader.Read(file);

            IModel model = voxFile.Models.First();

            foreach (Voxel voxel in model.Voxels)
            {
                voxel.Color.Should().Be(new Color(123, 162, 63, 255));
            }
        }

        [Fact]
        public void VoxReader_Read_VoxelCountIsCorrectForLargestModel()
        {
            string file = Zip.UnzipFilesFromSevenZipArchive(TestFile_256x256).First();

            IVoxFile voxFile = VoxReader.Read(file);

            IModel model = voxFile.Models.First();

            model.Voxels.Should().HaveCount(256 * 256 * 256);
        }
    }
}