﻿using FluentAssertions;
using Xunit;

namespace VoxReader.UnitTests;

public class Matrix3Tests
{
    [Theory]
    [InlineData(12, 3, -23)]
    [InlineData(-1, 0, 8)]
    [InlineData(1, 1, 1)]
    [InlineData(0, 0, 0)]
    public void Matrix3_MultiplyIdentityWithVector_VectorShouldNotChange(int x, int y, int z)
    {
        var v = new Vector3(x, y, z);
        Vector3 result = Matrix3.Identity * v;
        result.Should().Be(v);
    }

    [Fact]
    public void Matrix3_MultiplyMatrixWithOtherMatrix_ResultShouldBeCorrect()
    {
        int[,] dataA = new[,]
        {
            { 2, 5, -7 },
            { -3, -5, 6 },
            { 0, 3, 2 }
        };
        var a = new Matrix3(dataA);

        int[,] dataB = new[,]
        {
            { 5, 5, 5 },
            { 6, -2, -3 },
            { 2, -3, -2 }
        };
        var b = new Matrix3(dataB);

        int[,] dataResult = new[,]
        {
            { 26, 21, 9 },
            { -33, -23, -12 },
            { 22, -12, -13 }
        };
        var r = new Matrix3(dataResult);

        (a * b).Should().Be(r);
    }

    [Fact]
    public void Matrix3_MuiltiplyMatrixWithVector_ResultShouldBeCorrect()
    {
        int[,] data = new[,]
        {
            { 6, 2, 4 },
            { -1, 4, 3 },
            { -2, 9, 3 }
        };
        var a = new Matrix3(data);

        var b = new Vector3(4, -2, 1);

        var result = new Vector3(24, -9, -23);

        (a * b).Should().Be(result);
    }
}