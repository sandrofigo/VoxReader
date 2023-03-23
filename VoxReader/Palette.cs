using System.Text;
using VoxReader.Interfaces;

namespace VoxReader
{
    public class Palette : IPalette
    {
        public Color[] Colors { get; }
        public string[] Notes { get; }
        public Color[] GetColorsByNote(string note)
        {
            throw new System.NotImplementedException();
        }

        public Palette(Color[] colors, string[] notes)
        {
            Colors = colors;
            Notes = notes;
        }

        public override string ToString()
        {
            var output = new StringBuilder();
            
            for (int i = 0; i < Colors.Length - 1; i++)
            {
                output.AppendLine(GetText(i));
            }
            output.Append(GetText(Colors.Length - 1));

            string GetText(int index) => $"{index}: [{Colors[index]}]";

            return output.ToString();
        }
    }
}