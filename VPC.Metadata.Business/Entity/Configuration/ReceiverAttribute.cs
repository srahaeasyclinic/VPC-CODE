using System;

namespace VPC.Metadata.Business.Entity.Configuration
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class ReceiverAttribute : Attribute
    {

        private string _method;
        private string _reciver;
        public ReceiverAttribute(string fieldPropertyName, string method)
        {
            _method = method;
            _reciver = fieldPropertyName;
        }

        // public Type GetMethod(){
        //     return _method;
        // }
        // public Type GetReceiver(){
        //     return _reciver;
        // }

        public string GetMethodName()
        {
            return _method;
        }

        public string GetReceiverTypeName()
        {
             return _reciver;
        }
    }
}
