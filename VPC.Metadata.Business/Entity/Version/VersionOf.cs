using System;
using System.Collections.Generic;
using System.Text;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.Operator.DataAnnotations;
using VPC.Metadata.Business.Validator.Schema;

namespace VPC.Metadata.Business.Entity.Version {

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class VersionOf : Attribute
    {
        private string _versionOf;
        public VersionOf(string versionOfEntityName)
        {
           _versionOf = versionOfEntityName;
        }

        public string GetVersionOfName(){
return _versionOf;
        }
    }
}