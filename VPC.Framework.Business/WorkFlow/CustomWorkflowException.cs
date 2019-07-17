using System;
using System.Runtime.Serialization; 
namespace VPC.Framework.Business.WorkFlow
{
    [Serializable]
    public class CustomWorkflowException<T> : System.Exception
    {
        private readonly T _errorCode;

        public CustomWorkflowException()
        {
        }

        public CustomWorkflowException(string message)
            : base(message)
        {
        }

        public CustomWorkflowException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }

        public CustomWorkflowException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }

        public CustomWorkflowException(string format, System.Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        {
        }

        protected CustomWorkflowException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _errorCode = (T) Enum.Parse(typeof (T), info.GetString("ErrorCode"));
        }


        public CustomWorkflowException(T errorCode)
            : base(String.Empty)
        {
            _errorCode = errorCode;
        }


        public T ErrorCode
        {
            get { return _errorCode; }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("ErrorCode", _errorCode);
        }
    }
}