using System;
using VoxReader.Interfaces;

namespace VoxReader.Chunks
{
    internal class SizeChunk : Chunk, ISizeChunk
    {
        public Vector3 Size { get; }

        public SizeChunk(byte[] data) : base(data)
        {
            int x = BitConverter.ToInt32(Content, 0);
            int y = BitConverter.ToInt32(Content, 4);
            int z = BitConverter.ToInt32(Content, 8);

            Size = new Vector3(x, y, z);
        }

        public override string ToString()
        {
            return $"{base.ToString()}, {Size}";
        }
    }
}