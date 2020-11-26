namespace VoxReader.Interfaces
{
    public interface IVoxFile
    {
        int VersionNumber { get; }

        IChunk MainChunk { get; }
    }
}