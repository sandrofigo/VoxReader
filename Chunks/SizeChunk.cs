using System;

namespace VoxReader.Chunks
{
    public class SizeChunk : Chunk
    {
        public int X { get; }
        public int Y { get; }
        public int Z { get; }

        public SizeChunk(byte[] data) : base(data)
        {
            X = BitConverter.ToInt32(data, 12);
            Y = BitConverter.ToInt32(data, 16);
            Z = BitConverter.ToInt32(data, 20);
        }

        public override string ToString()
        {
            return $"{base.ToString()} X: {X} Y: {Y} Z: {Z}";
        }
    }
}