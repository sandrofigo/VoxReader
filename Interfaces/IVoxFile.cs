namespace VoxReader.Interfaces
{
    public interface IVoxFile
    {
        int VersionNumber { get; }

        IModel[] Models { get; }
        
        IPalette Palette { get; }
    }
}