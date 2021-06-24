using VoxReader.Interfaces;

namespace VoxReader.Chunks
{
    internal class GroupNodeChunk : Chunk, IGroupNodeChunk
    {
        public int NodeId { get; }
        public int ChildrenCount => ChildrenNodes.Length;
        public int[] ChildrenNodes { get; }

        public GroupNodeChunk(byte[] data) : base(data)
        {
            var formatParser = new FormatParser(Content);

            NodeId = formatParser.ParseInt32();

            var attributes = formatParser.ParseDictionary();

            int childCount = formatParser.ParseInt32();

            ChildrenNodes = new int[childCount];
            for (int i = 0; i < childCount; i++)
            {
                ChildrenNodes[i] = formatParser.ParseInt32();
            }
        }
    }
}