using System;
using VoxReader.Interfaces;

namespace VoxReader.Chunks
{
    internal class VoxelChunk : Chunk, IVoxelChunk
    {
        public RawVoxel[] Voxels { get; }

        public VoxelChunk(byte[] data) : base(data)
        {
            int voxelCount = BitConverter.ToInt32(data, 12);
            
            Voxels = new RawVoxel[voxelCount];

            for (int i = 0; i < voxelCount; i++)
            {
                var position = new Vector3(data[16 + i * 4], data[17 + i * 4], data[18 + i * 4]);
                int colorIndex = data[19 + i * 4];
                Voxels[i] = new RawVoxel(position, colorIndex);
            }
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Voxel Count: {Voxels.Length}";
        }
    }
}