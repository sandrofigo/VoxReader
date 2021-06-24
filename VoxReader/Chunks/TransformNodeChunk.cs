using System.Linq;
using VoxReader.Interfaces;

namespace VoxReader.Chunks
{
    internal class TransformNodeChunk : Chunk, ITransformNodeChunk
    {
        public int NodeId { get; }
        public string Name { get; }
        public bool IsHidden { get; }
        public int ChildNodeId { get; }
        public int ReservedId { get; }
        public int LayerId { get; }
        
        public int FrameCount => Frames.Length;
        public Frame[] Frames { get; }

        public TransformNodeChunk(byte[] data) : base(data)
        {
            var formatParser = new FormatParser(Content);

            NodeId = formatParser.ParseInt32();

            var attributes = formatParser.ParseDictionary();

            attributes.TryGetValue("_name", out string name);
            Name = name;
            
            attributes.TryGetValue("_hidden", out string hidden);
            IsHidden = hidden == "1";
            
            ChildNodeId = formatParser.ParseInt32();
            ReservedId = formatParser.ParseInt32();
            LayerId = formatParser.ParseInt32();

            int frameCount = formatParser.ParseInt32();

            if (frameCount > 0)
            {
                //TODO: implement frame parsing
                Frames = Enumerable.Repeat(new Frame(), frameCount).ToArray();
            }
        }
    }
}