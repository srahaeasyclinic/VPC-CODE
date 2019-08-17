using System;
using System.Data;
using System.Data.SqlClient;
using VPC.Framework.Business.Data;
using VPC.Entities.SchedulerConfiguration;
using VPC.Entities.BatchType;
using VPC.Metadata.Business.DataTypes;

namespace VPC.Framework.Business.SchedulerConfiguration.Scheduler.Data
{
    internal sealed class DataScheduler : EntityModelData
    {
    //      #region Manage
    //     internal bool Create(Guid tenantId, SchedulerInfo info)
    //     {
    //         try
    //         {
    //             var cmd = CreateProcedureCommand("dbo.Scheduler_Create");
    //             cmd.AppendGuid("@guidTenantId", tenantId);
    //             cmd.AppendGuid("@guidSchedulerId", info.SchedulerId);
    //             cmd.AppendGuid("@guidBatchTypeId", info.BatchTypeId);
    //             cmd.AppendInt("@intSyncTime", info.SyncTime);   
    //             cmd.AppendTinyInt("@intRecurrenceType", (byte)info.RecurrenceType);               
    //             ExecuteCommand(cmd);
    //             return true;               
    //         }
    //         catch (SqlException e)
    //         {
    //             throw ReportAndTranslateException(e, "DataScheduler::Scheduler_Create");
    //         }
    //     } 

    //     internal bool Update(Guid tenantId, SchedulerInfo info)
    //     {
    //         try
    //         {
    //             var cmd = CreateProcedureCommand("dbo.Scheduler_Update");
    //             cmd.AppendGuid("@guidTenantId", tenantId);
    //             cmd.AppendGuid("@guidSchedulerId", info.SchedulerId);
    //             cmd.AppendGuid("@guidBatchTypeId", info.BatchTypeId);
    //             cmd.AppendInt("@intSyncTime", info.SyncTime); 
    //             cmd.AppendTinyInt("@intRecurrenceType", (byte)info.RecurrenceType);              
    //             ExecuteCommand(cmd);
    //             return true;               
    //         }
    //         catch (SqlException e)
    //         {
    //             throw ReportAndTranslateException(e, "DataScheduler::Scheduler_Update");
    //         }
    //     }        

    //  #endregion
  
  

    #region Review
      internal BatchTypeScheduler GetScheduler(Guid tenantId,Guid schedulerId )
        {
            BatchTypeScheduler info =null;
            try
            {
                var cmd = CreateProcedureCommand("dbo.Scheduler_GetBy_BatchId");
                cmd.AppendGuid("@guidTenantId", tenantId);
                cmd.AppendGuid("@guidSchedulerId", schedulerId);
                using (SqlDataReader reader = ExecuteCommandAndReturnReader(cmd))
                { 
                        while (reader.Read())
                        {
                            info=ReadInfo(reader);
                        } 
                }
            }
            catch (SqlException e)
            {
                throw ReportAndTranslateException(e, "DataScheduler::Scheduler_GetBy_BatchId");
            }
            return info;
        }

        private static BatchTypeScheduler ReadInfo(SqlDataReader reader)
        {
           var info = new BatchTypeScheduler();
            
           var id=reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0);
           BatchIntervalType interval  = reader.IsDBNull(1) ? BatchIntervalType.Daily : (BatchIntervalType)reader.GetByte(1);
           byte hour  = reader.IsDBNull(2) ? (byte)0 : reader.GetByte(2);
           byte minute  = reader.IsDBNull(3) ? (byte)0 : reader.GetByte(3);
           SchedulerDailyUnitType? dailyUnit  = reader.IsDBNull(4) ? (SchedulerDailyUnitType?)null : (SchedulerDailyUnitType)reader.GetByte(4);            
           int dailyEveryDay =reader.IsDBNull(5) ? 1 : reader.GetInt32(5);

           byte? weeklyRecurunce =reader.IsDBNull(6) ? (byte?)null : reader.GetByte(6);
           bool monday= reader.IsDBNull(7) ? false : reader.GetBoolean(7);
           bool tuesday= reader.IsDBNull(8) ? false : reader.GetBoolean(8);
           bool wednesday= reader.IsDBNull(9) ? false : reader.GetBoolean(9);
           bool thrusday= reader.IsDBNull(10) ? false : reader.GetBoolean(10);
           bool friday= reader.IsDBNull(11) ? false : reader.GetBoolean(11);
           bool saturday= reader.IsDBNull(12) ? false : reader.GetBoolean(12);
           bool sunday= reader.IsDBNull(13) ? false : reader.GetBoolean(13);

           SchedulerMonthlyUnitType? monthlyUnit  = reader.IsDBNull(14) ? (SchedulerMonthlyUnitType?)null : (SchedulerMonthlyUnitType?)reader.GetByte(14);
           byte? monthlySpecificDay  = reader.IsDBNull(15) ? (byte?)null : reader.GetByte(15);
           byte? monthlySpecificMonth  = reader.IsDBNull(16) ? (byte?)null : reader.GetByte(16);
           byte? monthlyInferredWeekGroup  = reader.IsDBNull(17) ? (byte?)null : reader.GetByte(17);
           byte? monthlyInferredDay  = reader.IsDBNull(18) ? (byte?)null : reader.GetByte(18);
           byte? monthlyInferredMonth = reader.IsDBNull(19) ? (byte?)null : reader.GetByte(19);

           SchedulerYearlyUnitType? yearlyUnit  = reader.IsDBNull(20) ? (SchedulerYearlyUnitType?)null : (SchedulerYearlyUnitType?)reader.GetByte(20);
           int? yearlyRecc =reader.IsDBNull(21) ? (int?)null : reader.GetInt32(21);
           byte? yearlySpecificMonth  = reader.IsDBNull(22) ? (byte?)null : reader.GetByte(22);
           byte? yearlySpecificYear  = reader.IsDBNull(23) ? (byte?)null : reader.GetByte(23);
           byte? yearlyInferredWeekGroup  = reader.IsDBNull(24) ? (byte?)null : reader.GetByte(24);
           byte? yearlyInferredDay  = reader.IsDBNull(25) ? (byte?)null : reader.GetByte(25);
           byte? yearlyInferredMonth = reader.IsDBNull(26) ? (byte?)null : reader.GetByte(26);  

            info.InternalId=new InternalId();
            info.InternalId.Value=id.ToString();

            info.Interval=new PickList<BatchInterval>();
            info.Interval.Value = ((int)interval).ToString();

            info.Hour=new PickList<Hour>();
            info.Hour.Value = ((int)hour).ToString();

            info.Minute=new PickList<Minute>();
            info.Minute.Value =((int)minute).ToString(); 

            info.DailyUnit=new PickList<SchedulerDailyUnit>();
            info.DailyUnit.Value = dailyUnit.HasValue ? ((int)dailyUnit).ToString() : string.Empty; 

            info.DailyEveryDay=new NumericType();
            info.DailyEveryDay.Value =  dailyEveryDay.ToString();

            info.WeeklyReccurance=new NumericType();
            info.WeeklyReccurance.Value =  weeklyRecurunce.ToString() ;

            info.WeeklyMonday=new BooleanType();
            info.WeeklyMonday.Value =monday;

            info.WeeklyTuesday=new BooleanType();
            info.WeeklyTuesday.Value =tuesday;

            info.WeeklyWednesday=new BooleanType();
            info.WeeklyWednesday.Value =wednesday;

            info.WeeklyThrusday=new BooleanType();
            info.WeeklyThrusday.Value =thrusday;

            info.WeeklyFriday=new BooleanType();
            info.WeeklyFriday.Value =friday;

            info.WeeklySaturday=new BooleanType();
            info.WeeklySaturday.Value =saturday;

            info.WeeklySunday=new BooleanType();
            info.WeeklySunday.Value =sunday;

            info.MonthlyUnit=new PickList<SchedulerMonthlyUnit>();
            info.MonthlyUnit.Value =monthlyUnit.HasValue ?  ((int)monthlyUnit).ToString() : string.Empty;

            info.MonthlySpecificDay=new NumericType();
            info.MonthlySpecificDay.Value =  monthlySpecificDay.ToString();

            info.MonthlySpecificMonth=new NumericType();
            info.MonthlySpecificMonth.Value =  monthlySpecificMonth.ToString();

            info.MonthlyInferredWeekGroup=new PickList<Week>();
            info.MonthlyInferredWeekGroup.Value = monthlyInferredWeekGroup.ToString();

            info.MonthlyInferredDay=new PickList<Day>();
            info.MonthlyInferredDay.Value =  monthlyInferredDay.ToString() ;

            info.MonthlyInferredMonth=new NumericType();
            info.MonthlyInferredMonth.Value =  monthlyInferredMonth.ToString();


          //==============================

            info.YearlyReccurance=new NumericType();
            info.YearlyReccurance.Value =  yearlyRecc.ToString();

            info.YearlyUnit=new PickList<SchedulerYearlyUnit>();
            info.YearlyUnit.Value =   yearlyUnit.HasValue ?  ((int)yearlyUnit).ToString() : string.Empty;

            info.YearlySpecificMonth=new PickList<Month>();
            info.YearlySpecificMonth.Value =  yearlySpecificMonth.ToString() ; 

            info.YearlySpecificYear=new NumericType();
            info.YearlySpecificYear.Value =  yearlySpecificYear.ToString(); 

            info.YearlyInferredWeekGroup=new PickList<Week>();
            info.YearlyInferredWeekGroup.Value = yearlyInferredWeekGroup.ToString();

            info.YearlyInferredDay=new PickList<Day>();
            info.YearlyInferredDay.Value =  yearlyInferredDay.ToString();

            info.YearlyInferredMonth=new PickList<Month>();
            info.YearlyInferredMonth.Value =   yearlyInferredMonth.ToString();

            return info;
        }


         #endregion
       
       
    }
}
