using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace VoxReader.UnitTests
{
    public class Matrix3Tests
    {
        [Fact]
        public void TestIdentity()
        {
            Vector3 v = new Vector3(12, 3, -23);
            (Matrix3.Identity * v).Should().Be(v);
        }

        [Fact]
        public void TestMatrixMultMatrix()
        {
            var a = new Matrix3();
            var b = new Matrix3();

            a[0, 0] = 2;
            a[0, 1] = 5;
            a[0, 2] = -7;
            a[1, 0] = -3;
            a[1, 1] = -5;
            a[1, 2] = 6;
            a[2, 0] = 0;
            a[2, 1] = 3;
            a[2, 2] = 2;

            b[0, 0] = 5;
            b[0, 1] = 5;
            b[0, 2] = 5;
            b[1, 0] = 6;
            b[1, 1] = -2;
            b[1, 2] = -3;
            b[2, 0] = 2;
            b[2, 1] = -3;
            b[2, 2] = -2;

            var r = new Matrix3();
            r[0, 0] = 26;
            r[0, 1] = 21;
            r[0, 2] = 9;
            r[1, 0] = -33;
            r[1, 1] = -23;
            r[1, 2] = -12;
            r[2, 0] = 22;
            r[2, 1] = -12;
            r[2, 2] = -13;


            (a * b).Should().Be(r);
        }

        [Fact]
        public void TestMatrixMultVector()
        {
            var a = new Matrix3();
            var b = new Vector3(4, -2, 1);

            a[0, 0] = 6;
            a[0, 1] = 2;
            a[0, 2] = 4;
            a[1, 0] = -1;
            a[1, 1] = 4;
            a[1, 2] = 3;
            a[2, 0] = -2;
            a[2, 1] = 9;
            a[2, 2] = 3;

            var r = new Vector3(24, -9, -23);

            (a * b).Should().Be(r);
        }
    }
}
