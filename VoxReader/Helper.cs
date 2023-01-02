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

            var shapeNodeChunks = mainChunk.GetChildren<IShapeNodeChunk>();
            var transformNodeChunks = mainChunk.GetChildren<ITransformNodeChunk>();

            var transformNodesThatHaveAShapeNode = new Dictionary<ITransformNodeChunk, IShapeNodeChunk>();
            foreach (ITransformNodeChunk transformNodeChunk in transformNodeChunks)
            {
                foreach (IShapeNodeChunk shapeNodeChunk in shapeNodeChunks)
                {
                    if (transformNodeChunk.ChildNodeId != shapeNodeChunk.NodeId)
                        continue;

                    transformNodesThatHaveAShapeNode.Add(transformNodeChunk, shapeNodeChunk);
                    break;
                }
            }

            var processedModelIds = new HashSet<int>();

            foreach (var keyValuePair in transformNodesThatHaveAShapeNode)
            {
                int[] ids = keyValuePair.Value.Models;

                foreach (int id in ids)
                {
                    string name = keyValuePair.Key.Name;
                    Vector3 position = keyValuePair.Key.Frames[0].Translation;
                    Vector3 size = sizeChunks[id].Size;
                    var voxels = voxelChunks[id].Voxels.Select(voxel => new Voxel(voxel.Position, palette.Colors[voxel.ColorIndex - 1])).ToArray();

                    // Create new model
                    var model = new Model(id, name, position, size, voxels, !processedModelIds.Add(id));
                    yield return model;
                }
            }
        }
    }
}