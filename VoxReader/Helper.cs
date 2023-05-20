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
            var groupNodeChunks = mainChunk.GetChildren<IGroupNodeChunk>();

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

            // Create inverse index map for mapped color index on voxel
            var indexMapChunk = mainChunk.GetChild<IIndexMapChunk>();
            var inverseIndexMap = new Dictionary<int, int>();
            for (int i = 0; i < palette.RawColors.Length - 1; i++)
            {
                inverseIndexMap.Add(GetMappedColorIndex(indexMapChunk, i), i);
            }

            if (mainChunk.Children.Length == 3) // If a vox file was exported as .vox instead of saved as a .vox project it only contains a size, voxel and palette chunk
            {
                Vector3 size = sizeChunks[0].Size;
                var voxels = voxelChunks[0].Voxels.Select(voxel => new Voxel(voxel.Position, voxel.Position, palette.RawColors[voxel.ColorIndex - 1], inverseIndexMap[voxel.ColorIndex - 1])).ToArray();
                yield return new Model(0, null, new Vector3(), size, voxels, false);
                yield break;
            }

            var processedModelIds = new HashSet<int>();

            foreach (var keyValuePair in transformNodesThatHaveAShapeNode)
            {
                ITransformNodeChunk transformNodeChunk = keyValuePair.Key;
                IShapeNodeChunk shapeNodeChunk = keyValuePair.Value;

                int[] ids = shapeNodeChunk.Models;

                foreach (int id in ids)
                {
                    string name = transformNodeChunk.Name;
                    Vector3 size = sizeChunks[id].Size;
                    Vector3 translation = GetGlobalTranslation(transformNodeChunk);

                    var voxels = voxelChunks[id].Voxels.Select(voxel => new Voxel(voxel.Position, translation + voxel.Position - size / 2, palette.RawColors[voxel.ColorIndex - 1], inverseIndexMap[voxel.ColorIndex - 1])).ToArray();

                    // Create new model
                    var model = new Model(id, name, translation, size, voxels, !processedModelIds.Add(id));
                    yield return model;
                }
            }

            Vector3 GetGlobalTranslation(ITransformNodeChunk target)
            {
                Vector3 position = target.Frames[0].Translation;

                while (TryGetParentTransformNodeChunk(target, out ITransformNodeChunk parent))
                {
                    position += parent.Frames[0].Translation;

                    target = parent;
                }

                return position;
            }

            bool TryGetParentTransformNodeChunk(ITransformNodeChunk target, out ITransformNodeChunk parent)
            {
                //TODO: performance here is questionable; might need an additional scene structure to query the parent efficiently
                foreach (IGroupNodeChunk groupNodeChunk in groupNodeChunks)
                {
                    foreach (int parentGroupNodeChunkChildId in groupNodeChunk.ChildrenNodes)
                    {
                        if (parentGroupNodeChunkChildId != target.NodeId)
                            continue;

                        foreach (ITransformNodeChunk transformNodeChunk in transformNodeChunks)
                        {
                            if (transformNodeChunk.ChildNodeId != groupNodeChunk.NodeId)
                                continue;

                            parent = transformNodeChunk;
                            return true;
                        }
                    }
                }

                parent = null;
                return false;
            }
        }

        internal static int GetMappedColorIndex(IIndexMapChunk indexMapChunk, int rawIndex)
        {
            if (indexMapChunk == null)
                return rawIndex;

            return indexMapChunk.ColorIndices[rawIndex] - 1;
        }
    }
}