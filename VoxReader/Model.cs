using VoxReader.Interfaces;

namespace VoxReader
{
    public class Model : IModel
    {
        public Vector3 Size { get; }

        public Voxel[] Voxels { get; }

        public Model(Vector3 size, Voxel[] voxels)
        {
            Size = size;
            Voxels = voxels;
        }

        public override string ToString()
        {
            return $"Size: {Size}, Voxel Count: {Voxels.Length}";
        }
    }
}