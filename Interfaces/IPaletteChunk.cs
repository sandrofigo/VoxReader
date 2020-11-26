namespace VoxReader.Interfaces
{
    internal interface IPaletteChunk : IChunk
    {
        Color[] Colors { get; }
    }
}