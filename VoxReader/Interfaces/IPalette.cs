namespace VoxReader.Interfaces
{
    public interface IPalette
    {
        Color[] Colors { get; }
        string[] Notes { get; }
        Color[] GetColorsByNote(string note);
    }
}