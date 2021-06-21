using System.Collections.Generic;
using System.Linq;
using VoxReader.Exceptions;
using VoxReader.Interfaces;

namespace VoxReader
{
    internal static class Helper
    {
        internal static char[] GetCharArray(byte[] data, int startIndex, int length)
        {
            var array = new char[length];

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = (char)data[i + startIndex];
            }

            return array;
        }

        public static IEnumerable<IModel> ExtractModels(IChunk mainChunk, IPalette palette)
        {
            var sizeChunks = mainChunk.GetChildren<ISizeChunk>();
            var voxelChunks = mainChunk.GetChildren<IVoxelChunk>();

            if (sizeChunks.Length != voxelChunks.Length)
                throw new InvalidDataException("Can not extract models, because the number of SIZE chunks does not match the number of XYZI chunks!");

            for (int i = 0; i < sizeChunks.Length; i++)
            {
                Vector3 size = sizeChunks[i].Size;
                var voxels = voxelChunks[i].Voxels.Select(voxel => new Voxel(voxel.Position, palette.Colors[voxel.ColorIndex - 1])).ToArray();
                
                yield return new Model(size, voxels);
            }
        }
    }
}