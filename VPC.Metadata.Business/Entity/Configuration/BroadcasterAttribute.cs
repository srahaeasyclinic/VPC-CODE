using System;

namespace VPC.Metadata.Business.Entity.Configuration {
    [AttributeUsage (AttributeTargets.All, AllowMultiple = true)]
    public class BroadcasterAttribute : Attribute {

        private string _name;
        private Type _type;
        public BroadcasterAttribute (Type t) {
            _type = t;
            _name = _type.Name;
        }
        public BroadcasterAttribute (string name) {
            _name = name;
        }
        public Type GetBroadcasterType () {
            return _type;
        }
        public string GetBroadcastingMethodName () {
            return _name; //_method.Name;
        }
    }
}