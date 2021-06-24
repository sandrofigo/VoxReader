using System;

namespace VoxReader
{
    public enum ChunkType
    {
        Main,
        Pack,
        Size,
        Voxel,
        Palette,
        [Obsolete("This type of chunk will be removed in the future.")]
        MaterialOld,
        MaterialNew,
        TransformNode,
        GroupNode,
        ShapeNode,
        Layer,
        Object,
        Camera,
        Note
    }
}