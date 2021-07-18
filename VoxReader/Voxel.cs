namespace VoxReader
{
    public readonly struct Voxel
    {
        /// <summary>
        /// The position of the voxel.
        /// </summary>
        public Vector3 Position { get; }

        /// <summary>
        /// The color of the voxel.
        /// </summary>
        public Color Color { get; }

        internal Voxel(Vector3 position, Color color)
        {
            Position = position;
            Color = color;
        }

        public override string ToString()
        {
            return $"Position: [{Position}], Color: [{Color}]";
        }
    }
}