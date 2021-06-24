using VoxReader.Interfaces;

namespace VoxReader.Chunks
{
    internal class ShapeNodeChunk : Chunk, IShapeNodeChunk
    {
        public int NodeId { get; }
        public int ModelCount => Models.Length;
        public int[] Models { get; }

        public ShapeNodeChunk(byte[] data) : base(data)
        {
            var formatParser = new FormatParser(Content);

            NodeId = formatParser.ParseInt32(); //TODO: move to base class

            var nodeAttributes = formatParser.ParseDictionary(); //TODO: move to base class
            
            int modelCount = formatParser.ParseInt32();

            Models = new int[modelCount];
            
            for (int i = 0; i < modelCount; i++)
            {
                Models[i] = formatParser.ParseInt32();

                var modelAttributes = formatParser.ParseDictionary(); //TODO: parse attributes
            }
        }
    }
}