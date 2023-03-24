namespace VoxReader.Interfaces
{
    public interface IPalette
    {
        /// <summary>
        /// The raw colors stored in the palette.
        /// </summary>
        Color[] RawColors { get; }

        /// <summary>
        /// The mapped colors that are visible in the palette UI from MagicaVoxel.
        /// </summary>
        /// <remarks>The color index in MagicaVoxel starts at <c>1</c>, but this collection starts at <c>0</c>. You need to take this offset into account to get the right color.</remarks>
        Color[] Colors { get; }

        string[] Notes { get; }
        Color[] GetColorsByNote(string note);
    }
}