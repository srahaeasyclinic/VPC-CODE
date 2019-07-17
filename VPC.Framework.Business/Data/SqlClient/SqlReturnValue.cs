using System;

namespace VPC.Framework.Business.Data.SqlClient
{
    internal sealed class SpRetVal
    {
        private readonly int _returnValue;

        public SpRetVal(int returnValue)
        {
            _returnValue = returnValue;
        }

        public int Value
        {
            get { return _returnValue; }
        }

        public void ValidateAndThrow(string genericMessage, string argumentMessage)
        {
            if (_returnValue == 0) return; // Success
            switch (_returnValue)
            {
                case 2:
                    {
                        throw new ArgumentException(argumentMessage + ": Code=" + _returnValue);
                    }
                default:
                    {
                        throw new System.Exception(genericMessage + ": Code=" + _returnValue);
                    }
            }
        }

        public void ValidateAndThrow(string genericMessageFormat, params object[] arg)
        {
            string genericMessage = string.Format(genericMessageFormat, arg);
            ValidateAndThrow(genericMessage, genericMessage);
        }

        public void ValidateAndThrow(string genericMessageFormat, object arg0)
        {
            string genericMessage = string.Format(genericMessageFormat, arg0);
            ValidateAndThrow(genericMessage, genericMessage);
        }

        public void ValidateAndThrow(string genericMessage)
        {
            ValidateAndThrow(genericMessage, genericMessage);
        }

        public static void ValidateAndThrow(int returnValue, string genericMessage, string argumentMessage)
        {
            if (returnValue == 0) return; // Success
            switch (returnValue)
            {
                case 2:
                    {
                        throw new ArgumentException(argumentMessage + ": Code=" + returnValue);
                    }
                default:
                    {
                        throw new System.Exception(genericMessage + ": Code=" + returnValue);
                    }
            }
        }

        public static void ValidateAndThrow(int returnValue, string genericMessage)
        {
            ValidateAndThrow(returnValue, genericMessage, genericMessage);
        }


        public static bool Validate(int returnValue)
        {
            return returnValue != 0;
        }

        public static implicit operator int(SpRetVal retval)
        {
            return retval._returnValue;
        }

        public static implicit operator SpRetVal(int data)
        {
            return new SpRetVal(data);
        }
    }
}