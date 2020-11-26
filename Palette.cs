using System.Text;
using VoxReader.Interfaces;

namespace VoxReader
{
    public class Palette : IPalette
    {
        public Color[] Colors { get; }

        public Palette(Color[] colors)
        {
            Colors = colors;
        }

        public override string ToString()
        {
            var output = new StringBuilder();
            for (int i = 0; i < Colors.Length; i++)
            {
                output.AppendLine($"{i}: [{Colors[i]}]");
            }

            return output.ToString();
        }
    }
}