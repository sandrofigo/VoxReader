using System;
using System.Collections.Generic;
using System.Linq;
using VoxReader.Extensions;
using VoxReader.Interfaces;

namespace VoxReader.Chunks
{
    internal class Chunk : IChunk
    {
        public string Id { get; }
        public byte[] Content { get; }
        public IChunk[] Children { get; }

        public int TotalBytes { get; }

        /// <summary>
        /// Creates a new chunk using the given data.
        /// </summary>
        /// <param name="data">Data starting at the first byte of the chunk</param>
        public Chunk(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data), $"{nameof(data)} is null!");
            if (data.Length == 0)
                throw new ArgumentException($"{nameof(data)} is empty!");

            Id = GetChunkId(data);

            int contentLength = BitConverter.ToInt32(data, 4);

            Content = data.GetRange(12, contentLength);

            int childrenLength = BitConverter.ToInt32(data, 8);

            TotalBytes = 12 + contentLength + childrenLength;

            Children = GetChildrenChunks(data.GetRange(12 + contentLength, childrenLength));
        }

        public static string GetChunkId(byte[] chunkData)
        {
            return new string(Helper.GetCharArray(chunkData, 0, 4));
        }

        private static IChunk[] GetChildrenChunks(byte[] childrenData)
        {
            var children = new List<IChunk>();

            int currentChunkOffset = 0;

            while (currentChunkOffset < childrenData.Length)
            {
                IChunk childChunk = ChunkFactory.Parse(childrenData.GetRange(currentChunkOffset));
                children.Add(childChunk);
                currentChunkOffset += childChunk.TotalBytes;
            }

            return children.ToArray();
        }

        public T GetChild<T>() where T : class, IChunk
        {
            return Children.FirstOrDefault(c => c is T) as T;
        }

        public T[] GetChildren<T>() where T : class, IChunk
        {
            return Children.Where(c => c is T).Cast<T>().ToArray();
        }

        public override string ToString()
        {
            return $"Id: {Id}, Content Length: {Content.Length}, Children Length: {Children.Length}";
        }
    }
}