namespace VoxReader.Interfaces
{
    internal interface IChunk
    {
        /// <summary>
        /// The Id of the chunk.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// The byte content of the chunk.
        /// </summary>
        byte[] Content { get; }

        /// <summary>
        /// The children of the chunk, if there are any.
        /// </summary>
        IChunk[] Children { get; }

        /// <summary>
        /// The total bytes of the chunk data.
        /// </summary>
        int TotalBytes { get; }

        /// <summary>
        /// Returns the first child that matches the specified chunk type.
        /// </summary>
        T GetChild<T>() where T : class, IChunk;

        /// <summary>
        /// Returns all children that match the specified chunk type.
        /// </summary>
        T[] GetChildren<T>() where T : class, IChunk;
    }
}