using System.Collections.Generic;

namespace VoxReader
{
    internal static class ChunkTypeMapping
    {
        private static readonly Dictionary<string, ChunkType> _mappings = new();

        public static ChunkType GetChunkId(string chunkType) => _mappings[chunkType];
        
        static ChunkTypeMapping()
        {
            _mappings.Add("MAIN", ChunkType.Main);
            _mappings.Add("PACK", ChunkType.Pack);
            _mappings.Add("SIZE", ChunkType.Size);
            _mappings.Add("XYZI", ChunkType.Voxel);
            _mappings.Add("RGBA", ChunkType.Palette);
            _mappings.Add("MATT", ChunkType.MaterialOld);
            _mappings.Add("MATL", ChunkType.MaterialNew);
            _mappings.Add("nTRN", ChunkType.TransformNode);
            _mappings.Add("nGRP", ChunkType.GroupNode);
            _mappings.Add("nSHP", ChunkType.ShapeNode);
            _mappings.Add("LAYR", ChunkType.Layer);
            _mappings.Add("rOBJ", ChunkType.Object);
            _mappings.Add("rCAM", ChunkType.Camera);
            _mappings.Add("NOTE", ChunkType.Note);
        }
    }
}