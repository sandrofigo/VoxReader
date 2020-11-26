using System;
using System.Collections.Generic;
using VoxReader.Extensions;
using VoxReader.Interfaces;

namespace VoxReader.Chunks
{
    public class Chunk : IChunk
    {
        public string Id { get; }
        public byte[] Content { get; }
        public IChunk[] Children { get; }
        
        public int TotalBytes { get; }

        /// <summary>
        /// Creates a new chunk using the given data
        /// </summary>
        /// <param name="data">Data starting at the first byte of the chunk</param>
        public Chunk(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data), $"{nameof(data)} is null!");
            if (data.Length == 0)
                throw new ArgumentException($"{nameof(data)} is empty!");

            Id = new string(Helper.GetCharArray(data, 0, 4));

            int contentLength = BitConverter.ToInt32(data, 4);

            Content = data.GetRange(12, contentLength);

            int childrenLength = BitConverter.ToInt32(data, 8);

            TotalBytes = 12 + contentLength + childrenLength;
            
            Children = GetChildrenChunks(data.GetRange(12 + contentLength, childrenLength));
        }

        private IChunk[] GetChildrenChunks(byte[] childrenData)
        {
            var children = new List<IChunk>();

            int currentChunkOffset = 0;

            while (currentChunkOffset < childrenData.Length)
            {
                IChunk childChunk = new Chunk(childrenData.GetRange(currentChunkOffset));
                children.Add(childChunk);
                currentChunkOffset += childChunk.TotalBytes;
            }

            return children.ToArray();
        }

        public override string ToString()
        {
            return $"{Id} N: {Content.Length} M: {Children.Length}";
        }
    }
}