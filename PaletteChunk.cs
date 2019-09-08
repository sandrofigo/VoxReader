namespace VoxReader
{
    public class PaletteChunk : Chunk
    {
        public RGBA[] Colors { get; }

        public PaletteChunk(byte[] data) : base(data)
        {
            Colors = new RGBA[256];

            for (int i = 0; i < Colors.Length; i++)
            {
                Colors[i] = new RGBA(
                    data[12 + (i * 4)],
                    data[13 + (i * 4)],
                    data[14 + (i * 4)],
                    data[15 + (i * 4)]);
            }
        }
    }

    public class RGBA
    {
        public byte R { get; }
        public byte G { get; }
        public byte B { get; }
        public byte A { get; }

        public RGBA(byte r, byte g, byte b, byte a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }
        
        public override string ToString()
        {
            return $"[RGBA: {R} {G} {B} {A}]";
        }
    }
}