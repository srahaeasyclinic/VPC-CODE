using System;
using System.Collections.Generic;
using VPC.Entities.EntitySecurity;
using VPC.Entities.Role;
using VPC.Framework.Business.EntitySecurity.Data;
using VPC.Framework.Business.Role.Data;

namespace VPC.Framework.Business.EntitySecurity.APIs
{
 public interface IReviewEntitySecurity
    {        
      List<EntitySecurityInfo> GetEntitySecurities(Guid tenantId, string entityId,  Guid? roleId );
      EntitySecurityInfo GetEntitySecurity(Guid tenantId,string entityId , Guid roleId  );
      List<EntitySecurityInfo> GetEntitySecuritiesByUserCode(Guid tenantId,  Guid userId );


      List<EntitySecurityInfo> GetFunctionSecurities(Guid tenantId, string entityId,  Guid? roleId );
      List<EntitySecurityInfo> GetFunctionSecuritiesByUserCode(Guid tenantId, Guid userId);

    }
    
    internal  class ReviewEntitySecurity : IReviewEntitySecurity
    {
        private readonly DataEntitySecurity _dataEntitySecurity = new DataEntitySecurity();

        EntitySecurityInfo IReviewEntitySecurity.GetEntitySecurity(Guid tenantId,string entityId ,  Guid roleId )
        {
            return _dataEntitySecurity.GetEntitySecurity(tenantId, entityId , roleId );
        }

        List<EntitySecurityInfo> IReviewEntitySecurity.GetEntitySecurities(Guid tenantId, string entityId,  Guid? roleId )
        {
            return _dataEntitySecurity.GetEntitySecurities(tenantId, entityId , roleId );
        }

        List<EntitySecurityInfo> IReviewEntitySecurity.GetEntitySecuritiesByUserCode(Guid tenantId, Guid userId)
        {
            return _dataEntitySecurity.GetEntitySecuritiesByUserCode(tenantId,userId);
        }

        List<EntitySecurityInfo> IReviewEntitySecurity.GetFunctionSecurities(Guid tenantId, string entityId, Guid? roleId)
        {
            return _dataEntitySecurity.GetFunctionSecurities(tenantId,entityId,roleId);
        }

        List<EntitySecurityInfo> IReviewEntitySecurity.GetFunctionSecuritiesByUserCode(Guid tenantId, Guid userId)
        {
          return _dataEntitySecurity.GetFunctionSecuritiesByUserCode(tenantId,userId);
        }
    }
}