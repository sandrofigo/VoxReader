using System;
using System.Runtime.Serialization;

namespace VoxReader
{
    [Serializable]
    public class UnsupportedDataException : Exception
    {
        public UnsupportedDataException()
        {
        }

        public UnsupportedDataException(string message) : base(message)
        {
        }

        public UnsupportedDataException(string message, Exception inner) : base(message, inner)
        {
        }

        protected UnsupportedDataException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}