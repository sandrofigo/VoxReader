namespace VoxReader
{
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