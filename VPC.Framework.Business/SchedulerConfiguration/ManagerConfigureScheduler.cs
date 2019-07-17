using System;
using System.Transactions;
using VPC.Entities.SchedulerConfiguration;
using VPC.Framework.Business.SchedulerConfiguration.Scheduler.Contracts;
using VPC.Framework.Business.SchedulerConfiguration.SchedulerDaily.Contracts;
using VPC.Framework.Business.SchedulerConfiguration.SchedulerMonthly.Contracts;
using VPC.Framework.Business.SchedulerConfiguration.SchedulerWeekly.Contracts;
using VPC.Framework.Business.SchedulerConfiguration.SchedulerYearly.Contracts;

namespace VPC.Framework.Business.SchedulerConfiguration
{
    public interface IManagerConfigureScheduler
    {
        Guid ConfigureScheduler(Guid tenantId, SchedulerInfo info);
        SchedulerInfo GetConfigureScheduler(Guid tenantId, Guid batchTypeId);

    }
    public class ManagerConfigureScheduler : IManagerConfigureScheduler
    {
        private readonly IManagerScheduler _managerScheduler = new ManagerScheduler();
        private readonly IManagerSchedulerDaily _managerDaily = new ManagerSchedulerDaily();
        private readonly IManagerSchedulerMonthly _managerMonthly = new ManagerSchedulerMonthly();
        private readonly IManagerSchedulerWeekly _managerWeekly = new ManagerSchedulerWeekly();
        private readonly IManagerSchedulerYearly _managerYearly = new ManagerSchedulerYearly();

        Guid IManagerConfigureScheduler.ConfigureScheduler(Guid tenantId, SchedulerInfo info)
        {
            try
            {
                using (var scope1 = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (info.SchedulerId == Guid.Empty)
                        info.SchedulerId = _managerScheduler.Create(tenantId, info);
                    else
                        _managerScheduler.Update(tenantId, info);

                    _managerDaily.Delete(tenantId, info.SchedulerId);
                    _managerWeekly.Delete(tenantId, info.SchedulerId);
                    _managerMonthly.Delete(tenantId, info.SchedulerId);
                    _managerYearly.Delete(tenantId, info.SchedulerId);

                    if (info.RecurrenceType == RecurrencePattern.Daily)
                    {
                        info.Daily.SchedulerId = info.SchedulerId;
                        _managerDaily.Create(tenantId, info.Daily);

                    }
                    else if (info.RecurrenceType == RecurrencePattern.Weekly)
                    {
                        info.Weekly.SchedulerId = info.SchedulerId;
                        _managerWeekly.Create(tenantId, info.Weekly);
                    }
                    else if (info.RecurrenceType == RecurrencePattern.Monthly)
                    {
                        info.Monthly.SchedulerId = info.SchedulerId;
                        _managerMonthly.Create(tenantId, info.Monthly);
                    }
                    else if (info.RecurrenceType == RecurrencePattern.Yearly)
                    {
                        info.Yearly.SchedulerId = info.SchedulerId;
                        _managerYearly.Create(tenantId, info.Yearly);
                    }
                    scope1.Complete();
                }
            }

            catch
            {
                throw;
            }
            return info.SchedulerId;
        }

        SchedulerInfo IManagerConfigureScheduler.GetConfigureScheduler(Guid tenantId, Guid batchTypeId)
        {
            var info = _managerScheduler.GetScheduler(tenantId, batchTypeId);
            if (info == null)
            {
                info = new SchedulerInfo { BatchTypeId = batchTypeId };
                info.Daily = new SchedulerDailyInfo();
                info.Weekly = new SchedulerWeeklyInfo();
                info.Monthly = new SchedulerMonthlyInfo();
                info.Yearly = new SchedulerYearlyInfo();
            }

            if (info != null)
            {
                info.Daily = new SchedulerDailyInfo();
                info.Weekly = new SchedulerWeeklyInfo();
                info.Monthly = new SchedulerMonthlyInfo();
                info.Yearly = new SchedulerYearlyInfo();
                if (info.RecurrenceType == RecurrencePattern.Daily)
                {
                    info.Daily = _managerDaily.GetSchedulerDaily(tenantId, info.SchedulerId);
                }
                else if (info.RecurrenceType == RecurrencePattern.Weekly)
                {
                    info.Weekly = _managerWeekly.GetSchedulerWeekly(tenantId, info.SchedulerId);
                }
                else if (info.RecurrenceType == RecurrencePattern.Monthly)
                {
                    info.Monthly = _managerMonthly.GetSchedulerMonthly(tenantId, info.SchedulerId);
                }
                else if (info.RecurrenceType == RecurrencePattern.Yearly)
                {
                    info.Yearly = _managerYearly.GetSchedulerYearly(tenantId, info.SchedulerId);
                }

            }
            return info;
        }
    }
}