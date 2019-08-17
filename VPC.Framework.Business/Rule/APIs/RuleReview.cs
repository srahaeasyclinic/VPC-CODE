using System;
using System.Collections.Generic;
using System.Text;
using VPC.Entities.EntityCore.Metadata.Picklist;
using VPC.Entities.Rule;
using VPC.Framework.Business.MetadataManager.Contracts;
using VPC.Framework.Business.Rule.Data;
using VPC.Metadata.Business.DataTypes;

namespace VPC.Framework.Business.Rule.APIs
{

    public interface IRuleReview
    {
        List<RuleInfo> GetAllRules(Guid tenantId, string entityId = null);
        RuleInfo GetRuleById(Guid tenantId, Guid ruleId, string entityId = null);
        RuleInfo GetuniquefieldsRules(Guid tenantId, string entityId);

    }


    internal class RuleReview : IRuleReview
    {
        IMetadataManager iMetadataManager = new MetadataManager.Contracts.MetadataManager();
        private readonly DataRule dataRule = new DataRule();
        List<RuleInfo> IRuleReview.GetAllRules(Guid tenantId, string entityId)
        {            
            return dataRule.GetAllRules(tenantId, entityId);
        }

        RuleInfo IRuleReview.GetRuleById(Guid tenantId, Guid ruleId, string entityId)
        {
            return dataRule.GetById(tenantId, ruleId, entityId);
        }

        RuleInfo IRuleReview.GetuniquefieldsRules(Guid tenantId, string entityId)
        {
            return dataRule.GetUniquefieldsRulesByEntity(tenantId, entityId);
        }
    }
}
