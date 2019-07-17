using System;
using System.Runtime.Serialization;

namespace VPC.Framework.Business.Data
{
    [Serializable]
    public sealed class InvalidFilterException : FilterException
    {
        public InvalidFilterException()
        {
        }

        public InvalidFilterException(string message)
            : base(message)
        {
        }

        public InvalidFilterException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }

        private InvalidFilterException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}