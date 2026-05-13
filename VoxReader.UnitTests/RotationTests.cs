using System.Linq;
using Shouldly;
using VoxReader.Interfaces;
using Xunit;

namespace VoxReader.UnitTests;

public class RotationTests
{
    private const string RotationTestFile = "data/rotation.zip";
    private const string RotationTestFile2 = "data/rotation_2.zip";
    private const string RotationTestFile3 = "data/rotation_3.zip";
    private const string RotationTestFile4 = "data/rotation_4.zip";
    private const string RotationTestFile5 = "data/rotation_5.zip";

    [Fact]
    public void VoxReader_ReadRotatedModels3x3x3_GlobalVoxelPositionsAreCorrect()
    {
        byte[] file = Zip.GetBytesFromFirstFileInArchive(RotationTestFile2);
        IVoxFile voxFile = VoxReader.Read(file);

        voxFile.Models.First(m => m.Name == "default").Voxels.First(v => v.Color == Color.Red).GlobalPosition.ShouldBe(new Vector3(0, 0, 0));
        voxFile.Models.First(m => m.Name == "random").Voxels.First(v => v.Color == Color.Red).GlobalPosition.ShouldBe(new Vector3(-4, -4, 2));

        // Rotate x
        voxFile.Models.First(m => m.Name == "x1").Voxels.First(v => v.Color == Color.Red).GlobalPosition.ShouldBe(new Vector3(6, 2, 0));
        voxFile.Models.First(m => m.Name == "x2").Voxels.First(v => v.Color == Color.Red).GlobalPosition.ShouldBe(new Vector3(12, 2, 2));
        voxFile.Models.First(m => m.Name == "x3").Voxels.First(v => v.Color == Color.Red).GlobalPosition.ShouldBe(new Vector3(18, 0, 2));
        voxFile.Models.First(m => m.Name == "x4").Voxels.First(v => v.Color == Color.Red).GlobalPosition.ShouldBe(new Vector3(24, 0, 0));

        // Rotate y
        voxFile.Models.First(m => m.Name == "y1").Voxels.First(v => v.Color == Color.Red).GlobalPosition.ShouldBe(new Vector3(0, 6, 2));
        voxFile.Models.First(m => m.Name == "y2").Voxels.First(v => v.Color == Color.Red).GlobalPosition.ShouldBe(new Vector3(2, 12, 2));
        voxFile.Models.First(m => m.Name == "y3").Voxels.First(v => v.Color == Color.Red).GlobalPosition.ShouldBe(new Vector3(2, 18, 0));
        voxFile.Models.First(m => m.Name == "y4").Voxels.First(v => v.Color == Color.Red).GlobalPosition.ShouldBe(new Vector3(0, 24, 0));

        // Rotate z
        voxFile.Models.First(m => m.Name == "z1").Voxels.First(v => v.Color == Color.Red).GlobalPosition.ShouldBe(new Vector3(2, 0, 6));
        voxFile.Models.First(m => m.Name == "z2").Voxels.First(v => v.Color == Color.Red).GlobalPosition.ShouldBe(new Vector3(2, 2, 12));
        voxFile.Models.First(m => m.Name == "z3").Voxels.First(v => v.Color == Color.Red).GlobalPosition.ShouldBe(new Vector3(0, 2, 18));
        voxFile.Models.First(m => m.Name == "z4").Voxels.First(v => v.Color == Color.Red).GlobalPosition.ShouldBe(new Vector3(0, 0, 24));
    }

    [Fact]
    public void VoxReader_ReadRotatedModels3x3x3_LocalVoxelPositionsAreCorrect()
    {
        byte[] file = Zip.GetBytesFromFirstFileInArchive(RotationTestFile2);
        IVoxFile voxFile = VoxReader.Read(file);

        voxFile.Models.First(m => m.Name == "default").Voxels.First(v => v.Color == Color.Red).LocalPosition.ShouldBe(new Vector3(0, 0, 0));
        voxFile.Models.First(m => m.Name == "random").Voxels.First(v => v.Color == Color.Red).LocalPosition.ShouldBe(new Vector3(0, 0, 0));

        voxFile.Models.First(m => m.Name == "x1").Voxels.First(v => v.Color == Color.Red).LocalPosition.ShouldBe(new Vector3(0, 0, 0));
        voxFile.Models.First(m => m.Name == "x2").Voxels.First(v => v.Color == Color.Red).LocalPosition.ShouldBe(new Vector3(0, 0, 0));
        voxFile.Models.First(m => m.Name == "x3").Voxels.First(v => v.Color == Color.Red).LocalPosition.ShouldBe(new Vector3(0, 0, 0));
        voxFile.Models.First(m => m.Name == "x4").Voxels.First(v => v.Color == Color.Red).LocalPosition.ShouldBe(new Vector3(0, 0, 0));

        voxFile.Models.First(m => m.Name == "y1").Voxels.First(v => v.Color == Color.Red).LocalPosition.ShouldBe(new Vector3(0, 0, 0));
        voxFile.Models.First(m => m.Name == "y2").Voxels.First(v => v.Color == Color.Red).LocalPosition.ShouldBe(new Vector3(0, 0, 0));
        voxFile.Models.First(m => m.Name == "y3").Voxels.First(v => v.Color == Color.Red).LocalPosition.ShouldBe(new Vector3(0, 0, 0));
        voxFile.Models.First(m => m.Name == "y4").Voxels.First(v => v.Color == Color.Red).LocalPosition.ShouldBe(new Vector3(0, 0, 0));

        voxFile.Models.First(m => m.Name == "z1").Voxels.First(v => v.Color == Color.Red).LocalPosition.ShouldBe(new Vector3(0, 0, 0));
        voxFile.Models.First(m => m.Name == "z2").Voxels.First(v => v.Color == Color.Red).LocalPosition.ShouldBe(new Vector3(0, 0, 0));
        voxFile.Models.First(m => m.Name == "z3").Voxels.First(v => v.Color == Color.Red).LocalPosition.ShouldBe(new Vector3(0, 0, 0));
        voxFile.Models.First(m => m.Name == "z4").Voxels.First(v => v.Color == Color.Red).LocalPosition.ShouldBe(new Vector3(0, 0, 0));
    }

    [Fact]
    public void VoxReader_ReadRotatedModels3x5x4_GlobalVoxelPositionsAreCorrect()
    {
        byte[] file = Zip.GetBytesFromFirstFileInArchive(RotationTestFile3);
        IVoxFile voxFile = VoxReader.Read(file);

        voxFile.Models.First(m => m.Name == "default").Voxels.First(v => v.Color == Color.Red).GlobalPosition.ShouldBe(new Vector3(1, 0, 4));
        voxFile.Models.First(m => m.Name == "random").Voxels.First(v => v.Color == Color.Red).GlobalPosition.ShouldBe(new Vector3(-4, -5, 0));

        // Rotate x 90°
        voxFile.Models.First(m => m.Name == "x1").Voxels.First(v => v.Color == Color.Red).GlobalPosition.ShouldBe(new Vector3(7, 0, 0));
        voxFile.Models.First(m => m.Name == "x2").Voxels.First(v => v.Color == Color.Red).GlobalPosition.ShouldBe(new Vector3(13, 3, 0));
        voxFile.Models.First(m => m.Name == "x3").Voxels.First(v => v.Color == Color.Red).GlobalPosition.ShouldBe(new Vector3(19, 4, 3));
        voxFile.Models.First(m => m.Name == "x4").Voxels.First(v => v.Color == Color.Red).GlobalPosition.ShouldBe(new Vector3(25, 0, 4));

        // Rotate y 90°
        voxFile.Models.First(m => m.Name == "y1").Voxels.First(v => v.Color == Color.Red).GlobalPosition.ShouldBe(new Vector3(3, 7, 2));
        voxFile.Models.First(m => m.Name == "y2").Voxels.First(v => v.Color == Color.Red).GlobalPosition.ShouldBe(new Vector3(1, 14, 0));
        voxFile.Models.First(m => m.Name == "y3").Voxels.First(v => v.Color == Color.Red).GlobalPosition.ShouldBe(new Vector3(-1, 21, 2));
        voxFile.Models.First(m => m.Name == "y4").Voxels.First(v => v.Color == Color.Red).GlobalPosition.ShouldBe(new Vector3(1, 28, 4));

        // Rotate z 90°
        voxFile.Models.First(m => m.Name == "z1").Voxels.First(v => v.Color == Color.Red).GlobalPosition.ShouldBe(new Vector3(2, 2, 12));
        voxFile.Models.First(m => m.Name == "z2").Voxels.First(v => v.Color == Color.Red).GlobalPosition.ShouldBe(new Vector3(1, 3, 20));
        voxFile.Models.First(m => m.Name == "z3").Voxels.First(v => v.Color == Color.Red).GlobalPosition.ShouldBe(new Vector3(-1, 2, 28));
        voxFile.Models.First(m => m.Name == "z4").Voxels.First(v => v.Color == Color.Red).GlobalPosition.ShouldBe(new Vector3(1, 0, 36));
    }

    [Fact]
    public void VoxReader_ReadRotatedModels3x5x4_LocalVoxelPositionsAreCorrect()
    {
        byte[] file = Zip.GetBytesFromFirstFileInArchive(RotationTestFile3);
        IVoxFile voxFile = VoxReader.Read(file);

        voxFile.Models.First(m => m.Name == "default").Voxels.First(v => v.Color == Color.Red).LocalPosition.ShouldBe(new Vector3(1, 0, 4));
        voxFile.Models.First(m => m.Name == "random").Voxels.First(v => v.Color == Color.Red).LocalPosition.ShouldBe(new Vector3(1, 0, 4));

        voxFile.Models.First(m => m.Name == "x1").Voxels.First(v => v.Color == Color.Red).LocalPosition.ShouldBe(new Vector3(1, 0, 4));
        voxFile.Models.First(m => m.Name == "x2").Voxels.First(v => v.Color == Color.Red).LocalPosition.ShouldBe(new Vector3(1, 0, 4));
        voxFile.Models.First(m => m.Name == "x3").Voxels.First(v => v.Color == Color.Red).LocalPosition.ShouldBe(new Vector3(1, 0, 4));
        voxFile.Models.First(m => m.Name == "x4").Voxels.First(v => v.Color == Color.Red).LocalPosition.ShouldBe(new Vector3(1, 0, 4));

        voxFile.Models.First(m => m.Name == "y1").Voxels.First(v => v.Color == Color.Red).LocalPosition.ShouldBe(new Vector3(1, 0, 4));
        voxFile.Models.First(m => m.Name == "y2").Voxels.First(v => v.Color == Color.Red).LocalPosition.ShouldBe(new Vector3(1, 0, 4));
        voxFile.Models.First(m => m.Name == "y3").Voxels.First(v => v.Color == Color.Red).LocalPosition.ShouldBe(new Vector3(1, 0, 4));
        voxFile.Models.First(m => m.Name == "y4").Voxels.First(v => v.Color == Color.Red).LocalPosition.ShouldBe(new Vector3(1, 0, 4));

        voxFile.Models.First(m => m.Name == "z1").Voxels.First(v => v.Color == Color.Red).LocalPosition.ShouldBe(new Vector3(1, 0, 4));
        voxFile.Models.First(m => m.Name == "z2").Voxels.First(v => v.Color == Color.Red).LocalPosition.ShouldBe(new Vector3(1, 0, 4));
        voxFile.Models.First(m => m.Name == "z3").Voxels.First(v => v.Color == Color.Red).LocalPosition.ShouldBe(new Vector3(1, 0, 4));
        voxFile.Models.First(m => m.Name == "z4").Voxels.First(v => v.Color == Color.Red).LocalPosition.ShouldBe(new Vector3(1, 0, 4));
    }

    [Fact]
    public void VoxReader_ReadRotatedAndGroupedModels_GlobalVoxelPositionsAreCorrect()
    {
        byte[] file = Zip.GetBytesFromFirstFileInArchive(RotationTestFile4);
        IVoxFile voxFile = VoxReader.Read(file);

        voxFile.Models.First(m => m.Name == "default").Voxels.First(v => v.Color == Color.Red).GlobalPosition.ShouldBe(new Vector3(1, 0, 4));
        voxFile.Models.First(m => m.Name == "m1").Voxels.First(v => v.Color == Color.Red).GlobalPosition.ShouldBe(new Vector3(1, 6, 2));
        voxFile.Models.First(m => m.Name == "m2").Voxels.First(v => v.Color == Color.Red).GlobalPosition.ShouldBe(new Vector3(5, 11, 4));
    }

    [Fact]
    public void VoxReader_ReadRotatedAndGroupedModels_LocalVoxelPositionsAreCorrect()
    {
        byte[] file = Zip.GetBytesFromFirstFileInArchive(RotationTestFile4);
        IVoxFile voxFile = VoxReader.Read(file);

        voxFile.Models.First(m => m.Name == "default").Voxels.First(v => v.Color == Color.Red).LocalPosition.ShouldBe(new Vector3(1, 0, 4));
        voxFile.Models.First(m => m.Name == "m1").Voxels.First(v => v.Color == Color.Red).LocalPosition.ShouldBe(new Vector3(1, 0, 4));
        voxFile.Models.First(m => m.Name == "m2").Voxels.First(v => v.Color == Color.Red).LocalPosition.ShouldBe(new Vector3(1, 0, 4));
    }

    [Fact]
    public void VoxReader_ReadRotatedAndGroupedModelA_LocalRotationIsCorrect()
    {
        byte[] file = Zip.GetBytesFromFirstFileInArchive(RotationTestFile);
        IVoxFile voxFile = VoxReader.Read(file);
        IModel modelA = voxFile.Models.First(m => m.Name == "voxa");

        modelA.LocalRotation.ShouldBe(new Matrix3(0b0010_0001));
    }

    [Fact]
    public void VoxReader_ReadRotatedAndGroupedModelA_GlobalRotationIsCorrect()
    {
        byte[] file = Zip.GetBytesFromFirstFileInArchive(RotationTestFile);
        IVoxFile voxFile = VoxReader.Read(file);
        IModel modelA = voxFile.Models.First(m => m.Name == "voxa");

        modelA.GlobalRotation.ShouldBe(Matrix3.Identity);
    }

    [Fact]
    public void VoxReader_ReadRotatedAndGroupedModelB_LocalRotationIsCorrect()
    {
        byte[] file = Zip.GetBytesFromFirstFileInArchive(RotationTestFile);
        IVoxFile voxFile = VoxReader.Read(file);
        IModel modelA = voxFile.Models.First(m => m.Name == "voxb");

        modelA.LocalRotation.ShouldBe(Matrix3.Identity);
    }

    [Fact]
    public void VoxReader_ReadRotatedAndGroupedModelB_GlobalRotationIsCorrect()
    {
        byte[] file = Zip.GetBytesFromFirstFileInArchive(RotationTestFile);
        IVoxFile voxFile = VoxReader.Read(file);
        IModel modelA = voxFile.Models.First(m => m.Name == "voxb");

        modelA.GlobalRotation.ShouldBe(new Matrix3(0b0001_0001));
    }

    [Theory]
    [InlineData("voxa", -4, 0, 0)]
    [InlineData("voxb", 5, 0, 0)]
    public void VoxReader_ReadRotatedAndGroupedModel_LocalPositionIsCorrect(string modelName, int x, int y, int z)
    {
        byte[] file = Zip.GetBytesFromFirstFileInArchive(RotationTestFile);
        IVoxFile voxFile = VoxReader.Read(file);
        IModel modelA = voxFile.Models.First(m => m.Name == modelName);

        modelA.LocalPosition.ShouldBe(new Vector3(x, y, z));
    }

    [Theory]
    [InlineData("voxa", 4, -4, 2)]
    [InlineData("voxb", 4, 5, 2)]
    public void VoxReader_ReadRotatedAndGroupedModel_GlobalPositionIsCorrect(string modelName, int x, int y, int z)
    {
        byte[] file = Zip.GetBytesFromFirstFileInArchive(RotationTestFile);
        IVoxFile voxFile = VoxReader.Read(file);
        IModel modelA = voxFile.Models.First(m => m.Name == modelName);

        modelA.GlobalPosition.ShouldBe(new Vector3(x, y, z));
    }

    [Theory]
    [InlineData("voxa", 5, 4, 4)]
    [InlineData("voxb", 4, 4, 4)]
    public void VoxReader_ReadRotatedAndGroupedModel_LocalSizeIsCorrect(string modelName, int x, int y, int z)
    {
        byte[] file = Zip.GetBytesFromFirstFileInArchive(RotationTestFile);
        IVoxFile voxFile = VoxReader.Read(file);
        IModel modelA = voxFile.Models.First(m => m.Name == modelName);

        modelA.LocalSize.ShouldBe(new Vector3(x, y, z));
    }

    [Theory]
    [InlineData("voxa", 5, 4, 4)]
    [InlineData("voxb", 4, 4, 4)]
    public void VoxReader_ReadRotatedAndGroupedModel_GlobalSizeIsCorrect(string modelName, int x, int y, int z)
    {
        byte[] file = Zip.GetBytesFromFirstFileInArchive(RotationTestFile);
        IVoxFile voxFile = VoxReader.Read(file);
        IModel modelA = voxFile.Models.First(m => m.Name == modelName);

        modelA.GlobalSize.ShouldBe(new Vector3(x, y, z));
    }

    [Fact]
    public void VoxReader_ReadRotatedAndGroupedModel_GlobalVoxelPositionsAreInsideTheGlobalBounds2()
    {
        byte[] file = Zip.GetBytesFromFirstFileInArchive(RotationTestFile5);
        IVoxFile voxFile = VoxReader.Read(file);
        foreach (IModel modelA in voxFile.Models)
        {
            Vector3 min = modelA.GlobalPosition - modelA.GlobalSize / 2;
            Vector3 max = min + modelA.GlobalSize;
            foreach (Voxel v in modelA.Voxels)
            {
                v.GlobalPosition.ShouldSatisfyAllConditions(
                    k => k.X.ShouldBeGreaterThanOrEqualTo(min.X),
                    k => k.X.ShouldBeLessThan(max.X),
                    k => k.Y.ShouldBeGreaterThanOrEqualTo(min.Y),
                    k => k.Y.ShouldBeLessThan(max.Y),
                    k => k.Z.ShouldBeGreaterThanOrEqualTo(min.Z),
                    k => k.Z.ShouldBeLessThan(max.Z)
                );
            }
        }
    }

    [Theory]
    [InlineData("voxa")]
    [InlineData("voxb")]
    public void VoxReader_ReadRotatedAndGroupedModel_GlobalVoxelPositionsAreInsideTheGlobalBounds(string modelName)
    {
        byte[] file = Zip.GetBytesFromFirstFileInArchive(RotationTestFile);
        IVoxFile voxFile = VoxReader.Read(file);
        IModel modelA = voxFile.Models.First(m => m.Name == modelName);

        Vector3 min = modelA.GlobalPosition - modelA.GlobalSize / 2;
        Vector3 max = min + modelA.GlobalSize;
        foreach (Voxel v in modelA.Voxels)
        {
            v.GlobalPosition.ShouldSatisfyAllConditions(
                k => k.X.ShouldBeGreaterThanOrEqualTo(min.X),
                k => k.X.ShouldBeLessThan(max.X),
                k => k.Y.ShouldBeGreaterThanOrEqualTo(min.Y),
                k => k.Y.ShouldBeLessThan(max.Y),
                k => k.Z.ShouldBeGreaterThanOrEqualTo(min.Z),
                k => k.Z.ShouldBeLessThan(max.Z)
            );
        }
    }

    [Theory]
    [InlineData("voxa", 2, -6, 0)]
    [InlineData("voxb", 5, 3, 0)]
    public void VoxReader_ReadRotatedAndGroupedModel_GlobalVoxelPositionIsCorrect(string modelName, int x, int y, int z)
    {
        byte[] file = Zip.GetBytesFromFirstFileInArchive(RotationTestFile);
        IVoxFile voxFile = VoxReader.Read(file);
        IModel modelA = voxFile.Models.First(m => m.Name == modelName);

        modelA.Voxels.First(v => v.ColorIndex == 215).GlobalPosition.ShouldBe(new Vector3(x, y, z));
    }

    [Theory]
    [InlineData("voxa", 0, 0, 0)]
    [InlineData("voxb", 0, 0, 0)]
    public void VoxReader_ReadRotatedAndGroupedModel_LocalVoxelPositionIsCorrect(string modelName, int x, int y, int z)
    {
        byte[] file = Zip.GetBytesFromFirstFileInArchive(RotationTestFile);
        IVoxFile voxFile = VoxReader.Read(file);
        IModel modelA = voxFile.Models.First(m => m.Name == modelName);

        modelA.Voxels.First(v => v.ColorIndex == 215).LocalPosition.ShouldBe(new Vector3(x, y, z));
    }
}