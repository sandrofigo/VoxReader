using System;
using System.Collections.Generic;
using System.Text;
using VoxReader.Interfaces;

namespace VoxReader
{
    public class Palette : IPalette
    {
        public Color[] RawColors { get; }
        public Color[] Colors { get; }
        public string[] Notes { get; }

        public Color[] GetColorsByNote(string note)
        {
            var noteIndices = new List<int>();

            for (int i = 0; i < Notes.Length; i++)
            {
                if (Notes[i] == note)
                {
                    noteIndices.Add(i);
                }
            }

            if (noteIndices.Count == 0)
                return Array.Empty<Color>();

            var colors = new List<Color>();

            foreach (int noteIndex in noteIndices)
            {
                for (int i = 0; i < 8; i++)
                {
                    int colorIndex = Math.Abs(noteIndex - 31) * 8 + i;
                    
                    if(colorIndex >= Colors.Length)
                        continue;
                    
                    colors.Add(Colors[colorIndex]);
                }
            }

            return colors.ToArray();
        }

        public Palette(Color[] rawColors, Color[] colors, string[] notes)
        {
            RawColors = rawColors;
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