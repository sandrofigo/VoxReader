using VoxReader.Interfaces;

namespace VoxReader
{
    public class VoxFile : IVoxFile
    {
        public int VersionNumber { get; }
        public IModel[] Models { get; }
        public IPalette Palette { get; }

        internal VoxFile(int versionNumber, IModel[] models, IPalette palette)
        {
            VersionNumber = versionNumber;
            Models = models;
            Palette = palette;
        }
    }
}