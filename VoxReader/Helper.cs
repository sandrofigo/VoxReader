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
            var shapeNodeChunks = mainChunk.GetChildren<IShapeNodeChunk>();

            if (sizeChunks.Length != voxelChunks.Length)
                throw new InvalidDataException("Can not extract models, because the number of SIZE chunks does not match the number of XYZI chunks!");

            var shapeNodeChunksQueue = new Queue<IShapeNodeChunk>(shapeNodeChunks);

            var processedModels = new Dictionary<int, Model>();

            int duplicateModelCount = 0;
            
            for (int i = 0; i < shapeNodeChunks.Length; i++)
            {
                Vector3 size = sizeChunks[i - duplicateModelCount].Size;
                var voxels = voxelChunks[i - duplicateModelCount].Voxels.Select(voxel => new Voxel(voxel.Position, palette.Colors[voxel.ColorIndex - 1])).ToArray();

                int id = shapeNodeChunksQueue.Dequeue().Models[0];

                if (processedModels.ContainsKey(id))
                {
                    // Create copy of already existing model
                    duplicateModelCount++;
                    yield return processedModels[id].GetCopy();
                }
                else
                {
                    // Create new model
                    var model = new Model(id, size, voxels, false);
                    processedModels.Add(id, model);
                    yield return model;
                }
            }
        }
    }
}