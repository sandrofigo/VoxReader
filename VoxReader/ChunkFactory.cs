using VoxReader.Chunks;
using VoxReader.Interfaces;

namespace VoxReader
{
    internal static class ChunkFactory
    {
        public static IChunk Parse(byte[] chunkData)
        {
            string id = Chunk.GetChunkId(chunkData);

            switch (id)
            {
                case "MAIN":
                    return new Chunk(chunkData);
                case "SIZE":
                    return new SizeChunk(chunkData);
                case "XYZI":
                    return new VoxelChunk(chunkData);
                case "RGBA":
                    return new PaletteChunk(chunkData);
                case "PACK":
                    return new PackChunk(chunkData);
                default:
                    return new Chunk(chunkData);
            }
        }
    }
}