/* Reference:
https://github.com/ephtracy/voxel-model/blob/master/MagicaVoxel-file-format-vox.txt
https://github.com/ephtracy/voxel-model/blob/master/MagicaVoxel-file-format-vox-extension.txt */

using System.IO;
using VoxReader.Interfaces;

namespace VoxReader
{
    public class FileReader
    {
        public static IVoxFile ReadVoxFile(string filePath)
        {
            var data = File.ReadAllBytes(filePath);

            var voxFile = new VoxFile(data);

            return voxFile;
        }
    }
}