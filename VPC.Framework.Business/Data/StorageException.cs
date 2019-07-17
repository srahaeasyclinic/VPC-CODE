using System;
using System.Runtime.Serialization;
using VPC.Framework.Business.Exception;

namespace VPC.Framework.Business.Data
{
 
    public class StorageException : VPCException
    {
        public StorageException()
        {
        }

        public StorageException(string message)
            : base(message)
        {
        }

        public StorageException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }

        protected StorageException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}