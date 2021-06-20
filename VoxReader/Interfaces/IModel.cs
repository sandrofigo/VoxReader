namespace VoxReader.Interfaces
{
    public interface IModel
    {
        /// <summary>
        /// The size of the model.
        /// </summary>
        Vector3 Size { get; }
        
        /// <summary>
        /// All voxels that belong to the model.
        /// </summary>
        Voxel[] Voxels { get; }
    }
}