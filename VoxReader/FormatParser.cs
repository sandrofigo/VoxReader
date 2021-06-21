using System;
using System.Collections.Generic;
using System.Linq;
using VoxReader.Chunks;
using VoxReader.Extensions;
using VoxReader.Interfaces;

namespace VoxReader
{
    internal class FormatParser
    {
        private byte[] data;

        /// <summary>
        /// The current offset in bytes.
        /// </summary>
        private int currentOffset;

        public int CurrentOffset => currentOffset;

        public FormatParser(byte[] data)
        {
            this.data = data;
        }


        public byte ParseByte()
        {
            byte parsed = data[currentOffset];

            currentOffset++;

            return parsed;
        }

        public byte[] ParseBytes(int count)
        {
            byte[] parsed = data.GetRange(currentOffset, count);

            currentOffset += count;

            return parsed;
        }

        public int ParseIntFromByte()
        {
            int parsed = Convert.ToInt32(ParseByte());

            return parsed;
        }
        
        public int ParseInt32()
        {
            int parsed = BitConverter.ToInt32(data, currentOffset);

            currentOffset += 4;

            return parsed;
        }

        public string ParseString(int length)
        {
            string parsed = new string(Helper.GetCharArray(data, currentOffset, length));

            currentOffset += length;

            return parsed;
        }

        public IChunk[] ParseChunks(int count)
        {
            var parsed = GetChunks(data.GetRange(currentOffset, count));

            currentOffset += parsed.Sum(chunk => chunk.TotalBytes);

            return parsed;
        }

        private static IChunk[] GetChunks(byte[] data)
        {
            var children = new List<IChunk>();

            int currentChunkOffset = 0;

            while (currentChunkOffset < data.Length)
            {
                IChunk childChunk = ChunkFactory.Parse(data.GetRange(currentChunkOffset));
                children.Add(childChunk);
                currentChunkOffset += childChunk.TotalBytes;
            }

            return children.ToArray();
        }

        public Vector3 ParseVector3()
        {
            int x = ParseInt32();
            int y = ParseInt32();
            int z = ParseInt32();

            return new Vector3(x, y, z);
        }

        public RawVoxel ParseRawVoxel()
        {
            int x = ParseIntFromByte();
            int y = ParseIntFromByte();
            int z = ParseIntFromByte();

            var position = new Vector3(x, y, z);

            int colorIndex = ParseIntFromByte();

            return new RawVoxel(position, colorIndex);
        }

        public RawVoxel[] ParseRawVoxels(int count)
        {
            var voxels = new RawVoxel[count];

            for (int i = 0; i < count; i++)
            {
                voxels[i] = ParseRawVoxel();
            }

            return voxels;
        }

        public Color ParseColor()
        {
            byte r = ParseByte();
            byte g = ParseByte();
            byte b = ParseByte();
            byte a = ParseByte();

            return new Color(r, g, b, a);
        }

        public Color[] ParseColors(int count)
        {
            var colors = new Color[count];

            for (int i = 0; i < count; i++)
            {
                colors[i] = ParseColor();
            }

            return colors;
        }
    }
}