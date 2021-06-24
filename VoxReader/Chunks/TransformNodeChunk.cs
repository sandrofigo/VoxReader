using System.Linq;
using VoxReader.Interfaces;

namespace VoxReader.Chunks
{
    internal class TransformNodeChunk : NodeChunk, ITransformNodeChunk
    {
        public string Name { get; }
        public bool IsHidden { get; }
        public int ChildNodeId { get; }
        public int ReservedId { get; }
        public int LayerId { get; }

        public int FrameCount => Frames.Length;
        public Frame[] Frames { get; }

        public TransformNodeChunk(byte[] data) : base(data)
        {
            Attributes.TryGetValue("_name", out string name);
            Name = name;

            Attributes.TryGetValue("_hidden", out string hidden);
            IsHidden = hidden == "1";

            ChildNodeId = FormatParser.ParseInt32();
            ReservedId = FormatParser.ParseInt32();
            LayerId = FormatParser.ParseInt32();

            int frameCount = FormatParser.ParseInt32();

            if (frameCount > 0)
            {
                //TODO: implement frame parsing
                Frames = Enumerable.Repeat(new Frame(), frameCount).ToArray();
            }
        }
    }
}