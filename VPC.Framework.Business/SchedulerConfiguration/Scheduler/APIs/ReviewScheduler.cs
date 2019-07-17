using System;
using VPC.Entities.SchedulerConfiguration;
using VPC.Framework.Business.SchedulerConfiguration.Scheduler.Data;

namespace VPC.Framework.Business.SchedulerConfiguration.Scheduler.APIs
{
 public interface IReviewScheduler
    {        
      SchedulerInfo GetScheduler(Guid tenantId,Guid batchTypeId );
    }
    internal  class ReviewScheduler : IReviewScheduler
    {
        private readonly DataScheduler _data = new DataScheduler();

        SchedulerInfo IReviewScheduler.GetScheduler(Guid tenantId, Guid batchTypeId)
        {
           return _data.GetScheduler(tenantId,batchTypeId);
        }
    }
}