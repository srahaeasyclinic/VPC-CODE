using System;
using System.Runtime.Serialization;

namespace VPC.Framework.Business.Exception
{
   
    public class ErrorCodeException<T> : VPCException
    {
        private readonly T _errorCode;

        public ErrorCodeException(T errorCode)
            : base(String.Empty)
        {
            _errorCode = errorCode;
        }

        protected ErrorCodeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info != null)
            {
                _errorCode = (T) Enum.Parse(typeof (T), info.GetString("ErrorCode"));
            }
        }

        public T ErrorCode
        {
            get { return _errorCode; }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            if (info != null)
            {
                info.AddValue("ErrorCode", _errorCode);
            }
        }
    }
}