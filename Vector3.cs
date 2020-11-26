namespace VoxReader
{
    public struct Vector3
    {
        /// <summary>
        /// The x-component of the vector.
        /// </summary>
        public readonly int X;
        
        /// <summary>
        /// The y-component of the vector.
        /// </summary>
        public readonly int Y;
        
        /// <summary>
        /// The z-component of the vector.
        /// </summary>
        public readonly int Z;

        public Vector3(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override string ToString()
        {
            return $"X: {X}, Y: {Y}, Z: {Z}";
        }
    }
}