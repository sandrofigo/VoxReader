namespace VoxReader
{
    public readonly struct Voxel
    {
        /// <summary>
        /// The position of the voxel in the model.
        /// </summary>
        public Vector3 Position { get; }
        
        /// <summary>
        /// The global position of the voxel in the scene.
        /// </summary>
        public Vector3 GlobalPosition { get; }

        /// <summary>
        /// The color of the voxel.
        /// </summary>
        public Color Color { get; }

        internal Voxel(Vector3 position, Vector3 globalPosition, Color color)
        {
            Position = position;
            GlobalPosition = globalPosition;
            Color = color;
        }

        public override string ToString()
        {
            return $"Position: [{Position}], Color: [{Color}]";
        }
    }
}