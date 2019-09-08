using System;

namespace VoxReader
{
    public class VoxelChunk : Chunk
    {
        public Voxel[] Voxels { get; }

        public VoxelChunk(byte[] data) : base(data)
        {
            Voxels = new Voxel[BitConverter.ToInt32(data, 12)];

            for (int i = 0; i < Voxels.Length; i++)
            {
                Voxels[i] = new Voxel(
                    data[16 + (i * 4)],
                    data[17 + (i * 4)],
                    data[18 + (i * 4)],
                    data[19 + (i * 4)]);
            }
        }

        public override string ToString()
        {
            return $"{base.ToString()} [VoxelCount: {Voxels.Length}]";
        }
    }

    public class Voxel
    {
        public int X { get; }
        public int Y { get; }
        public int Z { get; }
        public int ColorIndex { get; }

        public Voxel(int x, int y, int z, int colorIndex)
        {
            X = x;
            Y = y;
            Z = z;
            ColorIndex = colorIndex;
        }

        public override string ToString()
        {
            return $"[X: {X}, Y: {Y}, Z: {Z}, ColorIndex: {ColorIndex}]";
        }
    }
}