namespace VoxReader.Interfaces
{
    public interface IChunk
    {
        string Id { get; }

        byte[] Content { get; }

        IChunk[] Children { get; }
        
        int TotalBytes { get; }
    }
}