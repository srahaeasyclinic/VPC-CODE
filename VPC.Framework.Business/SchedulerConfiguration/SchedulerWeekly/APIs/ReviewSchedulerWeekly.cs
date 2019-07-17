using System;
using VPC.Entities.SchedulerConfiguration;
using VPC.Framework.Business.SchedulerConfiguration.SchedulerWeekly.Data;

namespace VPC.Framework.Business.SchedulerConfiguration.SchedulerWeekly.APIs
{
 public interface IReviewSchedulerWeekly
    {        
      SchedulerWeeklyInfo GetSchedulerWeekly(Guid tenantId,Guid schedulerId );
    }
    internal  class ReviewSchedulerWeekly : IReviewSchedulerWeekly
    {
        private readonly DataSchedulerWeekly _data = new DataSchedulerWeekly();

        SchedulerWeeklyInfo IReviewSchedulerWeekly.GetSchedulerWeekly(Guid tenantId, Guid schedulerId)
        {
           return _data.GetSchedulerWeekly(tenantId,schedulerId);
        }
    }
}