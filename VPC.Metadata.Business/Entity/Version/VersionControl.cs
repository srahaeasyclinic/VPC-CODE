using System;
using System.Collections.Generic;
using System.Text;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.Operator.DataAnnotations;
using VPC.Metadata.Business.Validator.Schema;

namespace VPC.Metadata.Business.Entity.Version {

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class VersionControl : Attribute
    {
        private string _version;
        public VersionControl(string versionControlEntityName)
        {
           _version = versionControlEntityName;
        }

        public string GetVersionClassName(){
            return _version;
        }
    }
}