using System;

namespace VPC.Metadata.Business.Entity.Configuration {
    [AttributeUsage (AttributeTargets.All, AllowMultiple = false)]
    public class IntersectColumnAttribute : Attribute {

        private string _col;
        public IntersectColumnAttribute (string columnName) {
            _col = columnName;
        }
        public string GetColumn () {
            return _col;
        }
    }
}