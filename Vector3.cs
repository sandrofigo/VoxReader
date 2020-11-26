namespace VoxReader
{
    public struct Vector3
    {
        public readonly int X;
        public readonly int Y;
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