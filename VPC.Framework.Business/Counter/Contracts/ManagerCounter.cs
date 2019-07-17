using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using NLog;
using VPC.Entities.Counter;
using VPC.Entities.Role;
using VPC.Framework.Business.Common;
using VPC.Framework.Business.Counter.APIs;
using VPC.Framework.Business.Counter.Data;
using VPC.Framework.Business.MetadataManager.Contracts;
using VPC.Framework.Business.Role.Data;

namespace VPC.Framework.Business.Counter.Contracts
{
    public interface IManagerCounter
    {
        bool Create(Guid tenantId, CounterInfo info);
        bool Update(Guid tenantId, CounterInfo info);
        bool Delete(Guid tenantId, Guid counterId);

        CounterInfo GetCounter(Guid tenantId, Guid counterId);
        CounterInfo GetCounters(Guid tenantId, string entityName);

    }

    public class ManagerCounter : IManagerCounter
    {
        private readonly Logger _log = LogManager.GetCurrentClassLogger();
        private readonly IAdminCounter _adminCounter = new AdminCounter();
        private readonly IReviewCounter _reviewCounter = new ReviewCounter();

        // public ManagerBatchType () { }

        bool IManagerCounter.Create(Guid tenantId, CounterInfo info)
        {
            IMetadataManager _iMetadataManager = new VPC.Framework.Business.MetadataManager.Contracts.MetadataManager();
            string entityId = _iMetadataManager.GetEntityContextByEntityName(info.EntityName);
            info.CounterId = Guid.NewGuid();
            return _adminCounter.Create(tenantId, info, entityId);
        }

        bool IManagerCounter.Update(Guid tenantId, CounterInfo info)
        {
            IMetadataManager _iMetadataManager = new VPC.Framework.Business.MetadataManager.Contracts.MetadataManager();
            string entityId = _iMetadataManager.GetEntityContextByEntityName(info.EntityName);
            return _adminCounter.Update(tenantId, info, entityId);
        }

        bool IManagerCounter.Delete(Guid tenantId, Guid counterId)
        {
            return _adminCounter.Delete(tenantId, counterId);
        }
        CounterInfo IManagerCounter.GetCounter(Guid tenantId, Guid counterId)
        {
            return _reviewCounter.GetCounter(tenantId, counterId);
        }
       CounterInfo IManagerCounter.GetCounters(Guid tenantId, string entityName)
        {
            IMetadataManager _iMetadataManager = new VPC.Framework.Business.MetadataManager.Contracts.MetadataManager();
            string entityId = _iMetadataManager.GetEntityContextByEntityName(entityName);
            return _reviewCounter.GetCounters(tenantId, entityId);
        }
    }
}