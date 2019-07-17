using System;
using System.Collections.Generic;
using System.Text;
using VPC.Entities.Rule;
using VPC.Framework.Business.MetadataManager.Contracts;
using VPC.Framework.Business.Rule.Data;

namespace VPC.Framework.Business.Rule.APIs
{
    public interface IRuleAdmin
    {
        bool Create(Guid tenantId, RuleInfo ruleInfo, ref string strMsg);
        bool Update(Guid tenantId, RuleInfo ruleInfo, ref string strMsg);
        bool Delete(Guid tenantId, Guid ruleId);
    }

    internal class RuleAdmin : IRuleAdmin
    {
        private readonly DataRule dataRule = new DataRule();

        bool IRuleAdmin.Create(Guid tenantId, RuleInfo ruleInfo, ref string strMsg)
        {            
            var lstSource = ruleInfo.SourceList;
            ruleInfo.Source = Newtonsoft.Json.JsonConvert.SerializeObject(lstSource);
            var lstTarget = ruleInfo.TargetList;
            ruleInfo.Target = Newtonsoft.Json.JsonConvert.SerializeObject(lstTarget);
            ruleInfo.Id = Guid.NewGuid();         
            return dataRule.Create(tenantId, ruleInfo, ref strMsg);
        }


        bool IRuleAdmin.Delete(Guid tenantId, Guid ruleId)
        {
            return dataRule.Delete(tenantId, ruleId);
        }

        bool IRuleAdmin.Update(Guid tenantId, RuleInfo ruleInfo, ref string strMsg)
        {
            var lstSource = ruleInfo.SourceList;
            ruleInfo.Source = Newtonsoft.Json.JsonConvert.SerializeObject(lstSource);
            var lstTarget = ruleInfo.TargetList;
            ruleInfo.Target = Newtonsoft.Json.JsonConvert.SerializeObject(lstTarget);
            return dataRule.Update(tenantId, ruleInfo, ref strMsg);
        }
    }
}
