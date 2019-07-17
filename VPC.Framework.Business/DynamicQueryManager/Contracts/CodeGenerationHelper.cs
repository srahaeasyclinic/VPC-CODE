using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Linq;
using VPC.Entities.EntityCore.Metadata.Runtime;
using VPC.Entities.EntityCore.Model.Query;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Entities.SampleEntity;
using VPC.Framework.Business.Data;
using VPC.Framework.Business.Data.SqlClient;
using VPC.Framework.Business.DynamicQueryManager.Core.Enums;
using VPC.Framework.Business.MetadataManager.Contracts;

namespace VPC.Framework.Business.EntityResourceManager.Contracts {
    internal static class CodeGenerationHelper {


        internal static string Generate (string entityName, JObject resource) {
            var code = resource["Code"];
            if (code == null || string.IsNullOrEmpty (code.ToString ())) {
                Random rnd = new Random ();
                int no = rnd.Next (1, 1000000);
                code = entityName.ToString ().Substring (0, 3) + no.ToString ();
            }
            return code.ToString ();
        }

    }
}