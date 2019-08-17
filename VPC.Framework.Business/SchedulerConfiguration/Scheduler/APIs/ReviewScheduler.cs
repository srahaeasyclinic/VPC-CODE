using System;
using VPC.Entities.BatchType;
using VPC.Entities.SchedulerConfiguration;
using VPC.Framework.Business.SchedulerConfiguration.Scheduler.Data;

namespace VPC.Framework.Business.SchedulerConfiguration.Scheduler.APIs
{
 public interface IReviewScheduler
    {        
      BatchTypeScheduler GetScheduler(Guid tenantId,Guid schedulerId );
    }
    internal  class ReviewScheduler : IReviewScheduler
    {
        private readonly DataScheduler _data = new DataScheduler();

        BatchTypeScheduler IReviewScheduler.GetScheduler(Guid tenantId, Guid schedulerId)
        {
           return _data.GetScheduler(tenantId,schedulerId);
        }
    }
}