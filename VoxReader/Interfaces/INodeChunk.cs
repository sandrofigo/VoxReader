namespace VoxReader.Interfaces
{
    internal interface INodeChunk : IChunk
    {
        /// <summary>
        /// The id of the node.
        /// </summary>
        int NodeId { get; }
    }
}