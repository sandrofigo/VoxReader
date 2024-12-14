using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxReader.Interfaces;
using Xunit;

namespace VoxReader.UnitTests
{
    public class RotationTests
    {
        private const string RotationTestFile = "data/rotation.zip";

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
