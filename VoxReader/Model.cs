using VoxReader.Interfaces;

namespace VoxReader
{
    public class Model : IModel
    {
        public string Name { get; }
        public Vector3 Position { get; }
        public Matrix3 Rotation { get; }
        public Vector3 Size { get; }
        public Voxel[] Voxels { get; }
        public int Id { get; }
        public bool IsCopy { get; }

        public Model(int id, string name, Vector3 position, Matrix3 rotation, Vector3 size, Voxel[] voxels, bool isCopy)
        {
            Id = id;
            Name = name;
            Position = position;
            Rotation = rotation;
            Size = size;
            Voxels = voxels;
            IsCopy = isCopy;
        }

        internal Model GetCopy()
        {
            return new(Id, Name, Position, Rotation, Size, Voxels, true);
        }
        
        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Position: {Position}, Rotation: {Rotation}, Size: {Size}, Voxel Count: {Voxels.Length}";
        }
    }
}