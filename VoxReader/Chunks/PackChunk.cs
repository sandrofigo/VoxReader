using System;
using VoxReader.Interfaces;

namespace VoxReader.Chunks
{
    internal class PackChunk : Chunk, IPackChunk
    {
        public int ModelCount { get; }

        public PackChunk(byte[] data) : base(data)
        {
            ModelCount = BitConverter.ToInt32(Content, 0);
        }
    }
}