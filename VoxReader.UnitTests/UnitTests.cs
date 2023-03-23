using System;
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
        private const string TestFile_MultipleModels = "data/multiple_models.zip";
        private const string TestFile_3x3x3_at_center_with_corner = "data/3x3x3_at_center_with_corner.zip";
        private const string TestFile_groups = "data/groups.zip";
        private const string TestFile_notes = "data/color_notes.zip";

        [Fact]
        public void VoxReader_Read_NotesAreParsedCorrectly()
        {
            string file = Zip.UnzipFilesFromSevenZipArchive(TestFile_notes).First();

            IVoxFile voxFile = VoxReader.Read(file);

            for (int i = 0; i < 32; i++)
            {
                int iLocal = i;
                voxFile.Palette.Notes.Should().ContainSingle(note => note == $"note {iLocal + 1}");
            }
        }

        [Theory]
        [InlineData(TestFile_3x3, 32)]
        [InlineData(TestFile_notes, 32)]
        public void VoxReader_Read_NoteCountIsCorrect(string testFile, int expectedCount)
        {
            string file = Zip.UnzipFilesFromSevenZipArchive(testFile).First();

            IVoxFile voxFile = VoxReader.Read(file);

            voxFile.Palette.Notes.Should().HaveCount(expectedCount);
        }

        [Fact]
        public void VoxReader_Read_GlobalVoxelPositionIsCorrect()
        {
            string file = Zip.UnzipFilesFromSevenZipArchive(TestFile_groups).First();

            IVoxFile voxFile = VoxReader.Read(file);

            voxFile.Models.Single(m => m.Name == "obj1").Voxels[0].GlobalPosition.Should().Be(new Vector3(0, 0, 0));
            voxFile.Models.Single(m => m.Name == "obj2").Voxels[0].GlobalPosition.Should().Be(new Vector3(0, 0, 2));

            voxFile.Models.Single(m => m.Name == "obj3").Voxels.Single(v => v.Color == Color.Blue).GlobalPosition.Should().Be(new Vector3(-3, 0, 3));
            voxFile.Models.Single(m => m.Name == "obj3").Voxels.Single(v => v.Color == Color.Red).GlobalPosition.Should().Be(new Vector3(-1, 2, 5));

            voxFile.Models.Single(m => m.Name == "obj4").Voxels.Single(v => v.Color == Color.Blue).GlobalPosition.Should().Be(new Vector3(-3, 0, 7));
            voxFile.Models.Single(m => m.Name == "obj4").Voxels.Single(v => v.Color == Color.Red).GlobalPosition.Should().Be(new Vector3(-1, 2, 9));
        }

        [Fact]
        public void VoxReader_Read_ModelNamesAreParsedCorrectly()
        {
            string file = Zip.UnzipFilesFromSevenZipArchive(TestFile_groups).First();

            IVoxFile voxFile = VoxReader.Read(file);

            voxFile.Models.Should().ContainSingle(m => m.Name == "obj1");
            voxFile.Models.Should().ContainSingle(m => m.Name == "obj2");
            voxFile.Models.Should().ContainSingle(m => m.Name == "obj3");
            voxFile.Models.Should().ContainSingle(m => m.Name == "obj4");
            voxFile.Models.Should().ContainSingle(m => m.Name == "obj5");
        }

        [Fact]
        public void VoxReader_Read_ModelPositionsAreCorrectInGroups()
        {
            string file = Zip.UnzipFilesFromSevenZipArchive(TestFile_groups).First();

            IVoxFile voxFile = VoxReader.Read(file);

            voxFile.Models.Single(m => m.Name == "obj1").Position.Should().Be(new Vector3(0, 0, 0));
            voxFile.Models.Single(m => m.Name == "obj2").Position.Should().Be(new Vector3(0, 0, 2));
            voxFile.Models.Single(m => m.Name == "obj3").Position.Should().Be(new Vector3(-2, 1, 4));
            voxFile.Models.Single(m => m.Name == "obj4").Position.Should().Be(new Vector3(-2, 1, 8));
            voxFile.Models.Single(m => m.Name == "obj5").Position.Should().Be(new Vector3(2, 1, 8));
        }

        [Fact]
        public void VoxReader_Read_ModelPositionsAreCorrect()
        {
            string file = Zip.UnzipFilesFromSevenZipArchive(TestFile_MultipleModels).First();

            IVoxFile voxFile = VoxReader.Read(file);

            voxFile.Models.Single(m => m.Name == "black").Position.Should().Be(new Vector3(0, 0, 0));
            voxFile.Models.Single(m => m.Name == "red").Position.Should().Be(new Vector3(2, 0, 0));
            voxFile.Models.Single(m => m.Name == "green").Position.Should().Be(new Vector3(0, 2, 0));
            voxFile.Models.Single(m => m.Name == "blue").Position.Should().Be(new Vector3(0, 0, 2));
            voxFile.Models.Single(m => m.Name == "yellow").Position.Should().Be(new Vector3(0, 0, -2));
            voxFile.Models.Single(m => m.Name == "magenta").Position.Should().Be(new Vector3(0, -2, 0));
            voxFile.Models.Single(m => m.Name == "cyan").Position.Should().Be(new Vector3(-2, 0, 0));
        }

        [Fact]
        public void VoxReader_Read_ModelPositionIsCorrectFor3x3x3Model()
        {
            string file = Zip.UnzipFilesFromSevenZipArchive(TestFile_3x3x3_at_center_with_corner).First();

            IVoxFile voxFile = VoxReader.Read(file);

            voxFile.Models.Single(m => m.Name == "obj1").Position.Should().Be(new Vector3(1, 1, 1));
        }

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