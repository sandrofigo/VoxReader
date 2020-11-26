using System;
using VoxReader.Chunks;
using VoxReader.Extensions;
using VoxReader.Interfaces;

namespace VoxReader
{
    public class VoxFile : IVoxFile
    {
        public int VersionNumber { get; }
        public IChunk MainChunk { get; }

        public VoxFile(byte[] allBytes)
        {
            VersionNumber = BitConverter.ToInt32(allBytes, 4);
            
            MainChunk = new Chunk(allBytes.GetRange(8));
        }
    }
}