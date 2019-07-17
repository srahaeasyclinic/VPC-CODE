using System;
using System.Collections.Generic;
using VPC.Entities.BatchType;
using VPC.Entities.Role;
using VPC.Framework.Business.Counter.Data;
using VPC.Framework.Business.Role.Data;
using  VPC.Entities.Counter;
namespace VPC.Framework.Business.Counter.APIs
{
 public interface IAdminCounter
    {    
        bool Create(Guid tenantId, CounterInfo info,string entityId);
        bool Update(Guid tenantId, CounterInfo info,string entityId);
        bool Delete(Guid tenantId, Guid counterId);  
    
    }
    
    internal  class AdminCounter : IAdminCounter
    {
        private readonly DataCounter _data = new DataCounter();
        bool IAdminCounter.Create(Guid tenantId, CounterInfo info,string entityId)
        {
            return _data.Create(tenantId,info,entityId);
        }

      

        bool IAdminCounter.Delete(Guid tenantId, Guid counterId)
        {
            return _data.Delete(tenantId,counterId);
        }

        bool IAdminCounter.Update(Guid tenantId, CounterInfo info,string entityId)
        {
             return _data.Update(tenantId,info,entityId);
        }

     
    }
}