namespace VoxReader
{
    public class Voxel
    {
        /// <summary>
        /// The position of the voxel on the x-axis
        /// </summary>
        public int X { get; }
        
        /// <summary>
        /// The position of the voxel on the y-axis
        /// </summary>
        public int Y { get; }
        
        /// <summary>
        /// The position of the voxel on the z-axis
        /// </summary>
        public int Z { get; }
        
        /// <summary>
        /// The index of the color from the palette
        /// </summary>
        public int ColorIndex { get; }

        public Voxel(int x, int y, int z, int colorIndex)
        {
            X = x;
            Y = y;
            Z = z;
            ColorIndex = colorIndex;
        }

        public override string ToString()
        {
            return $"X: {X} Y: {Y} Z: {Z} ColorIndex: {ColorIndex}";
        }
    }
}