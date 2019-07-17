using System;
using System.Runtime.Serialization;

namespace VPC.Framework.Business.Exception
{
 
    public class VPCException : System.Exception
    {
        public VPCException()
        {
        }

        public VPCException(string message)
            : base(message)
        {
        }

        public VPCException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }

        protected VPCException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}