using System.Linq;
using FluentAssertions;
using VoxReader.Interfaces;
using Xunit;

namespace VoxReader.UnitTests;

public class GeneralTests
{
    private const string TestFile3X3 = "data/3x3.zip";
    private const string TestFile3X3ExportedAsVox = "data/3x3_exported_as_vox.zip";
    private const string TestFile3X32 = "data/3x3_2.zip";
    private const string TestFile3X33 = "data/3x3_3.zip";
    private const string TestFile1X1 = "data/1x1.zip";
    private const string TestFile1X1ExportedAsVox = "data/1x1_exported_as_vox.zip";
    private const string TestFile256X256 = "data/256x256.zip";
    private const string TestFile256X256ExportedAsVox = "data/256x256_exported_as_vox.zip";
    private const string TestFileMultipleModels = "data/multiple_models.zip";
    private const string TestFile3X3X3AtCenterWithCorner = "data/3x3x3_at_center_with_corner.zip";
    private const string TestFileGroups = "data/groups.zip";
    private const string TestFileNotes = "data/color_notes.zip";
    private const string TestFileNoNotes = "data/no_notes.zip";
    private const string TestFileColorIndices = "data/color_indices.zip";
    private const string TestFileColorIndicesExportedAsVox = "data/color_indices_exported_as_vox.zip";
    private const string TestFileColorIndices2 = "data/color_indices_2.zip";
    private const string TestFileColorIndices2ExportedAsVox = "data/color_indices_2_exported_as_vox.zip";

    [Fact]
    public void VoxReader_GetColorIndicesByNote_ReturnsEmptyArrayWhenNoteTextDoesNotMatch()
    {
        string file = Zip.UnzipFilesFromZipArchive(TestFileColorIndices2).First();

        IVoxFile voxFile = VoxReader.Read(file);

        voxFile.Palette.GetColorIndicesByNote("no match").Should().BeEmpty();
    }

    [Fact]
    public void VoxReader_GetColorIndicesByNote_ColorIndicesAreCorrect()
    {
        string file = Zip.UnzipFilesFromZipArchive(TestFileColorIndices2).First();

        IVoxFile voxFile = VoxReader.Read(file);

        voxFile.Palette.GetColorIndicesByNote("red").Should().ContainInOrder(248, 249, 250, 251, 252, 253, 254);
        voxFile.Palette.GetColorIndicesByNote("mixed").Should().ContainInOrder(144, 145, 146, 147, 148, 149, 150);
        voxFile.Palette.GetColorIndicesByNote("green").Should().ContainInOrder(0, 1, 2, 3, 4, 5, 6);
    }

    [Theory]
    [InlineData(TestFileColorIndices, 0, 2, 0, 152)]
    [InlineData(TestFileColorIndices, 1, 1, 0, 99)]
    [InlineData(TestFileColorIndices, 2, 0, 0, 16)]
    [InlineData(TestFileColorIndices2, 0, 1, 0, 0)]
    [InlineData(TestFileColorIndices2, 1, 2, 0, 144)]
    [InlineData(TestFileColorIndices2, 2, 0, 0, 254)]
    [InlineData(TestFileColorIndicesExportedAsVox, 0, 2, 0, 152)]
    [InlineData(TestFileColorIndicesExportedAsVox, 1, 1, 0, 99)]
    [InlineData(TestFileColorIndicesExportedAsVox, 2, 0, 0, 16)]
    [InlineData(TestFileColorIndices2ExportedAsVox, 0, 1, 0, 0)]
    [InlineData(TestFileColorIndices2ExportedAsVox, 1, 2, 0, 144)]
    [InlineData(TestFileColorIndices2ExportedAsVox, 2, 0, 0, 254)]
    public void VoxReader_Read_ColorIndicesOnVoxelAreCorrect(string testFile, int x, int y, int z, int expectedIndex)
    {
        string file = Zip.UnzipFilesFromZipArchive(testFile).First();

        IVoxFile voxFile = VoxReader.Read(file);

        voxFile.Models.First().Voxels.First(voxel => voxel.LocalPosition == new Vector3(x, y, z)).ColorIndex.Should().Be(expectedIndex);
    }

    [Theory]
    [InlineData(TestFileColorIndices)]
    [InlineData(TestFileColorIndicesExportedAsVox)]
    public void VoxReader_Read_ColorIndicesAreCorrect(string testFile)
    {
        string file = Zip.UnzipFilesFromZipArchive(testFile).First();

        IVoxFile voxFile = VoxReader.Read(file);

        voxFile.Palette.Colors[254].Should().Be(Color.Cyan);
        voxFile.Palette.Colors[251].Should().Be(Color.Yellow);
        voxFile.Palette.Colors[154].Should().Be(Color.Blue);
        voxFile.Palette.Colors[152].Should().Be(Color.Red);
        voxFile.Palette.Colors[133].Should().Be(Color.Yellow);
        voxFile.Palette.Colors[99].Should().Be(Color.Green);
        voxFile.Palette.Colors[90].Should().Be(Color.Magenta);
        voxFile.Palette.Colors[16].Should().Be(Color.Blue);
    }

    [Fact]
    public void VoxReader_GetColorsByNote_NoteNameMatchesColorsInTheSameRow()
    {
        string file = Zip.UnzipFilesFromZipArchive(TestFileColorIndices).First();

        IVoxFile voxFile = VoxReader.Read(file);

        voxFile.Palette.GetColorsByNote("note 1").Should().ContainInOrder(Color.Red, Color.Black, Color.Blue, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black);
        voxFile.Palette.GetColorsByNote("note 2").Should().ContainInOrder(Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Yellow, Color.Black, Color.Black);
        voxFile.Palette.GetColorsByNote("note 3").Should().ContainInOrder(Color.Black, Color.Black, Color.Black, Color.Green, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Magenta, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black);
        voxFile.Palette.GetColorsByNote("note 4").Should().ContainInOrder(Color.Blue, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black);
        voxFile.Palette.GetColorsByNote("note 5").Should().ContainInOrder(Color.Black, Color.Black, Color.Black, Color.Yellow, Color.Black, Color.Black, Color.Cyan);
    }

    [Fact]
    public void VoxReader_GetColorsByNote_NotMatchingNoteReturnsEmptyCollection()
    {
        string file = Zip.UnzipFilesFromZipArchive(TestFileColorIndices).First();

        IVoxFile voxFile = VoxReader.Read(file);

        voxFile.Palette.GetColorsByNote("no match").Should().BeEmpty();
    }

    [Fact]
    public void VoxReader_Read_PaletteColorPositionMatchesNoteRow()
    {
        string file = Zip.UnzipFilesFromZipArchive(TestFileNotes).First();

        IVoxFile voxFile = VoxReader.Read(file);

        voxFile.Palette.Colors[248].Should().Be(Color.Red);
        voxFile.Palette.Colors[136].Should().Be(Color.Green);
        voxFile.Palette.Colors[0].Should().Be(Color.Blue);
    }

    [Fact]
    public void VoxReader_ReadFileWithNoNotes_NotesAreEmptyStrings()
    {
        string file = Zip.UnzipFilesFromZipArchive(TestFileNoNotes).First();

        IVoxFile voxFile = VoxReader.Read(file);

        voxFile.Palette.Notes.Should().AllBe("");
    }

    [Fact]
    public void VoxReader_Read_NotesAreParsedCorrectly()
    {
        string file = Zip.UnzipFilesFromZipArchive(TestFileNotes).First();

        IVoxFile voxFile = VoxReader.Read(file);

        for (int i = 0; i < 32; i++)
        {
            int iLocal = i;
            voxFile.Palette.Notes.Should().ContainSingle(note => note == $"note {iLocal + 1}");
        }
    }

    [Theory]
    [InlineData(TestFile3X3, 32)]
    [InlineData(TestFileNotes, 32)]
    public void VoxReader_Read_NoteCountIsCorrect(string testFile, int expectedCount)
    {
        string file = Zip.UnzipFilesFromZipArchive(testFile).First();

        IVoxFile voxFile = VoxReader.Read(file);

        voxFile.Palette.Notes.Should().HaveCount(expectedCount);
    }

    [Theory]
    [InlineData(TestFile3X3, "yellow", 0, 0, 0)]
    [InlineData(TestFile3X3, "red", 2, 0, 0)]
    [InlineData(TestFile3X3, "green", 0, 2, 0)]
    [InlineData(TestFile3X3, "blue", 0, 0, 2)]
    [InlineData(TestFile3X3ExportedAsVox, "yellow", 0, 0, 0)]
    [InlineData(TestFile3X3ExportedAsVox, "red", 2, 0, 0)]
    [InlineData(TestFile3X3ExportedAsVox, "green", 0, 2, 0)]
    [InlineData(TestFile3X3ExportedAsVox, "blue", 0, 0, 2)]
    public void VoxReader_Read_VoxelPositionIsCorrect(string testFile, string voxelColorToSearch, int desiredPositionX, int desiredPositionY, int desiredPositionZ)
    {
        string file = Zip.UnzipFilesFromZipArchive(testFile).First();

        IVoxFile voxFile = VoxReader.Read(file);

        voxFile.Models.First().Voxels.Single(v => v.Color == Color.GetColorFromName(voxelColorToSearch)).LocalPosition.Should().Be(new Vector3(desiredPositionX, desiredPositionY, desiredPositionZ));
    }

    [Theory]
    [InlineData(TestFileGroups, "obj1", "red", 0, 0, 0)]
    [InlineData(TestFileGroups, "obj2", "red", 0, 0, 2)]
    [InlineData(TestFileGroups, "obj3", "blue", -3, 0, 3)]
    [InlineData(TestFileGroups, "obj3", "red", -1, 2, 5)]
    [InlineData(TestFileGroups, "obj4", "blue", -3, 0, 7)]
    [InlineData(TestFileGroups, "obj4", "red", -1, 2, 9)]
    [InlineData(TestFile3X3, null, "yellow", -2, -2, 0)]
    [InlineData(TestFile3X3, null, "red", 0, -2, 0)]
    [InlineData(TestFile3X3, null, "green", -2, 0, 0)]
    [InlineData(TestFile3X3, null, "blue", -2, -2, 2)]
    [InlineData(TestFile3X3ExportedAsVox, null, "yellow", 0, 0, 0)]
    [InlineData(TestFile3X3ExportedAsVox, null, "red", 2, 0, 0)]
    [InlineData(TestFile3X3ExportedAsVox, null, "green", 0, 2, 0)]
    [InlineData(TestFile3X3ExportedAsVox, null, "blue", 0, 0, 2)]
    public void VoxReader_Read_GlobalVoxelPositionIsCorrect(string testFile, string modelName, string voxelColorToSearch, int desiredGlobalPositionX, int desiredGlobalPositionY, int desiredGlobalPositionZ)
    {
        string file = Zip.UnzipFilesFromZipArchive(testFile).First();

        IVoxFile voxFile = VoxReader.Read(file);

        voxFile.Models.Single(m => m.Name == modelName).Voxels.Single(v => v.Color == Color.GetColorFromName(voxelColorToSearch)).GlobalPosition.Should().Be(new Vector3(desiredGlobalPositionX, desiredGlobalPositionY, desiredGlobalPositionZ));
    }

    [Fact]
    public void VoxReader_Read_ModelNamesAreParsedCorrectly()
    {
        string file = Zip.UnzipFilesFromZipArchive(TestFileGroups).First();

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
        string file = Zip.UnzipFilesFromZipArchive(TestFileGroups).First();

        IVoxFile voxFile = VoxReader.Read(file);

        voxFile.Models.Single(m => m.Name == "obj1").GlobalPosition.Should().Be(new Vector3(0, 0, 0));
        voxFile.Models.Single(m => m.Name == "obj2").GlobalPosition.Should().Be(new Vector3(0, 0, 2));
        voxFile.Models.Single(m => m.Name == "obj3").GlobalPosition.Should().Be(new Vector3(-2, 1, 4));
        voxFile.Models.Single(m => m.Name == "obj4").GlobalPosition.Should().Be(new Vector3(-2, 1, 8));
        voxFile.Models.Single(m => m.Name == "obj5").GlobalPosition.Should().Be(new Vector3(2, 1, 8));
    }

    [Fact]
    public void VoxReader_Read_ModelPositionsAreCorrect()
    {
        string file = Zip.UnzipFilesFromZipArchive(TestFileMultipleModels).First();

        IVoxFile voxFile = VoxReader.Read(file);

        voxFile.Models.Single(m => m.Name == "black").GlobalPosition.Should().Be(new Vector3(0, 0, 0));
        voxFile.Models.Single(m => m.Name == "red").GlobalPosition.Should().Be(new Vector3(2, 0, 0));
        voxFile.Models.Single(m => m.Name == "green").GlobalPosition.Should().Be(new Vector3(0, 2, 0));
        voxFile.Models.Single(m => m.Name == "blue").GlobalPosition.Should().Be(new Vector3(0, 0, 2));
        voxFile.Models.Single(m => m.Name == "yellow").GlobalPosition.Should().Be(new Vector3(0, 0, -2));
        voxFile.Models.Single(m => m.Name == "magenta").GlobalPosition.Should().Be(new Vector3(0, -2, 0));
        voxFile.Models.Single(m => m.Name == "cyan").GlobalPosition.Should().Be(new Vector3(-2, 0, 0));
    }

    [Fact]
    public void VoxReader_Read_ModelPositionIsCorrectFor3x3x3Model()
    {
        string file = Zip.UnzipFilesFromZipArchive(TestFile3X3X3AtCenterWithCorner).First();

        IVoxFile voxFile = VoxReader.Read(file);

        voxFile.Models.Single(m => m.Name == "obj1").GlobalPosition.Should().Be(new Vector3(1, 1, 1));
    }

    [Theory]
    [InlineData(TestFile3X3, 1)]
    [InlineData(TestFile3X3ExportedAsVox, 1)]
    [InlineData(TestFile3X32, 1)]
    [InlineData(TestFile3X33, 4)]
    public void VoxReader_Read_ModelCountIsCorrect(string file, int expectedCount)
    {
        file = Zip.UnzipFilesFromZipArchive(file).First();

        IVoxFile voxFile = VoxReader.Read(file);

        voxFile.Models.Should().HaveCount(expectedCount);
    }

    [Theory]
    [InlineData(TestFile3X3, 4)]
    [InlineData(TestFile3X3ExportedAsVox, 4)]
    [InlineData(TestFile3X32, 3)]
    [InlineData(TestFile3X33, 1, 1, 1, 1)]
    public void VoxReader_Read_VoxelCountIsCorrect(string file, params int[] expectedCount)
    {
        file = Zip.UnzipFilesFromZipArchive(file).First();

        IVoxFile voxFile = VoxReader.Read(file);

        var models = voxFile.Models;

        models.Should().HaveCount(expectedCount.Length);

        for (int i = 0; i < expectedCount.Length; i++)
        {
            models[i].Voxels.Should().HaveCount(expectedCount[i]);
        }
    }

    [Theory]
    [InlineData(TestFileColorIndices)]
    [InlineData(TestFileColorIndicesExportedAsVox)]
    public void VoxReader_ReadFileFromVersion0_99_6_4_VoxelColorIsCorrect(string testFile)
    {
        string file = Zip.UnzipFilesFromZipArchive(testFile).First();

        IVoxFile voxFile = VoxReader.Read(file);

        IModel model = voxFile.Models.First();

        model.Voxels.First(voxel => voxel.LocalPosition == new Vector3(0, 2, 0)).Color.Should().Be(Color.Red);
        model.Voxels.First(voxel => voxel.LocalPosition == new Vector3(1, 1, 0)).Color.Should().Be(Color.Green);
        model.Voxels.First(voxel => voxel.LocalPosition == new Vector3(2, 0, 0)).Color.Should().Be(Color.Blue);
    }

    [Theory]
    [InlineData(TestFile3X3)]
    [InlineData(TestFile3X3ExportedAsVox)]
    public void VoxReader_Read_VoxelColorIsCorrect(string testFile)
    {
        string file = Zip.UnzipFilesFromZipArchive(testFile).First();

        IVoxFile voxFile = VoxReader.Read(file);

        IModel model = voxFile.Models.First();

        model.Voxels.First(voxel => voxel.LocalPosition == new Vector3(0, 0, 0)).Color.Should().Be(Color.Yellow);
        model.Voxels.First(voxel => voxel.LocalPosition == new Vector3(2, 0, 0)).Color.Should().Be(Color.Red);
        model.Voxels.First(voxel => voxel.LocalPosition == new Vector3(0, 2, 0)).Color.Should().Be(Color.Green);
        model.Voxels.First(voxel => voxel.LocalPosition == new Vector3(0, 0, 2)).Color.Should().Be(Color.Blue);
    }

    [Theory]
    [InlineData(TestFile1X1)]
    [InlineData(TestFile1X1ExportedAsVox)]
    public void VoxReader_Read_VoxelColorIsCorrectForSmallestModel(string testFile)
    {
        string file = Zip.UnzipFilesFromZipArchive(testFile).First();

        IVoxFile voxFile = VoxReader.Read(file);

        IModel model = voxFile.Models.First();

        model.Voxels.First(voxel => voxel.LocalPosition == new Vector3(0, 0, 0)).Color.Should().Be(new Color(123, 162, 63, 255));
    }

    [Theory]
    [InlineData(TestFile1X1)]
    [InlineData(TestFile1X1ExportedAsVox)]
    public void VoxReader_Read_VoxelCountIsCorrectForSmallestModel(string testFile)
    {
        string file = Zip.UnzipFilesFromZipArchive(testFile).First();

        IVoxFile voxFile = VoxReader.Read(file);

        IModel model = voxFile.Models.First();

        model.Voxels.Should().HaveCount(1);
    }

    [Theory]
    [InlineData(TestFile256X256)]
    [InlineData(TestFile256X256ExportedAsVox)]
    public void VoxReader_Read_VoxelColorIsCorrectForLargestModel(string testFile)
    {
        string file = Zip.UnzipFilesFromZipArchive(testFile).First();

        IVoxFile voxFile = VoxReader.Read(file);

        IModel model = voxFile.Models.First();

        foreach (Voxel voxel in model.Voxels)
        {
            voxel.Color.Should().Be(new Color(123, 162, 63, 255));
        }
    }

    [Theory]
    [InlineData(TestFile256X256)]
    [InlineData(TestFile256X256ExportedAsVox)]
    public void VoxReader_Read_VoxelCountIsCorrectForLargestModel(string testFile)
    {
        string file = Zip.UnzipFilesFromZipArchive(testFile).First();

        IVoxFile voxFile = VoxReader.Read(file);

        IModel model = voxFile.Models.First();

        model.Voxels.Should().HaveCount(256 * 256 * 256 - 254 * 254 * 254);
    }
}