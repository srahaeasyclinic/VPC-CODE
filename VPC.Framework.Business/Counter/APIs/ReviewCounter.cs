using System;
using System.Collections.Generic;
using VPC.Entities.Counter;
using VPC.Entities.Role;
using VPC.Framework.Business.Counter.Data;
using VPC.Framework.Business.Role.Data;

namespace VPC.Framework.Business.Counter.APIs {
    public interface IReviewCounter {

        CounterInfo GetCounter (Guid tenantId, Guid counterId);
     CounterInfo GetCounters (Guid tenantId,string entityId);
    }

    internal class ReviewCounter : IReviewCounter {
        private readonly DataCounter _data = new DataCounter ();

        CounterInfo IReviewCounter.GetCounter (Guid tenantId, Guid counterId) {
            return _data.GetCounter (tenantId, counterId);
        }

        CounterInfo IReviewCounter.GetCounters (Guid tenantId,string entityId) {
            return _data.GetCounters (tenantId,entityId);
        }

    }
}