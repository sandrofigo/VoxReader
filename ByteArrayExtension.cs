using System;

namespace VoxReader
{
    public static class ByteArrayExtension
    {
        public static byte[] GetRange(this byte[] data, int startIndex, int length)
        {
            byte[] output = new byte[length];
            Buffer.BlockCopy(data, startIndex, output, 0, length);
            return output;
        }
    }
}