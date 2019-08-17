using System;
using VPC.Entities.EntityCore.Metadata.Picklist;
using VPC.Entities.EntityCore.Metadata.Picklist.ExtendedPicklistValue;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Entities.SchedulerConfiguration;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;

namespace VPC.Entities.BatchType
{

    [TableProperties("[dbo].[BatchTypeScheduler]", "[Id]")]
    [DisplayName("Scheduler")]
    [PluralName("Schedulers")]
    [CascadeDelete]
     public class BatchTypeScheduler   : ExtendedEntity
        {
        [NonQueryable]
        [ColumnName("[TenantId]")]
        [NotNull]
        [DisplayName("Tenant Id")]
        public InternalId TenantId { get; set; }

        [AccessibleLayout((int)LayoutType.View,(int)LayoutType.List)]
        [NonQueryable]
        [ColumnName("[Id]")]
        [NotNull]
        [DisplayName("Internal Id")]
        public override InternalId InternalId { get; set; }

        public override EntityContext EntityContext { get; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[Interval]")]
        [DisplayName("Interval")]
        // [NotNull]
        public PickList<BatchInterval> Interval { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[Hour]")]
       // [NotNull]
        [DisplayName("Hour")]
        public PickList<Hour> Hour { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[Minute]")]
        //[NotNull]
         [DisplayName("Minute")]
        public PickList<Minute> Minute { get; set; }

        // [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        // [ColumnName("Unit")]
        // public PickList<SchedulerUnit> Unit { get; set; }

        //--------------Daily----------------

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[DailyUnit]")]
        [DisplayName("Daily unit")]
        public PickList<SchedulerDailyUnit> DailyUnit { get; set; }

        
         [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[DailyEveryDay]")]
        [DisplayName("Daily every day")]
        public NumericType DailyEveryDay { get; set; }

        //----------------------Weekly--------------------------
        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[WeeklyReccurance]")]
        [DisplayName("Weekly reccurance")]
        public NumericType WeeklyReccurance { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[WeeklyMonday]")]
         [DisplayName("Weekly monday")]
        public BooleanType WeeklyMonday { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[WeeklyTuesday]")]
        [DisplayName("Weekly tuesday")]
        public BooleanType WeeklyTuesday { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[WeeklyWednesday]")]
        [DisplayName("Weekly wednesday")]
        public BooleanType WeeklyWednesday { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[WeeklyThrusday]")]
        [DisplayName("Weekly thrusday")]
        public BooleanType WeeklyThrusday { get; set; }
        
        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[WeeklyFriday]")]
        [DisplayName("Weekly friday")]
        public BooleanType WeeklyFriday { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[WeeklySaturday]")]
        [DisplayName("Weekly saturday")]
        public BooleanType WeeklySaturday { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[WeeklySunday]")]
        [DisplayName("Weekly sunday")]
        public BooleanType WeeklySunday { get; set; }       


        //------------------------Monthly-----------------------------

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[MonthlyUnit]")]
        [DisplayName("Monthly unit")]
        public PickList<SchedulerMonthlyUnit> MonthlyUnit { get; set; }

        
        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[MonthlySpecificDay]")]
        [DisplayName("Monthly specific day")]
        public NumericType MonthlySpecificDay { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[MonthlySpecificMonth]")]
        [DisplayName("Monthly specific month")]
        public NumericType MonthlySpecificMonth { get; set; }


        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[MonthlyInferredWeekGroup]")]
        [DisplayName("Monthly inferred week group")]
        public PickList<Week> MonthlyInferredWeekGroup { get; set; }


        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[MonthlyInferredDay]")]
        [DisplayName("Monthly inferred day")]
        public PickList<Day> MonthlyInferredDay { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[MonthlyInferredMonth]")]
        [DisplayName("Monthly inferred month")]
        public NumericType MonthlyInferredMonth { get; set; }

        //---------------------------Yearly

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[YearlyUnit]")]
        [DisplayName("Yearly unit")]
        public PickList<SchedulerYearlyUnit> YearlyUnit { get; set; }

       
        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[YearlyReccurance]")]
        [DisplayName("Yearly reccurance")]
        public NumericType YearlyReccurance { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[YearlySpecificMonth]")]
        [DisplayName("Yearly specific month")]
        public PickList<Month> YearlySpecificMonth { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[YearlySpecificYear]")]
        [DisplayName("Yearly specific year")]
        public NumericType YearlySpecificYear { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[YearlyInferredWeekGroup]")]
        [DisplayName("Yearly inferredWeek group")]
         public PickList<Week> YearlyInferredWeekGroup { get; set; }


        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[YearlyInferredDay]")]
        [DisplayName("Yearly inferred day")]
        public PickList<Day> YearlyInferredDay { get; set; }

        [AccessibleLayout((int)LayoutType.View, (int)LayoutType.Form, (int)LayoutType.List)]
        [ColumnName("[YearlyInferredMonth]")]
        [DisplayName("Yearly inferred month")]
        public PickList<Month> YearlyInferredMonth { get; set; }


        }
}
