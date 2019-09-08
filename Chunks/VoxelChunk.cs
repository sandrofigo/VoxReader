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
}