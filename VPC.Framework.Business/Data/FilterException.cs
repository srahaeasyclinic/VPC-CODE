using System;
using System.Runtime.Serialization;

namespace VPC.Framework.Business.Data
{
    [Serializable]
    public class FilterException : StorageException
    {
        public FilterException()
        {
        }

        public FilterException(string message)
            : base(message)
        {
        }

        public FilterException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }

        protected FilterException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}