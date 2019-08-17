using System;

namespace VPC.Metadata.Business.Entity.Configuration
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class AutoIncrementAttribute : Attribute
    {
        private IncrementType _auto;
        public AutoIncrementAttribute(IncrementType value)
        {
            _auto = value;
        }

        public IncrementType GetIncrementType(){
            return _auto;
        }

    }


    public enum IncrementType{
        Table = 1, // not implemented
        Version = 2
    }
}
