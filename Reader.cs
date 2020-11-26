using System;
using System.Collections.Generic;
using VoxReader.Chunks;
using VoxReader.Exceptions;
using VoxReader.Extensions;

namespace VoxReader
{
    //Reference: https://github.com/ephtracy/voxel-model/blob/master/MagicaVoxel-file-format-vox.txt

    public static class Reader
    {
        /// <summary>
        /// Returns an array of all chunks that are children of the MAIN chunk
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Chunk[] GetChunks(byte[] data)
        {
            List<Chunk> chunks = new List<Chunk>();

            // Get MAIN chunk length
            int mainChunkContentSize = BitConverter.ToInt32(data, 12);
            int mainChunkChildrenSize = BitConverter.ToInt32(data, 16);

            byte[] mainChunkChildren = data.GetRange(20 + mainChunkContentSize, mainChunkChildrenSize);

            for (int i = 0; i < mainChunkChildren.Length; i++)
            {
                int chunkContentSize = BitConverter.ToInt32(mainChunkChildren, i + 4);
                int chunkChildrenSize = BitConverter.ToInt32(mainChunkChildren, i + 8);

                int chunkSize = 12 + chunkContentSize + chunkChildrenSize;
                byte[] chunkData = mainChunkChildren.GetRange(i, chunkSize);

                string id = new string(Helper.GetCharArray(chunkData, 0, 4));

                Chunk chunk = null;

                switch (id)
                {
                    case nameof(ChunkType.PACK):
                        throw new UnsupportedDataException("A file with more than one model is not supported! (MAIN chunk contains a PACK chunk)");

                    case nameof(ChunkType.SIZE):
                        chunk = new SizeChunk(chunkData);
                        break;

                    case nameof(ChunkType.XYZI):
                        chunk = new VoxelChunk(chunkData);
                        break;

                    case nameof(ChunkType.RGBA):
                        chunk = new PaletteChunk(chunkData);
                        break;
                }

                if (chunk != null)
                {
                    chunks.Add(chunk);
                }

                i += chunkSize - 1;
            }

            return chunks.ToArray();
        }
    }

    public enum ChunkType
    {
        PACK,
        SIZE,
        XYZI,
        RGBA
    }
}