using System;
using System.Runtime.Serialization;

namespace VPC.Framework.Business.Data
{
    internal sealed class DataRelationException : StorageException
    {
        public DataRelationException()
        {
        }

        public DataRelationException(string message)
            : base(message)
        {
        }

        public DataRelationException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }

        private DataRelationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}