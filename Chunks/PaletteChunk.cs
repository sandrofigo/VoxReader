using VoxReader.Interfaces;

namespace VoxReader.Chunks
{
    internal class PaletteChunk : Chunk, IPaletteChunk
    {
        public Color[] Colors { get; }

        public PaletteChunk(byte[] data) : base(data)
        {
            Colors = new Color[256];

            for (int i = 0; i < Colors.Length; i++)
            {
                Colors[i] = new Color(
                    data[12 + i * 4],
                    data[13 + i * 4],
                    data[14 + i * 4],
                    data[15 + i * 4]);
            }
        }
    }
}