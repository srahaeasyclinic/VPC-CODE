using System;
using VPC.Entities.SchedulerConfiguration;
using VPC.Framework.Business.SchedulerConfiguration.SchedulerDaily.Data;

namespace VPC.Framework.Business.SchedulerConfiguration.SchedulerDaily.APIs
{
 public interface IReviewSchedulerDaily
    {        
      SchedulerDailyInfo GetSchedulerDaily(Guid tenantId,Guid schedulerId );
    }
    internal  class ReviewSchedulerDaily : IReviewSchedulerDaily
    {
        private readonly DataSchedulerDaily _data = new DataSchedulerDaily();

        SchedulerDailyInfo IReviewSchedulerDaily.GetSchedulerDaily(Guid tenantId, Guid schedulerId)
        {
           return _data.GetSchedulerDaily(tenantId,schedulerId);
        }
    }
}