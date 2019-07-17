using System;
using System.Collections.Generic;
using System.Text;
using VPC.Entities.Rule;
using VPC.Framework.Business.MetadataManager.Contracts;
using VPC.Framework.Business.Rule;
using VPC.Framework.Business.Rule.APIs;

namespace VPC.Framework.Business.Rule.Contracts
{

    public interface IManageRule
    {
        List<RuleInfo> GetAllRules(Guid tenantId, string entityId = null);
        RuleInfo GetRuleById(Guid tenantId, Guid ruleId, string entityId = null);
        bool Create(Guid tenantId, RuleInfo ruleInfo, ref string strMsg);
        bool Update(Guid tenantId, RuleInfo ruleInfo, ref string strMsg);
        bool Delete(Guid tenantId, Guid ruleId);

    }
    public class RuleManager : IManageRule
    {        
        private readonly IRuleAdmin ruleAdmin = new RuleAdmin();
        private readonly IRuleReview ruleReview = new RuleReview();
        
        bool IManageRule.Create(Guid tenantId, RuleInfo ruleInfo, ref string strMsg)
        {            
            return ruleAdmin.Create(tenantId, ruleInfo, ref strMsg);
        }

        bool IManageRule.Delete(Guid tenantId, Guid ruleId)
        {
            return ruleAdmin.Delete(tenantId, ruleId);
        }

       
        List<RuleInfo> IManageRule.GetAllRules(Guid tenantId, string entityId)
        {           
            return ruleReview.GetAllRules(tenantId, entityId);          
        }

        RuleInfo IManageRule.GetRuleById(Guid tenantId, Guid ruleId, string entityId)
        {   
            return ruleReview.GetRuleById(tenantId, ruleId,entityId);
        }

        bool IManageRule.Update(Guid tenantId, RuleInfo ruleInfo, ref string strMsg)
        {
            return ruleAdmin.Update(tenantId, ruleInfo, ref strMsg);
        }
    }
}
