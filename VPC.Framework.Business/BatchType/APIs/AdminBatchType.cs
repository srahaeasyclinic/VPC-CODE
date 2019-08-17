using System;
using System.Collections.Generic;
using VPC.Entities.BatchType;
using VPC.Entities.Role;
using VPC.Framework.Business.BatchType.Data;
using VPC.Framework.Business.Role.Data;

namespace VPC.Framework.Business.BatchType.APIs
{
 public interface IAdminBatchType
    {    
        // bool Create(Guid tenantId, BatchTypeInfo info);
        // bool Update(Guid tenantId, BatchTypeInfo info);
        // bool CreateBatchTypes(Guid tenantId, List<BatchTypeInfo> infos);
        // bool Delete(Guid tenantId, Guid batchTypeId);
        // bool UpdateStatus(Guid tenantId, Guid batchTypeId);    
    
    }
    
    internal  class AdminBatchType : IAdminBatchType
    {
        private readonly DataBatchType _data = new DataBatchType();

        // bool IAdminBatchType.Create(Guid tenantId, BatchTypeInfo info)
        // {
        //     return _data.Create(tenantId,info);
        // }

        // bool IAdminBatchType.CreateBatchTypes(Guid tenantId, List<BatchTypeInfo> infos)
        // {
        //    return _data.CreateBatchTypes(tenantId,infos);
        // }

        // bool IAdminBatchType.Delete(Guid tenantId, Guid batchTypeId)
        // {
        //     return _data.Delete(tenantId,batchTypeId);
        // }

        // bool IAdminBatchType.Update(Guid tenantId, BatchTypeInfo info)
        // {
        //      return _data.Update(tenantId,info);
        // }

        // bool IAdminBatchType.UpdateStatus(Guid tenantId, Guid batchTypeId)
        // {
        //      return _data.UpdateStatus(tenantId,batchTypeId);
        // }
    }
}