using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("VoxReader.UnitTests")]

namespace VoxReader
{
    public readonly struct Color
    {
        /// <summary>
        /// The red component of the color.
        /// </summary>
        public byte R { get; }

        /// <summary>
        /// The green component of the color.
        /// </summary>
        public byte G { get; }

        /// <summary>
        /// The blue component of the color.
        /// </summary>
        public byte B { get; }

        /// <summary>
        /// The alpha component of the color.
        /// </summary>
        public byte A { get; }

        internal Color(byte r, byte g, byte b, byte a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public override string ToString()
        {
            return $"R: {R} G: {G} B: {B} A: {A}";
        }
    }
}