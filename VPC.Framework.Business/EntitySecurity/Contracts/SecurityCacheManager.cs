using System;
using System.Collections.Generic;
using System.Linq;
using VPC.Cache;
using VPC.Entities.BatchType;
using VPC.Entities.EntitySecurity;
using VPC.Entities.WorkFlow;
using VPC.Framework.Business.BatchType.Contracts;
using VPC.Framework.Business.WorkFlow.Contracts;

namespace VPC.Framework.Business.EntitySecurity.APIs
{

    public interface ISecurityCacheManager
    {
        EntitySecurityCacheInfo SecurityCache(Guid tenantId, Guid userId,bool isSuperAdmin);
        void Clear(Guid tenantId, Guid userId, string keyType);
        List<KeyValuePair<Guid,BatchTypeInfo>> BatchTypesCache();
        void BatchTypeClear();
    }


    public class SecurityCacheManager : ISecurityCacheManager
    {
        IManagerBatchType batchTypeManager=new ManagerBatchType();
        IManagerWorkFlowSecurity workFlowManager = new ManagerWorkFlowSecurity();
        IManagerEntitySecurity _managerEntitySecurity = new ManagerEntitySecurity();
        EntitySecurityCacheInfo ISecurityCacheManager.SecurityCache(Guid tenantId, Guid userId,bool isSuperAdmin)
        {
            var info = new EntitySecurityCacheInfo
            {
                WorkFlow = GetWorkFlowCache(tenantId, userId,isSuperAdmin),
                EntitySecurity = GetEntityCache(tenantId, userId),
                FunctionSecurity=GetFunctionCache(tenantId,userId)
            };
            return info;
        }

        private List<EntitySecurityInfo> GetEntityCache(Guid tenantId, Guid userId)
        {
            if (VPCCache.GetInstance().Contains<List<EntitySecurityInfo>>(string.Format("{0}-{1}", EntityCacheType.Entity, userId)))
            {
                return VPCCache.GetInstance().Get<List<EntitySecurityInfo>>(string.Format("{0}-{1}", EntityCacheType.Entity, userId));
            }
            var entitySecurities = _managerEntitySecurity.GetEntitySecuritiesByUserCode(tenantId, userId);
            var uniqueEntityIds=new List<string>();
            uniqueEntityIds=(from entitySecurity in entitySecurities select entitySecurity.EntityId).Distinct().ToList();
            var filteredSecurities=new List<EntitySecurityInfo>();
            foreach(var uniqueEntityId in uniqueEntityIds)
            {
                var itsEntities=(from entitySecurity in entitySecurities where entitySecurity.EntityId==uniqueEntityId select entitySecurity).ToList();
                int[] values =  new int[] { 0, 0, 0, 0, 0, 0, 0 };
                foreach(var itsEntity in itsEntities)
                {
                        var itsCodes = itsEntity.SecurityCode.ToString().Select(t => int.Parse(t.ToString())).ToArray();
                         for (int i = 0; i < itsCodes.Count(); i++)
                            {
                                if (itsCodes[i] > values[i])
                                {
                                    values[i] = itsCodes[i];
                                }
                            }
                }
                var itsCode = string.Join("", values);          
                filteredSecurities.Add(new EntitySecurityInfo{EntityId=uniqueEntityId,SecurityCode=Convert.ToInt32(itsCode)});
            }

            VPCCache.GetInstance().Set(string.Format("{0}-{1}", EntityCacheType.Entity, userId), filteredSecurities);
            return filteredSecurities;
        }



        private List<EntitySecurityInfo> GetFunctionCache(Guid tenantId, Guid userId)
        {
            if (VPCCache.GetInstance().Contains<List<EntitySecurityInfo>>(string.Format("{0}-{1}", EntityCacheType.Function, userId)))
            {
                return VPCCache.GetInstance().Get<List<EntitySecurityInfo>>(string.Format("{0}-{1}", EntityCacheType.Function, userId));
            }
            var functionSecurities = _managerEntitySecurity.GetFunctionSecuritiesByUserCode(tenantId, userId);
            var uniqueFunctionContextIds=new List<Guid>();
            uniqueFunctionContextIds=(from functionSecurity in functionSecurities select functionSecurity.FunctionContext).Distinct().ToList();
            var filteredSecurities=new List<EntitySecurityInfo>();
            foreach(var uniqueFunctionContextId in uniqueFunctionContextIds)
            {
                var itsFunctions=(from functionSecurity in functionSecurities where functionSecurity.FunctionContext==uniqueFunctionContextId select functionSecurity).ToList();
                int[] values =  new int[] { 0, 0, 0, 0, 0, 0, 0 };
                var functionContext=Guid.Empty;
                foreach(var itsFunction in itsFunctions)
                {
                    functionContext=itsFunction.FunctionContext;
                        var itsCodes = itsFunction.SecurityCode.ToString().Select(t => int.Parse(t.ToString())).ToArray();
                         for (int i = 0; i < itsCodes.Count(); i++)
                            {
                                if (itsCodes[i] > values[i])
                                {
                                    values[i] = itsCodes[i];
                                }
                            }
                }
                var itsCode = string.Join("", values);          
                filteredSecurities.Add(new EntitySecurityInfo{SecurityCode=Convert.ToInt32(itsCode),FunctionContext=functionContext});
            }

            VPCCache.GetInstance().Set(string.Format("{0}-{1}", EntityCacheType.Function, userId), filteredSecurities);
            return filteredSecurities;
        }



        private List<WorkFlowInfo> GetWorkFlowCache(Guid tenantId, Guid userId,bool isSuperAdmin)
        {
             VPCCache.GetInstance().Remove<List<WorkFlowInfo>>(string.Format("{0}-{1}", EntityCacheType.WorkFlow, userId));
            if (VPCCache.GetInstance().Contains<List<WorkFlowInfo>>(string.Format("{0}-{1}", EntityCacheType.WorkFlow, userId)))
            {
                return VPCCache.GetInstance().Get<List<WorkFlowInfo>>(string.Format("{0}-{1}", EntityCacheType.WorkFlow, userId));
            }
            var workFlows = workFlowManager.GetWorkFlowsByUserCode(tenantId, userId,isSuperAdmin);
            VPCCache.GetInstance().Set(string.Format("{0}-{1}", EntityCacheType.WorkFlow, userId), workFlows);
            return workFlows;
        }

        void ISecurityCacheManager.Clear(Guid tenantId,Guid userId, string keyType)
        {
            if (keyType == EntityCacheType.Entity)
            {
                if (VPCCache.GetInstance().Contains<List<EntitySecurityInfo>>(string.Format("{0}-{1}", keyType, userId)))
                {
                    VPCCache.GetInstance().Remove<List<EntitySecurityInfo>>(string.Format("{0}-{1}", keyType, userId));
                }
            }
            else if (keyType == EntityCacheType.WorkFlow)
            {
                if (VPCCache.GetInstance().Contains<List<WorkFlowInfo>>(string.Format("{0}-{1}", keyType, userId)))
                {
                    VPCCache.GetInstance().Remove<List<WorkFlowInfo>>(string.Format("{0}-{1}", keyType, userId));
                }
            }
            else if (keyType == EntityCacheType.Function)
            {
                if (VPCCache.GetInstance().Contains<List<EntitySecurityInfo>>(string.Format("{0}-{1}", keyType, userId)))
                {
                    VPCCache.GetInstance().Remove<List<EntitySecurityInfo>>(string.Format("{0}-{1}", keyType, userId));
                }
            }
        }

        List<KeyValuePair<Guid,BatchTypeInfo>> ISecurityCacheManager.BatchTypesCache()
        {           
            if (VPCCache.GetInstance().Contains<List<KeyValuePair<Guid,BatchTypeInfo>>>(string.Format("{0}", EntityCacheType.BatchTypes)))
            {
                return VPCCache.GetInstance().Get<List<KeyValuePair<Guid,BatchTypeInfo>>>(string.Format("{0}", EntityCacheType.BatchTypes));
            }
            var batchTypes=batchTypeManager.GetEnabledBatchTypes();            
            VPCCache.GetInstance().Set(string.Format("{0}", EntityCacheType.BatchTypes), batchTypes);
            return batchTypes;            
        }

        void ISecurityCacheManager.BatchTypeClear()
        {
            if (VPCCache.GetInstance().Contains<List<KeyValuePair<Guid,BatchTypeInfo>>>(string.Format("{0}",EntityCacheType.BatchTypes)))
                {
                    VPCCache.GetInstance().Remove<List<KeyValuePair<Guid,BatchTypeInfo>>>(string.Format("{0}", EntityCacheType.BatchTypes));
                }            
        }
    }
}