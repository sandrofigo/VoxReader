namespace VoxReader.Interfaces
{
    public interface IModel
    {
        /// <summary>
        /// The name of the model.
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// The position of the model.
        /// </summary>
        Vector3 Position { get; }
        
        /// <summary>
        /// The size of the model.
        /// </summary>
        Vector3 Size { get; }
        
        /// <summary>
        /// All voxels that belong to the model.
        /// </summary>
        Voxel[] Voxels { get; }
        
        /// <summary>
        /// The id of the model.
        /// </summary>
        /// <remarks>Copies of models share the same id.</remarks>
        int Id { get; }
        
        /// <summary>
        /// Indicates if the model is a copy another model.
        /// </summary>
        bool IsCopy { get; }
    }
}