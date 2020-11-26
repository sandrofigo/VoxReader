/* Reference:
https://github.com/ephtracy/voxel-model/blob/master/MagicaVoxel-file-format-vox.txt
https://github.com/ephtracy/voxel-model/blob/master/MagicaVoxel-file-format-vox-extension.txt */

using System;
using System.IO;
using System.Linq;
using VoxReader.Extensions;
using VoxReader.Interfaces;

namespace VoxReader
{
    public static class FileReader
    {
        public static IVoxFile ReadVoxFile(string filePath)
        {
            var data = File.ReadAllBytes(filePath);

            int versionNumber = BitConverter.ToInt32(data, 4);

            IChunk mainChunk = ChunkFactory.Parse(data.GetRange(8));

            var palette = new Palette(mainChunk.GetChild<IPaletteChunk>().Colors);

            var models = Helper.ExtractModels(mainChunk, palette).ToArray();

            return new VoxFile(versionNumber, models, palette);
        }
    }
}