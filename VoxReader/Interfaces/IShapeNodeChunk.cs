namespace VoxReader.Interfaces
{
    internal interface IShapeNodeChunk : INodeChunk
    {
        /// <summary>
        /// The number of models. Must be '1'.
        /// </summary>
        int ModelCount { get; }
        
        /// <summary>
        /// The ids of the models.
        /// </summary>
        int[] Models { get; }
    }
}