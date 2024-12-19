using FluentAssertions;
using System.Linq;
using VoxReader.Interfaces;
using Xunit;

namespace VoxReader.UnitTests
{
    public class RotationTests
    {
        private const string RotationTestFile = "data/rotation.zip";
        private const string RotationTestFile2 = "data/rotation_2.zip";
        private const string RotationTestFile3 = "data/rotation_3.zip";
        private const string RotationTestFile4 = "data/rotation_4.zip";

        // TODO: create tests for local/global position and local/global size
        
        [Fact]
        public void TestRot()
        {
            string file = Zip.UnzipFilesFromSevenZipArchive(RotationTestFile2).First();
            IVoxFile voxFile = VoxReader.Read(file);

            voxFile.Models.First(m => m.Name == "default").Voxels.First(v => v.Color == Color.Red).GlobalPosition.Should().Be(new Vector3(0, 0, 0));
            voxFile.Models.First(m => m.Name == "default").Voxels.First(v => v.Color == Color.Red).LocalPosition.Should().Be(new Vector3(0, 0, 0));

            voxFile.Models.First(m => m.Name == "random").Voxels.First(v => v.Color == Color.Red).GlobalPosition.Should().Be(new Vector3(-4, -4, 2));
            voxFile.Models.First(m => m.Name == "random").Voxels.First(v => v.Color == Color.Red).LocalPosition.Should().Be(new Vector3(0, 0, 0));

            // Rotate x 90°
            voxFile.Models.First(m => m.Name == "x1").Voxels.First(v => v.Color == Color.Red).GlobalPosition.Should().Be(new Vector3(6, 2, 0));
            voxFile.Models.First(m => m.Name == "x1").Voxels.First(v => v.Color == Color.Red).LocalPosition.Should().Be(new Vector3(0, 0, 0));

            voxFile.Models.First(m => m.Name == "x2").Voxels.First(v => v.Color == Color.Red).GlobalPosition.Should().Be(new Vector3(12, 2, 2));
            voxFile.Models.First(m => m.Name == "x2").Voxels.First(v => v.Color == Color.Red).LocalPosition.Should().Be(new Vector3(0, 0, 0));

            voxFile.Models.First(m => m.Name == "x3").Voxels.First(v => v.Color == Color.Red).GlobalPosition.Should().Be(new Vector3(18, 0, 2));
            voxFile.Models.First(m => m.Name == "x3").Voxels.First(v => v.Color == Color.Red).LocalPosition.Should().Be(new Vector3(0, 0, 0));

            voxFile.Models.First(m => m.Name == "x4").Voxels.First(v => v.Color == Color.Red).GlobalPosition.Should().Be(new Vector3(24, 0, 0));
            voxFile.Models.First(m => m.Name == "x4").Voxels.First(v => v.Color == Color.Red).LocalPosition.Should().Be(new Vector3(0, 0, 0));

            // Rotate y 90°
            voxFile.Models.First(m => m.Name == "y1").Voxels.First(v => v.Color == Color.Red).GlobalPosition.Should().Be(new Vector3(0, 6, 2));
            voxFile.Models.First(m => m.Name == "y1").Voxels.First(v => v.Color == Color.Red).LocalPosition.Should().Be(new Vector3(0, 0, 0));

            voxFile.Models.First(m => m.Name == "y2").Voxels.First(v => v.Color == Color.Red).GlobalPosition.Should().Be(new Vector3(2, 12, 2));
            voxFile.Models.First(m => m.Name == "y2").Voxels.First(v => v.Color == Color.Red).LocalPosition.Should().Be(new Vector3(0, 0, 0));

            voxFile.Models.First(m => m.Name == "y3").Voxels.First(v => v.Color == Color.Red).GlobalPosition.Should().Be(new Vector3(2, 18, 0));
            voxFile.Models.First(m => m.Name == "y3").Voxels.First(v => v.Color == Color.Red).LocalPosition.Should().Be(new Vector3(0, 0, 0));

            voxFile.Models.First(m => m.Name == "y4").Voxels.First(v => v.Color == Color.Red).GlobalPosition.Should().Be(new Vector3(0, 24, 0));
            voxFile.Models.First(m => m.Name == "y4").Voxels.First(v => v.Color == Color.Red).LocalPosition.Should().Be(new Vector3(0, 0, 0));

            // Rotate z 90°
            voxFile.Models.First(m => m.Name == "z1").Voxels.First(v => v.Color == Color.Red).GlobalPosition.Should().Be(new Vector3(2, 0, 6));
            voxFile.Models.First(m => m.Name == "z1").Voxels.First(v => v.Color == Color.Red).LocalPosition.Should().Be(new Vector3(0, 0, 0));

            voxFile.Models.First(m => m.Name == "z2").Voxels.First(v => v.Color == Color.Red).GlobalPosition.Should().Be(new Vector3(2, 2, 12));
            voxFile.Models.First(m => m.Name == "z2").Voxels.First(v => v.Color == Color.Red).LocalPosition.Should().Be(new Vector3(0, 0, 0));

            voxFile.Models.First(m => m.Name == "z3").Voxels.First(v => v.Color == Color.Red).GlobalPosition.Should().Be(new Vector3(0, 2, 18));
            voxFile.Models.First(m => m.Name == "z3").Voxels.First(v => v.Color == Color.Red).LocalPosition.Should().Be(new Vector3(0, 0, 0));

            voxFile.Models.First(m => m.Name == "z4").Voxels.First(v => v.Color == Color.Red).GlobalPosition.Should().Be(new Vector3(0, 0, 24));
            voxFile.Models.First(m => m.Name == "z4").Voxels.First(v => v.Color == Color.Red).LocalPosition.Should().Be(new Vector3(0, 0, 0));
        }

        [Fact]
        public void TestRot2()
        {
            string file = Zip.UnzipFilesFromSevenZipArchive(RotationTestFile3).First();
            IVoxFile voxFile = VoxReader.Read(file);

            voxFile.Models.First(m => m.Name == "default").Voxels.First(v => v.Color == Color.Red).GlobalPosition.Should().Be(new Vector3(1, 0, 4));
            voxFile.Models.First(m => m.Name == "default").Voxels.First(v => v.Color == Color.Red).LocalPosition.Should().Be(new Vector3(1, 0, 4));

            voxFile.Models.First(m => m.Name == "random").Voxels.First(v => v.Color == Color.Red).GlobalPosition.Should().Be(new Vector3(-4, -5, 0));
            voxFile.Models.First(m => m.Name == "random").Voxels.First(v => v.Color == Color.Red).LocalPosition.Should().Be(new Vector3(1, 0, 4));

            // Rotate x 90°
            voxFile.Models.First(m => m.Name == "x1").Voxels.First(v => v.Color == Color.Red).GlobalPosition.Should().Be(new Vector3(7, 0, 0));
            voxFile.Models.First(m => m.Name == "x1").Voxels.First(v => v.Color == Color.Red).LocalPosition.Should().Be(new Vector3(1, 0, 4));

            voxFile.Models.First(m => m.Name == "x2").Voxels.First(v => v.Color == Color.Red).GlobalPosition.Should().Be(new Vector3(13, 3, 0));
            voxFile.Models.First(m => m.Name == "x2").Voxels.First(v => v.Color == Color.Red).LocalPosition.Should().Be(new Vector3(1, 0, 4));

            voxFile.Models.First(m => m.Name == "x3").Voxels.First(v => v.Color == Color.Red).GlobalPosition.Should().Be(new Vector3(19, 4, 3));
            voxFile.Models.First(m => m.Name == "x3").Voxels.First(v => v.Color == Color.Red).LocalPosition.Should().Be(new Vector3(1, 0, 4));

            voxFile.Models.First(m => m.Name == "x4").Voxels.First(v => v.Color == Color.Red).GlobalPosition.Should().Be(new Vector3(25, 0, 4));
            voxFile.Models.First(m => m.Name == "x4").Voxels.First(v => v.Color == Color.Red).LocalPosition.Should().Be(new Vector3(1, 0, 4));

            // Rotate y 90°
            voxFile.Models.First(m => m.Name == "y1").Voxels.First(v => v.Color == Color.Red).GlobalPosition.Should().Be(new Vector3(3, 7, 2));
            voxFile.Models.First(m => m.Name == "y1").Voxels.First(v => v.Color == Color.Red).LocalPosition.Should().Be(new Vector3(1, 0, 4));

            voxFile.Models.First(m => m.Name == "y2").Voxels.First(v => v.Color == Color.Red).GlobalPosition.Should().Be(new Vector3(1, 14, 0));
            voxFile.Models.First(m => m.Name == "y2").Voxels.First(v => v.Color == Color.Red).LocalPosition.Should().Be(new Vector3(1, 0, 4));

            voxFile.Models.First(m => m.Name == "y3").Voxels.First(v => v.Color == Color.Red).GlobalPosition.Should().Be(new Vector3(-1, 21, 2));
            voxFile.Models.First(m => m.Name == "y3").Voxels.First(v => v.Color == Color.Red).LocalPosition.Should().Be(new Vector3(1, 0, 4));

            voxFile.Models.First(m => m.Name == "y4").Voxels.First(v => v.Color == Color.Red).GlobalPosition.Should().Be(new Vector3(1, 28, 4));
            voxFile.Models.First(m => m.Name == "y4").Voxels.First(v => v.Color == Color.Red).LocalPosition.Should().Be(new Vector3(1, 0, 4));

            // Rotate z 90°
            voxFile.Models.First(m => m.Name == "z1").Voxels.First(v => v.Color == Color.Red).GlobalPosition.Should().Be(new Vector3(2, 2, 12));
            voxFile.Models.First(m => m.Name == "z1").Voxels.First(v => v.Color == Color.Red).LocalPosition.Should().Be(new Vector3(1, 0, 4));

            voxFile.Models.First(m => m.Name == "z2").Voxels.First(v => v.Color == Color.Red).GlobalPosition.Should().Be(new Vector3(1, 3, 20));
            voxFile.Models.First(m => m.Name == "z2").Voxels.First(v => v.Color == Color.Red).LocalPosition.Should().Be(new Vector3(1, 0, 4));

            voxFile.Models.First(m => m.Name == "z3").Voxels.First(v => v.Color == Color.Red).GlobalPosition.Should().Be(new Vector3(-1, 2, 28));
            voxFile.Models.First(m => m.Name == "z3").Voxels.First(v => v.Color == Color.Red).LocalPosition.Should().Be(new Vector3(1, 0, 4));

            voxFile.Models.First(m => m.Name == "z4").Voxels.First(v => v.Color == Color.Red).GlobalPosition.Should().Be(new Vector3(1, 0, 36));
            voxFile.Models.First(m => m.Name == "z4").Voxels.First(v => v.Color == Color.Red).LocalPosition.Should().Be(new Vector3(1, 0, 4));
        }

        [Fact]
        public void TestRot3()
        {
            string file = Zip.UnzipFilesFromSevenZipArchive(RotationTestFile4).First();
            IVoxFile voxFile = VoxReader.Read(file);

            voxFile.Models.First(m => m.Name == "default").Voxels.First(v => v.Color == Color.Red).GlobalPosition.Should().Be(new Vector3(1, 0, 4));
            voxFile.Models.First(m => m.Name == "default").Voxels.First(v => v.Color == Color.Red).LocalPosition.Should().Be(new Vector3(1, 0, 4));

            voxFile.Models.First(m => m.Name == "m1").Voxels.First(v => v.Color == Color.Red).GlobalPosition.Should().Be(new Vector3(1, 6, 2));
            voxFile.Models.First(m => m.Name == "m1").Voxels.First(v => v.Color == Color.Red).LocalPosition.Should().Be(new Vector3(1, 0, 4));

            voxFile.Models.First(m => m.Name == "m2").Voxels.First(v => v.Color == Color.Red).GlobalPosition.Should().Be(new Vector3(5, 11, 4));
            voxFile.Models.First(m => m.Name == "m2").Voxels.First(v => v.Color == Color.Red).LocalPosition.Should().Be(new Vector3(1, 0, 4));
        }

        [Fact]
        public void TestApplyingRotationVoxA()
        {
            string file = Zip.UnzipFilesFromSevenZipArchive(RotationTestFile).First();
            IVoxFile voxFile = VoxReader.Read(file);

            var voxa = voxFile.Models.First(m => m.Name == "voxa");
            voxa.LocalRotation.Should().Be(new Matrix3(0b0010_0001));
            voxa.GlobalRotation.Should().Be(Matrix3.Identity);
            voxa.LocalPosition.Should().Be(new Vector3(-4, 0, 0));
            voxa.GlobalPosition.Should().Be(new Vector3(4, -4, 2));
            voxa.LocalSize.Should().Be(new Vector3(5, 4, 4));
            voxa.GlobalSize.Should().Be(new Vector3(5, 4, 4));

            Vector3 min = voxa.GlobalPosition - voxa.GlobalSize / 2;
            Vector3 max = min + voxa.GlobalSize;
            // global position should all be in global bounds
            foreach (var v in voxa.Voxels)
            {
                var k = v.GlobalPosition;
                k.Should().Match<Vector3>(k =>
                    k.X >= min.X && k.X < max.X &&
                    k.Y >= min.Y && k.Y < max.Y &&
                    k.Z >= min.Z && k.Z < max.Z);
            }

            voxa.Voxels.First(voxel => voxel.LocalPosition == new Vector3(0, 0, 0)).ColorIndex.Should().Be(215);
            voxa.Voxels.First(voxel => voxel.GlobalPosition == new Vector3(2, -6, 0)).ColorIndex.Should().Be(215);
        }

        [Fact]
        public void TestApplyingRotationVoxB()
        {
            string file = Zip.UnzipFilesFromSevenZipArchive(RotationTestFile).First();
            IVoxFile voxFile = VoxReader.Read(file);

            var voxb = voxFile.Models.First(m => m.Name == "voxb");
            voxb.LocalRotation.Should().Be(Matrix3.Identity);
            voxb.GlobalRotation.Should().Be(new Matrix3(0b0001_0001));
            voxb.LocalPosition.Should().Be(new Vector3(5, 0, 0));
            voxb.GlobalPosition.Should().Be(new Vector3(4, 5, 2));
            voxb.LocalSize.Should().Be(new Vector3(4, 4, 4));
            voxb.GlobalSize.Should().Be(new Vector3(4, 4, 4));

            Vector3 min = voxb.GlobalPosition - voxb.GlobalSize / 2;
            Vector3 max = min + voxb.GlobalSize;
            // global position should all be in global bounds
            foreach (var v in voxb.Voxels)
            {
                var k = v.GlobalPosition;
                k.Should().Match<Vector3>(k =>
                    k.X >= min.X && k.X < max.X &&
                    k.Y >= min.Y && k.Y < max.Y &&
                    k.Z >= min.Z && k.Z < max.Z);
            }

            voxb.Voxels.First(voxel => voxel.LocalPosition == new Vector3(0, 0, 0)).ColorIndex.Should().Be(215);
            voxb.Voxels.First(voxel => voxel.GlobalPosition == new Vector3(5, 3, 0)).ColorIndex.Should().Be(215);
        }
    }
}