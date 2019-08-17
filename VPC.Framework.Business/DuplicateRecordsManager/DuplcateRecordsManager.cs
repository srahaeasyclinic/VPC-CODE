using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VPC.Entities.Rule;
using VPC.Framework.Business.DynamicQueryManager.Contracts;
using VPC.Framework.Business.Rule.Contracts;

namespace VPC.Framework.Business.DuplicateRecordsManager
{
    public interface IDuplicateRecordsManager
    {
        bool CheckDuplicatesData(Guid tenantId, string entityname, JObject resource, Guid? id);
    }
    public class DuplcateRecordsManager : IDuplicateRecordsManager
    {
        private readonly IEntityQueryManager _queryManager = new EntityQueryManager();
        //private readonly MetadataManager.Contracts.ILayoutManager _iILayoutManager =new MetadataManager.Contracts.LayoutManager();
        private readonly MetadataManager.Contracts.IMetadataManager _IMetadataManager =new MetadataManager.Contracts.MetadataManager();
        private readonly IManageRule _ManageRule = new RuleManager();

        bool IDuplicateRecordsManager.CheckDuplicatesData(Guid tenantId, string entityname, JObject resource, Guid? id)
        {
            string EntityConextId = _IMetadataManager.GetEntityContextByEntityName(entityname);
            if(string.IsNullOrEmpty(EntityConextId))
            {
                throw new ArgumentException("Entity is not found");
            }

            RuleInfo uniqueRuleObj = _ManageRule.GetUniqueFieldsRuleByEntity(tenantId, EntityConextId);

            if (uniqueRuleObj == null) return false;
            
            var fieldsArr = uniqueRuleObj.TargetList.Select(t=>t.Name).ToList();

            Dictionary<string, dynamic> fields = new Dictionary<string, dynamic>();
            foreach(var fieldname in fieldsArr)
            {
                var value = GetJArrayValue(resource, fieldname);
                fields.Add(fieldname, value);
            }
            //var fields = string.Join(",", fieldsArr);

            var status = _IMetadataManager.GetDuplicateStatus(entityname, fields, id);

            return false;
        }

        private  dynamic GetJArrayValue(JObject resource, string key)
        {
            foreach (KeyValuePair<string, JToken> keyValuePair in resource)
            {
                if (key == keyValuePair.Key)
                {
                    return keyValuePair.Value;
                }
            }
           return null;
        }

    }
}
