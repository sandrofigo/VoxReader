namespace VoxReader.Interfaces
{
    internal interface IVoxelChunk : IChunk
    {
        RawVoxel[] Voxels { get; }
    }
}