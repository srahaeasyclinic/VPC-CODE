/****** Object:  UserDefinedDataType [dbo].[xSmallText]    Script Date: 15-Jul-19 15:42:25 PM ******/
IF  EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'xSmallText' AND ss.name = N'dbo')
DROP TYPE [dbo].[xSmallText]
GO
/****** Object:  UserDefinedDataType [dbo].[xLargeText]    Script Date: 15-Jul-19 15:42:25 PM ******/
IF  EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'xLargeText' AND ss.name = N'dbo')
DROP TYPE [dbo].[xLargeText]
GO
/****** Object:  UserDefinedDataType [dbo].[url]    Script Date: 15-Jul-19 15:42:25 PM ******/
IF  EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'url' AND ss.name = N'dbo')
DROP TYPE [dbo].[url]
GO
/****** Object:  UserDefinedDataType [dbo].[smallText]    Script Date: 15-Jul-19 15:42:25 PM ******/
IF  EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'smallText' AND ss.name = N'dbo')
DROP TYPE [dbo].[smallText]
GO
/****** Object:  UserDefinedDataType [dbo].[phone]    Script Date: 15-Jul-19 15:42:25 PM ******/
IF  EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'phone' AND ss.name = N'dbo')
DROP TYPE [dbo].[phone]
GO
/****** Object:  UserDefinedDataType [dbo].[Password]    Script Date: 15-Jul-19 15:42:25 PM ******/
IF  EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'Password' AND ss.name = N'dbo')
DROP TYPE [dbo].[Password]
GO
/****** Object:  UserDefinedDataType [dbo].[mediumText]    Script Date: 15-Jul-19 15:42:25 PM ******/
IF  EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'mediumText' AND ss.name = N'dbo')
DROP TYPE [dbo].[mediumText]
GO
/****** Object:  UserDefinedDataType [dbo].[largeText]    Script Date: 15-Jul-19 15:42:25 PM ******/
IF  EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'largeText' AND ss.name = N'dbo')
DROP TYPE [dbo].[largeText]
GO
/****** Object:  UserDefinedDataType [dbo].[email]    Script Date: 15-Jul-19 15:42:25 PM ******/
IF  EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'email' AND ss.name = N'dbo')
DROP TYPE [dbo].[email]
GO
/****** Object:  UserDefinedDataType [dbo].[amount]    Script Date: 15-Jul-19 15:42:25 PM ******/
IF  EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'amount' AND ss.name = N'dbo')
DROP TYPE [dbo].[amount]
GO
/****** Object:  UserDefinedDataType [dbo].[amount]    Script Date: 15-Jul-19 15:42:25 PM ******/
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'amount' AND ss.name = N'dbo')
CREATE TYPE [dbo].[amount] FROM [decimal](12, 2) NULL
GO
/****** Object:  UserDefinedDataType [dbo].[email]    Script Date: 15-Jul-19 15:42:25 PM ******/
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'email' AND ss.name = N'dbo')
CREATE TYPE [dbo].[email] FROM [nvarchar](255) NOT NULL
GO
/****** Object:  UserDefinedDataType [dbo].[largeText]    Script Date: 15-Jul-19 15:42:25 PM ******/
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'largeText' AND ss.name = N'dbo')
CREATE TYPE [dbo].[largeText] FROM [nvarchar](1000) NULL
GO
/****** Object:  UserDefinedDataType [dbo].[mediumText]    Script Date: 15-Jul-19 15:42:25 PM ******/
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'mediumText' AND ss.name = N'dbo')
CREATE TYPE [dbo].[mediumText] FROM [nvarchar](255) NULL
GO
/****** Object:  UserDefinedDataType [dbo].[Password]    Script Date: 15-Jul-19 15:42:25 PM ******/
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'Password' AND ss.name = N'dbo')
CREATE TYPE [dbo].[Password] FROM [nvarchar](20) NOT NULL
GO
/****** Object:  UserDefinedDataType [dbo].[phone]    Script Date: 15-Jul-19 15:42:25 PM ******/
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'phone' AND ss.name = N'dbo')
CREATE TYPE [dbo].[phone] FROM [nvarchar](25) NULL
GO
/****** Object:  UserDefinedDataType [dbo].[smallText]    Script Date: 15-Jul-19 15:42:25 PM ******/
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'smallText' AND ss.name = N'dbo')
CREATE TYPE [dbo].[smallText] FROM [nvarchar](25) NULL
GO
/****** Object:  UserDefinedDataType [dbo].[url]    Script Date: 15-Jul-19 15:42:25 PM ******/
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'url' AND ss.name = N'dbo')
CREATE TYPE [dbo].[url] FROM [nvarchar](255) NULL
GO
/****** Object:  UserDefinedDataType [dbo].[xLargeText]    Script Date: 15-Jul-19 15:42:25 PM ******/
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'xLargeText' AND ss.name = N'dbo')
CREATE TYPE [dbo].[xLargeText] FROM [nvarchar](max) NULL
GO
/****** Object:  UserDefinedDataType [dbo].[xSmallText]    Script Date: 15-Jul-19 15:42:25 PM ******/
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'xSmallText' AND ss.name = N'dbo')
CREATE TYPE [dbo].[xSmallText] FROM [nvarchar](10) NULL
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowTransitionHistory_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowTransitionHistory]'))
ALTER TABLE [dbo].[WorkFlowTransitionHistory] DROP CONSTRAINT [FK_WorkFlowTransitionHistory_User]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowTransitionHistory_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowTransitionHistory]'))
ALTER TABLE [dbo].[WorkFlowTransitionHistory] DROP CONSTRAINT [FK_WorkFlowTransitionHistory_Tenant]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowTransitionHistory_CreatedBy]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowTransitionHistory]'))
ALTER TABLE [dbo].[WorkFlowTransitionHistory] DROP CONSTRAINT [FK_WorkFlowTransitionHistory_CreatedBy]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowSteps_WorkFlowTransitionHistory]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowTransitionHistory]'))
ALTER TABLE [dbo].[WorkFlowTransitionHistory] DROP CONSTRAINT [FK_WorkFlowSteps_WorkFlowTransitionHistory]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowStep_WorkFlow]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowStep]'))
ALTER TABLE [dbo].[WorkFlowStep] DROP CONSTRAINT [FK_WorkFlowStep_WorkFlow]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowStep_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowStep]'))
ALTER TABLE [dbo].[WorkFlowStep] DROP CONSTRAINT [FK_WorkFlowStep_Tenant]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowStep_WorkFlowRole]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowRole]'))
ALTER TABLE [dbo].[WorkFlowRole] DROP CONSTRAINT [FK_WorkFlowStep_WorkFlowRole]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowRole_WorkFlow]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowRole]'))
ALTER TABLE [dbo].[WorkFlowRole] DROP CONSTRAINT [FK_WorkFlowRole_WorkFlow]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowRole_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowRole]'))
ALTER TABLE [dbo].[WorkFlowRole] DROP CONSTRAINT [FK_WorkFlowRole_Tenant]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowRole_Role]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowRole]'))
ALTER TABLE [dbo].[WorkFlowRole] DROP CONSTRAINT [FK_WorkFlowRole_Role]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowProcessTask_WorkFlowProcess]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowProcessTask]'))
ALTER TABLE [dbo].[WorkFlowProcessTask] DROP CONSTRAINT [FK_WorkFlowProcessTask_WorkFlowProcess]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowProcessTask_WorkFlow]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowProcessTask]'))
ALTER TABLE [dbo].[WorkFlowProcessTask] DROP CONSTRAINT [FK_WorkFlowProcessTask_WorkFlow]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowProcessTask_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowProcessTask]'))
ALTER TABLE [dbo].[WorkFlowProcessTask] DROP CONSTRAINT [FK_WorkFlowProcessTask_Tenant]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowProcess_WorkFlow]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowProcess]'))
ALTER TABLE [dbo].[WorkFlowProcess] DROP CONSTRAINT [FK_WorkFlowProcess_WorkFlow]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowProcess_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowProcess]'))
ALTER TABLE [dbo].[WorkFlowProcess] DROP CONSTRAINT [FK_WorkFlowProcess_Tenant]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowOperation_WorkFlow]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowOperation]'))
ALTER TABLE [dbo].[WorkFlowOperation] DROP CONSTRAINT [FK_WorkFlowOperation_WorkFlow]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowOperation_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowOperation]'))
ALTER TABLE [dbo].[WorkFlowOperation] DROP CONSTRAINT [FK_WorkFlowOperation_Tenant]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowStep_WorkFlowInnerStep]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowInnerStep]'))
ALTER TABLE [dbo].[WorkFlowInnerStep] DROP CONSTRAINT [FK_WorkFlowStep_WorkFlowInnerStep]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowInnerStep_WorkFlow]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowInnerStep]'))
ALTER TABLE [dbo].[WorkFlowInnerStep] DROP CONSTRAINT [FK_WorkFlowInnerStep_WorkFlow]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowInnerStep_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowInnerStep]'))
ALTER TABLE [dbo].[WorkFlowInnerStep] DROP CONSTRAINT [FK_WorkFlowInnerStep_Tenant]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlow_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlow]'))
ALTER TABLE [dbo].[WorkFlow] DROP CONSTRAINT [FK_WorkFlow_Tenant]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkcenterUser_Workcenter]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkcenterUser]'))
ALTER TABLE [dbo].[WorkcenterUser] DROP CONSTRAINT [FK_WorkcenterUser_Workcenter]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkcenterTool_Workcenter]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkcenterTool]'))
ALTER TABLE [dbo].[WorkcenterTool] DROP CONSTRAINT [FK_WorkcenterTool_Workcenter]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkcenterTool_Tool]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkcenterTool]'))
ALTER TABLE [dbo].[WorkcenterTool] DROP CONSTRAINT [FK_WorkcenterTool_Tool]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkcenterEquipment_Workcenter]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkcenterEquipment]'))
ALTER TABLE [dbo].[WorkcenterEquipment] DROP CONSTRAINT [FK_WorkcenterEquipment_Workcenter]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkcenterEquipment_Equipment]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkcenterEquipment]'))
ALTER TABLE [dbo].[WorkcenterEquipment] DROP CONSTRAINT [FK_WorkcenterEquipment_Equipment]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkcenterConfiguration_Workcenter]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkcenterConfiguration]'))
ALTER TABLE [dbo].[WorkcenterConfiguration] DROP CONSTRAINT [FK_WorkcenterConfiguration_Workcenter]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkcenterConfiguration_Warehouse_To]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkcenterConfiguration]'))
ALTER TABLE [dbo].[WorkcenterConfiguration] DROP CONSTRAINT [FK_WorkcenterConfiguration_Warehouse_To]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkcenterConfiguration_Warehouse_From]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkcenterConfiguration]'))
ALTER TABLE [dbo].[WorkcenterConfiguration] DROP CONSTRAINT [FK_WorkcenterConfiguration_Warehouse_From]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkcenterCompetence_Workcenter]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkcenterCompetence]'))
ALTER TABLE [dbo].[WorkcenterCompetence] DROP CONSTRAINT [FK_WorkcenterCompetence_Workcenter]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Workcenter_Vendor]') AND parent_object_id = OBJECT_ID(N'[dbo].[Workcenter]'))
ALTER TABLE [dbo].[Workcenter] DROP CONSTRAINT [FK_Workcenter_Vendor]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WarehouseLocation_Warehouse]') AND parent_object_id = OBJECT_ID(N'[dbo].[WarehouseLocation]'))
ALTER TABLE [dbo].[WarehouseLocation] DROP CONSTRAINT [FK_WarehouseLocation_Warehouse]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserLocation_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserLocation]'))
ALTER TABLE [dbo].[UserLocation] DROP CONSTRAINT [FK_UserLocation_User]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserDepartment_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserDepartment]'))
ALTER TABLE [dbo].[UserDepartment] DROP CONSTRAINT [FK_UserDepartment_User]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserCompany_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserCompany]'))
ALTER TABLE [dbo].[UserCompany] DROP CONSTRAINT [FK_UserCompany_User]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserCompany_Company]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserCompany]'))
ALTER TABLE [dbo].[UserCompany] DROP CONSTRAINT [FK_UserCompany_Company]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_User_UserEmployment]') AND parent_object_id = OBJECT_ID(N'[dbo].[User]'))
ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_User_UserEmployment]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_User_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[User]'))
ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_User_User]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_User_Item]') AND parent_object_id = OBJECT_ID(N'[dbo].[User]'))
ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_User_Item]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TenantSubscriptionEntityDetail_TenantSubscriptionEntity]') AND parent_object_id = OBJECT_ID(N'[dbo].[TenantSubscriptionEntityDetail]'))
ALTER TABLE [dbo].[TenantSubscriptionEntityDetail] DROP CONSTRAINT [FK_TenantSubscriptionEntityDetail_TenantSubscriptionEntity]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TenantSubscriptionEntityDetail_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[TenantSubscriptionEntityDetail]'))
ALTER TABLE [dbo].[TenantSubscriptionEntityDetail] DROP CONSTRAINT [FK_TenantSubscriptionEntityDetail_Tenant]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TenantSubscriptionEntity_TenantSubscription]') AND parent_object_id = OBJECT_ID(N'[dbo].[TenantSubscriptionEntity]'))
ALTER TABLE [dbo].[TenantSubscriptionEntity] DROP CONSTRAINT [FK_TenantSubscriptionEntity_TenantSubscription]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TenantSubscriptionEntity_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[TenantSubscriptionEntity]'))
ALTER TABLE [dbo].[TenantSubscriptionEntity] DROP CONSTRAINT [FK_TenantSubscriptionEntity_Tenant]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TenantSubscription_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[TenantSubscription]'))
ALTER TABLE [dbo].[TenantSubscription] DROP CONSTRAINT [FK_TenantSubscription_Tenant]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TenantIPRange_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[TenantIPRange]'))
ALTER TABLE [dbo].[TenantIPRange] DROP CONSTRAINT [FK_TenantIPRange_Tenant]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tenant_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tenant]'))
ALTER TABLE [dbo].[Tenant] DROP CONSTRAINT [FK_Tenant_Tenant]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tenant_Image]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tenant]'))
ALTER TABLE [dbo].[Tenant] DROP CONSTRAINT [FK_Tenant_Image]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SchedulerYearly_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[SchedulerYearly]'))
ALTER TABLE [dbo].[SchedulerYearly] DROP CONSTRAINT [FK_SchedulerYearly_Tenant]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SchedulerYearly_Scheduler]') AND parent_object_id = OBJECT_ID(N'[dbo].[SchedulerYearly]'))
ALTER TABLE [dbo].[SchedulerYearly] DROP CONSTRAINT [FK_SchedulerYearly_Scheduler]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SchedulerWeekly_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[SchedulerWeekly]'))
ALTER TABLE [dbo].[SchedulerWeekly] DROP CONSTRAINT [FK_SchedulerWeekly_Tenant]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SchedulerWeekly_Scheduler]') AND parent_object_id = OBJECT_ID(N'[dbo].[SchedulerWeekly]'))
ALTER TABLE [dbo].[SchedulerWeekly] DROP CONSTRAINT [FK_SchedulerWeekly_Scheduler]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SchedulerMonthly_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[SchedulerMonthly]'))
ALTER TABLE [dbo].[SchedulerMonthly] DROP CONSTRAINT [FK_SchedulerMonthly_Tenant]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SchedulerMonthly_Scheduler]') AND parent_object_id = OBJECT_ID(N'[dbo].[SchedulerMonthly]'))
ALTER TABLE [dbo].[SchedulerMonthly] DROP CONSTRAINT [FK_SchedulerMonthly_Scheduler]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SchedulerDaily_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[SchedulerDaily]'))
ALTER TABLE [dbo].[SchedulerDaily] DROP CONSTRAINT [FK_SchedulerDaily_Tenant]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SchedulerDaily_Scheduler]') AND parent_object_id = OBJECT_ID(N'[dbo].[SchedulerDaily]'))
ALTER TABLE [dbo].[SchedulerDaily] DROP CONSTRAINT [FK_SchedulerDaily_Scheduler]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Scheduler_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[Scheduler]'))
ALTER TABLE [dbo].[Scheduler] DROP CONSTRAINT [FK_Scheduler_Tenant]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Scheduler_BatchType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Scheduler]'))
ALTER TABLE [dbo].[Scheduler] DROP CONSTRAINT [FK_Scheduler_BatchType]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RouteTemplateOperationWorkcenterTool_Tool]') AND parent_object_id = OBJECT_ID(N'[dbo].[RouteTemplateOperationWorkcenterTool]'))
ALTER TABLE [dbo].[RouteTemplateOperationWorkcenterTool] DROP CONSTRAINT [FK_RouteTemplateOperationWorkcenterTool_Tool]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RouteTemplateOperationWorkcenterTool_RouteTemplateOperationWorkcenter]') AND parent_object_id = OBJECT_ID(N'[dbo].[RouteTemplateOperationWorkcenterTool]'))
ALTER TABLE [dbo].[RouteTemplateOperationWorkcenterTool] DROP CONSTRAINT [FK_RouteTemplateOperationWorkcenterTool_RouteTemplateOperationWorkcenter]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RouteTemplateOperationWorkcenterConfiguration_Warehouse1]') AND parent_object_id = OBJECT_ID(N'[dbo].[RouteTemplateOperationWorkcenterConfiguration]'))
ALTER TABLE [dbo].[RouteTemplateOperationWorkcenterConfiguration] DROP CONSTRAINT [FK_RouteTemplateOperationWorkcenterConfiguration_Warehouse1]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RouteTemplateOperationWorkcenterConfiguration_Warehouse]') AND parent_object_id = OBJECT_ID(N'[dbo].[RouteTemplateOperationWorkcenterConfiguration]'))
ALTER TABLE [dbo].[RouteTemplateOperationWorkcenterConfiguration] DROP CONSTRAINT [FK_RouteTemplateOperationWorkcenterConfiguration_Warehouse]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RouteTemplateOperationWorkcenterConfiguration_RouteTemplateOperationWorkcenter]') AND parent_object_id = OBJECT_ID(N'[dbo].[RouteTemplateOperationWorkcenterConfiguration]'))
ALTER TABLE [dbo].[RouteTemplateOperationWorkcenterConfiguration] DROP CONSTRAINT [FK_RouteTemplateOperationWorkcenterConfiguration_RouteTemplateOperationWorkcenter]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RouteTemplateOperationWorkcenterCompetence_RouteTemplateOperationWorkcenter]') AND parent_object_id = OBJECT_ID(N'[dbo].[RouteTemplateOperationWorkcenterCompetence]'))
ALTER TABLE [dbo].[RouteTemplateOperationWorkcenterCompetence] DROP CONSTRAINT [FK_RouteTemplateOperationWorkcenterCompetence_RouteTemplateOperationWorkcenter]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RouteTemplateOperationWorkcenter_Workcenter]') AND parent_object_id = OBJECT_ID(N'[dbo].[RouteTemplateOperationWorkcenter]'))
ALTER TABLE [dbo].[RouteTemplateOperationWorkcenter] DROP CONSTRAINT [FK_RouteTemplateOperationWorkcenter_Workcenter]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RouteTemplateOperationWorkcenter_RouteTemplateOperation]') AND parent_object_id = OBJECT_ID(N'[dbo].[RouteTemplateOperationWorkcenter]'))
ALTER TABLE [dbo].[RouteTemplateOperationWorkcenter] DROP CONSTRAINT [FK_RouteTemplateOperationWorkcenter_RouteTemplateOperation]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RouteTemplateOperation_RouteTemplate]') AND parent_object_id = OBJECT_ID(N'[dbo].[RouteTemplateOperation]'))
ALTER TABLE [dbo].[RouteTemplateOperation] DROP CONSTRAINT [FK_RouteTemplateOperation_RouteTemplate]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Room_RoomType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Room]'))
ALTER TABLE [dbo].[Room] DROP CONSTRAINT [FK_Room_RoomType]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Room_Location]') AND parent_object_id = OBJECT_ID(N'[dbo].[Room]'))
ALTER TABLE [dbo].[Room] DROP CONSTRAINT [FK_Room_Location]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Role_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[Role]'))
ALTER TABLE [dbo].[Role] DROP CONSTRAINT [FK_Role_Tenant]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductVendor_Vendor]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductVendor]'))
ALTER TABLE [dbo].[ProductVendor] DROP CONSTRAINT [FK_ProductVendor_Vendor]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductVariantRuleAttributeValue_ProductVariantRuleAttribute]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductVariantRuleAttributeValue]'))
ALTER TABLE [dbo].[ProductVariantRuleAttributeValue] DROP CONSTRAINT [FK_ProductVariantRuleAttributeValue_ProductVariantRuleAttribute]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductVariantRuleAttributeValue_AttributeValue]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductVariantRuleAttributeValue]'))
ALTER TABLE [dbo].[ProductVariantRuleAttributeValue] DROP CONSTRAINT [FK_ProductVariantRuleAttributeValue_AttributeValue]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductVariantRuleAttribute_ProductVariantRuleAttribute]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductVariantRuleAttribute]'))
ALTER TABLE [dbo].[ProductVariantRuleAttribute] DROP CONSTRAINT [FK_ProductVariantRuleAttribute_ProductVariantRuleAttribute]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductVariantRuleAttribute_ProductVariantRule]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductVariantRuleAttribute]'))
ALTER TABLE [dbo].[ProductVariantRuleAttribute] DROP CONSTRAINT [FK_ProductVariantRuleAttribute_ProductVariantRule]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductVariantAttributeValue_ProductVariantAttribute]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductVariantAttributeValue]'))
ALTER TABLE [dbo].[ProductVariantAttributeValue] DROP CONSTRAINT [FK_ProductVariantAttributeValue_ProductVariantAttribute]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductVariantAttributeValue_AttributeValue]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductVariantAttributeValue]'))
ALTER TABLE [dbo].[ProductVariantAttributeValue] DROP CONSTRAINT [FK_ProductVariantAttributeValue_AttributeValue]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductVariantAttribute_ProductVariant]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductVariantAttribute]'))
ALTER TABLE [dbo].[ProductVariantAttribute] DROP CONSTRAINT [FK_ProductVariantAttribute_ProductVariant]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductRoute_BOM]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductRoute]'))
ALTER TABLE [dbo].[ProductRoute] DROP CONSTRAINT [FK_ProductRoute_BOM]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductOperationWorkcenterConfiguration_WarehouseProduced]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductOperationWorkcenterConfiguration]'))
ALTER TABLE [dbo].[ProductOperationWorkcenterConfiguration] DROP CONSTRAINT [FK_ProductOperationWorkcenterConfiguration_WarehouseProduced]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductOperationWorkcenterConfiguration_Warehouse]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductOperationWorkcenterConfiguration]'))
ALTER TABLE [dbo].[ProductOperationWorkcenterConfiguration] DROP CONSTRAINT [FK_ProductOperationWorkcenterConfiguration_Warehouse]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductOperationWorkcenterConfiguration_ProductOperationWorkcenter]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductOperationWorkcenterConfiguration]'))
ALTER TABLE [dbo].[ProductOperationWorkcenterConfiguration] DROP CONSTRAINT [FK_ProductOperationWorkcenterConfiguration_ProductOperationWorkcenter]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductOperationWorkcenterCompetence_ProductOperationWorkcenter]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductOperationWorkcenterCompetence]'))
ALTER TABLE [dbo].[ProductOperationWorkcenterCompetence] DROP CONSTRAINT [FK_ProductOperationWorkcenterCompetence_ProductOperationWorkcenter]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductOperationWorkcenter_Workcenter]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductOperationWorkcenter]'))
ALTER TABLE [dbo].[ProductOperationWorkcenter] DROP CONSTRAINT [FK_ProductOperationWorkcenter_Workcenter]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductOperationWorkcenter_ProductOperation]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductOperationWorkcenter]'))
ALTER TABLE [dbo].[ProductOperationWorkcenter] DROP CONSTRAINT [FK_ProductOperationWorkcenter_ProductOperation]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductOperation_ProductRoute]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductOperation]'))
ALTER TABLE [dbo].[ProductOperation] DROP CONSTRAINT [FK_ProductOperation_ProductRoute]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionTask_ProductionOrderOperation]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionTask]'))
ALTER TABLE [dbo].[ProductionTask] DROP CONSTRAINT [FK_ProductionTask_ProductionOrderOperation]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionTask_Collection]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionTask]'))
ALTER TABLE [dbo].[ProductionTask] DROP CONSTRAINT [FK_ProductionTask_Collection]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionSetProduced_ProductionTask]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionSetProduced]'))
ALTER TABLE [dbo].[ProductionSetProduced] DROP CONSTRAINT [FK_ProductionSetProduced_ProductionTask]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionSetProduced_ProductionSet]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionSetProduced]'))
ALTER TABLE [dbo].[ProductionSetProduced] DROP CONSTRAINT [FK_ProductionSetProduced_ProductionSet]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionSetConsumed_ProductionTask]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionSetConsumed]'))
ALTER TABLE [dbo].[ProductionSetConsumed] DROP CONSTRAINT [FK_ProductionSetConsumed_ProductionTask]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionSetConsumed_ProductionSetProduced]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionSetConsumed]'))
ALTER TABLE [dbo].[ProductionSetConsumed] DROP CONSTRAINT [FK_ProductionSetConsumed_ProductionSetProduced]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionSetConsumed_ProductionSet]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionSetConsumed]'))
ALTER TABLE [dbo].[ProductionSetConsumed] DROP CONSTRAINT [FK_ProductionSetConsumed_ProductionSet]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionSet_ProductionTask]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionSet]'))
ALTER TABLE [dbo].[ProductionSet] DROP CONSTRAINT [FK_ProductionSet_ProductionTask]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionOrderProduced_ProductionOrder]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionOrderProduced]'))
ALTER TABLE [dbo].[ProductionOrderProduced] DROP CONSTRAINT [FK_ProductionOrderProduced_ProductionOrder]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionOrderOperationTool_Tool]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionOrderOperationTool]'))
ALTER TABLE [dbo].[ProductionOrderOperationTool] DROP CONSTRAINT [FK_ProductionOrderOperationTool_Tool]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionOrderOperationTool_ProductionOrderOperation]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionOrderOperationTool]'))
ALTER TABLE [dbo].[ProductionOrderOperationTool] DROP CONSTRAINT [FK_ProductionOrderOperationTool_ProductionOrderOperation]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionOrderOperationConfiguration_ProductionOrderOperation]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionOrderOperationConfiguration]'))
ALTER TABLE [dbo].[ProductionOrderOperationConfiguration] DROP CONSTRAINT [FK_ProductionOrderOperationConfiguration_ProductionOrderOperation]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionOrderOperationCompetence_ProductionOrderOperation]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionOrderOperationCompetence]'))
ALTER TABLE [dbo].[ProductionOrderOperationCompetence] DROP CONSTRAINT [FK_ProductionOrderOperationCompetence_ProductionOrderOperation]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionOrderOperation_ProductionOrder]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionOrderOperation]'))
ALTER TABLE [dbo].[ProductionOrderOperation] DROP CONSTRAINT [FK_ProductionOrderOperation_ProductionOrder]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionOrderComponent_ProductVariant]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionOrderComponent]'))
ALTER TABLE [dbo].[ProductionOrderComponent] DROP CONSTRAINT [FK_ProductionOrderComponent_ProductVariant]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionOrderComponent_ProductionOrderOperation]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionOrderComponent]'))
ALTER TABLE [dbo].[ProductionOrderComponent] DROP CONSTRAINT [FK_ProductionOrderComponent_ProductionOrderOperation]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionOrderComponent_ProductionOrder]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionOrderComponent]'))
ALTER TABLE [dbo].[ProductionOrderComponent] DROP CONSTRAINT [FK_ProductionOrderComponent_ProductionOrder]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionOrderAlternativeComponent_Vendor]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionOrderAlternativeComponent]'))
ALTER TABLE [dbo].[ProductionOrderAlternativeComponent] DROP CONSTRAINT [FK_ProductionOrderAlternativeComponent_Vendor]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionOrderAlternativeComponent_ProductVariant]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionOrderAlternativeComponent]'))
ALTER TABLE [dbo].[ProductionOrderAlternativeComponent] DROP CONSTRAINT [FK_ProductionOrderAlternativeComponent_ProductVariant]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionOrderAlternativeComponent_ProductionOrderComponent]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionOrderAlternativeComponent]'))
ALTER TABLE [dbo].[ProductionOrderAlternativeComponent] DROP CONSTRAINT [FK_ProductionOrderAlternativeComponent_ProductionOrderComponent]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionOrder_ProductVariant]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionOrder]'))
ALTER TABLE [dbo].[ProductionOrder] DROP CONSTRAINT [FK_ProductionOrder_ProductVariant]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionOrder_ProductRoute]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionOrder]'))
ALTER TABLE [dbo].[ProductionOrder] DROP CONSTRAINT [FK_ProductionOrder_ProductRoute]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionOrder_ProductBOM]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionOrder]'))
ALTER TABLE [dbo].[ProductionOrder] DROP CONSTRAINT [FK_ProductionOrder_ProductBOM]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductAttributeValue_ProductAttribute]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductAttributeValue]'))
ALTER TABLE [dbo].[ProductAttributeValue] DROP CONSTRAINT [FK_ProductAttributeValue_ProductAttribute]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductAttributeValue_AttributeValue]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductAttributeValue]'))
ALTER TABLE [dbo].[ProductAttributeValue] DROP CONSTRAINT [FK_ProductAttributeValue_AttributeValue]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductAttribute_Attribute]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductAttribute]'))
ALTER TABLE [dbo].[ProductAttribute] DROP CONSTRAINT [FK_ProductAttribute_Attribute]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Product_Manufacturer]') AND parent_object_id = OBJECT_ID(N'[dbo].[Product]'))
ALTER TABLE [dbo].[Product] DROP CONSTRAINT [FK_Product_Manufacturer]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Product_Counter_SerialNo]') AND parent_object_id = OBJECT_ID(N'[dbo].[Product]'))
ALTER TABLE [dbo].[Product] DROP CONSTRAINT [FK_Product_Counter_SerialNo]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Product_Counter_BatchNo]') AND parent_object_id = OBJECT_ID(N'[dbo].[Product]'))
ALTER TABLE [dbo].[Product] DROP CONSTRAINT [FK_Product_Counter_BatchNo]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PickListValueForTimeZone_PickListValue]') AND parent_object_id = OBJECT_ID(N'[dbo].[PickListValueForTimeZone]'))
ALTER TABLE [dbo].[PickListValueForTimeZone] DROP CONSTRAINT [FK_PickListValueForTimeZone_PickListValue]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PickListValueForState_PickListValue]') AND parent_object_id = OBJECT_ID(N'[dbo].[PickListValueForState]'))
ALTER TABLE [dbo].[PickListValueForState] DROP CONSTRAINT [FK_PickListValueForState_PickListValue]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PickListValueForState_Country]') AND parent_object_id = OBJECT_ID(N'[dbo].[PickListValueForState]'))
ALTER TABLE [dbo].[PickListValueForState] DROP CONSTRAINT [FK_PickListValueForState_Country]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PickListValueForSecurityFunction_PickListValue]') AND parent_object_id = OBJECT_ID(N'[dbo].[PickListValueForSecurityFunction]'))
ALTER TABLE [dbo].[PickListValueForSecurityFunction] DROP CONSTRAINT [FK_PickListValueForSecurityFunction_PickListValue]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PickListValueForMunicipality_State]') AND parent_object_id = OBJECT_ID(N'[dbo].[PickListValueForMunicipality]'))
ALTER TABLE [dbo].[PickListValueForMunicipality] DROP CONSTRAINT [FK_PickListValueForMunicipality_State]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PickListValueForMunicipality_PickListValue]') AND parent_object_id = OBJECT_ID(N'[dbo].[PickListValueForMunicipality]'))
ALTER TABLE [dbo].[PickListValueForMunicipality] DROP CONSTRAINT [FK_PickListValueForMunicipality_PickListValue]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PickListValueForMunicipality_Country]') AND parent_object_id = OBJECT_ID(N'[dbo].[PickListValueForMunicipality]'))
ALTER TABLE [dbo].[PickListValueForMunicipality] DROP CONSTRAINT [FK_PickListValueForMunicipality_Country]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PickListValueForMenuGroup_PickListValue]') AND parent_object_id = OBJECT_ID(N'[dbo].[PickListValueForMenuGroup]'))
ALTER TABLE [dbo].[PickListValueForMenuGroup] DROP CONSTRAINT [FK_PickListValueForMenuGroup_PickListValue]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PickListValueForLanguage_PickListValue]') AND parent_object_id = OBJECT_ID(N'[dbo].[PickListValueForLanguage]'))
ALTER TABLE [dbo].[PickListValueForLanguage] DROP CONSTRAINT [FK_PickListValueForLanguage_PickListValue]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PickListValueForCurrency_PickListValue]') AND parent_object_id = OBJECT_ID(N'[dbo].[PickListValueForCurrency]'))
ALTER TABLE [dbo].[PickListValueForCurrency] DROP CONSTRAINT [FK_PickListValueForCurrency_PickListValue]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PickListValueForCountry_PickListValue]') AND parent_object_id = OBJECT_ID(N'[dbo].[PickListValueForCountry]'))
ALTER TABLE [dbo].[PickListValueForCountry] DROP CONSTRAINT [FK_PickListValueForCountry_PickListValue]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PickListValueForCity_PickListValue]') AND parent_object_id = OBJECT_ID(N'[dbo].[PickListValueForCity]'))
ALTER TABLE [dbo].[PickListValueForCity] DROP CONSTRAINT [FK_PickListValueForCity_PickListValue]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PickListValueFavourite_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[PickListValueFavourite]'))
ALTER TABLE [dbo].[PickListValueFavourite] DROP CONSTRAINT [FK_PickListValueFavourite_User]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PickListValueFavourite_PickListValue]') AND parent_object_id = OBJECT_ID(N'[dbo].[PickListValueFavourite]'))
ALTER TABLE [dbo].[PickListValueFavourite] DROP CONSTRAINT [FK_PickListValueFavourite_PickListValue]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PickListValue_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[PickListValue]'))
ALTER TABLE [dbo].[PickListValue] DROP CONSTRAINT [FK_PickListValue_User]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PickList_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[PickList]'))
ALTER TABLE [dbo].[PickList] DROP CONSTRAINT [FK_PickList_User]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Location_Timezone]') AND parent_object_id = OBJECT_ID(N'[dbo].[Location]'))
ALTER TABLE [dbo].[Location] DROP CONSTRAINT [FK_Location_Timezone]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Location_ContactInformation]') AND parent_object_id = OBJECT_ID(N'[dbo].[Location]'))
ALTER TABLE [dbo].[Location] DROP CONSTRAINT [FK_Location_ContactInformation]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Location_Company]') AND parent_object_id = OBJECT_ID(N'[dbo].[Location]'))
ALTER TABLE [dbo].[Location] DROP CONSTRAINT [FK_Location_Company]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Location_Address]') AND parent_object_id = OBJECT_ID(N'[dbo].[Location]'))
ALTER TABLE [dbo].[Location] DROP CONSTRAINT [FK_Location_Address]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ImageBinary_Image]') AND parent_object_id = OBJECT_ID(N'[dbo].[ImageBinary]'))
ALTER TABLE [dbo].[ImageBinary] DROP CONSTRAINT [FK_ImageBinary_Image]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EntitySecurity_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[EntitySecurity]'))
ALTER TABLE [dbo].[EntitySecurity] DROP CONSTRAINT [FK_EntitySecurity_Tenant]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EntitySecurity_Role]') AND parent_object_id = OBJECT_ID(N'[dbo].[EntitySecurity]'))
ALTER TABLE [dbo].[EntitySecurity] DROP CONSTRAINT [FK_EntitySecurity_Role]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Contact_PickListValue]') AND parent_object_id = OBJECT_ID(N'[dbo].[Contact]'))
ALTER TABLE [dbo].[Contact] DROP CONSTRAINT [FK_Contact_PickListValue]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CollectionDetail_ProductVariant]') AND parent_object_id = OBJECT_ID(N'[dbo].[CollectionDetail]'))
ALTER TABLE [dbo].[CollectionDetail] DROP CONSTRAINT [FK_CollectionDetail_ProductVariant]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CollectionDetail_ProductionTask]') AND parent_object_id = OBJECT_ID(N'[dbo].[CollectionDetail]'))
ALTER TABLE [dbo].[CollectionDetail] DROP CONSTRAINT [FK_CollectionDetail_ProductionTask]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CollectionDetail_Collection]') AND parent_object_id = OBJECT_ID(N'[dbo].[CollectionDetail]'))
ALTER TABLE [dbo].[CollectionDetail] DROP CONSTRAINT [FK_CollectionDetail_Collection]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BOMProduced_BOM]') AND parent_object_id = OBJECT_ID(N'[dbo].[BOMProduced]'))
ALTER TABLE [dbo].[BOMProduced] DROP CONSTRAINT [FK_BOMProduced_BOM]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BOMComponentAttributeValue_BOMComponentAttribute]') AND parent_object_id = OBJECT_ID(N'[dbo].[BOMComponentAttributeValue]'))
ALTER TABLE [dbo].[BOMComponentAttributeValue] DROP CONSTRAINT [FK_BOMComponentAttributeValue_BOMComponentAttribute]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BOMComponentAttributeValue_AttributeValue]') AND parent_object_id = OBJECT_ID(N'[dbo].[BOMComponentAttributeValue]'))
ALTER TABLE [dbo].[BOMComponentAttributeValue] DROP CONSTRAINT [FK_BOMComponentAttributeValue_AttributeValue]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BOMComponentAttribute_BOMComponent]') AND parent_object_id = OBJECT_ID(N'[dbo].[BOMComponentAttribute]'))
ALTER TABLE [dbo].[BOMComponentAttribute] DROP CONSTRAINT [FK_BOMComponentAttribute_BOMComponent]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BOMComponentAttribute_Attribute]') AND parent_object_id = OBJECT_ID(N'[dbo].[BOMComponentAttribute]'))
ALTER TABLE [dbo].[BOMComponentAttribute] DROP CONSTRAINT [FK_BOMComponentAttribute_Attribute]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BOMComponent_ProductOperation]') AND parent_object_id = OBJECT_ID(N'[dbo].[BOMComponent]'))
ALTER TABLE [dbo].[BOMComponent] DROP CONSTRAINT [FK_BOMComponent_ProductOperation]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BOMComponent_BOM1]') AND parent_object_id = OBJECT_ID(N'[dbo].[BOMComponent]'))
ALTER TABLE [dbo].[BOMComponent] DROP CONSTRAINT [FK_BOMComponent_BOM1]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BOMComponent_BOM]') AND parent_object_id = OBJECT_ID(N'[dbo].[BOMComponent]'))
ALTER TABLE [dbo].[BOMComponent] DROP CONSTRAINT [FK_BOMComponent_BOM]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BOMAlternativeComponent_ProductVendor]') AND parent_object_id = OBJECT_ID(N'[dbo].[BOMAlternativeComponent]'))
ALTER TABLE [dbo].[BOMAlternativeComponent] DROP CONSTRAINT [FK_BOMAlternativeComponent_ProductVendor]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BOMAlternativeComponent_BOMComponent]') AND parent_object_id = OBJECT_ID(N'[dbo].[BOMAlternativeComponent]'))
ALTER TABLE [dbo].[BOMAlternativeComponent] DROP CONSTRAINT [FK_BOMAlternativeComponent_BOMComponent]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BatchType_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[BatchTypes]'))
ALTER TABLE [dbo].[BatchTypes] DROP CONSTRAINT [FK_BatchType_Tenant]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AttributeValue_Attribute]') AND parent_object_id = OBJECT_ID(N'[dbo].[AttributeValue]'))
ALTER TABLE [dbo].[AttributeValue] DROP CONSTRAINT [FK_AttributeValue_Attribute]
GO
/****** Object:  Table [dbo].[WorkFlowTransitionHistory]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowTransitionHistory]') AND type in (N'U'))
DROP TABLE [dbo].[WorkFlowTransitionHistory]
GO
/****** Object:  Table [dbo].[WorkFlowStep]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowStep]') AND type in (N'U'))
DROP TABLE [dbo].[WorkFlowStep]
GO
/****** Object:  Table [dbo].[WorkFlowRole]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowRole]') AND type in (N'U'))
DROP TABLE [dbo].[WorkFlowRole]
GO
/****** Object:  Table [dbo].[WorkFlowProcessTask]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowProcessTask]') AND type in (N'U'))
DROP TABLE [dbo].[WorkFlowProcessTask]
GO
/****** Object:  Table [dbo].[WorkFlowProcess]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowProcess]') AND type in (N'U'))
DROP TABLE [dbo].[WorkFlowProcess]
GO
/****** Object:  Table [dbo].[WorkFlowOperation]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowOperation]') AND type in (N'U'))
DROP TABLE [dbo].[WorkFlowOperation]
GO
/****** Object:  Table [dbo].[WorkFlowInnerStep]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowInnerStep]') AND type in (N'U'))
DROP TABLE [dbo].[WorkFlowInnerStep]
GO
/****** Object:  Table [dbo].[WorkFlow]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlow]') AND type in (N'U'))
DROP TABLE [dbo].[WorkFlow]
GO
/****** Object:  Table [dbo].[WorkcenterUser]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkcenterUser]') AND type in (N'U'))
DROP TABLE [dbo].[WorkcenterUser]
GO
/****** Object:  Table [dbo].[WorkcenterTool]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkcenterTool]') AND type in (N'U'))
DROP TABLE [dbo].[WorkcenterTool]
GO
/****** Object:  Table [dbo].[WorkcenterEquipment]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkcenterEquipment]') AND type in (N'U'))
DROP TABLE [dbo].[WorkcenterEquipment]
GO
/****** Object:  Table [dbo].[WorkcenterConfiguration]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkcenterConfiguration]') AND type in (N'U'))
DROP TABLE [dbo].[WorkcenterConfiguration]
GO
/****** Object:  Table [dbo].[WorkcenterCompetence]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkcenterCompetence]') AND type in (N'U'))
DROP TABLE [dbo].[WorkcenterCompetence]
GO
/****** Object:  Table [dbo].[Workcenter]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Workcenter]') AND type in (N'U'))
DROP TABLE [dbo].[Workcenter]
GO
/****** Object:  Table [dbo].[WarehouseLocation]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WarehouseLocation]') AND type in (N'U'))
DROP TABLE [dbo].[WarehouseLocation]
GO
/****** Object:  Table [dbo].[Warehouse]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Warehouse]') AND type in (N'U'))
DROP TABLE [dbo].[Warehouse]
GO
/****** Object:  Table [dbo].[Vendor]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Vendor]') AND type in (N'U'))
DROP TABLE [dbo].[Vendor]
GO
/****** Object:  Table [dbo].[UserLocation]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserLocation]') AND type in (N'U'))
DROP TABLE [dbo].[UserLocation]
GO
/****** Object:  Table [dbo].[UserInRole]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserInRole]') AND type in (N'U'))
DROP TABLE [dbo].[UserInRole]
GO
/****** Object:  Table [dbo].[UserEmployment]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserEmployment]') AND type in (N'U'))
DROP TABLE [dbo].[UserEmployment]
GO
/****** Object:  Table [dbo].[UserDepartment]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserDepartment]') AND type in (N'U'))
DROP TABLE [dbo].[UserDepartment]
GO
/****** Object:  Table [dbo].[UserCompany]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserCompany]') AND type in (N'U'))
DROP TABLE [dbo].[UserCompany]
GO
/****** Object:  Table [dbo].[User]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
DROP TABLE [dbo].[User]
GO
/****** Object:  Table [dbo].[Tool]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tool]') AND type in (N'U'))
DROP TABLE [dbo].[Tool]
GO
/****** Object:  Table [dbo].[TenantSubscriptionEntityDetail]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TenantSubscriptionEntityDetail]') AND type in (N'U'))
DROP TABLE [dbo].[TenantSubscriptionEntityDetail]
GO
/****** Object:  Table [dbo].[TenantSubscriptionEntity]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TenantSubscriptionEntity]') AND type in (N'U'))
DROP TABLE [dbo].[TenantSubscriptionEntity]
GO
/****** Object:  Table [dbo].[TenantSubscription]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TenantSubscription]') AND type in (N'U'))
DROP TABLE [dbo].[TenantSubscription]
GO
/****** Object:  Table [dbo].[TenantServiceStatus]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TenantServiceStatus]') AND type in (N'U'))
DROP TABLE [dbo].[TenantServiceStatus]
GO
/****** Object:  Table [dbo].[TenantIPRange]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TenantIPRange]') AND type in (N'U'))
DROP TABLE [dbo].[TenantIPRange]
GO
/****** Object:  Table [dbo].[Tenant]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tenant]') AND type in (N'U'))
DROP TABLE [dbo].[Tenant]
GO
/****** Object:  Table [dbo].[Task]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Task]') AND type in (N'U'))
DROP TABLE [dbo].[Task]
GO
/****** Object:  Table [dbo].[SMSTemplate]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SMSTemplate]') AND type in (N'U'))
DROP TABLE [dbo].[SMSTemplate]
GO
/****** Object:  Table [dbo].[SMS]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SMS]') AND type in (N'U'))
DROP TABLE [dbo].[SMS]
GO
/****** Object:  Table [dbo].[Settings]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Settings]') AND type in (N'U'))
DROP TABLE [dbo].[Settings]
GO
/****** Object:  Table [dbo].[SchedulerYearly]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchedulerYearly]') AND type in (N'U'))
DROP TABLE [dbo].[SchedulerYearly]
GO
/****** Object:  Table [dbo].[SchedulerWeekly]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchedulerWeekly]') AND type in (N'U'))
DROP TABLE [dbo].[SchedulerWeekly]
GO
/****** Object:  Table [dbo].[SchedulerMonthly]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchedulerMonthly]') AND type in (N'U'))
DROP TABLE [dbo].[SchedulerMonthly]
GO
/****** Object:  Table [dbo].[SchedulerDaily]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchedulerDaily]') AND type in (N'U'))
DROP TABLE [dbo].[SchedulerDaily]
GO
/****** Object:  Table [dbo].[Scheduler]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Scheduler]') AND type in (N'U'))
DROP TABLE [dbo].[Scheduler]
GO
/****** Object:  Table [dbo].[Rule]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Rule]') AND type in (N'U'))
DROP TABLE [dbo].[Rule]
GO
/****** Object:  Table [dbo].[RouteTemplateOperationWorkcenterTool]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RouteTemplateOperationWorkcenterTool]') AND type in (N'U'))
DROP TABLE [dbo].[RouteTemplateOperationWorkcenterTool]
GO
/****** Object:  Table [dbo].[RouteTemplateOperationWorkcenterConfiguration]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RouteTemplateOperationWorkcenterConfiguration]') AND type in (N'U'))
DROP TABLE [dbo].[RouteTemplateOperationWorkcenterConfiguration]
GO
/****** Object:  Table [dbo].[RouteTemplateOperationWorkcenterCompetence]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RouteTemplateOperationWorkcenterCompetence]') AND type in (N'U'))
DROP TABLE [dbo].[RouteTemplateOperationWorkcenterCompetence]
GO
/****** Object:  Table [dbo].[RouteTemplateOperationWorkcenter]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RouteTemplateOperationWorkcenter]') AND type in (N'U'))
DROP TABLE [dbo].[RouteTemplateOperationWorkcenter]
GO
/****** Object:  Table [dbo].[RouteTemplateOperation]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RouteTemplateOperation]') AND type in (N'U'))
DROP TABLE [dbo].[RouteTemplateOperation]
GO
/****** Object:  Table [dbo].[RouteTemplate]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RouteTemplate]') AND type in (N'U'))
DROP TABLE [dbo].[RouteTemplate]
GO
/****** Object:  Table [dbo].[Room]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Room]') AND type in (N'U'))
DROP TABLE [dbo].[Room]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Role]') AND type in (N'U'))
DROP TABLE [dbo].[Role]
GO
/****** Object:  Table [dbo].[Resource_Test]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource_Test]') AND type in (N'U'))
DROP TABLE [dbo].[Resource_Test]
GO
/****** Object:  Table [dbo].[Resource]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource]') AND type in (N'U'))
DROP TABLE [dbo].[Resource]
GO
/****** Object:  Table [dbo].[ProductVendor]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductVendor]') AND type in (N'U'))
DROP TABLE [dbo].[ProductVendor]
GO
/****** Object:  Table [dbo].[ProductVariantRuleAttributeValue]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductVariantRuleAttributeValue]') AND type in (N'U'))
DROP TABLE [dbo].[ProductVariantRuleAttributeValue]
GO
/****** Object:  Table [dbo].[ProductVariantRuleAttribute]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductVariantRuleAttribute]') AND type in (N'U'))
DROP TABLE [dbo].[ProductVariantRuleAttribute]
GO
/****** Object:  Table [dbo].[ProductVariantRule]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductVariantRule]') AND type in (N'U'))
DROP TABLE [dbo].[ProductVariantRule]
GO
/****** Object:  Table [dbo].[ProductVariantAttributeValue]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductVariantAttributeValue]') AND type in (N'U'))
DROP TABLE [dbo].[ProductVariantAttributeValue]
GO
/****** Object:  Table [dbo].[ProductVariantAttribute]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductVariantAttribute]') AND type in (N'U'))
DROP TABLE [dbo].[ProductVariantAttribute]
GO
/****** Object:  Table [dbo].[ProductVariant]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductVariant]') AND type in (N'U'))
DROP TABLE [dbo].[ProductVariant]
GO
/****** Object:  Table [dbo].[ProductRoute]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductRoute]') AND type in (N'U'))
DROP TABLE [dbo].[ProductRoute]
GO
/****** Object:  Table [dbo].[ProductOperationWorkcenterConfiguration]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductOperationWorkcenterConfiguration]') AND type in (N'U'))
DROP TABLE [dbo].[ProductOperationWorkcenterConfiguration]
GO
/****** Object:  Table [dbo].[ProductOperationWorkcenterCompetence]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductOperationWorkcenterCompetence]') AND type in (N'U'))
DROP TABLE [dbo].[ProductOperationWorkcenterCompetence]
GO
/****** Object:  Table [dbo].[ProductOperationWorkcenter]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductOperationWorkcenter]') AND type in (N'U'))
DROP TABLE [dbo].[ProductOperationWorkcenter]
GO
/****** Object:  Table [dbo].[ProductOperation]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductOperation]') AND type in (N'U'))
DROP TABLE [dbo].[ProductOperation]
GO
/****** Object:  Table [dbo].[ProductionTask]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductionTask]') AND type in (N'U'))
DROP TABLE [dbo].[ProductionTask]
GO
/****** Object:  Table [dbo].[ProductionSetProduced]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductionSetProduced]') AND type in (N'U'))
DROP TABLE [dbo].[ProductionSetProduced]
GO
/****** Object:  Table [dbo].[ProductionSetConsumed]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductionSetConsumed]') AND type in (N'U'))
DROP TABLE [dbo].[ProductionSetConsumed]
GO
/****** Object:  Table [dbo].[ProductionSet]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductionSet]') AND type in (N'U'))
DROP TABLE [dbo].[ProductionSet]
GO
/****** Object:  Table [dbo].[ProductionOrderProduced]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductionOrderProduced]') AND type in (N'U'))
DROP TABLE [dbo].[ProductionOrderProduced]
GO
/****** Object:  Table [dbo].[ProductionOrderOperationTool]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductionOrderOperationTool]') AND type in (N'U'))
DROP TABLE [dbo].[ProductionOrderOperationTool]
GO
/****** Object:  Table [dbo].[ProductionOrderOperationConfiguration]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductionOrderOperationConfiguration]') AND type in (N'U'))
DROP TABLE [dbo].[ProductionOrderOperationConfiguration]
GO
/****** Object:  Table [dbo].[ProductionOrderOperationCompetence]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductionOrderOperationCompetence]') AND type in (N'U'))
DROP TABLE [dbo].[ProductionOrderOperationCompetence]
GO
/****** Object:  Table [dbo].[ProductionOrderOperation]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductionOrderOperation]') AND type in (N'U'))
DROP TABLE [dbo].[ProductionOrderOperation]
GO
/****** Object:  Table [dbo].[ProductionOrderComponent]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductionOrderComponent]') AND type in (N'U'))
DROP TABLE [dbo].[ProductionOrderComponent]
GO
/****** Object:  Table [dbo].[ProductionOrderAlternativeComponent]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductionOrderAlternativeComponent]') AND type in (N'U'))
DROP TABLE [dbo].[ProductionOrderAlternativeComponent]
GO
/****** Object:  Table [dbo].[ProductionOrder]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductionOrder]') AND type in (N'U'))
DROP TABLE [dbo].[ProductionOrder]
GO
/****** Object:  Table [dbo].[ProductAttributeValue]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductAttributeValue]') AND type in (N'U'))
DROP TABLE [dbo].[ProductAttributeValue]
GO
/****** Object:  Table [dbo].[ProductAttribute]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductAttribute]') AND type in (N'U'))
DROP TABLE [dbo].[ProductAttribute]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Product]') AND type in (N'U'))
DROP TABLE [dbo].[Product]
GO
/****** Object:  Table [dbo].[ProcurementRule]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProcurementRule]') AND type in (N'U'))
DROP TABLE [dbo].[ProcurementRule]
GO
/****** Object:  Table [dbo].[PickListValueForTimeZone]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PickListValueForTimeZone]') AND type in (N'U'))
DROP TABLE [dbo].[PickListValueForTimeZone]
GO
/****** Object:  Table [dbo].[PickListValueForState]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PickListValueForState]') AND type in (N'U'))
DROP TABLE [dbo].[PickListValueForState]
GO
/****** Object:  Table [dbo].[PickListValueForSecurityFunction]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PickListValueForSecurityFunction]') AND type in (N'U'))
DROP TABLE [dbo].[PickListValueForSecurityFunction]
GO
/****** Object:  Table [dbo].[PickListValueForMunicipality]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PickListValueForMunicipality]') AND type in (N'U'))
DROP TABLE [dbo].[PickListValueForMunicipality]
GO
/****** Object:  Table [dbo].[PickListValueForMenuGroup]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PickListValueForMenuGroup]') AND type in (N'U'))
DROP TABLE [dbo].[PickListValueForMenuGroup]
GO
/****** Object:  Table [dbo].[PickListValueForLanguage]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PickListValueForLanguage]') AND type in (N'U'))
DROP TABLE [dbo].[PickListValueForLanguage]
GO
/****** Object:  Table [dbo].[PickListValueForCurrency]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PickListValueForCurrency]') AND type in (N'U'))
DROP TABLE [dbo].[PickListValueForCurrency]
GO
/****** Object:  Table [dbo].[PickListValueForCountry]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PickListValueForCountry]') AND type in (N'U'))
DROP TABLE [dbo].[PickListValueForCountry]
GO
/****** Object:  Table [dbo].[PickListValueForCity]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PickListValueForCity]') AND type in (N'U'))
DROP TABLE [dbo].[PickListValueForCity]
GO
/****** Object:  Table [dbo].[PickListValueFavourite]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PickListValueFavourite]') AND type in (N'U'))
DROP TABLE [dbo].[PickListValueFavourite]
GO
/****** Object:  Table [dbo].[PickListValue]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PickListValue]') AND type in (N'U'))
DROP TABLE [dbo].[PickListValue]
GO
/****** Object:  Table [dbo].[PicklistLayout]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistLayout]') AND type in (N'U'))
DROP TABLE [dbo].[PicklistLayout]
GO
/****** Object:  Table [dbo].[PickList]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PickList]') AND type in (N'U'))
DROP TABLE [dbo].[PickList]
GO
/****** Object:  Table [dbo].[PasswordPolicy]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PasswordPolicy]') AND type in (N'U'))
DROP TABLE [dbo].[PasswordPolicy]
GO
/****** Object:  Table [dbo].[OrganizationUnit]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrganizationUnit]') AND type in (N'U'))
DROP TABLE [dbo].[OrganizationUnit]
GO
/****** Object:  Table [dbo].[Menu]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Menu]') AND type in (N'U'))
DROP TABLE [dbo].[Menu]
GO
/****** Object:  Table [dbo].[Manufacturer]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Manufacturer]') AND type in (N'U'))
DROP TABLE [dbo].[Manufacturer]
GO
/****** Object:  Table [dbo].[Location]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Location]') AND type in (N'U'))
DROP TABLE [dbo].[Location]
GO
/****** Object:  Table [dbo].[Item]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Item]') AND type in (N'U'))
DROP TABLE [dbo].[Item]
GO
/****** Object:  Table [dbo].[ImageBinary]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ImageBinary]') AND type in (N'U'))
DROP TABLE [dbo].[ImageBinary]
GO
/****** Object:  Table [dbo].[Image]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Image]') AND type in (N'U'))
DROP TABLE [dbo].[Image]
GO
/****** Object:  Table [dbo].[Equipment]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Equipment]') AND type in (N'U'))
DROP TABLE [dbo].[Equipment]
GO
/****** Object:  Table [dbo].[EntitySecurity]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EntitySecurity]') AND type in (N'U'))
DROP TABLE [dbo].[EntitySecurity]
GO
/****** Object:  Table [dbo].[EntityLayout]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EntityLayout]') AND type in (N'U'))
DROP TABLE [dbo].[EntityLayout]
GO
/****** Object:  Table [dbo].[EmailTemplate]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmailTemplate]') AND type in (N'U'))
DROP TABLE [dbo].[EmailTemplate]
GO
/****** Object:  Table [dbo].[Email]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Email]') AND type in (N'U'))
DROP TABLE [dbo].[Email]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Department]') AND type in (N'U'))
DROP TABLE [dbo].[Department]
GO
/****** Object:  Table [dbo].[CustomerCredential]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomerCredential]') AND type in (N'U'))
DROP TABLE [dbo].[CustomerCredential]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND type in (N'U'))
DROP TABLE [dbo].[Customer]
GO
/****** Object:  Table [dbo].[CredentialHistory]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CredentialHistory]') AND type in (N'U'))
DROP TABLE [dbo].[CredentialHistory]
GO
/****** Object:  Table [dbo].[Credential]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Credential]') AND type in (N'U'))
DROP TABLE [dbo].[Credential]
GO
/****** Object:  Table [dbo].[Counter]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Counter]') AND type in (N'U'))
DROP TABLE [dbo].[Counter]
GO
/****** Object:  Table [dbo].[ContactInformation]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContactInformation]') AND type in (N'U'))
DROP TABLE [dbo].[ContactInformation]
GO
/****** Object:  Table [dbo].[Contact_BCK_28062019]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Contact_BCK_28062019]') AND type in (N'U'))
DROP TABLE [dbo].[Contact_BCK_28062019]
GO
/****** Object:  Table [dbo].[Contact]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Contact]') AND type in (N'U'))
DROP TABLE [dbo].[Contact]
GO
/****** Object:  Table [dbo].[Company_bck]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Company_bck]') AND type in (N'U'))
DROP TABLE [dbo].[Company_bck]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Company]') AND type in (N'U'))
DROP TABLE [dbo].[Company]
GO
/****** Object:  Table [dbo].[CollectionDetail]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CollectionDetail]') AND type in (N'U'))
DROP TABLE [dbo].[CollectionDetail]
GO
/****** Object:  Table [dbo].[Collection]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Collection]') AND type in (N'U'))
DROP TABLE [dbo].[Collection]
GO
/****** Object:  Table [dbo].[BOMProduced]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BOMProduced]') AND type in (N'U'))
DROP TABLE [dbo].[BOMProduced]
GO
/****** Object:  Table [dbo].[BOMComponentAttributeValue]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BOMComponentAttributeValue]') AND type in (N'U'))
DROP TABLE [dbo].[BOMComponentAttributeValue]
GO
/****** Object:  Table [dbo].[BOMComponentAttribute]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BOMComponentAttribute]') AND type in (N'U'))
DROP TABLE [dbo].[BOMComponentAttribute]
GO
/****** Object:  Table [dbo].[BOMComponent]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BOMComponent]') AND type in (N'U'))
DROP TABLE [dbo].[BOMComponent]
GO
/****** Object:  Table [dbo].[BOMAlternativeComponent]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BOMAlternativeComponent]') AND type in (N'U'))
DROP TABLE [dbo].[BOMAlternativeComponent]
GO
/****** Object:  Table [dbo].[BOM]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BOM]') AND type in (N'U'))
DROP TABLE [dbo].[BOM]
GO
/****** Object:  Table [dbo].[BatchTypes]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BatchTypes]') AND type in (N'U'))
DROP TABLE [dbo].[BatchTypes]
GO
/****** Object:  Table [dbo].[AttributeValue]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AttributeValue]') AND type in (N'U'))
DROP TABLE [dbo].[AttributeValue]
GO
/****** Object:  Table [dbo].[Attribute]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Attribute]') AND type in (N'U'))
DROP TABLE [dbo].[Attribute]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 15-Jul-19 15:43:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Address]') AND type in (N'U'))
DROP TABLE [dbo].[Address]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Address]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Address](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[AddressLine1] [dbo].[mediumText] NULL,
	[AddressLine2] [dbo].[mediumText] NULL,
	[AddressLine3] [dbo].[mediumText] NULL,
	[CareOf] [dbo].[mediumText] NULL,
	[POBox] [dbo].[mediumText] NULL,
	[CountryId] [uniqueidentifier] NULL,
	[CityId] [uniqueidentifier] NULL,
	[MunicipalityId] [uniqueidentifier] NULL,
	[StateId] [uniqueidentifier] NULL,
	[PostalCode] [dbo].[xSmallText] NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Attribute]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Attribute]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Attribute](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Code] [dbo].[xSmallText] NOT NULL,
	[Name] [nchar](10) NULL,
 CONSTRAINT [PK_Attribute] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[AttributeValue]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AttributeValue]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AttributeValue](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Code] [dbo].[xSmallText] NOT NULL,
	[AttributeCode] [dbo].[xSmallText] NOT NULL,
	[Value] [dbo].[smallText] NOT NULL,
 CONSTRAINT [PK_AttributeValue] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[BatchTypes]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BatchTypes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BatchTypes](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[Context] [dbo].[xSmallText] NOT NULL,
	[Priority] [int] NULL,
	[IdleTime] [int] NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_BatchTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[BOM]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BOM]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BOM](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[ProductCode] [dbo].[xSmallText] NOT NULL,
	[Quantity] [decimal](18, 2) NULL,
	[UOM] [uniqueidentifier] NULL,
	[Type] [uniqueidentifier] NULL,
	[Category] [uniqueidentifier] NULL,
	[Revision] [dbo].[xSmallText] NULL,
	[IsDefault] [bit] NULL,
	[MergeComponent] [bit] NULL,
	[IsImported] [bit] NULL,
 CONSTRAINT [PK_BOM] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[BOMAlternativeComponent]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BOMAlternativeComponent]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BOMAlternativeComponent](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[BOMComponentId] [uniqueidentifier] NOT NULL,
	[ProductVendorId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_BOMAlternativeComponent] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[BOMComponent]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BOMComponent]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BOMComponent](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[BOMId] [uniqueidentifier] NOT NULL,
	[ProductCode] [dbo].[xSmallText] NOT NULL,
	[ChildBOMId] [uniqueidentifier] NULL,
	[Position] [int] NULL,
	[OperationNo] [dbo].[xSmallText] NULL,
	[Quantity] [decimal](18, 0) NULL,
	[UOM] [uniqueidentifier] NULL,
	[Step] [int] NULL,
	[IsFixedStep] [bit] NULL,
	[Factor] [decimal](18, 2) NULL,
	[WastagePercentage] [decimal](18, 2) NULL,
	[StopStructureDrilldown] [bit] NULL,
	[Cost] [dbo].[amount] NULL,
	[IsSparepart] [bit] NULL,
	[Comment] [dbo].[xLargeText] NULL,
	[BOMText] [dbo].[smallText] NULL,
	[BOMText2] [dbo].[smallText] NULL,
	[DrawingPosition] [dbo].[smallText] NULL,
	[MaterialFactor] [int] NULL,
	[Length] [decimal](18, 2) NULL,
	[LengthUOM] [uniqueidentifier] NULL,
	[Width] [decimal](18, 2) NULL,
	[WidthUOM] [uniqueidentifier] NULL,
	[Height] [decimal](18, 2) NULL,
	[HeightUOM] [uniqueidentifier] NULL,
	[Amount] [dbo].[amount] NULL,
 CONSTRAINT [PK_BOMComponent] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[BOMComponentAttribute]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BOMComponentAttribute]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BOMComponentAttribute](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[BOMComponentId] [uniqueidentifier] NOT NULL,
	[AttributeCode] [dbo].[xSmallText] NOT NULL,
 CONSTRAINT [PK_BOMComponentAttribute] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[BOMComponentAttributeValue]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BOMComponentAttributeValue]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BOMComponentAttributeValue](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[BOMComponentAttributeId] [uniqueidentifier] NOT NULL,
	[AttributeValueCode] [dbo].[xSmallText] NOT NULL,
 CONSTRAINT [PK_BOMComponentAttributeValue] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[BOMProduced]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BOMProduced]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BOMProduced](
	[TenantId] [uniqueidentifier] NOT NULL,
	[ID] [uniqueidentifier] NOT NULL,
	[BOMId] [uniqueidentifier] NOT NULL,
	[ProductCode] [dbo].[xSmallText] NOT NULL,
	[Quantity] [decimal](18, 2) NOT NULL,
	[UOM] [uniqueidentifier] NULL,
	[Cost] [dbo].[amount] NULL,
	[ProducedType] [tinyint] NOT NULL,
	[SharePercentage] [decimal](18, 2) NULL,
	[YieldPercentage] [decimal](18, 2) NULL,
 CONSTRAINT [PK_BOMProduced] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Collection]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Collection]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Collection](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[Status] [tinyint] NOT NULL,
 CONSTRAINT [PK_Collection] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[CollectionDetail]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CollectionDetail]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CollectionDetail](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[CollectionId] [uniqueidentifier] NOT NULL,
	[CollectionTaskId] [uniqueidentifier] NOT NULL,
	[ParentId] [uniqueidentifier] NULL,
	[ProductVariantCode] [dbo].[xSmallText] NOT NULL,
	[ProductName] [dbo].[smallText] NOT NULL,
	[Placed] [dbo].[smallText] NULL,
	[Type] [tinyint] NOT NULL,
	[PlannedQuantity] [decimal](18, 2) NULL,
	[Qualtity] [decimal](18, 2) NULL,
	[PalletId] [uniqueidentifier] NULL,
	[BatchNo] [nchar](10) NULL,
	[SerialNo] [nchar](10) NULL,
	[SetNo] [dbo].[smallText] NOT NULL,
	[LocationId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_CollectionDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Company]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Company]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Company](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[IsLegalEntity] [bit] NULL,
	[Description] [dbo].[largeText] NULL,
	[ContactInformationId] [uniqueidentifier] NULL,
	[OfficialAddressId] [uniqueidentifier] NULL,
	[InvoiceAddressId] [uniqueidentifier] NULL,
	[PostalAddressId] [uniqueidentifier] NULL,
	[AvatarId] [uniqueidentifier] NULL,
	[TimezoneId] [uniqueidentifier] NULL,
	[PreferredLanguageId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Company_bck]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Company_bck]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Company_bck](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[IsLegalEntity] [bit] NOT NULL,
	[Description] [dbo].[largeText] NULL,
	[ContactInformationId] [uniqueidentifier] NULL,
	[OfficialAddressId] [uniqueidentifier] NULL,
	[InvoiceAddressId] [uniqueidentifier] NULL,
	[PostalAddressId] [uniqueidentifier] NULL,
	[AvatarId] [uniqueidentifier] NULL,
	[TimezoneId] [uniqueidentifier] NULL,
	[PreferredLanguageId] [uniqueidentifier] NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Contact]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Contact]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Contact](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[TitleId] [uniqueidentifier] NULL,
	[FirstName] [dbo].[smallText] NOT NULL,
	[MiddleName] [dbo].[smallText] NULL,
	[LastName] [dbo].[smallText] NULL,
 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Contact_BCK_28062019]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Contact_BCK_28062019]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Contact_BCK_28062019](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[Phone] [dbo].[xSmallText] NULL,
	[Email] [dbo].[smallText] NOT NULL,
	[TitleId] [uniqueidentifier] NULL,
	[FirstName] [dbo].[smallText] NOT NULL,
	[MiddleName] [dbo].[smallText] NULL,
	[LastName] [dbo].[smallText] NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ContactInformation]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContactInformation]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ContactInformation](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[PersonalPhone1] [dbo].[xSmallText] NULL,
	[PersonalPhone2] [dbo].[xSmallText] NULL,
	[PersonalMobile1] [dbo].[xSmallText] NULL,
	[PersonalEmail1] [dbo].[smallText] NULL,
	[PersonalEmail2] [dbo].[smallText] NULL,
	[WorkPhone1] [dbo].[xSmallText] NULL,
	[WorkPhone2] [dbo].[xSmallText] NULL,
	[WorkPhoneExtension] [dbo].[xSmallText] NULL,
	[WorkMobile1] [dbo].[xSmallText] NULL,
	[WorkFax1] [dbo].[xSmallText] NULL,
	[WorkEmail1] [dbo].[smallText] NULL,
 CONSTRAINT [PK_ContactInformation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Counter]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Counter]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Counter](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[Description] [dbo].[mediumText] NOT NULL,
	[Text] [dbo].[mediumText] NOT NULL,
	[CounterN] [int] NULL,
	[CounterO] [int] NULL,
	[CounterP] [int] NULL,
	[ResetCounterN] [int] NULL,
	[ResetCounterO] [int] NULL,
	[ResetCounterP] [int] NULL,
	[UpdatedOn] [datetime] NOT NULL,
	[UpdatedBy] [uniqueidentifier] NOT NULL,
	[EntityId] [dbo].[xSmallText] NULL,
 CONSTRAINT [PK_Counter] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Credential]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Credential]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Credential](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[RefId] [uniqueidentifier] NULL,
	[UserName] [dbo].[mediumText] NOT NULL,
	[PasswordHash] [nvarchar](255) NOT NULL,
	[PasswordSalt] [nvarchar](255) NOT NULL,
	[IsNew] [bit] NULL CONSTRAINT [DF_Credential_IsNew]  DEFAULT ((1)),
	[SecurityQuestion1] [dbo].[mediumText] NULL,
	[SecurityAnswer1] [dbo].[mediumText] NULL,
	[SecurityQuestion2] [dbo].[mediumText] NULL,
	[SecurityAnswer2] [dbo].[mediumText] NULL,
	[InvalidAttemptCount] [int] NULL,
	[IsLocked] [bit] NULL,
	[LockedOn] [datetime] NULL,
	[PasswordChangedOn] [datetime] NULL,
 CONSTRAINT [PK_Credential] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[CredentialHistory]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CredentialHistory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CredentialHistory](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[RefId] [uniqueidentifier] NOT NULL,
	[UserName] [dbo].[mediumText] NOT NULL,
	[PasswordHash] [dbo].[mediumText] NOT NULL,
	[PasswordSalt] [dbo].[mediumText] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_CredentialHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Customer](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[FirstName] [dbo].[smallText] NOT NULL,
	[MiddleName] [dbo].[smallText] NULL,
	[LastName] [dbo].[smallText] NULL,
	[ContactInformationId] [uniqueidentifier] NULL,
	[OfficialAddressId] [uniqueidentifier] NULL,
	[InvoiceAddressId] [uniqueidentifier] NULL,
	[PostalAddressId] [uniqueidentifier] NULL,
	[AvatarId] [uniqueidentifier] NULL,
	[Comment] [dbo].[xLargeText] NULL,
	[CustomerCredentialId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[CustomerCredential]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomerCredential]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CustomerCredential](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[TenantCode] [uniqueidentifier] NOT NULL,
	[CustomerName] [dbo].[smallText] NOT NULL,
	[Password] [dbo].[Password] NOT NULL,
	[SecurityQuestion1] [dbo].[smallText] NULL,
	[SecurityAnswer1] [dbo].[smallText] NULL,
	[SecurityQuestion2] [dbo].[smallText] NULL,
	[SecurityAnswer2] [dbo].[smallText] NULL,
 CONSTRAINT [PK_CustomerCredential] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Department]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Department]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Department](
	[TenantCode] [dbo].[xSmallText] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[IsLegalEntity] [bit] NOT NULL,
	[Code] [dbo].[xSmallText] NULL,
	[Description] [dbo].[largeText] NULL,
	[ContactInformationId] [uniqueidentifier] NULL,
	[OfficialAddressId] [uniqueidentifier] NULL,
	[InvoiceAddressId] [uniqueidentifier] NULL,
	[PostalAddressId] [uniqueidentifier] NULL,
	[AvatarId] [uniqueidentifier] NULL,
	[TimezoneId] [uniqueidentifier] NULL,
	[LanguageId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Email]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Email]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Email](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Recipient] [dbo].[mediumText] NOT NULL,
	[Sender] [dbo].[mediumText] NOT NULL,
	[Subject] [dbo].[mediumText] NULL,
	[Body] [dbo].[xLargeText] NOT NULL,
	[CurrentWorkFlowStep] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Email] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[EmailTemplate]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmailTemplate]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EmailTemplate](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [dbo].[mediumText] NOT NULL,
	[Context] [dbo].[xSmallText] NOT NULL,
	[Body] [dbo].[xLargeText] NOT NULL,
	[ContextType] [int] NULL,
 CONSTRAINT [PK_EmailTemplate] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[EntityLayout]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EntityLayout]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EntityLayout](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[EntityId] [dbo].[xSmallText] NOT NULL,
	[Name] [dbo].[mediumText] NOT NULL,
	[Type] [int] NOT NULL,
	[SubType] [dbo].[smallText] NULL,
	[LayoutContext] [int] NULL,
	[Layout] [dbo].[xLargeText] NULL,
	[UpdatedOn] [datetime] NOT NULL,
	[UpdatedBy] [uniqueidentifier] NOT NULL,
	[Default] [bit] NOT NULL,
 CONSTRAINT [PK_EL_EntityLayout] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[EntitySecurity]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EntitySecurity]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EntitySecurity](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[EntityId] [dbo].[xSmallText] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	[SecurityCode] [int] NOT NULL,
	[FunctionContext] [uniqueidentifier] NULL,
 CONSTRAINT [PK_WF_EntitySecurity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Equipment]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Equipment]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Equipment](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Code] [dbo].[xSmallText] NOT NULL,
 CONSTRAINT [PK_Equipment] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Image]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Image]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Image](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [dbo].[mediumText] NULL,
	[MimeType] [dbo].[smallText] NULL,
	[FileSize] [int] NULL,
	[FileName] [dbo].[mediumText] NULL,
	[FilePath] [dbo].[largeText] NULL,
	[IsBlobStorage] [bit] NOT NULL,
	[Width] [int] NULL,
	[Height] [int] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_Image] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ImageBinary]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ImageBinary]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ImageBinary](
	[TenantId] [uniqueidentifier] NOT NULL,
	[ImageId] [uniqueidentifier] NOT NULL,
	[FileContent] [varbinary](max) NOT NULL,
 CONSTRAINT [PK_ImageBinary] PRIMARY KEY CLUSTERED 
(
	[ImageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Item]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Item]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Item](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[EntityCode] [dbo].[xSmallText] NOT NULL,
	[EntitySubTypeCode] [nvarchar](30) NOT NULL,
	[Code] [dbo].[mediumText] NULL,
	[Name] [dbo].[largeText] NOT NULL,
	[Active] [bit] NOT NULL,
	[UpdatedBy] [uniqueidentifier] NOT NULL,
	[UpdatedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_Item] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Location]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Location]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Location](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[CompanyId] [uniqueidentifier] NOT NULL,
	[OfficialAddressId] [uniqueidentifier] NULL,
	[ContactInformationId] [uniqueidentifier] NULL,
	[RegistrationNo] [dbo].[smallText] NULL,
	[TimezoneId] [uniqueidentifier] NULL,
	[Notes] [dbo].[mediumText] NULL,
 CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Manufacturer]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Manufacturer]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Manufacturer](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Code] [dbo].[xSmallText] NOT NULL,
 CONSTRAINT [PK_Manufacturer] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Menu]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Menu]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Menu](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[GroupId] [uniqueidentifier] NOT NULL,
	[Name] [dbo].[mediumText] NOT NULL,
	[MenuTypeId] [int] NOT NULL,
	[ReferenceEntityId] [dbo].[smallText] NULL,
	[ActionTypeId] [int] NULL,
	[LayoutId] [uniqueidentifier] NULL,
	[WellKnownLink] [dbo].[mediumText] NULL,
	[Link] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NOT NULL,
	[UpdatedBy] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Menu_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[OrganizationUnit]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrganizationUnit]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[OrganizationUnit](
	[TenantId] [uniqueidentifier] NOT NULL,
	[IsLegalEntity] [bit] NOT NULL,
	[Description] [dbo].[largeText] NULL,
	[ContactInformationId] [uniqueidentifier] NULL,
	[OfficialAddressId] [uniqueidentifier] NULL,
	[InvoiceAddressId] [uniqueidentifier] NULL,
	[PostalAddressId] [uniqueidentifier] NULL,
	[AvatarId] [uniqueidentifier] NULL,
	[Timezone] [uniqueidentifier] NULL,
	[Language] [uniqueidentifier] NULL,
 CONSTRAINT [PK_OrganizationUnit] PRIMARY KEY CLUSTERED 
(
	[TenantId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PasswordPolicy]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PasswordPolicy]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PasswordPolicy](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[LockoutAttempt] [int] NULL,
	[LockoutDuration] [int] NULL,
	[PreviousPasswordDifference] [int] NULL,
	[ResetOnFirstLogin] [bit] NULL,
	[IsUppercase] [bit] NULL,
	[IsLowercase] [bit] NULL,
	[IsNumber] [bit] NULL,
	[IsNonAlphaNumeric] [bit] NULL,
	[CanUserChangeOwnPassword] [bit] NULL,
	[AllowFirstLastName] [bit] NULL,
	[PasswordLength] [int] NULL,
	[PasswordAge] [int] NULL,
	[CanAdminResetPassword] [bit] NULL,
	[AllowRecoveryViaMail] [bit] NULL,
 CONSTRAINT [PK_PasswordPolicy] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PickList]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PickList]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PickList](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [smallint] NOT NULL,
	[Name] [dbo].[smallText] NOT NULL,
	[IsStandard] [bit] NOT NULL,
	[EntityId] [smallint] NULL,
	[FixedValueList] [bit] NOT NULL,
	[CustomizeValue] [bit] NOT NULL,
	[IsKeyValueType] [bit] NOT NULL,
	[Active] [bit] NOT NULL,
	[IsDeletetd] [bit] NOT NULL,
	[UpdatedBy] [uniqueidentifier] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_PickList_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PicklistLayout]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistLayout]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PicklistLayout](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[PicklistId] [dbo].[xSmallText] NOT NULL,
	[Name] [dbo].[mediumText] NOT NULL,
	[Type] [int] NOT NULL,
	[LayoutContext] [int] NULL,
	[Layout] [dbo].[xLargeText] NULL,
	[UpdatedOn] [datetime] NOT NULL,
	[UpdatedBy] [uniqueidentifier] NOT NULL,
	[Default] [bit] NOT NULL,
 CONSTRAINT [PK_PicklistLayout] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PickListValue]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PickListValue]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PickListValue](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[PickListId] [smallint] NOT NULL,
	[Key] [dbo].[mediumText] NOT NULL,
	[Text] [dbo].[mediumText] NOT NULL,
	[Active] [bit] NOT NULL,
	[IsDeletetd] [bit] NOT NULL,
	[Flagged] [bit] NOT NULL,
	[IsDefault] [bit] NULL,
	[UpdatedBy] [uniqueidentifier] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[ParentId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_PickListValue] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PickListValueFavourite]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PickListValueFavourite]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PickListValueFavourite](
	[TenantId] [uniqueidentifier] NOT NULL,
	[PickListId] [smallint] NOT NULL,
	[PickListValueId] [uniqueidentifier] NOT NULL,
	[User] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_PickListValueFavourite_1] PRIMARY KEY CLUSTERED 
(
	[PickListValueId] ASC,
	[User] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PickListValueForCity]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PickListValueForCity]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PickListValueForCity](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[PickListValueId] [uniqueidentifier] NOT NULL,
	[CountryId] [uniqueidentifier] NOT NULL,
	[StateId] [uniqueidentifier] NULL,
	[MunicipalityId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_PickListValueForCity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PickListValueForCountry]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PickListValueForCountry]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PickListValueForCountry](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[PickListValueId] [uniqueidentifier] NOT NULL,
	[Currency] [uniqueidentifier] NOT NULL,
	[Language] [uniqueidentifier] NOT NULL,
	[Timezone] [uniqueidentifier] NOT NULL,
	[IsoCode] [dbo].[smallText] NULL,
	[Nationality] [dbo].[smallText] NOT NULL,
 CONSTRAINT [PK_PickListValueForCountry] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PickListValueForCurrency]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PickListValueForCurrency]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PickListValueForCurrency](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[PickListValueId] [uniqueidentifier] NOT NULL,
	[DecimalPrecision] [tinyint] NOT NULL,
	[DecimalVisualization] [tinyint] NOT NULL,
	[IsoCode] [dbo].[smallText] NOT NULL,
 CONSTRAINT [PK_PickListValueForCurrency] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PickListValueForLanguage]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PickListValueForLanguage]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PickListValueForLanguage](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[PickListValueId] [uniqueidentifier] NOT NULL,
	[DateFormat] [dbo].[smallText] NOT NULL,
	[IsoCode] [dbo].[smallText] NULL,
 CONSTRAINT [PK_PickListValueForLanguage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PickListValueForMenuGroup]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PickListValueForMenuGroup]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PickListValueForMenuGroup](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[PickListValueId] [uniqueidentifier] NOT NULL,
	[IconClass] [dbo].[smallText] NULL,
 CONSTRAINT [PK_PickListValueForMenuGroup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PickListValueForMunicipality]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PickListValueForMunicipality]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PickListValueForMunicipality](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[PickListValueId] [uniqueidentifier] NOT NULL,
	[CountryId] [uniqueidentifier] NOT NULL,
	[StateId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_PickListValueForMunicipality] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PickListValueForSecurityFunction]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PickListValueForSecurityFunction]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PickListValueForSecurityFunction](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[PickListValueId] [uniqueidentifier] NOT NULL,
	[scopeEntityId] [smallint] NOT NULL,
 CONSTRAINT [PK_PickListValueForSecurityFunction] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PickListValueForState]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PickListValueForState]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PickListValueForState](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[PickListValueId] [uniqueidentifier] NOT NULL,
	[CountryId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_PickListValueForState] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PickListValueForTimeZone]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PickListValueForTimeZone]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PickListValueForTimeZone](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[PickListValueId] [uniqueidentifier] NOT NULL,
	[GmtDeviation] [decimal](18, 2) NOT NULL,
	[SummerTimeStart] [dbo].[xLargeText] NULL,
	[WinterTimeStart] [dbo].[xLargeText] NULL,
 CONSTRAINT [PK_PickListValueForTimeZone] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ProcurementRule]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProcurementRule]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProcurementRule](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[WarehouseCode] [uniqueidentifier] NOT NULL,
	[ProcurementGroup] [uniqueidentifier] NOT NULL,
	[LocationCode] [uniqueidentifier] NOT NULL,
	[MinimumQuantity] [decimal](18, 2) NULL,
	[MaximumQuantity] [decimal](18, 2) NULL,
	[MultiplicationFactor] [decimal](18, 2) NULL,
	[DaysToPurchase] [decimal](18, 2) NULL,
 CONSTRAINT [PK_ProcurementRule] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Product]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Product]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Product](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[AvatarId] [uniqueidentifier] NULL,
	[CanBeSold] [bit] NULL,
	[CanBePurchased] [bit] NULL,
	[CanBeManufactured] [bit] NULL,
	[ProductCategoryId] [uniqueidentifier] NULL,
	[ProductGroupId] [uniqueidentifier] NULL,
	[ProductTypeId] [uniqueidentifier] NULL,
	[ProcessingTypeId] [uniqueidentifier] NULL,
	[DrawingRefNo] [dbo].[xSmallText] NULL,
	[Revision] [dbo].[xSmallText] NULL,
	[SalesPrice] [dbo].[amount] NULL,
	[Cost] [dbo].[amount] NULL,
	[UOM] [uniqueidentifier] NULL,
	[PurchaseUOM] [uniqueidentifier] NULL,
	[SalesUOM] [uniqueidentifier] NULL,
	[DangerousGoods] [dbo].[xSmallText] NULL,
	[TaxCode] [dbo].[xSmallText] NULL,
	[EANCode] [dbo].[xSmallText] NULL,
	[YieldPercentage] [decimal](18, 2) NULL,
	[ManufacturingLeadTime] [decimal](18, 2) NULL,
	[CustomerLeadTime] [decimal](18, 2) NULL,
	[StockGroupCategoryId] [uniqueidentifier] NULL,
	[DefaultWarehouseId] [uniqueidentifier] NULL,
	[DefaultLocationId] [uniqueidentifier] NULL,
	[StopStructureDrilldown] [bit] NULL,
	[UseMinimumStockLevel] [bit] NULL,
	[MinimumStockLevel] [decimal](18, 2) NULL,
	[IncludeInRequirementCalculation] [bit] NULL,
	[ProcurementRuleId] [uniqueidentifier] NULL,
	[MinimumQuantity] [decimal](18, 2) NULL,
	[MaximumQuantity] [decimal](18, 2) NULL,
	[MultiplicationFactor] [int] NULL,
	[SerialNoCounterId] [uniqueidentifier] NULL,
	[UseInBondSerialNo] [bit] NULL,
	[UseOutBondSerialNo] [bit] NULL,
	[UseInventorySerialNo] [bit] NULL,
	[BatchNoCounterId] [uniqueidentifier] NULL,
	[UseInBondBatchNo] [bit] NULL,
	[UseOutBondBatchNo] [bit] NULL,
	[UseInventoryBatchNo] [bit] NULL,
	[Weight] [decimal](18, 2) NULL,
	[WeightUOM] [uniqueidentifier] NULL,
	[Volume] [decimal](18, 2) NULL,
	[VolumeUOM] [uniqueidentifier] NULL,
	[BatchSize] [decimal](18, 2) NULL,
	[LotSize] [decimal](18, 2) NULL,
	[ManufacturerCode] [dbo].[xSmallText] NULL,
	[ManufacturerProductNumber] [dbo].[xSmallText] NULL,
	[CanHaveVariant] [bit] NULL,
	[LockCode] [uniqueidentifier] NULL,
	[DangerousGoodsId] [uniqueidentifier] NULL,
	[CustomsTariff] [uniqueidentifier] NULL,
	[CountryOfOrigin] [uniqueidentifier] NULL,
	[UseInBoundSerialNo] [bit] NULL,
	[UseOutBoundSerialNo] [bit] NULL,
	[UseInBoundBatchNo] [bit] NULL,
	[UseOutBoundBatchNo] [bit] NULL,
	[CurrentWorkFlowStep] [uniqueidentifier] NULL,
 CONSTRAINT [Product_Id_pk] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ProductAttribute]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductAttribute]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProductAttribute](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[ProductCode] [dbo].[xSmallText] NOT NULL,
	[Position] [int] NOT NULL,
	[AttributeCode] [dbo].[xSmallText] NOT NULL,
 CONSTRAINT [PK_ProductAttribute] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ProductAttributeValue]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductAttributeValue]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProductAttributeValue](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[ProductAttributeId] [uniqueidentifier] NOT NULL,
	[AttributeValueCode] [dbo].[xSmallText] NOT NULL,
	[IsCustomValue] [bit] NOT NULL,
	[AdditionalPrice] [dbo].[amount] NULL,
 CONSTRAINT [PK_ProductAttributeValue] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ProductionOrder]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductionOrder]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProductionOrder](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Code] [dbo].[xSmallText] NOT NULL,
	[ProductCode] [dbo].[xSmallText] NOT NULL,
	[ProductVariantCode] [dbo].[xSmallText] NOT NULL,
	[ProductBOMId] [uniqueidentifier] NOT NULL,
	[Quantity] [decimal](18, 2) NOT NULL,
	[RouteId] [uniqueidentifier] NOT NULL,
	[StartDate] [date] NULL,
	[DueDate] [nchar](10) NULL,
	[Responsible] [uniqueidentifier] NULL,
	[Source] [dbo].[smallText] NULL,
	[Customer] [uniqueidentifier] NULL,
	[ProjectNumber] [dbo].[smallText] NULL,
 CONSTRAINT [PK_ProductionOrder] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ProductionOrderAlternativeComponent]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductionOrderAlternativeComponent]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProductionOrderAlternativeComponent](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[ProductionOrderComponentId] [uniqueidentifier] NOT NULL,
	[ProductVariantCode] [dbo].[xSmallText] NOT NULL,
	[VendorCode] [dbo].[xSmallText] NULL,
	[ManufacturerCode] [dbo].[xSmallText] NULL,
	[Quantity] [decimal](18, 2) NULL,
	[UOM] [uniqueidentifier] NULL,
 CONSTRAINT [PK_ProductionOrderAlternativeComponent] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ProductionOrderComponent]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductionOrderComponent]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProductionOrderComponent](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[ProductionOrderNo] [dbo].[xSmallText] NOT NULL,
	[ProductVariantCode] [dbo].[xSmallText] NOT NULL,
	[ParentId] [uniqueidentifier] NOT NULL,
	[OperationNo] [uniqueidentifier] NOT NULL,
	[Quantity] [decimal](18, 0) NULL,
	[UOM] [uniqueidentifier] NULL,
	[StopStructureDrilldown] [bit] NULL,
	[Cost] [dbo].[amount] NULL,
	[IsSparepart] [bit] NULL,
	[Comment] [dbo].[xLargeText] NULL,
	[BOMText] [dbo].[smallText] NULL,
	[BOMText2] [dbo].[smallText] NULL,
	[DrawingPosition] [dbo].[smallText] NULL,
	[MaterialFactor] [int] NULL,
	[Length] [decimal](18, 2) NULL,
	[LengthUOM] [uniqueidentifier] NULL,
	[Width] [decimal](18, 2) NULL,
	[WidthUOM] [uniqueidentifier] NULL,
	[Height] [decimal](18, 2) NULL,
	[HeightUOM] [uniqueidentifier] NULL,
	[Amount] [dbo].[amount] NULL,
	[TrackingRequired] [bit] NOT NULL,
 CONSTRAINT [PK_ProductionOrderComponent] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ProductionOrderOperation]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductionOrderOperation]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProductionOrderOperation](
	[TenantId] [uniqueidentifier] NOT NULL,
	[OperationNo] [uniqueidentifier] NOT NULL,
	[ProductionOrderNo] [dbo].[xSmallText] NOT NULL,
	[Position] [smallint] NOT NULL,
	[WorkcenterCode] [dbo].[xSmallText] NOT NULL,
 CONSTRAINT [PK_ProductionOrderOperation] PRIMARY KEY CLUSTERED 
(
	[OperationNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ProductionOrderOperationCompetence]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductionOrderOperationCompetence]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProductionOrderOperationCompetence](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[OperationNo] [uniqueidentifier] NOT NULL,
	[QualificationId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_ProductionOrderOperationWorkcenterCompetence] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ProductionOrderOperationConfiguration]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductionOrderOperationConfiguration]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProductionOrderOperationConfiguration](
	[TenantId] [uniqueidentifier] NOT NULL,
	[OperationNo] [uniqueidentifier] NOT NULL,
	[CostPerHour] [dbo].[amount] NULL,
	[LabourCostPerHour] [dbo].[amount] NULL,
	[PricePerHour] [dbo].[amount] NULL,
	[LabourPricePerHour] [dbo].[amount] NULL,
	[AutoReleaseMode] [uniqueidentifier] NULL,
	[AutoReleaseNextOperation] [bit] NULL,
	[CanUseUnmannedStop] [bit] NULL,
	[Slack] [decimal](18, 2) NULL,
	[SlackUOM] [uniqueidentifier] NULL,
	[LossTimePercentage] [decimal](18, 2) NULL,
	[CapitalCost] [decimal](18, 2) NULL,
	[MaintenanceCost] [decimal](18, 2) NULL,
	[VariousCost] [decimal](18, 2) NULL,
	[UseWarehouse] [bit] NULL,
	[FromWareHouseId] [uniqueidentifier] NULL,
	[ToWareHouseId] [uniqueidentifier] NULL,
	[TimeCalculationType] [uniqueidentifier] NULL,
	[ProductionCount] [int] NULL,
	[SetupTime] [decimal](18, 2) NULL,
	[ProductionTime] [decimal](18, 2) NULL,
	[RestructuringTime] [decimal](18, 2) NULL,
	[ProgrammingTime] [decimal](18, 2) NULL,
	[OperatorShareSetup] [decimal](18, 2) NULL,
	[OperatorShareProduction] [decimal](18, 2) NULL,
	[OperatorShareRestructuring] [decimal](18, 2) NULL,
	[UseOperationOffset] [bit] NULL,
	[OffsetTime] [decimal](18, 2) NULL,
	[OffsetUnit] [decimal](18, 2) NULL,
	[RegisterOperatorTime] [bit] NULL,
	[RegisterMachineTime] [bit] NULL,
	[ShowPlannedOperatorTime] [bit] NULL,
	[RegisterPlannedMachineTime] [bit] NULL,
	[RegisterConsumed] [bit] NULL,
	[RegisterProduced] [bit] NULL,
 CONSTRAINT [PK_ProductionOrderOperationConfiguration] PRIMARY KEY CLUSTERED 
(
	[OperationNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ProductionOrderOperationTool]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductionOrderOperationTool]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProductionOrderOperationTool](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[OperationNo] [uniqueidentifier] NOT NULL,
	[ToolCode] [dbo].[xSmallText] NOT NULL,
 CONSTRAINT [PK_ProductionOrderOperationTool] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ProductionOrderProduced]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductionOrderProduced]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProductionOrderProduced](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[ProductionOrderId] [dbo].[xSmallText] NOT NULL,
	[ProductId] [dbo].[xSmallText] NOT NULL,
	[Quantity] [decimal](18, 2) NOT NULL,
	[UOM] [uniqueidentifier] NOT NULL,
	[Cost] [dbo].[amount] NOT NULL,
	[ProducedType] [tinyint] NOT NULL,
 CONSTRAINT [PK_ProductionOrderProduced] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ProductionSet]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductionSet]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProductionSet](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[ProductionTaskId] [uniqueidentifier] NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_ProductionSet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ProductionSetConsumed]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductionSetConsumed]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProductionSetConsumed](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[ProductionTaskId] [uniqueidentifier] NOT NULL,
	[ProductionSetId] [uniqueidentifier] NOT NULL,
	[ProductionSetProducedId] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NOT NULL,
	[ProductVariantId] [uniqueidentifier] NOT NULL,
	[PlannedQuantity] [decimal](18, 2) NULL,
	[ConsumedQuantity] [decimal](18, 2) NULL,
	[SerialNo] [dbo].[smallText] NULL,
	[BatchNo] [dbo].[smallText] NULL,
 CONSTRAINT [PK_ProductionSetConsumed] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ProductionSetProduced]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductionSetProduced]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProductionSetProduced](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[ProductionTaskId] [uniqueidentifier] NOT NULL,
	[ProductionSetId] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NOT NULL,
	[ProductVariantId] [uniqueidentifier] NOT NULL,
	[PlannedQuantity] [decimal](18, 2) NOT NULL,
	[ProducedQuantity] [decimal](18, 2) NULL,
	[SerialNo] [dbo].[xSmallText] NULL,
	[BatchNo] [dbo].[xSmallText] NULL,
	[Result] [tinyint] NULL,
	[ScrapCode] [dbo].[smallText] NULL,
 CONSTRAINT [PK_ProductionSetProduced] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ProductionTask]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductionTask]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProductionTask](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[CollectionId] [uniqueidentifier] NULL,
	[OperationId] [uniqueidentifier] NOT NULL,
	[ProductionOrderId] [uniqueidentifier] NOT NULL,
	[PlannedQuantity] [decimal](18, 2) NULL,
	[PlannedStartTime] [datetime] NULL,
	[PlannedEndTime] [datetime] NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
 CONSTRAINT [PK_ProductionTask] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ProductOperation]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductOperation]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProductOperation](
	[TenantId] [uniqueidentifier] NOT NULL,
	[OperationNo] [dbo].[xSmallText] NOT NULL,
	[ProductRouteId] [uniqueidentifier] NOT NULL,
	[Position] [smallint] NOT NULL,
	[OperationTypeId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_ProductOperation] PRIMARY KEY CLUSTERED 
(
	[OperationNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ProductOperationWorkcenter]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductOperationWorkcenter]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProductOperationWorkcenter](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[OperationNo] [dbo].[xSmallText] NOT NULL,
	[WorkCenterCode] [dbo].[xSmallText] NOT NULL,
	[IsDefault] [bit] NOT NULL,
 CONSTRAINT [PK_ProductOperationWorkcenter] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ProductOperationWorkcenterCompetence]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductOperationWorkcenterCompetence]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProductOperationWorkcenterCompetence](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[OperationWorkcenterId] [uniqueidentifier] NOT NULL,
	[CompetenceId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_ProductOperationWorkcenterCompetence] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ProductOperationWorkcenterConfiguration]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductOperationWorkcenterConfiguration]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProductOperationWorkcenterConfiguration](
	[TenantId] [uniqueidentifier] NOT NULL,
	[OperationWorkcenterId] [uniqueidentifier] NOT NULL,
	[CostPerHour] [dbo].[amount] NULL,
	[LabourCostPerHour] [dbo].[amount] NULL,
	[PricePerHour] [dbo].[amount] NULL,
	[LabourPricePerHour] [dbo].[amount] NULL,
	[AutoReleaseMode] [uniqueidentifier] NULL,
	[AutoReleaseNextOperation] [bit] NULL,
	[IsUnmannedStop] [bit] NULL,
	[Slack] [decimal](18, 2) NULL,
	[SlackUOM] [uniqueidentifier] NULL,
	[LossTimePercentage] [decimal](18, 2) NULL,
	[CapitalCost] [decimal](18, 2) NULL,
	[MaintenanceCost] [decimal](18, 2) NULL,
	[VariousCost] [decimal](18, 2) NULL,
	[UseWarehouseForConsumables] [bit] NULL,
	[ConsumableWarehouseCode] [dbo].[xSmallText] NULL,
	[UseWarehouseForProduced] [bit] NULL,
	[ProducedWarehouseCode] [dbo].[xSmallText] NULL,
	[TimeCalculationType] [uniqueidentifier] NULL,
	[ProductionCount] [int] NULL,
	[SetupTime] [decimal](18, 2) NULL,
	[ProductionTime] [decimal](18, 2) NULL,
	[RestructuringTime] [decimal](18, 2) NULL,
	[ProgrammingTime] [decimal](18, 2) NULL,
	[OperatorShareSetup] [decimal](18, 2) NULL,
	[OperatorShareProduction] [decimal](18, 2) NULL,
	[OperatorShareRestructuring] [decimal](18, 2) NULL,
	[OperatorShareProgramming] [decimal](18, 2) NULL,
	[UseOperationOffset] [bit] NULL,
	[OffsetTime] [decimal](18, 2) NULL,
	[OffsetUnit] [decimal](18, 2) NULL,
	[RegisterOperatorTime] [bit] NULL,
	[RegisterMachineTime] [bit] NULL,
	[ShowPlannedOperatorTime] [bit] NULL,
	[ShowPlannedMachineTime] [bit] NULL,
	[RegisterConsumed] [bit] NULL,
	[RegisterProduced] [bit] NULL,
 CONSTRAINT [PK_ProductOperationWorkcenterConfiguration] PRIMARY KEY CLUSTERED 
(
	[OperationWorkcenterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ProductRoute]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductRoute]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProductRoute](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[ProductCode] [dbo].[xSmallText] NOT NULL,
	[IsDefault] [bit] NOT NULL,
	[Revision] [dbo].[xSmallText] NULL,
	[BoMId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_ProductRoute] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ProductVariant]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductVariant]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProductVariant](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Code] [dbo].[xSmallText] NOT NULL,
	[ProductCode] [dbo].[xSmallText] NOT NULL,
	[SalePrice] [dbo].[amount] NULL,
	[Cost] [dbo].[amount] NULL,
	[Weight] [decimal](18, 2) NULL,
	[WeightUOM] [uniqueidentifier] NULL,
	[Volume] [decimal](18, 2) NULL,
	[VolumeUOM] [uniqueidentifier] NULL,
	[AvatarId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_ProductVariant] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ProductVariantAttribute]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductVariantAttribute]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProductVariantAttribute](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[ProductVariantCode] [dbo].[xSmallText] NOT NULL,
	[AttributeCode] [dbo].[smallText] NOT NULL,
 CONSTRAINT [PK_ProductVariantAttribute] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ProductVariantAttributeValue]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductVariantAttributeValue]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProductVariantAttributeValue](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[VariantAttributeId] [uniqueidentifier] NOT NULL,
	[AttributeValueCode] [dbo].[xSmallText] NOT NULL,
	[IsCustomValue] [bit] NOT NULL,
	[AdditionalPrice] [dbo].[amount] NOT NULL,
 CONSTRAINT [PK_ProductVariantAttributeValue] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ProductVariantRule]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductVariantRule]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProductVariantRule](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[ProductCode] [dbo].[xSmallText] NOT NULL,
	[RuleType] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_ProductVariantRule] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ProductVariantRuleAttribute]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductVariantRuleAttribute]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProductVariantRuleAttribute](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[ProductVariantRuleId] [uniqueidentifier] NOT NULL,
	[AttributeCode] [dbo].[xSmallText] NOT NULL,
 CONSTRAINT [PK_ProductVariantExclusionDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ProductVariantRuleAttributeValue]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductVariantRuleAttributeValue]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProductVariantRuleAttributeValue](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[RuleAttributeId] [uniqueidentifier] NOT NULL,
	[AttributeValueCode] [dbo].[xSmallText] NOT NULL,
 CONSTRAINT [PK_ProductVariantRuleAttributeValue] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ProductVendor]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductVendor]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProductVendor](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[ProductCode] [dbo].[xSmallText] NOT NULL,
	[VendorCode] [dbo].[xSmallText] NOT NULL,
	[IsDefault] [bit] NOT NULL,
 CONSTRAINT [PK_ProductVendor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Resource]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Resource](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Key] [dbo].[mediumText] NOT NULL,
	[Value] [dbo].[xLargeText] NOT NULL,
	[Language] [dbo].[xSmallText] NOT NULL,
 CONSTRAINT [PK_Resource] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Resource_Test]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource_Test]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Resource_Test](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Key] [dbo].[mediumText] NOT NULL,
	[Value] [dbo].[xLargeText] NOT NULL,
	[Language] [dbo].[xSmallText] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Role]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Role]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Role](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [dbo].[mediumText] NOT NULL,
	[Type] [tinyint] NOT NULL,
 CONSTRAINT [PK_RL_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Room]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Room]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Room](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[LocationId] [uniqueidentifier] NOT NULL,
	[RoomNo] [int] NULL,
	[RoomTypeId] [uniqueidentifier] NOT NULL,
	[Floor] [int] NULL,
 CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[RouteTemplate]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RouteTemplate]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RouteTemplate](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Code] [dbo].[xSmallText] NOT NULL,
	[TemplateType] [uniqueidentifier] NOT NULL,
	[ProductSubGroupId] [uniqueidentifier] NOT NULL,
	[IsDefault] [bit] NOT NULL,
 CONSTRAINT [PK_RouteTemplate] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[RouteTemplateOperation]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RouteTemplateOperation]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RouteTemplateOperation](
	[TenantId] [uniqueidentifier] NOT NULL,
	[OperationNo] [dbo].[xSmallText] NOT NULL,
	[RouteTemplateCode] [dbo].[xSmallText] NOT NULL,
	[Position] [smallint] NOT NULL,
	[OperationTypeId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_RouteTemplateOperation] PRIMARY KEY CLUSTERED 
(
	[OperationNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[RouteTemplateOperationWorkcenter]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RouteTemplateOperationWorkcenter]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RouteTemplateOperationWorkcenter](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[OperationNo] [dbo].[xSmallText] NOT NULL,
	[WorkcenterCode] [dbo].[xSmallText] NOT NULL,
	[IsDefault] [bit] NOT NULL,
 CONSTRAINT [PK_RouteTemplateOperationWorkcenter] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[RouteTemplateOperationWorkcenterCompetence]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RouteTemplateOperationWorkcenterCompetence]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RouteTemplateOperationWorkcenterCompetence](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[OperationWorkcenterId] [uniqueidentifier] NOT NULL,
	[CompetenceId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_RouteTemplateOperationWorkcenterCompetence] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[RouteTemplateOperationWorkcenterConfiguration]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RouteTemplateOperationWorkcenterConfiguration]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RouteTemplateOperationWorkcenterConfiguration](
	[TenantId] [uniqueidentifier] NOT NULL,
	[OperationWorkcenterId] [uniqueidentifier] NOT NULL,
	[CostPerHour] [dbo].[amount] NULL,
	[LabourCostPerHour] [dbo].[amount] NULL,
	[PricePerHour] [dbo].[amount] NULL,
	[LabourPricePerHour] [dbo].[amount] NULL,
	[AutoReleaseMode] [uniqueidentifier] NULL,
	[AutoReleaseNextOperation] [bit] NULL,
	[IsUnmannedStop] [bit] NULL,
	[Slack] [decimal](18, 2) NULL,
	[SlackUOM] [uniqueidentifier] NULL,
	[LossTimePercentage] [decimal](18, 2) NULL,
	[CapitalCost] [dbo].[amount] NULL,
	[MaintenanceCost] [dbo].[amount] NULL,
	[VariousCost] [dbo].[amount] NULL,
	[UseWarehouseForConsumables] [bit] NULL,
	[ConsumableWarehouseCode] [dbo].[xSmallText] NULL,
	[UseWarehouseForProduced] [bit] NULL,
	[ProducedWarehouseCode] [dbo].[xSmallText] NULL,
	[TimeCalculationType] [uniqueidentifier] NULL,
	[ProductionCount] [int] NULL,
	[SetupTime] [decimal](18, 2) NULL,
	[ProductionTime] [decimal](18, 2) NULL,
	[RestructuringTime] [decimal](18, 2) NULL,
	[ProgrammingTime] [decimal](18, 2) NULL,
	[OperatorShareSetup] [decimal](18, 2) NULL,
	[OperatorShareProduction] [decimal](18, 2) NULL,
	[OperatorShareRestructuring] [decimal](18, 2) NULL,
	[OperatorShareProgramming] [decimal](18, 2) NULL,
	[UseOperationOffset] [bit] NULL,
	[OffsetTime] [decimal](18, 2) NULL,
	[OffsetUnit] [decimal](18, 2) NULL,
	[RegisterOperatorTime] [bit] NULL,
	[RegisterMachineTime] [bit] NULL,
	[ShowPlannedOperatorTime] [bit] NULL,
	[ShowPlannedMachineTime] [bit] NULL,
	[RegisterConsumed] [bit] NULL,
	[RegisterProduced] [bit] NULL,
 CONSTRAINT [PK_RouteTemplateOperationWorkcenterConfiguration] PRIMARY KEY CLUSTERED 
(
	[OperationWorkcenterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[RouteTemplateOperationWorkcenterTool]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RouteTemplateOperationWorkcenterTool]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RouteTemplateOperationWorkcenterTool](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[OperationWorkcenterId] [uniqueidentifier] NOT NULL,
	[ToolCode] [dbo].[xSmallText] NOT NULL,
 CONSTRAINT [PK_RouteTemplateOperationWorkcenterTool] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Rule]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Rule]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Rule](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[EntityId] [dbo].[xSmallText] NOT NULL,
	[RuleName] [dbo].[mediumText] NOT NULL,
	[RuleType] [int] NOT NULL,
	[Source] [nvarchar](max) NULL,
	[Target] [nvarchar](max) NULL,
	[UpdatedOn] [datetime] NOT NULL CONSTRAINT [DF_Rule_UpdatedOn]  DEFAULT (getdate()),
	[UpdatedBy] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Rule] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Scheduler]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Scheduler]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Scheduler](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[BatchTypeId] [uniqueidentifier] NOT NULL,
	[SyncTime] [int] NULL,
	[RecurrencePattern] [tinyint] NULL,
 CONSTRAINT [PK_Scheduler] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[SchedulerDaily]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchedulerDaily]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SchedulerDaily](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[SchedulerId] [uniqueidentifier] NOT NULL,
	[Unit] [int] NULL,
	[Value] [int] NULL,
 CONSTRAINT [PK_SchedulerDaily] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[SchedulerMonthly]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchedulerMonthly]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SchedulerMonthly](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[SchedulerId] [uniqueidentifier] NOT NULL,
	[Unit] [int] NULL,
	[DayValue1] [int] NULL,
	[DayValue2] [int] NULL,
	[TheValue1] [int] NULL,
	[TheValue2] [int] NULL,
	[TheValue3] [int] NULL,
 CONSTRAINT [PK_SchedulerMonthly] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[SchedulerWeekly]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchedulerWeekly]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SchedulerWeekly](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[SchedulerId] [uniqueidentifier] NOT NULL,
	[Value] [int] NULL,
	[Monday] [bit] NULL,
	[Tuesday] [bit] NULL,
	[Wednesday] [bit] NULL,
	[Thrusday] [bit] NULL,
	[Friday] [bit] NULL,
	[Saturday] [bit] NULL,
	[Sunday] [bit] NULL,
 CONSTRAINT [PK_SchedulerWeekly] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[SchedulerYearly]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchedulerYearly]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SchedulerYearly](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[SchedulerId] [uniqueidentifier] NOT NULL,
	[RecurrenceValue] [int] NULL,
	[Unit] [int] NULL,
	[OnValue1] [int] NULL,
	[OnValue2] [int] NULL,
	[TheValue1] [int] NULL,
	[TheValue2] [int] NULL,
	[TheValue3] [int] NULL,
 CONSTRAINT [PK_SchedulerYearly] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Settings]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Settings]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Settings](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[Context] [int] NULL,
	[Content] [dbo].[xLargeText] NULL,
	[UpdatedOn] [datetime] NOT NULL,
	[UpdatedBy] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_EL_Settings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[SMS]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SMS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SMS](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Recipient] [dbo].[smallText] NOT NULL,
	[Sender] [dbo].[smallText] NOT NULL,
	[Message] [dbo].[largeText] NOT NULL,
	[CommunicationDirectionId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_SMS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[SMSTemplate]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SMSTemplate]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SMSTemplate](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [dbo].[mediumText] NOT NULL,
	[Context] [dbo].[xSmallText] NOT NULL,
	[Body] [dbo].[xLargeText] NOT NULL,
	[ContextType] [int] NULL,
 CONSTRAINT [PK_SMSTemplate] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Task]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Task]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Task](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[ProductionOrderOperationId] [uniqueidentifier] NOT NULL,
	[WorkcenterCode] [dbo].[xSmallText] NOT NULL,
	[ProjectNumber] [dbo].[smallText] NULL,
	[PlannedDateTime] [datetime] NULL,
 CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Tenant]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tenant]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tenant](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[IsOrganization] [bit] NULL,
	[OrgNo] [dbo].[xSmallText] NULL,
	[IsLegalEntity] [bit] NULL,
	[TenantType] [tinyint] NOT NULL,
	[ContactInformationId] [uniqueidentifier] NULL,
	[ContactId] [uniqueidentifier] NULL,
	[OfficialAddressId] [uniqueidentifier] NULL,
	[InvoiceAddressId] [uniqueidentifier] NULL,
	[PostalAddressId] [uniqueidentifier] NULL,
	[AvatarId] [uniqueidentifier] NULL,
	[PasswordPolicyId] [uniqueidentifier] NULL,
	[IsSystemRoot] [bit] NULL,
	[DefaultTimezoneId] [uniqueidentifier] NULL,
	[SuperAdminId] [uniqueidentifier] NULL,
	[SubscriptionId] [uniqueidentifier] NULL,
	[TenantServiceStatusId] [uniqueidentifier] NULL,
	[PreferredLanguageId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Tenant_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[TenantIPRange]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TenantIPRange]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TenantIPRange](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[StartIP] [dbo].[mediumText] NOT NULL,
	[EndIP] [dbo].[mediumText] NOT NULL,
	[ParentId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_TenantIPRange] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[TenantServiceStatus]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TenantServiceStatus]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TenantServiceStatus](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[Status] [int] NOT NULL,
	[Version] [dbo].[smallText] NULL,
	[Command] [int] NULL,
	[Param] [dbo].[mediumText] NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[TenantSubscription]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TenantSubscription]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TenantSubscription](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [dbo].[mediumText] NULL,
	[Group] [uniqueidentifier] NOT NULL,
	[RecurringPrice] [dbo].[amount] NULL,
	[Duration] [tinyint] NULL,
	[SetUpPrice] [dbo].[amount] NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_TenentSubscription] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[TenantSubscriptionEntity]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TenantSubscriptionEntity]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TenantSubscriptionEntity](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[TenantSubscriptionId] [uniqueidentifier] NOT NULL,
	[EntityId] [dbo].[xSmallText] NOT NULL,
	[LimitNumber] [int] NULL,
	[LimitType] [tinyint] NULL,
 CONSTRAINT [PK_TenentSubscriptionEntity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[TenantSubscriptionEntityDetail]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TenantSubscriptionEntityDetail]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TenantSubscriptionEntityDetail](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[TenantSubscriptionEntityId] [uniqueidentifier] NOT NULL,
	[Context] [uniqueidentifier] NOT NULL,
	[RecurringPrice] [dbo].[amount] NULL,
	[RecurringDuration] [tinyint] NULL,
	[OneTimePrice] [dbo].[amount] NULL,
	[OneTimeDuration] [tinyint] NULL,
 CONSTRAINT [PK_TenentSubscriptionEntityDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Tool]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tool]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tool](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Code] [dbo].[xSmallText] NOT NULL,
 CONSTRAINT [PK_Tool] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[User]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[User](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[FirstName] [dbo].[smallText] NOT NULL,
	[MiddleName] [dbo].[smallText] NULL,
	[LastName] [dbo].[smallText] NOT NULL,
	[ContactInformationId] [uniqueidentifier] NULL,
	[OfficialAddressId] [uniqueidentifier] NULL,
	[InvoiceAddressId] [uniqueidentifier] NULL,
	[PostalAddressId] [uniqueidentifier] NULL,
	[AvatarId] [uniqueidentifier] NULL,
	[OrgUnitId] [dbo].[xSmallText] NULL,
	[TimezoneId] [uniqueidentifier] NULL,
	[PreferredLanguageId] [uniqueidentifier] NULL,
	[Gender] [tinyint] NULL,
	[DOB] [date] NULL,
	[DOBIsApproximate] [bit] NULL,
	[NationalityId] [uniqueidentifier] NULL,
	[CategoryId] [uniqueidentifier] NULL,
	[CrewId] [uniqueidentifier] NULL,
	[HourlyRate] [dbo].[amount] NULL,
	[UserGroupId] [uniqueidentifier] NULL,
	[Comments] [dbo].[largeText] NULL,
	[CurrentWorkFlowStep] [uniqueidentifier] NULL,
	[UserCredentialId] [uniqueidentifier] NULL,
	[UserEmploymentId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_US_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[UserCompany]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserCompany]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserCompany](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[CompanyId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_UserCompany] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[UserDepartment]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserDepartment]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserDepartment](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[DepartmentId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_UserDepartment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[UserEmployment]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserEmployment]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserEmployment](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[EmploymentStart] [date] NULL,
	[EmploymentEnd] [date] NULL,
	[EmploymentStatusId] [uniqueidentifier] NULL,
	[ReasonForLeavingId] [uniqueidentifier] NULL,
	[DesignationId] [uniqueidentifier] NULL,
	[ReportsToUserId] [uniqueidentifier] NULL,
	[AnnualLeaveEntitlementDays] [decimal](18, 2) NULL,
 CONSTRAINT [PK_UserEmployment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[UserInRole]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserInRole]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserInRole](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_UserInRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[UserLocation]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserLocation]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserLocation](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[LocationId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_UserLocation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Vendor]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Vendor]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Vendor](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Code] [dbo].[xSmallText] NOT NULL,
	[Comment] [dbo].[xLargeText] NULL,
 CONSTRAINT [PK_Vendor] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Warehouse]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Warehouse]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Warehouse](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Code] [dbo].[xSmallText] NOT NULL,
	[AllowNegative] [bit] NOT NULL,
	[Comment] [dbo].[xLargeText] NULL,
 CONSTRAINT [PK_Warehouse] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[WarehouseLocation]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WarehouseLocation]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WarehouseLocation](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Code] [dbo].[xSmallText] NOT NULL,
	[WarehouseCode] [dbo].[xSmallText] NOT NULL,
 CONSTRAINT [PK_WarehouseLocation] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Workcenter]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Workcenter]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Workcenter](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Code] [dbo].[xSmallText] NOT NULL,
	[TypeId] [uniqueidentifier] NOT NULL,
	[SubTypeId] [uniqueidentifier] NOT NULL,
	[AvatarId] [uniqueidentifier] NULL,
	[OperationTypeId] [uniqueidentifier] NULL,
	[DepartmentId] [uniqueidentifier] NULL,
	[ProductionLineId] [uniqueidentifier] NULL,
	[PlanningGroupCode] [dbo].[xSmallText] NULL,
	[IsObsolete] [bit] NULL,
	[CalendarId] [uniqueidentifier] NULL,
	[VendorCode] [dbo].[xSmallText] NULL,
	[ProductCode] [dbo].[xSmallText] NULL,
	[IsDefault] [bit] NULL,
 CONSTRAINT [PK_Workcenter] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[WorkcenterCompetence]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkcenterCompetence]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WorkcenterCompetence](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[WorkcenterCode] [dbo].[xSmallText] NOT NULL,
	[CompetenceId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_WorkcenterCompetence] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[WorkcenterConfiguration]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkcenterConfiguration]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WorkcenterConfiguration](
	[TenantId] [uniqueidentifier] NOT NULL,
	[WorkcenterCode] [dbo].[xSmallText] NOT NULL,
	[CostPerHour] [dbo].[amount] NULL,
	[LabourCostPerHour] [dbo].[amount] NULL,
	[PricePerHour] [dbo].[amount] NULL,
	[LabourPricePerHour] [dbo].[amount] NULL,
	[AutoReleaseNextOperation] [bit] NULL,
	[AutoReleaseMode] [uniqueidentifier] NULL,
	[IsUnmannedStop] [bit] NULL,
	[Slack] [decimal](18, 2) NULL,
	[SlackUOM] [uniqueidentifier] NULL,
	[LossTimePercentage] [decimal](18, 2) NULL,
	[CapitalCost] [decimal](18, 2) NULL,
	[MaintenanceCost] [decimal](18, 2) NULL,
	[VariousCost] [decimal](18, 2) NULL,
	[UseWarehouseForConsumables] [bit] NULL,
	[ConsumableWarehouseCode] [dbo].[xSmallText] NULL,
	[UseWarehouseForProduced] [bit] NULL,
	[ProducedWarehouseCode] [dbo].[xSmallText] NULL,
	[RegisterOperatorTime] [bit] NULL,
	[RegisterMachineTime] [bit] NULL,
	[ShowPlannedOperatorTime] [bit] NULL,
	[ShowPlannedMachineTime] [bit] NULL,
	[RegisterConsumed] [bit] NULL,
	[RegisterProduced] [bit] NULL,
 CONSTRAINT [PK_WorkcenterConfiguration] PRIMARY KEY CLUSTERED 
(
	[WorkcenterCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[WorkcenterEquipment]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkcenterEquipment]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WorkcenterEquipment](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[WorkcenterCode] [dbo].[xSmallText] NOT NULL,
	[EquipmentCode] [dbo].[xSmallText] NOT NULL,
 CONSTRAINT [PK_WorkcenterEquipment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[WorkcenterTool]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkcenterTool]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WorkcenterTool](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[WorkcenterCode] [dbo].[xSmallText] NOT NULL,
	[ToolCode] [dbo].[xSmallText] NOT NULL,
 CONSTRAINT [PK_WorkcenterTool] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[WorkcenterUser]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkcenterUser]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WorkcenterUser](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[WorkcenterCode] [dbo].[xSmallText] NOT NULL,
	[UserCode] [dbo].[xSmallText] NOT NULL,
 CONSTRAINT [PK_WorkcenterUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[WorkFlow]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlow]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WorkFlow](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[EntityId] [dbo].[xSmallText] NOT NULL,
	[Status] [bit] NOT NULL,
	[SubType] [dbo].[smallText] NULL,
 CONSTRAINT [PK_WF_WorkFlowConfiguration] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[WorkFlowInnerStep]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowInnerStep]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WorkFlowInnerStep](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[WorkFlowStepId] [uniqueidentifier] NOT NULL,
	[WorkFlowId] [uniqueidentifier] NOT NULL,
	[TransitionType] [uniqueidentifier] NULL,
	[SequenceNumber] [tinyint] NULL,
 CONSTRAINT [PK_WorkFlowInnerStepConfiguration] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[WorkFlowOperation]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowOperation]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WorkFlowOperation](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[OperationType] [tinyint] NOT NULL,
	[WorkFlowId] [uniqueidentifier] NOT NULL,
	[IsSync] [bit] NULL,
 CONSTRAINT [PK_WF_Operation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[WorkFlowProcess]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowProcess]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WorkFlowProcess](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[WorkFlowId] [uniqueidentifier] NOT NULL,
	[OperationOrTransactionId] [uniqueidentifier] NOT NULL,
	[OperationOrTransactionType] [tinyint] NOT NULL,
	[ProcessType] [tinyint] NOT NULL,
 CONSTRAINT [PK_WF_WorkFlowProcess] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[WorkFlowProcessTask]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowProcessTask]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WorkFlowProcessTask](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[WorkFlowId] [uniqueidentifier] NOT NULL,
	[WorkFlowProcessId] [uniqueidentifier] NOT NULL,
	[ProcessCode] [uniqueidentifier] NULL,
	[SequenceNumber] [tinyint] NULL,
 CONSTRAINT [PK_WF_WorkFlowProcessTask] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[WorkFlowRole]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowRole]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WorkFlowRole](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[WorkFlowStepId] [uniqueidentifier] NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	[WorkFlowId] [uniqueidentifier] NULL,
	[AssignmentOperatorType] [tinyint] NOT NULL,
 CONSTRAINT [PK_WF_ConfigurationRoleAssignment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[WorkFlowStep]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowStep]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WorkFlowStep](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[WorkFlowId] [uniqueidentifier] NULL,
	[TransitionType] [uniqueidentifier] NULL,
	[SequenceNumber] [tinyint] NULL,
	[IsAssignmentMandatory] [bit] NULL,
	[AllotedTime] [int] NULL,
	[CriticalTime] [int] NULL,
 CONSTRAINT [PK_WorkFlowStep] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[WorkFlowTransitionHistory]    Script Date: 15-Jul-19 15:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowTransitionHistory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WorkFlowTransitionHistory](
	[TenantId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[RefId] [uniqueidentifier] NOT NULL,
	[EntityId] [dbo].[xSmallText] NOT NULL,
	[WorkFlowStepId] [uniqueidentifier] NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[AssignedUserId] [uniqueidentifier] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
 CONSTRAINT [PK_WF_TransitionHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AttributeValue_Attribute]') AND parent_object_id = OBJECT_ID(N'[dbo].[AttributeValue]'))
ALTER TABLE [dbo].[AttributeValue]  WITH CHECK ADD  CONSTRAINT [FK_AttributeValue_Attribute] FOREIGN KEY([AttributeCode])
REFERENCES [dbo].[Attribute] ([Code])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AttributeValue_Attribute]') AND parent_object_id = OBJECT_ID(N'[dbo].[AttributeValue]'))
ALTER TABLE [dbo].[AttributeValue] CHECK CONSTRAINT [FK_AttributeValue_Attribute]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BatchType_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[BatchTypes]'))
ALTER TABLE [dbo].[BatchTypes]  WITH CHECK ADD  CONSTRAINT [FK_BatchType_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BatchType_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[BatchTypes]'))
ALTER TABLE [dbo].[BatchTypes] CHECK CONSTRAINT [FK_BatchType_Tenant]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BOMAlternativeComponent_BOMComponent]') AND parent_object_id = OBJECT_ID(N'[dbo].[BOMAlternativeComponent]'))
ALTER TABLE [dbo].[BOMAlternativeComponent]  WITH CHECK ADD  CONSTRAINT [FK_BOMAlternativeComponent_BOMComponent] FOREIGN KEY([BOMComponentId])
REFERENCES [dbo].[BOMComponent] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BOMAlternativeComponent_BOMComponent]') AND parent_object_id = OBJECT_ID(N'[dbo].[BOMAlternativeComponent]'))
ALTER TABLE [dbo].[BOMAlternativeComponent] CHECK CONSTRAINT [FK_BOMAlternativeComponent_BOMComponent]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BOMAlternativeComponent_ProductVendor]') AND parent_object_id = OBJECT_ID(N'[dbo].[BOMAlternativeComponent]'))
ALTER TABLE [dbo].[BOMAlternativeComponent]  WITH CHECK ADD  CONSTRAINT [FK_BOMAlternativeComponent_ProductVendor] FOREIGN KEY([ProductVendorId])
REFERENCES [dbo].[ProductVendor] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BOMAlternativeComponent_ProductVendor]') AND parent_object_id = OBJECT_ID(N'[dbo].[BOMAlternativeComponent]'))
ALTER TABLE [dbo].[BOMAlternativeComponent] CHECK CONSTRAINT [FK_BOMAlternativeComponent_ProductVendor]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BOMComponent_BOM]') AND parent_object_id = OBJECT_ID(N'[dbo].[BOMComponent]'))
ALTER TABLE [dbo].[BOMComponent]  WITH CHECK ADD  CONSTRAINT [FK_BOMComponent_BOM] FOREIGN KEY([BOMId])
REFERENCES [dbo].[BOM] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BOMComponent_BOM]') AND parent_object_id = OBJECT_ID(N'[dbo].[BOMComponent]'))
ALTER TABLE [dbo].[BOMComponent] CHECK CONSTRAINT [FK_BOMComponent_BOM]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BOMComponent_BOM1]') AND parent_object_id = OBJECT_ID(N'[dbo].[BOMComponent]'))
ALTER TABLE [dbo].[BOMComponent]  WITH CHECK ADD  CONSTRAINT [FK_BOMComponent_BOM1] FOREIGN KEY([ChildBOMId])
REFERENCES [dbo].[BOM] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BOMComponent_BOM1]') AND parent_object_id = OBJECT_ID(N'[dbo].[BOMComponent]'))
ALTER TABLE [dbo].[BOMComponent] CHECK CONSTRAINT [FK_BOMComponent_BOM1]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BOMComponent_ProductOperation]') AND parent_object_id = OBJECT_ID(N'[dbo].[BOMComponent]'))
ALTER TABLE [dbo].[BOMComponent]  WITH CHECK ADD  CONSTRAINT [FK_BOMComponent_ProductOperation] FOREIGN KEY([OperationNo])
REFERENCES [dbo].[ProductOperation] ([OperationNo])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BOMComponent_ProductOperation]') AND parent_object_id = OBJECT_ID(N'[dbo].[BOMComponent]'))
ALTER TABLE [dbo].[BOMComponent] CHECK CONSTRAINT [FK_BOMComponent_ProductOperation]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BOMComponentAttribute_Attribute]') AND parent_object_id = OBJECT_ID(N'[dbo].[BOMComponentAttribute]'))
ALTER TABLE [dbo].[BOMComponentAttribute]  WITH CHECK ADD  CONSTRAINT [FK_BOMComponentAttribute_Attribute] FOREIGN KEY([AttributeCode])
REFERENCES [dbo].[Attribute] ([Code])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BOMComponentAttribute_Attribute]') AND parent_object_id = OBJECT_ID(N'[dbo].[BOMComponentAttribute]'))
ALTER TABLE [dbo].[BOMComponentAttribute] CHECK CONSTRAINT [FK_BOMComponentAttribute_Attribute]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BOMComponentAttribute_BOMComponent]') AND parent_object_id = OBJECT_ID(N'[dbo].[BOMComponentAttribute]'))
ALTER TABLE [dbo].[BOMComponentAttribute]  WITH CHECK ADD  CONSTRAINT [FK_BOMComponentAttribute_BOMComponent] FOREIGN KEY([BOMComponentId])
REFERENCES [dbo].[BOMComponent] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BOMComponentAttribute_BOMComponent]') AND parent_object_id = OBJECT_ID(N'[dbo].[BOMComponentAttribute]'))
ALTER TABLE [dbo].[BOMComponentAttribute] CHECK CONSTRAINT [FK_BOMComponentAttribute_BOMComponent]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BOMComponentAttributeValue_AttributeValue]') AND parent_object_id = OBJECT_ID(N'[dbo].[BOMComponentAttributeValue]'))
ALTER TABLE [dbo].[BOMComponentAttributeValue]  WITH CHECK ADD  CONSTRAINT [FK_BOMComponentAttributeValue_AttributeValue] FOREIGN KEY([AttributeValueCode])
REFERENCES [dbo].[AttributeValue] ([Code])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BOMComponentAttributeValue_AttributeValue]') AND parent_object_id = OBJECT_ID(N'[dbo].[BOMComponentAttributeValue]'))
ALTER TABLE [dbo].[BOMComponentAttributeValue] CHECK CONSTRAINT [FK_BOMComponentAttributeValue_AttributeValue]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BOMComponentAttributeValue_BOMComponentAttribute]') AND parent_object_id = OBJECT_ID(N'[dbo].[BOMComponentAttributeValue]'))
ALTER TABLE [dbo].[BOMComponentAttributeValue]  WITH CHECK ADD  CONSTRAINT [FK_BOMComponentAttributeValue_BOMComponentAttribute] FOREIGN KEY([BOMComponentAttributeId])
REFERENCES [dbo].[BOMComponentAttribute] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BOMComponentAttributeValue_BOMComponentAttribute]') AND parent_object_id = OBJECT_ID(N'[dbo].[BOMComponentAttributeValue]'))
ALTER TABLE [dbo].[BOMComponentAttributeValue] CHECK CONSTRAINT [FK_BOMComponentAttributeValue_BOMComponentAttribute]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BOMProduced_BOM]') AND parent_object_id = OBJECT_ID(N'[dbo].[BOMProduced]'))
ALTER TABLE [dbo].[BOMProduced]  WITH CHECK ADD  CONSTRAINT [FK_BOMProduced_BOM] FOREIGN KEY([BOMId])
REFERENCES [dbo].[BOM] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BOMProduced_BOM]') AND parent_object_id = OBJECT_ID(N'[dbo].[BOMProduced]'))
ALTER TABLE [dbo].[BOMProduced] CHECK CONSTRAINT [FK_BOMProduced_BOM]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CollectionDetail_Collection]') AND parent_object_id = OBJECT_ID(N'[dbo].[CollectionDetail]'))
ALTER TABLE [dbo].[CollectionDetail]  WITH CHECK ADD  CONSTRAINT [FK_CollectionDetail_Collection] FOREIGN KEY([CollectionId])
REFERENCES [dbo].[Collection] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CollectionDetail_Collection]') AND parent_object_id = OBJECT_ID(N'[dbo].[CollectionDetail]'))
ALTER TABLE [dbo].[CollectionDetail] CHECK CONSTRAINT [FK_CollectionDetail_Collection]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CollectionDetail_ProductionTask]') AND parent_object_id = OBJECT_ID(N'[dbo].[CollectionDetail]'))
ALTER TABLE [dbo].[CollectionDetail]  WITH CHECK ADD  CONSTRAINT [FK_CollectionDetail_ProductionTask] FOREIGN KEY([CollectionTaskId])
REFERENCES [dbo].[ProductionTask] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CollectionDetail_ProductionTask]') AND parent_object_id = OBJECT_ID(N'[dbo].[CollectionDetail]'))
ALTER TABLE [dbo].[CollectionDetail] CHECK CONSTRAINT [FK_CollectionDetail_ProductionTask]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CollectionDetail_ProductVariant]') AND parent_object_id = OBJECT_ID(N'[dbo].[CollectionDetail]'))
ALTER TABLE [dbo].[CollectionDetail]  WITH CHECK ADD  CONSTRAINT [FK_CollectionDetail_ProductVariant] FOREIGN KEY([ProductVariantCode])
REFERENCES [dbo].[ProductVariant] ([Code])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CollectionDetail_ProductVariant]') AND parent_object_id = OBJECT_ID(N'[dbo].[CollectionDetail]'))
ALTER TABLE [dbo].[CollectionDetail] CHECK CONSTRAINT [FK_CollectionDetail_ProductVariant]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Contact_PickListValue]') AND parent_object_id = OBJECT_ID(N'[dbo].[Contact]'))
ALTER TABLE [dbo].[Contact]  WITH CHECK ADD  CONSTRAINT [FK_Contact_PickListValue] FOREIGN KEY([TitleId])
REFERENCES [dbo].[PickListValue] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Contact_PickListValue]') AND parent_object_id = OBJECT_ID(N'[dbo].[Contact]'))
ALTER TABLE [dbo].[Contact] CHECK CONSTRAINT [FK_Contact_PickListValue]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EntitySecurity_Role]') AND parent_object_id = OBJECT_ID(N'[dbo].[EntitySecurity]'))
ALTER TABLE [dbo].[EntitySecurity]  WITH CHECK ADD  CONSTRAINT [FK_EntitySecurity_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EntitySecurity_Role]') AND parent_object_id = OBJECT_ID(N'[dbo].[EntitySecurity]'))
ALTER TABLE [dbo].[EntitySecurity] CHECK CONSTRAINT [FK_EntitySecurity_Role]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EntitySecurity_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[EntitySecurity]'))
ALTER TABLE [dbo].[EntitySecurity]  WITH CHECK ADD  CONSTRAINT [FK_EntitySecurity_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EntitySecurity_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[EntitySecurity]'))
ALTER TABLE [dbo].[EntitySecurity] CHECK CONSTRAINT [FK_EntitySecurity_Tenant]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ImageBinary_Image]') AND parent_object_id = OBJECT_ID(N'[dbo].[ImageBinary]'))
ALTER TABLE [dbo].[ImageBinary]  WITH CHECK ADD  CONSTRAINT [FK_ImageBinary_Image] FOREIGN KEY([ImageId])
REFERENCES [dbo].[Image] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ImageBinary_Image]') AND parent_object_id = OBJECT_ID(N'[dbo].[ImageBinary]'))
ALTER TABLE [dbo].[ImageBinary] CHECK CONSTRAINT [FK_ImageBinary_Image]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Location_Address]') AND parent_object_id = OBJECT_ID(N'[dbo].[Location]'))
ALTER TABLE [dbo].[Location]  WITH CHECK ADD  CONSTRAINT [FK_Location_Address] FOREIGN KEY([OfficialAddressId])
REFERENCES [dbo].[Address] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Location_Address]') AND parent_object_id = OBJECT_ID(N'[dbo].[Location]'))
ALTER TABLE [dbo].[Location] CHECK CONSTRAINT [FK_Location_Address]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Location_Company]') AND parent_object_id = OBJECT_ID(N'[dbo].[Location]'))
ALTER TABLE [dbo].[Location]  WITH CHECK ADD  CONSTRAINT [FK_Location_Company] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Company] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Location_Company]') AND parent_object_id = OBJECT_ID(N'[dbo].[Location]'))
ALTER TABLE [dbo].[Location] CHECK CONSTRAINT [FK_Location_Company]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Location_ContactInformation]') AND parent_object_id = OBJECT_ID(N'[dbo].[Location]'))
ALTER TABLE [dbo].[Location]  WITH CHECK ADD  CONSTRAINT [FK_Location_ContactInformation] FOREIGN KEY([ContactInformationId])
REFERENCES [dbo].[ContactInformation] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Location_ContactInformation]') AND parent_object_id = OBJECT_ID(N'[dbo].[Location]'))
ALTER TABLE [dbo].[Location] CHECK CONSTRAINT [FK_Location_ContactInformation]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Location_Timezone]') AND parent_object_id = OBJECT_ID(N'[dbo].[Location]'))
ALTER TABLE [dbo].[Location]  WITH CHECK ADD  CONSTRAINT [FK_Location_Timezone] FOREIGN KEY([TimezoneId])
REFERENCES [dbo].[PickListValue] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Location_Timezone]') AND parent_object_id = OBJECT_ID(N'[dbo].[Location]'))
ALTER TABLE [dbo].[Location] CHECK CONSTRAINT [FK_Location_Timezone]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PickList_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[PickList]'))
ALTER TABLE [dbo].[PickList]  WITH CHECK ADD  CONSTRAINT [FK_PickList_User] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[User] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PickList_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[PickList]'))
ALTER TABLE [dbo].[PickList] CHECK CONSTRAINT [FK_PickList_User]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PickListValue_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[PickListValue]'))
ALTER TABLE [dbo].[PickListValue]  WITH CHECK ADD  CONSTRAINT [FK_PickListValue_User] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[User] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PickListValue_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[PickListValue]'))
ALTER TABLE [dbo].[PickListValue] CHECK CONSTRAINT [FK_PickListValue_User]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PickListValueFavourite_PickListValue]') AND parent_object_id = OBJECT_ID(N'[dbo].[PickListValueFavourite]'))
ALTER TABLE [dbo].[PickListValueFavourite]  WITH CHECK ADD  CONSTRAINT [FK_PickListValueFavourite_PickListValue] FOREIGN KEY([PickListValueId])
REFERENCES [dbo].[PickListValue] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PickListValueFavourite_PickListValue]') AND parent_object_id = OBJECT_ID(N'[dbo].[PickListValueFavourite]'))
ALTER TABLE [dbo].[PickListValueFavourite] CHECK CONSTRAINT [FK_PickListValueFavourite_PickListValue]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PickListValueFavourite_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[PickListValueFavourite]'))
ALTER TABLE [dbo].[PickListValueFavourite]  WITH CHECK ADD  CONSTRAINT [FK_PickListValueFavourite_User] FOREIGN KEY([User])
REFERENCES [dbo].[User] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PickListValueFavourite_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[PickListValueFavourite]'))
ALTER TABLE [dbo].[PickListValueFavourite] CHECK CONSTRAINT [FK_PickListValueFavourite_User]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PickListValueForCity_PickListValue]') AND parent_object_id = OBJECT_ID(N'[dbo].[PickListValueForCity]'))
ALTER TABLE [dbo].[PickListValueForCity]  WITH CHECK ADD  CONSTRAINT [FK_PickListValueForCity_PickListValue] FOREIGN KEY([PickListValueId])
REFERENCES [dbo].[PickListValue] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PickListValueForCity_PickListValue]') AND parent_object_id = OBJECT_ID(N'[dbo].[PickListValueForCity]'))
ALTER TABLE [dbo].[PickListValueForCity] CHECK CONSTRAINT [FK_PickListValueForCity_PickListValue]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PickListValueForCountry_PickListValue]') AND parent_object_id = OBJECT_ID(N'[dbo].[PickListValueForCountry]'))
ALTER TABLE [dbo].[PickListValueForCountry]  WITH CHECK ADD  CONSTRAINT [FK_PickListValueForCountry_PickListValue] FOREIGN KEY([PickListValueId])
REFERENCES [dbo].[PickListValue] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PickListValueForCountry_PickListValue]') AND parent_object_id = OBJECT_ID(N'[dbo].[PickListValueForCountry]'))
ALTER TABLE [dbo].[PickListValueForCountry] CHECK CONSTRAINT [FK_PickListValueForCountry_PickListValue]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PickListValueForCurrency_PickListValue]') AND parent_object_id = OBJECT_ID(N'[dbo].[PickListValueForCurrency]'))
ALTER TABLE [dbo].[PickListValueForCurrency]  WITH CHECK ADD  CONSTRAINT [FK_PickListValueForCurrency_PickListValue] FOREIGN KEY([PickListValueId])
REFERENCES [dbo].[PickListValue] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PickListValueForCurrency_PickListValue]') AND parent_object_id = OBJECT_ID(N'[dbo].[PickListValueForCurrency]'))
ALTER TABLE [dbo].[PickListValueForCurrency] CHECK CONSTRAINT [FK_PickListValueForCurrency_PickListValue]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PickListValueForLanguage_PickListValue]') AND parent_object_id = OBJECT_ID(N'[dbo].[PickListValueForLanguage]'))
ALTER TABLE [dbo].[PickListValueForLanguage]  WITH CHECK ADD  CONSTRAINT [FK_PickListValueForLanguage_PickListValue] FOREIGN KEY([PickListValueId])
REFERENCES [dbo].[PickListValue] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PickListValueForLanguage_PickListValue]') AND parent_object_id = OBJECT_ID(N'[dbo].[PickListValueForLanguage]'))
ALTER TABLE [dbo].[PickListValueForLanguage] CHECK CONSTRAINT [FK_PickListValueForLanguage_PickListValue]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PickListValueForMenuGroup_PickListValue]') AND parent_object_id = OBJECT_ID(N'[dbo].[PickListValueForMenuGroup]'))
ALTER TABLE [dbo].[PickListValueForMenuGroup]  WITH CHECK ADD  CONSTRAINT [FK_PickListValueForMenuGroup_PickListValue] FOREIGN KEY([PickListValueId])
REFERENCES [dbo].[PickListValue] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PickListValueForMenuGroup_PickListValue]') AND parent_object_id = OBJECT_ID(N'[dbo].[PickListValueForMenuGroup]'))
ALTER TABLE [dbo].[PickListValueForMenuGroup] CHECK CONSTRAINT [FK_PickListValueForMenuGroup_PickListValue]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PickListValueForMunicipality_Country]') AND parent_object_id = OBJECT_ID(N'[dbo].[PickListValueForMunicipality]'))
ALTER TABLE [dbo].[PickListValueForMunicipality]  WITH CHECK ADD  CONSTRAINT [FK_PickListValueForMunicipality_Country] FOREIGN KEY([CountryId])
REFERENCES [dbo].[PickListValue] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PickListValueForMunicipality_Country]') AND parent_object_id = OBJECT_ID(N'[dbo].[PickListValueForMunicipality]'))
ALTER TABLE [dbo].[PickListValueForMunicipality] CHECK CONSTRAINT [FK_PickListValueForMunicipality_Country]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PickListValueForMunicipality_PickListValue]') AND parent_object_id = OBJECT_ID(N'[dbo].[PickListValueForMunicipality]'))
ALTER TABLE [dbo].[PickListValueForMunicipality]  WITH CHECK ADD  CONSTRAINT [FK_PickListValueForMunicipality_PickListValue] FOREIGN KEY([PickListValueId])
REFERENCES [dbo].[PickListValue] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PickListValueForMunicipality_PickListValue]') AND parent_object_id = OBJECT_ID(N'[dbo].[PickListValueForMunicipality]'))
ALTER TABLE [dbo].[PickListValueForMunicipality] CHECK CONSTRAINT [FK_PickListValueForMunicipality_PickListValue]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PickListValueForMunicipality_State]') AND parent_object_id = OBJECT_ID(N'[dbo].[PickListValueForMunicipality]'))
ALTER TABLE [dbo].[PickListValueForMunicipality]  WITH CHECK ADD  CONSTRAINT [FK_PickListValueForMunicipality_State] FOREIGN KEY([StateId])
REFERENCES [dbo].[PickListValue] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PickListValueForMunicipality_State]') AND parent_object_id = OBJECT_ID(N'[dbo].[PickListValueForMunicipality]'))
ALTER TABLE [dbo].[PickListValueForMunicipality] CHECK CONSTRAINT [FK_PickListValueForMunicipality_State]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PickListValueForSecurityFunction_PickListValue]') AND parent_object_id = OBJECT_ID(N'[dbo].[PickListValueForSecurityFunction]'))
ALTER TABLE [dbo].[PickListValueForSecurityFunction]  WITH CHECK ADD  CONSTRAINT [FK_PickListValueForSecurityFunction_PickListValue] FOREIGN KEY([PickListValueId])
REFERENCES [dbo].[PickListValue] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PickListValueForSecurityFunction_PickListValue]') AND parent_object_id = OBJECT_ID(N'[dbo].[PickListValueForSecurityFunction]'))
ALTER TABLE [dbo].[PickListValueForSecurityFunction] CHECK CONSTRAINT [FK_PickListValueForSecurityFunction_PickListValue]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PickListValueForState_Country]') AND parent_object_id = OBJECT_ID(N'[dbo].[PickListValueForState]'))
ALTER TABLE [dbo].[PickListValueForState]  WITH CHECK ADD  CONSTRAINT [FK_PickListValueForState_Country] FOREIGN KEY([CountryId])
REFERENCES [dbo].[PickListValue] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PickListValueForState_Country]') AND parent_object_id = OBJECT_ID(N'[dbo].[PickListValueForState]'))
ALTER TABLE [dbo].[PickListValueForState] CHECK CONSTRAINT [FK_PickListValueForState_Country]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PickListValueForState_PickListValue]') AND parent_object_id = OBJECT_ID(N'[dbo].[PickListValueForState]'))
ALTER TABLE [dbo].[PickListValueForState]  WITH CHECK ADD  CONSTRAINT [FK_PickListValueForState_PickListValue] FOREIGN KEY([PickListValueId])
REFERENCES [dbo].[PickListValue] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PickListValueForState_PickListValue]') AND parent_object_id = OBJECT_ID(N'[dbo].[PickListValueForState]'))
ALTER TABLE [dbo].[PickListValueForState] CHECK CONSTRAINT [FK_PickListValueForState_PickListValue]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PickListValueForTimeZone_PickListValue]') AND parent_object_id = OBJECT_ID(N'[dbo].[PickListValueForTimeZone]'))
ALTER TABLE [dbo].[PickListValueForTimeZone]  WITH CHECK ADD  CONSTRAINT [FK_PickListValueForTimeZone_PickListValue] FOREIGN KEY([PickListValueId])
REFERENCES [dbo].[PickListValue] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PickListValueForTimeZone_PickListValue]') AND parent_object_id = OBJECT_ID(N'[dbo].[PickListValueForTimeZone]'))
ALTER TABLE [dbo].[PickListValueForTimeZone] CHECK CONSTRAINT [FK_PickListValueForTimeZone_PickListValue]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Product_Counter_BatchNo]') AND parent_object_id = OBJECT_ID(N'[dbo].[Product]'))
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Counter_BatchNo] FOREIGN KEY([BatchNoCounterId])
REFERENCES [dbo].[Counter] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Product_Counter_BatchNo]') AND parent_object_id = OBJECT_ID(N'[dbo].[Product]'))
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Counter_BatchNo]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Product_Counter_SerialNo]') AND parent_object_id = OBJECT_ID(N'[dbo].[Product]'))
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Counter_SerialNo] FOREIGN KEY([SerialNoCounterId])
REFERENCES [dbo].[Counter] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Product_Counter_SerialNo]') AND parent_object_id = OBJECT_ID(N'[dbo].[Product]'))
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Counter_SerialNo]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Product_Manufacturer]') AND parent_object_id = OBJECT_ID(N'[dbo].[Product]'))
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Manufacturer] FOREIGN KEY([ManufacturerCode])
REFERENCES [dbo].[Manufacturer] ([Code])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Product_Manufacturer]') AND parent_object_id = OBJECT_ID(N'[dbo].[Product]'))
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Manufacturer]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductAttribute_Attribute]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductAttribute]'))
ALTER TABLE [dbo].[ProductAttribute]  WITH CHECK ADD  CONSTRAINT [FK_ProductAttribute_Attribute] FOREIGN KEY([AttributeCode])
REFERENCES [dbo].[Attribute] ([Code])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductAttribute_Attribute]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductAttribute]'))
ALTER TABLE [dbo].[ProductAttribute] CHECK CONSTRAINT [FK_ProductAttribute_Attribute]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductAttributeValue_AttributeValue]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductAttributeValue]'))
ALTER TABLE [dbo].[ProductAttributeValue]  WITH CHECK ADD  CONSTRAINT [FK_ProductAttributeValue_AttributeValue] FOREIGN KEY([AttributeValueCode])
REFERENCES [dbo].[AttributeValue] ([Code])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductAttributeValue_AttributeValue]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductAttributeValue]'))
ALTER TABLE [dbo].[ProductAttributeValue] CHECK CONSTRAINT [FK_ProductAttributeValue_AttributeValue]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductAttributeValue_ProductAttribute]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductAttributeValue]'))
ALTER TABLE [dbo].[ProductAttributeValue]  WITH CHECK ADD  CONSTRAINT [FK_ProductAttributeValue_ProductAttribute] FOREIGN KEY([ProductAttributeId])
REFERENCES [dbo].[ProductAttribute] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductAttributeValue_ProductAttribute]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductAttributeValue]'))
ALTER TABLE [dbo].[ProductAttributeValue] CHECK CONSTRAINT [FK_ProductAttributeValue_ProductAttribute]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionOrder_ProductBOM]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionOrder]'))
ALTER TABLE [dbo].[ProductionOrder]  WITH CHECK ADD  CONSTRAINT [FK_ProductionOrder_ProductBOM] FOREIGN KEY([ProductBOMId])
REFERENCES [dbo].[BOM] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionOrder_ProductBOM]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionOrder]'))
ALTER TABLE [dbo].[ProductionOrder] CHECK CONSTRAINT [FK_ProductionOrder_ProductBOM]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionOrder_ProductRoute]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionOrder]'))
ALTER TABLE [dbo].[ProductionOrder]  WITH CHECK ADD  CONSTRAINT [FK_ProductionOrder_ProductRoute] FOREIGN KEY([RouteId])
REFERENCES [dbo].[ProductRoute] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionOrder_ProductRoute]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionOrder]'))
ALTER TABLE [dbo].[ProductionOrder] CHECK CONSTRAINT [FK_ProductionOrder_ProductRoute]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionOrder_ProductVariant]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionOrder]'))
ALTER TABLE [dbo].[ProductionOrder]  WITH CHECK ADD  CONSTRAINT [FK_ProductionOrder_ProductVariant] FOREIGN KEY([ProductVariantCode])
REFERENCES [dbo].[ProductVariant] ([Code])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionOrder_ProductVariant]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionOrder]'))
ALTER TABLE [dbo].[ProductionOrder] CHECK CONSTRAINT [FK_ProductionOrder_ProductVariant]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionOrderAlternativeComponent_ProductionOrderComponent]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionOrderAlternativeComponent]'))
ALTER TABLE [dbo].[ProductionOrderAlternativeComponent]  WITH CHECK ADD  CONSTRAINT [FK_ProductionOrderAlternativeComponent_ProductionOrderComponent] FOREIGN KEY([ProductionOrderComponentId])
REFERENCES [dbo].[ProductionOrderComponent] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionOrderAlternativeComponent_ProductionOrderComponent]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionOrderAlternativeComponent]'))
ALTER TABLE [dbo].[ProductionOrderAlternativeComponent] CHECK CONSTRAINT [FK_ProductionOrderAlternativeComponent_ProductionOrderComponent]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionOrderAlternativeComponent_ProductVariant]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionOrderAlternativeComponent]'))
ALTER TABLE [dbo].[ProductionOrderAlternativeComponent]  WITH CHECK ADD  CONSTRAINT [FK_ProductionOrderAlternativeComponent_ProductVariant] FOREIGN KEY([ProductVariantCode])
REFERENCES [dbo].[ProductVariant] ([Code])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionOrderAlternativeComponent_ProductVariant]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionOrderAlternativeComponent]'))
ALTER TABLE [dbo].[ProductionOrderAlternativeComponent] CHECK CONSTRAINT [FK_ProductionOrderAlternativeComponent_ProductVariant]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionOrderAlternativeComponent_Vendor]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionOrderAlternativeComponent]'))
ALTER TABLE [dbo].[ProductionOrderAlternativeComponent]  WITH CHECK ADD  CONSTRAINT [FK_ProductionOrderAlternativeComponent_Vendor] FOREIGN KEY([VendorCode])
REFERENCES [dbo].[Vendor] ([Code])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionOrderAlternativeComponent_Vendor]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionOrderAlternativeComponent]'))
ALTER TABLE [dbo].[ProductionOrderAlternativeComponent] CHECK CONSTRAINT [FK_ProductionOrderAlternativeComponent_Vendor]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionOrderComponent_ProductionOrder]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionOrderComponent]'))
ALTER TABLE [dbo].[ProductionOrderComponent]  WITH CHECK ADD  CONSTRAINT [FK_ProductionOrderComponent_ProductionOrder] FOREIGN KEY([ProductionOrderNo])
REFERENCES [dbo].[ProductionOrder] ([Code])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionOrderComponent_ProductionOrder]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionOrderComponent]'))
ALTER TABLE [dbo].[ProductionOrderComponent] CHECK CONSTRAINT [FK_ProductionOrderComponent_ProductionOrder]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionOrderComponent_ProductionOrderOperation]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionOrderComponent]'))
ALTER TABLE [dbo].[ProductionOrderComponent]  WITH CHECK ADD  CONSTRAINT [FK_ProductionOrderComponent_ProductionOrderOperation] FOREIGN KEY([OperationNo])
REFERENCES [dbo].[ProductionOrderOperation] ([OperationNo])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionOrderComponent_ProductionOrderOperation]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionOrderComponent]'))
ALTER TABLE [dbo].[ProductionOrderComponent] CHECK CONSTRAINT [FK_ProductionOrderComponent_ProductionOrderOperation]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionOrderComponent_ProductVariant]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionOrderComponent]'))
ALTER TABLE [dbo].[ProductionOrderComponent]  WITH CHECK ADD  CONSTRAINT [FK_ProductionOrderComponent_ProductVariant] FOREIGN KEY([ProductVariantCode])
REFERENCES [dbo].[ProductVariant] ([Code])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionOrderComponent_ProductVariant]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionOrderComponent]'))
ALTER TABLE [dbo].[ProductionOrderComponent] CHECK CONSTRAINT [FK_ProductionOrderComponent_ProductVariant]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionOrderOperation_ProductionOrder]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionOrderOperation]'))
ALTER TABLE [dbo].[ProductionOrderOperation]  WITH CHECK ADD  CONSTRAINT [FK_ProductionOrderOperation_ProductionOrder] FOREIGN KEY([ProductionOrderNo])
REFERENCES [dbo].[ProductionOrder] ([Code])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionOrderOperation_ProductionOrder]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionOrderOperation]'))
ALTER TABLE [dbo].[ProductionOrderOperation] CHECK CONSTRAINT [FK_ProductionOrderOperation_ProductionOrder]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionOrderOperationCompetence_ProductionOrderOperation]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionOrderOperationCompetence]'))
ALTER TABLE [dbo].[ProductionOrderOperationCompetence]  WITH CHECK ADD  CONSTRAINT [FK_ProductionOrderOperationCompetence_ProductionOrderOperation] FOREIGN KEY([OperationNo])
REFERENCES [dbo].[ProductionOrderOperation] ([OperationNo])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionOrderOperationCompetence_ProductionOrderOperation]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionOrderOperationCompetence]'))
ALTER TABLE [dbo].[ProductionOrderOperationCompetence] CHECK CONSTRAINT [FK_ProductionOrderOperationCompetence_ProductionOrderOperation]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionOrderOperationConfiguration_ProductionOrderOperation]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionOrderOperationConfiguration]'))
ALTER TABLE [dbo].[ProductionOrderOperationConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_ProductionOrderOperationConfiguration_ProductionOrderOperation] FOREIGN KEY([OperationNo])
REFERENCES [dbo].[ProductionOrderOperation] ([OperationNo])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionOrderOperationConfiguration_ProductionOrderOperation]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionOrderOperationConfiguration]'))
ALTER TABLE [dbo].[ProductionOrderOperationConfiguration] CHECK CONSTRAINT [FK_ProductionOrderOperationConfiguration_ProductionOrderOperation]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionOrderOperationTool_ProductionOrderOperation]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionOrderOperationTool]'))
ALTER TABLE [dbo].[ProductionOrderOperationTool]  WITH CHECK ADD  CONSTRAINT [FK_ProductionOrderOperationTool_ProductionOrderOperation] FOREIGN KEY([OperationNo])
REFERENCES [dbo].[ProductionOrderOperation] ([OperationNo])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionOrderOperationTool_ProductionOrderOperation]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionOrderOperationTool]'))
ALTER TABLE [dbo].[ProductionOrderOperationTool] CHECK CONSTRAINT [FK_ProductionOrderOperationTool_ProductionOrderOperation]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionOrderOperationTool_Tool]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionOrderOperationTool]'))
ALTER TABLE [dbo].[ProductionOrderOperationTool]  WITH CHECK ADD  CONSTRAINT [FK_ProductionOrderOperationTool_Tool] FOREIGN KEY([ToolCode])
REFERENCES [dbo].[Tool] ([Code])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionOrderOperationTool_Tool]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionOrderOperationTool]'))
ALTER TABLE [dbo].[ProductionOrderOperationTool] CHECK CONSTRAINT [FK_ProductionOrderOperationTool_Tool]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionOrderProduced_ProductionOrder]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionOrderProduced]'))
ALTER TABLE [dbo].[ProductionOrderProduced]  WITH CHECK ADD  CONSTRAINT [FK_ProductionOrderProduced_ProductionOrder] FOREIGN KEY([ProductionOrderId])
REFERENCES [dbo].[ProductionOrder] ([Code])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionOrderProduced_ProductionOrder]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionOrderProduced]'))
ALTER TABLE [dbo].[ProductionOrderProduced] CHECK CONSTRAINT [FK_ProductionOrderProduced_ProductionOrder]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionSet_ProductionTask]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionSet]'))
ALTER TABLE [dbo].[ProductionSet]  WITH CHECK ADD  CONSTRAINT [FK_ProductionSet_ProductionTask] FOREIGN KEY([ProductionTaskId])
REFERENCES [dbo].[ProductionTask] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionSet_ProductionTask]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionSet]'))
ALTER TABLE [dbo].[ProductionSet] CHECK CONSTRAINT [FK_ProductionSet_ProductionTask]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionSetConsumed_ProductionSet]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionSetConsumed]'))
ALTER TABLE [dbo].[ProductionSetConsumed]  WITH CHECK ADD  CONSTRAINT [FK_ProductionSetConsumed_ProductionSet] FOREIGN KEY([ProductionSetId])
REFERENCES [dbo].[ProductionSet] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionSetConsumed_ProductionSet]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionSetConsumed]'))
ALTER TABLE [dbo].[ProductionSetConsumed] CHECK CONSTRAINT [FK_ProductionSetConsumed_ProductionSet]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionSetConsumed_ProductionSetProduced]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionSetConsumed]'))
ALTER TABLE [dbo].[ProductionSetConsumed]  WITH CHECK ADD  CONSTRAINT [FK_ProductionSetConsumed_ProductionSetProduced] FOREIGN KEY([ProductionSetProducedId])
REFERENCES [dbo].[ProductionSetProduced] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionSetConsumed_ProductionSetProduced]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionSetConsumed]'))
ALTER TABLE [dbo].[ProductionSetConsumed] CHECK CONSTRAINT [FK_ProductionSetConsumed_ProductionSetProduced]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionSetConsumed_ProductionTask]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionSetConsumed]'))
ALTER TABLE [dbo].[ProductionSetConsumed]  WITH CHECK ADD  CONSTRAINT [FK_ProductionSetConsumed_ProductionTask] FOREIGN KEY([ProductionTaskId])
REFERENCES [dbo].[ProductionTask] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionSetConsumed_ProductionTask]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionSetConsumed]'))
ALTER TABLE [dbo].[ProductionSetConsumed] CHECK CONSTRAINT [FK_ProductionSetConsumed_ProductionTask]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionSetProduced_ProductionSet]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionSetProduced]'))
ALTER TABLE [dbo].[ProductionSetProduced]  WITH CHECK ADD  CONSTRAINT [FK_ProductionSetProduced_ProductionSet] FOREIGN KEY([ProductionSetId])
REFERENCES [dbo].[ProductionSet] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionSetProduced_ProductionSet]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionSetProduced]'))
ALTER TABLE [dbo].[ProductionSetProduced] CHECK CONSTRAINT [FK_ProductionSetProduced_ProductionSet]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionSetProduced_ProductionTask]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionSetProduced]'))
ALTER TABLE [dbo].[ProductionSetProduced]  WITH CHECK ADD  CONSTRAINT [FK_ProductionSetProduced_ProductionTask] FOREIGN KEY([ProductionTaskId])
REFERENCES [dbo].[ProductionTask] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionSetProduced_ProductionTask]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionSetProduced]'))
ALTER TABLE [dbo].[ProductionSetProduced] CHECK CONSTRAINT [FK_ProductionSetProduced_ProductionTask]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionTask_Collection]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionTask]'))
ALTER TABLE [dbo].[ProductionTask]  WITH CHECK ADD  CONSTRAINT [FK_ProductionTask_Collection] FOREIGN KEY([CollectionId])
REFERENCES [dbo].[Collection] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionTask_Collection]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionTask]'))
ALTER TABLE [dbo].[ProductionTask] CHECK CONSTRAINT [FK_ProductionTask_Collection]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionTask_ProductionOrderOperation]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionTask]'))
ALTER TABLE [dbo].[ProductionTask]  WITH CHECK ADD  CONSTRAINT [FK_ProductionTask_ProductionOrderOperation] FOREIGN KEY([OperationId])
REFERENCES [dbo].[ProductionOrderOperation] ([OperationNo])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductionTask_ProductionOrderOperation]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductionTask]'))
ALTER TABLE [dbo].[ProductionTask] CHECK CONSTRAINT [FK_ProductionTask_ProductionOrderOperation]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductOperation_ProductRoute]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductOperation]'))
ALTER TABLE [dbo].[ProductOperation]  WITH CHECK ADD  CONSTRAINT [FK_ProductOperation_ProductRoute] FOREIGN KEY([ProductRouteId])
REFERENCES [dbo].[ProductRoute] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductOperation_ProductRoute]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductOperation]'))
ALTER TABLE [dbo].[ProductOperation] CHECK CONSTRAINT [FK_ProductOperation_ProductRoute]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductOperationWorkcenter_ProductOperation]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductOperationWorkcenter]'))
ALTER TABLE [dbo].[ProductOperationWorkcenter]  WITH CHECK ADD  CONSTRAINT [FK_ProductOperationWorkcenter_ProductOperation] FOREIGN KEY([OperationNo])
REFERENCES [dbo].[ProductOperation] ([OperationNo])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductOperationWorkcenter_ProductOperation]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductOperationWorkcenter]'))
ALTER TABLE [dbo].[ProductOperationWorkcenter] CHECK CONSTRAINT [FK_ProductOperationWorkcenter_ProductOperation]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductOperationWorkcenter_Workcenter]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductOperationWorkcenter]'))
ALTER TABLE [dbo].[ProductOperationWorkcenter]  WITH CHECK ADD  CONSTRAINT [FK_ProductOperationWorkcenter_Workcenter] FOREIGN KEY([WorkCenterCode])
REFERENCES [dbo].[Workcenter] ([Code])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductOperationWorkcenter_Workcenter]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductOperationWorkcenter]'))
ALTER TABLE [dbo].[ProductOperationWorkcenter] CHECK CONSTRAINT [FK_ProductOperationWorkcenter_Workcenter]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductOperationWorkcenterCompetence_ProductOperationWorkcenter]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductOperationWorkcenterCompetence]'))
ALTER TABLE [dbo].[ProductOperationWorkcenterCompetence]  WITH CHECK ADD  CONSTRAINT [FK_ProductOperationWorkcenterCompetence_ProductOperationWorkcenter] FOREIGN KEY([OperationWorkcenterId])
REFERENCES [dbo].[ProductOperationWorkcenter] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductOperationWorkcenterCompetence_ProductOperationWorkcenter]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductOperationWorkcenterCompetence]'))
ALTER TABLE [dbo].[ProductOperationWorkcenterCompetence] CHECK CONSTRAINT [FK_ProductOperationWorkcenterCompetence_ProductOperationWorkcenter]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductOperationWorkcenterConfiguration_ProductOperationWorkcenter]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductOperationWorkcenterConfiguration]'))
ALTER TABLE [dbo].[ProductOperationWorkcenterConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_ProductOperationWorkcenterConfiguration_ProductOperationWorkcenter] FOREIGN KEY([OperationWorkcenterId])
REFERENCES [dbo].[ProductOperationWorkcenter] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductOperationWorkcenterConfiguration_ProductOperationWorkcenter]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductOperationWorkcenterConfiguration]'))
ALTER TABLE [dbo].[ProductOperationWorkcenterConfiguration] CHECK CONSTRAINT [FK_ProductOperationWorkcenterConfiguration_ProductOperationWorkcenter]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductOperationWorkcenterConfiguration_Warehouse]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductOperationWorkcenterConfiguration]'))
ALTER TABLE [dbo].[ProductOperationWorkcenterConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_ProductOperationWorkcenterConfiguration_Warehouse] FOREIGN KEY([ConsumableWarehouseCode])
REFERENCES [dbo].[Warehouse] ([Code])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductOperationWorkcenterConfiguration_Warehouse]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductOperationWorkcenterConfiguration]'))
ALTER TABLE [dbo].[ProductOperationWorkcenterConfiguration] CHECK CONSTRAINT [FK_ProductOperationWorkcenterConfiguration_Warehouse]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductOperationWorkcenterConfiguration_WarehouseProduced]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductOperationWorkcenterConfiguration]'))
ALTER TABLE [dbo].[ProductOperationWorkcenterConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_ProductOperationWorkcenterConfiguration_WarehouseProduced] FOREIGN KEY([ProducedWarehouseCode])
REFERENCES [dbo].[Warehouse] ([Code])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductOperationWorkcenterConfiguration_WarehouseProduced]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductOperationWorkcenterConfiguration]'))
ALTER TABLE [dbo].[ProductOperationWorkcenterConfiguration] CHECK CONSTRAINT [FK_ProductOperationWorkcenterConfiguration_WarehouseProduced]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductRoute_BOM]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductRoute]'))
ALTER TABLE [dbo].[ProductRoute]  WITH CHECK ADD  CONSTRAINT [FK_ProductRoute_BOM] FOREIGN KEY([BoMId])
REFERENCES [dbo].[BOM] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductRoute_BOM]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductRoute]'))
ALTER TABLE [dbo].[ProductRoute] CHECK CONSTRAINT [FK_ProductRoute_BOM]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductVariantAttribute_ProductVariant]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductVariantAttribute]'))
ALTER TABLE [dbo].[ProductVariantAttribute]  WITH CHECK ADD  CONSTRAINT [FK_ProductVariantAttribute_ProductVariant] FOREIGN KEY([ProductVariantCode])
REFERENCES [dbo].[ProductVariant] ([Code])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductVariantAttribute_ProductVariant]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductVariantAttribute]'))
ALTER TABLE [dbo].[ProductVariantAttribute] CHECK CONSTRAINT [FK_ProductVariantAttribute_ProductVariant]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductVariantAttributeValue_AttributeValue]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductVariantAttributeValue]'))
ALTER TABLE [dbo].[ProductVariantAttributeValue]  WITH CHECK ADD  CONSTRAINT [FK_ProductVariantAttributeValue_AttributeValue] FOREIGN KEY([AttributeValueCode])
REFERENCES [dbo].[AttributeValue] ([Code])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductVariantAttributeValue_AttributeValue]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductVariantAttributeValue]'))
ALTER TABLE [dbo].[ProductVariantAttributeValue] CHECK CONSTRAINT [FK_ProductVariantAttributeValue_AttributeValue]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductVariantAttributeValue_ProductVariantAttribute]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductVariantAttributeValue]'))
ALTER TABLE [dbo].[ProductVariantAttributeValue]  WITH CHECK ADD  CONSTRAINT [FK_ProductVariantAttributeValue_ProductVariantAttribute] FOREIGN KEY([VariantAttributeId])
REFERENCES [dbo].[ProductVariantAttribute] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductVariantAttributeValue_ProductVariantAttribute]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductVariantAttributeValue]'))
ALTER TABLE [dbo].[ProductVariantAttributeValue] CHECK CONSTRAINT [FK_ProductVariantAttributeValue_ProductVariantAttribute]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductVariantRuleAttribute_ProductVariantRule]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductVariantRuleAttribute]'))
ALTER TABLE [dbo].[ProductVariantRuleAttribute]  WITH CHECK ADD  CONSTRAINT [FK_ProductVariantRuleAttribute_ProductVariantRule] FOREIGN KEY([ProductVariantRuleId])
REFERENCES [dbo].[ProductVariantRule] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductVariantRuleAttribute_ProductVariantRule]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductVariantRuleAttribute]'))
ALTER TABLE [dbo].[ProductVariantRuleAttribute] CHECK CONSTRAINT [FK_ProductVariantRuleAttribute_ProductVariantRule]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductVariantRuleAttribute_ProductVariantRuleAttribute]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductVariantRuleAttribute]'))
ALTER TABLE [dbo].[ProductVariantRuleAttribute]  WITH CHECK ADD  CONSTRAINT [FK_ProductVariantRuleAttribute_ProductVariantRuleAttribute] FOREIGN KEY([AttributeCode])
REFERENCES [dbo].[Attribute] ([Code])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductVariantRuleAttribute_ProductVariantRuleAttribute]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductVariantRuleAttribute]'))
ALTER TABLE [dbo].[ProductVariantRuleAttribute] CHECK CONSTRAINT [FK_ProductVariantRuleAttribute_ProductVariantRuleAttribute]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductVariantRuleAttributeValue_AttributeValue]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductVariantRuleAttributeValue]'))
ALTER TABLE [dbo].[ProductVariantRuleAttributeValue]  WITH CHECK ADD  CONSTRAINT [FK_ProductVariantRuleAttributeValue_AttributeValue] FOREIGN KEY([AttributeValueCode])
REFERENCES [dbo].[AttributeValue] ([Code])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductVariantRuleAttributeValue_AttributeValue]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductVariantRuleAttributeValue]'))
ALTER TABLE [dbo].[ProductVariantRuleAttributeValue] CHECK CONSTRAINT [FK_ProductVariantRuleAttributeValue_AttributeValue]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductVariantRuleAttributeValue_ProductVariantRuleAttribute]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductVariantRuleAttributeValue]'))
ALTER TABLE [dbo].[ProductVariantRuleAttributeValue]  WITH CHECK ADD  CONSTRAINT [FK_ProductVariantRuleAttributeValue_ProductVariantRuleAttribute] FOREIGN KEY([RuleAttributeId])
REFERENCES [dbo].[ProductVariantRuleAttribute] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductVariantRuleAttributeValue_ProductVariantRuleAttribute]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductVariantRuleAttributeValue]'))
ALTER TABLE [dbo].[ProductVariantRuleAttributeValue] CHECK CONSTRAINT [FK_ProductVariantRuleAttributeValue_ProductVariantRuleAttribute]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductVendor_Vendor]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductVendor]'))
ALTER TABLE [dbo].[ProductVendor]  WITH CHECK ADD  CONSTRAINT [FK_ProductVendor_Vendor] FOREIGN KEY([VendorCode])
REFERENCES [dbo].[Vendor] ([Code])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProductVendor_Vendor]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProductVendor]'))
ALTER TABLE [dbo].[ProductVendor] CHECK CONSTRAINT [FK_ProductVendor_Vendor]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Role_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[Role]'))
ALTER TABLE [dbo].[Role]  WITH CHECK ADD  CONSTRAINT [FK_Role_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Role_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[Role]'))
ALTER TABLE [dbo].[Role] CHECK CONSTRAINT [FK_Role_Tenant]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Room_Location]') AND parent_object_id = OBJECT_ID(N'[dbo].[Room]'))
ALTER TABLE [dbo].[Room]  WITH CHECK ADD  CONSTRAINT [FK_Room_Location] FOREIGN KEY([LocationId])
REFERENCES [dbo].[Location] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Room_Location]') AND parent_object_id = OBJECT_ID(N'[dbo].[Room]'))
ALTER TABLE [dbo].[Room] CHECK CONSTRAINT [FK_Room_Location]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Room_RoomType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Room]'))
ALTER TABLE [dbo].[Room]  WITH CHECK ADD  CONSTRAINT [FK_Room_RoomType] FOREIGN KEY([RoomTypeId])
REFERENCES [dbo].[PickListValue] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Room_RoomType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Room]'))
ALTER TABLE [dbo].[Room] CHECK CONSTRAINT [FK_Room_RoomType]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RouteTemplateOperation_RouteTemplate]') AND parent_object_id = OBJECT_ID(N'[dbo].[RouteTemplateOperation]'))
ALTER TABLE [dbo].[RouteTemplateOperation]  WITH CHECK ADD  CONSTRAINT [FK_RouteTemplateOperation_RouteTemplate] FOREIGN KEY([RouteTemplateCode])
REFERENCES [dbo].[RouteTemplate] ([Code])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RouteTemplateOperation_RouteTemplate]') AND parent_object_id = OBJECT_ID(N'[dbo].[RouteTemplateOperation]'))
ALTER TABLE [dbo].[RouteTemplateOperation] CHECK CONSTRAINT [FK_RouteTemplateOperation_RouteTemplate]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RouteTemplateOperationWorkcenter_RouteTemplateOperation]') AND parent_object_id = OBJECT_ID(N'[dbo].[RouteTemplateOperationWorkcenter]'))
ALTER TABLE [dbo].[RouteTemplateOperationWorkcenter]  WITH CHECK ADD  CONSTRAINT [FK_RouteTemplateOperationWorkcenter_RouteTemplateOperation] FOREIGN KEY([OperationNo])
REFERENCES [dbo].[RouteTemplateOperation] ([OperationNo])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RouteTemplateOperationWorkcenter_RouteTemplateOperation]') AND parent_object_id = OBJECT_ID(N'[dbo].[RouteTemplateOperationWorkcenter]'))
ALTER TABLE [dbo].[RouteTemplateOperationWorkcenter] CHECK CONSTRAINT [FK_RouteTemplateOperationWorkcenter_RouteTemplateOperation]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RouteTemplateOperationWorkcenter_Workcenter]') AND parent_object_id = OBJECT_ID(N'[dbo].[RouteTemplateOperationWorkcenter]'))
ALTER TABLE [dbo].[RouteTemplateOperationWorkcenter]  WITH CHECK ADD  CONSTRAINT [FK_RouteTemplateOperationWorkcenter_Workcenter] FOREIGN KEY([WorkcenterCode])
REFERENCES [dbo].[Workcenter] ([Code])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RouteTemplateOperationWorkcenter_Workcenter]') AND parent_object_id = OBJECT_ID(N'[dbo].[RouteTemplateOperationWorkcenter]'))
ALTER TABLE [dbo].[RouteTemplateOperationWorkcenter] CHECK CONSTRAINT [FK_RouteTemplateOperationWorkcenter_Workcenter]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RouteTemplateOperationWorkcenterCompetence_RouteTemplateOperationWorkcenter]') AND parent_object_id = OBJECT_ID(N'[dbo].[RouteTemplateOperationWorkcenterCompetence]'))
ALTER TABLE [dbo].[RouteTemplateOperationWorkcenterCompetence]  WITH CHECK ADD  CONSTRAINT [FK_RouteTemplateOperationWorkcenterCompetence_RouteTemplateOperationWorkcenter] FOREIGN KEY([OperationWorkcenterId])
REFERENCES [dbo].[RouteTemplateOperationWorkcenter] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RouteTemplateOperationWorkcenterCompetence_RouteTemplateOperationWorkcenter]') AND parent_object_id = OBJECT_ID(N'[dbo].[RouteTemplateOperationWorkcenterCompetence]'))
ALTER TABLE [dbo].[RouteTemplateOperationWorkcenterCompetence] CHECK CONSTRAINT [FK_RouteTemplateOperationWorkcenterCompetence_RouteTemplateOperationWorkcenter]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RouteTemplateOperationWorkcenterConfiguration_RouteTemplateOperationWorkcenter]') AND parent_object_id = OBJECT_ID(N'[dbo].[RouteTemplateOperationWorkcenterConfiguration]'))
ALTER TABLE [dbo].[RouteTemplateOperationWorkcenterConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_RouteTemplateOperationWorkcenterConfiguration_RouteTemplateOperationWorkcenter] FOREIGN KEY([OperationWorkcenterId])
REFERENCES [dbo].[RouteTemplateOperationWorkcenter] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RouteTemplateOperationWorkcenterConfiguration_RouteTemplateOperationWorkcenter]') AND parent_object_id = OBJECT_ID(N'[dbo].[RouteTemplateOperationWorkcenterConfiguration]'))
ALTER TABLE [dbo].[RouteTemplateOperationWorkcenterConfiguration] CHECK CONSTRAINT [FK_RouteTemplateOperationWorkcenterConfiguration_RouteTemplateOperationWorkcenter]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RouteTemplateOperationWorkcenterConfiguration_Warehouse]') AND parent_object_id = OBJECT_ID(N'[dbo].[RouteTemplateOperationWorkcenterConfiguration]'))
ALTER TABLE [dbo].[RouteTemplateOperationWorkcenterConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_RouteTemplateOperationWorkcenterConfiguration_Warehouse] FOREIGN KEY([ConsumableWarehouseCode])
REFERENCES [dbo].[Warehouse] ([Code])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RouteTemplateOperationWorkcenterConfiguration_Warehouse]') AND parent_object_id = OBJECT_ID(N'[dbo].[RouteTemplateOperationWorkcenterConfiguration]'))
ALTER TABLE [dbo].[RouteTemplateOperationWorkcenterConfiguration] CHECK CONSTRAINT [FK_RouteTemplateOperationWorkcenterConfiguration_Warehouse]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RouteTemplateOperationWorkcenterConfiguration_Warehouse1]') AND parent_object_id = OBJECT_ID(N'[dbo].[RouteTemplateOperationWorkcenterConfiguration]'))
ALTER TABLE [dbo].[RouteTemplateOperationWorkcenterConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_RouteTemplateOperationWorkcenterConfiguration_Warehouse1] FOREIGN KEY([ProducedWarehouseCode])
REFERENCES [dbo].[Warehouse] ([Code])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RouteTemplateOperationWorkcenterConfiguration_Warehouse1]') AND parent_object_id = OBJECT_ID(N'[dbo].[RouteTemplateOperationWorkcenterConfiguration]'))
ALTER TABLE [dbo].[RouteTemplateOperationWorkcenterConfiguration] CHECK CONSTRAINT [FK_RouteTemplateOperationWorkcenterConfiguration_Warehouse1]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RouteTemplateOperationWorkcenterTool_RouteTemplateOperationWorkcenter]') AND parent_object_id = OBJECT_ID(N'[dbo].[RouteTemplateOperationWorkcenterTool]'))
ALTER TABLE [dbo].[RouteTemplateOperationWorkcenterTool]  WITH CHECK ADD  CONSTRAINT [FK_RouteTemplateOperationWorkcenterTool_RouteTemplateOperationWorkcenter] FOREIGN KEY([OperationWorkcenterId])
REFERENCES [dbo].[RouteTemplateOperationWorkcenter] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RouteTemplateOperationWorkcenterTool_RouteTemplateOperationWorkcenter]') AND parent_object_id = OBJECT_ID(N'[dbo].[RouteTemplateOperationWorkcenterTool]'))
ALTER TABLE [dbo].[RouteTemplateOperationWorkcenterTool] CHECK CONSTRAINT [FK_RouteTemplateOperationWorkcenterTool_RouteTemplateOperationWorkcenter]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RouteTemplateOperationWorkcenterTool_Tool]') AND parent_object_id = OBJECT_ID(N'[dbo].[RouteTemplateOperationWorkcenterTool]'))
ALTER TABLE [dbo].[RouteTemplateOperationWorkcenterTool]  WITH CHECK ADD  CONSTRAINT [FK_RouteTemplateOperationWorkcenterTool_Tool] FOREIGN KEY([ToolCode])
REFERENCES [dbo].[Tool] ([Code])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RouteTemplateOperationWorkcenterTool_Tool]') AND parent_object_id = OBJECT_ID(N'[dbo].[RouteTemplateOperationWorkcenterTool]'))
ALTER TABLE [dbo].[RouteTemplateOperationWorkcenterTool] CHECK CONSTRAINT [FK_RouteTemplateOperationWorkcenterTool_Tool]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Scheduler_BatchType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Scheduler]'))
ALTER TABLE [dbo].[Scheduler]  WITH CHECK ADD  CONSTRAINT [FK_Scheduler_BatchType] FOREIGN KEY([BatchTypeId])
REFERENCES [dbo].[BatchTypes] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Scheduler_BatchType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Scheduler]'))
ALTER TABLE [dbo].[Scheduler] CHECK CONSTRAINT [FK_Scheduler_BatchType]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Scheduler_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[Scheduler]'))
ALTER TABLE [dbo].[Scheduler]  WITH CHECK ADD  CONSTRAINT [FK_Scheduler_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Scheduler_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[Scheduler]'))
ALTER TABLE [dbo].[Scheduler] CHECK CONSTRAINT [FK_Scheduler_Tenant]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SchedulerDaily_Scheduler]') AND parent_object_id = OBJECT_ID(N'[dbo].[SchedulerDaily]'))
ALTER TABLE [dbo].[SchedulerDaily]  WITH CHECK ADD  CONSTRAINT [FK_SchedulerDaily_Scheduler] FOREIGN KEY([SchedulerId])
REFERENCES [dbo].[Scheduler] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SchedulerDaily_Scheduler]') AND parent_object_id = OBJECT_ID(N'[dbo].[SchedulerDaily]'))
ALTER TABLE [dbo].[SchedulerDaily] CHECK CONSTRAINT [FK_SchedulerDaily_Scheduler]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SchedulerDaily_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[SchedulerDaily]'))
ALTER TABLE [dbo].[SchedulerDaily]  WITH CHECK ADD  CONSTRAINT [FK_SchedulerDaily_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SchedulerDaily_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[SchedulerDaily]'))
ALTER TABLE [dbo].[SchedulerDaily] CHECK CONSTRAINT [FK_SchedulerDaily_Tenant]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SchedulerMonthly_Scheduler]') AND parent_object_id = OBJECT_ID(N'[dbo].[SchedulerMonthly]'))
ALTER TABLE [dbo].[SchedulerMonthly]  WITH CHECK ADD  CONSTRAINT [FK_SchedulerMonthly_Scheduler] FOREIGN KEY([SchedulerId])
REFERENCES [dbo].[Scheduler] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SchedulerMonthly_Scheduler]') AND parent_object_id = OBJECT_ID(N'[dbo].[SchedulerMonthly]'))
ALTER TABLE [dbo].[SchedulerMonthly] CHECK CONSTRAINT [FK_SchedulerMonthly_Scheduler]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SchedulerMonthly_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[SchedulerMonthly]'))
ALTER TABLE [dbo].[SchedulerMonthly]  WITH CHECK ADD  CONSTRAINT [FK_SchedulerMonthly_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SchedulerMonthly_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[SchedulerMonthly]'))
ALTER TABLE [dbo].[SchedulerMonthly] CHECK CONSTRAINT [FK_SchedulerMonthly_Tenant]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SchedulerWeekly_Scheduler]') AND parent_object_id = OBJECT_ID(N'[dbo].[SchedulerWeekly]'))
ALTER TABLE [dbo].[SchedulerWeekly]  WITH CHECK ADD  CONSTRAINT [FK_SchedulerWeekly_Scheduler] FOREIGN KEY([SchedulerId])
REFERENCES [dbo].[Scheduler] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SchedulerWeekly_Scheduler]') AND parent_object_id = OBJECT_ID(N'[dbo].[SchedulerWeekly]'))
ALTER TABLE [dbo].[SchedulerWeekly] CHECK CONSTRAINT [FK_SchedulerWeekly_Scheduler]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SchedulerWeekly_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[SchedulerWeekly]'))
ALTER TABLE [dbo].[SchedulerWeekly]  WITH CHECK ADD  CONSTRAINT [FK_SchedulerWeekly_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SchedulerWeekly_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[SchedulerWeekly]'))
ALTER TABLE [dbo].[SchedulerWeekly] CHECK CONSTRAINT [FK_SchedulerWeekly_Tenant]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SchedulerYearly_Scheduler]') AND parent_object_id = OBJECT_ID(N'[dbo].[SchedulerYearly]'))
ALTER TABLE [dbo].[SchedulerYearly]  WITH CHECK ADD  CONSTRAINT [FK_SchedulerYearly_Scheduler] FOREIGN KEY([SchedulerId])
REFERENCES [dbo].[Scheduler] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SchedulerYearly_Scheduler]') AND parent_object_id = OBJECT_ID(N'[dbo].[SchedulerYearly]'))
ALTER TABLE [dbo].[SchedulerYearly] CHECK CONSTRAINT [FK_SchedulerYearly_Scheduler]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SchedulerYearly_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[SchedulerYearly]'))
ALTER TABLE [dbo].[SchedulerYearly]  WITH CHECK ADD  CONSTRAINT [FK_SchedulerYearly_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SchedulerYearly_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[SchedulerYearly]'))
ALTER TABLE [dbo].[SchedulerYearly] CHECK CONSTRAINT [FK_SchedulerYearly_Tenant]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tenant_Image]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tenant]'))
ALTER TABLE [dbo].[Tenant]  WITH CHECK ADD  CONSTRAINT [FK_Tenant_Image] FOREIGN KEY([AvatarId])
REFERENCES [dbo].[Image] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tenant_Image]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tenant]'))
ALTER TABLE [dbo].[Tenant] CHECK CONSTRAINT [FK_Tenant_Image]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tenant_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tenant]'))
ALTER TABLE [dbo].[Tenant]  WITH CHECK ADD  CONSTRAINT [FK_Tenant_Tenant] FOREIGN KEY([Id])
REFERENCES [dbo].[Tenant] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tenant_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tenant]'))
ALTER TABLE [dbo].[Tenant] CHECK CONSTRAINT [FK_Tenant_Tenant]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TenantIPRange_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[TenantIPRange]'))
ALTER TABLE [dbo].[TenantIPRange]  WITH CHECK ADD  CONSTRAINT [FK_TenantIPRange_Tenant] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Tenant] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TenantIPRange_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[TenantIPRange]'))
ALTER TABLE [dbo].[TenantIPRange] CHECK CONSTRAINT [FK_TenantIPRange_Tenant]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TenantSubscription_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[TenantSubscription]'))
ALTER TABLE [dbo].[TenantSubscription]  WITH CHECK ADD  CONSTRAINT [FK_TenantSubscription_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TenantSubscription_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[TenantSubscription]'))
ALTER TABLE [dbo].[TenantSubscription] CHECK CONSTRAINT [FK_TenantSubscription_Tenant]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TenantSubscriptionEntity_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[TenantSubscriptionEntity]'))
ALTER TABLE [dbo].[TenantSubscriptionEntity]  WITH CHECK ADD  CONSTRAINT [FK_TenantSubscriptionEntity_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TenantSubscriptionEntity_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[TenantSubscriptionEntity]'))
ALTER TABLE [dbo].[TenantSubscriptionEntity] CHECK CONSTRAINT [FK_TenantSubscriptionEntity_Tenant]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TenantSubscriptionEntity_TenantSubscription]') AND parent_object_id = OBJECT_ID(N'[dbo].[TenantSubscriptionEntity]'))
ALTER TABLE [dbo].[TenantSubscriptionEntity]  WITH CHECK ADD  CONSTRAINT [FK_TenantSubscriptionEntity_TenantSubscription] FOREIGN KEY([TenantSubscriptionId])
REFERENCES [dbo].[TenantSubscription] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TenantSubscriptionEntity_TenantSubscription]') AND parent_object_id = OBJECT_ID(N'[dbo].[TenantSubscriptionEntity]'))
ALTER TABLE [dbo].[TenantSubscriptionEntity] CHECK CONSTRAINT [FK_TenantSubscriptionEntity_TenantSubscription]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TenantSubscriptionEntityDetail_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[TenantSubscriptionEntityDetail]'))
ALTER TABLE [dbo].[TenantSubscriptionEntityDetail]  WITH CHECK ADD  CONSTRAINT [FK_TenantSubscriptionEntityDetail_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TenantSubscriptionEntityDetail_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[TenantSubscriptionEntityDetail]'))
ALTER TABLE [dbo].[TenantSubscriptionEntityDetail] CHECK CONSTRAINT [FK_TenantSubscriptionEntityDetail_Tenant]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TenantSubscriptionEntityDetail_TenantSubscriptionEntity]') AND parent_object_id = OBJECT_ID(N'[dbo].[TenantSubscriptionEntityDetail]'))
ALTER TABLE [dbo].[TenantSubscriptionEntityDetail]  WITH CHECK ADD  CONSTRAINT [FK_TenantSubscriptionEntityDetail_TenantSubscriptionEntity] FOREIGN KEY([TenantSubscriptionEntityId])
REFERENCES [dbo].[TenantSubscriptionEntity] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TenantSubscriptionEntityDetail_TenantSubscriptionEntity]') AND parent_object_id = OBJECT_ID(N'[dbo].[TenantSubscriptionEntityDetail]'))
ALTER TABLE [dbo].[TenantSubscriptionEntityDetail] CHECK CONSTRAINT [FK_TenantSubscriptionEntityDetail_TenantSubscriptionEntity]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_User_Item]') AND parent_object_id = OBJECT_ID(N'[dbo].[User]'))
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Item] FOREIGN KEY([Id])
REFERENCES [dbo].[Item] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_User_Item]') AND parent_object_id = OBJECT_ID(N'[dbo].[User]'))
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Item]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_User_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[User]'))
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_User] FOREIGN KEY([Id])
REFERENCES [dbo].[User] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_User_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[User]'))
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_User]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_User_UserEmployment]') AND parent_object_id = OBJECT_ID(N'[dbo].[User]'))
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_UserEmployment] FOREIGN KEY([UserEmploymentId])
REFERENCES [dbo].[UserEmployment] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_User_UserEmployment]') AND parent_object_id = OBJECT_ID(N'[dbo].[User]'))
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_UserEmployment]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserCompany_Company]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserCompany]'))
ALTER TABLE [dbo].[UserCompany]  WITH CHECK ADD  CONSTRAINT [FK_UserCompany_Company] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Company] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserCompany_Company]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserCompany]'))
ALTER TABLE [dbo].[UserCompany] CHECK CONSTRAINT [FK_UserCompany_Company]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserCompany_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserCompany]'))
ALTER TABLE [dbo].[UserCompany]  WITH CHECK ADD  CONSTRAINT [FK_UserCompany_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserCompany_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserCompany]'))
ALTER TABLE [dbo].[UserCompany] CHECK CONSTRAINT [FK_UserCompany_User]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserDepartment_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserDepartment]'))
ALTER TABLE [dbo].[UserDepartment]  WITH CHECK ADD  CONSTRAINT [FK_UserDepartment_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserDepartment_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserDepartment]'))
ALTER TABLE [dbo].[UserDepartment] CHECK CONSTRAINT [FK_UserDepartment_User]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserLocation_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserLocation]'))
ALTER TABLE [dbo].[UserLocation]  WITH CHECK ADD  CONSTRAINT [FK_UserLocation_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserLocation_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserLocation]'))
ALTER TABLE [dbo].[UserLocation] CHECK CONSTRAINT [FK_UserLocation_User]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WarehouseLocation_Warehouse]') AND parent_object_id = OBJECT_ID(N'[dbo].[WarehouseLocation]'))
ALTER TABLE [dbo].[WarehouseLocation]  WITH CHECK ADD  CONSTRAINT [FK_WarehouseLocation_Warehouse] FOREIGN KEY([WarehouseCode])
REFERENCES [dbo].[Warehouse] ([Code])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WarehouseLocation_Warehouse]') AND parent_object_id = OBJECT_ID(N'[dbo].[WarehouseLocation]'))
ALTER TABLE [dbo].[WarehouseLocation] CHECK CONSTRAINT [FK_WarehouseLocation_Warehouse]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Workcenter_Vendor]') AND parent_object_id = OBJECT_ID(N'[dbo].[Workcenter]'))
ALTER TABLE [dbo].[Workcenter]  WITH CHECK ADD  CONSTRAINT [FK_Workcenter_Vendor] FOREIGN KEY([VendorCode])
REFERENCES [dbo].[Vendor] ([Code])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Workcenter_Vendor]') AND parent_object_id = OBJECT_ID(N'[dbo].[Workcenter]'))
ALTER TABLE [dbo].[Workcenter] CHECK CONSTRAINT [FK_Workcenter_Vendor]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkcenterCompetence_Workcenter]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkcenterCompetence]'))
ALTER TABLE [dbo].[WorkcenterCompetence]  WITH CHECK ADD  CONSTRAINT [FK_WorkcenterCompetence_Workcenter] FOREIGN KEY([WorkcenterCode])
REFERENCES [dbo].[Workcenter] ([Code])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkcenterCompetence_Workcenter]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkcenterCompetence]'))
ALTER TABLE [dbo].[WorkcenterCompetence] CHECK CONSTRAINT [FK_WorkcenterCompetence_Workcenter]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkcenterConfiguration_Warehouse_From]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkcenterConfiguration]'))
ALTER TABLE [dbo].[WorkcenterConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_WorkcenterConfiguration_Warehouse_From] FOREIGN KEY([ConsumableWarehouseCode])
REFERENCES [dbo].[Warehouse] ([Code])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkcenterConfiguration_Warehouse_From]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkcenterConfiguration]'))
ALTER TABLE [dbo].[WorkcenterConfiguration] CHECK CONSTRAINT [FK_WorkcenterConfiguration_Warehouse_From]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkcenterConfiguration_Warehouse_To]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkcenterConfiguration]'))
ALTER TABLE [dbo].[WorkcenterConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_WorkcenterConfiguration_Warehouse_To] FOREIGN KEY([ProducedWarehouseCode])
REFERENCES [dbo].[Warehouse] ([Code])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkcenterConfiguration_Warehouse_To]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkcenterConfiguration]'))
ALTER TABLE [dbo].[WorkcenterConfiguration] CHECK CONSTRAINT [FK_WorkcenterConfiguration_Warehouse_To]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkcenterConfiguration_Workcenter]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkcenterConfiguration]'))
ALTER TABLE [dbo].[WorkcenterConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_WorkcenterConfiguration_Workcenter] FOREIGN KEY([WorkcenterCode])
REFERENCES [dbo].[Workcenter] ([Code])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkcenterConfiguration_Workcenter]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkcenterConfiguration]'))
ALTER TABLE [dbo].[WorkcenterConfiguration] CHECK CONSTRAINT [FK_WorkcenterConfiguration_Workcenter]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkcenterEquipment_Equipment]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkcenterEquipment]'))
ALTER TABLE [dbo].[WorkcenterEquipment]  WITH CHECK ADD  CONSTRAINT [FK_WorkcenterEquipment_Equipment] FOREIGN KEY([EquipmentCode])
REFERENCES [dbo].[Equipment] ([Code])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkcenterEquipment_Equipment]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkcenterEquipment]'))
ALTER TABLE [dbo].[WorkcenterEquipment] CHECK CONSTRAINT [FK_WorkcenterEquipment_Equipment]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkcenterEquipment_Workcenter]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkcenterEquipment]'))
ALTER TABLE [dbo].[WorkcenterEquipment]  WITH CHECK ADD  CONSTRAINT [FK_WorkcenterEquipment_Workcenter] FOREIGN KEY([WorkcenterCode])
REFERENCES [dbo].[Workcenter] ([Code])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkcenterEquipment_Workcenter]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkcenterEquipment]'))
ALTER TABLE [dbo].[WorkcenterEquipment] CHECK CONSTRAINT [FK_WorkcenterEquipment_Workcenter]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkcenterTool_Tool]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkcenterTool]'))
ALTER TABLE [dbo].[WorkcenterTool]  WITH CHECK ADD  CONSTRAINT [FK_WorkcenterTool_Tool] FOREIGN KEY([ToolCode])
REFERENCES [dbo].[Tool] ([Code])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkcenterTool_Tool]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkcenterTool]'))
ALTER TABLE [dbo].[WorkcenterTool] CHECK CONSTRAINT [FK_WorkcenterTool_Tool]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkcenterTool_Workcenter]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkcenterTool]'))
ALTER TABLE [dbo].[WorkcenterTool]  WITH CHECK ADD  CONSTRAINT [FK_WorkcenterTool_Workcenter] FOREIGN KEY([WorkcenterCode])
REFERENCES [dbo].[Workcenter] ([Code])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkcenterTool_Workcenter]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkcenterTool]'))
ALTER TABLE [dbo].[WorkcenterTool] CHECK CONSTRAINT [FK_WorkcenterTool_Workcenter]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkcenterUser_Workcenter]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkcenterUser]'))
ALTER TABLE [dbo].[WorkcenterUser]  WITH CHECK ADD  CONSTRAINT [FK_WorkcenterUser_Workcenter] FOREIGN KEY([WorkcenterCode])
REFERENCES [dbo].[Workcenter] ([Code])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkcenterUser_Workcenter]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkcenterUser]'))
ALTER TABLE [dbo].[WorkcenterUser] CHECK CONSTRAINT [FK_WorkcenterUser_Workcenter]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlow_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlow]'))
ALTER TABLE [dbo].[WorkFlow]  WITH CHECK ADD  CONSTRAINT [FK_WorkFlow_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlow_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlow]'))
ALTER TABLE [dbo].[WorkFlow] CHECK CONSTRAINT [FK_WorkFlow_Tenant]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowInnerStep_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowInnerStep]'))
ALTER TABLE [dbo].[WorkFlowInnerStep]  WITH CHECK ADD  CONSTRAINT [FK_WorkFlowInnerStep_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowInnerStep_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowInnerStep]'))
ALTER TABLE [dbo].[WorkFlowInnerStep] CHECK CONSTRAINT [FK_WorkFlowInnerStep_Tenant]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowInnerStep_WorkFlow]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowInnerStep]'))
ALTER TABLE [dbo].[WorkFlowInnerStep]  WITH CHECK ADD  CONSTRAINT [FK_WorkFlowInnerStep_WorkFlow] FOREIGN KEY([WorkFlowId])
REFERENCES [dbo].[WorkFlow] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowInnerStep_WorkFlow]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowInnerStep]'))
ALTER TABLE [dbo].[WorkFlowInnerStep] CHECK CONSTRAINT [FK_WorkFlowInnerStep_WorkFlow]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowStep_WorkFlowInnerStep]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowInnerStep]'))
ALTER TABLE [dbo].[WorkFlowInnerStep]  WITH CHECK ADD  CONSTRAINT [FK_WorkFlowStep_WorkFlowInnerStep] FOREIGN KEY([WorkFlowStepId])
REFERENCES [dbo].[WorkFlowStep] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowStep_WorkFlowInnerStep]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowInnerStep]'))
ALTER TABLE [dbo].[WorkFlowInnerStep] CHECK CONSTRAINT [FK_WorkFlowStep_WorkFlowInnerStep]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowOperation_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowOperation]'))
ALTER TABLE [dbo].[WorkFlowOperation]  WITH CHECK ADD  CONSTRAINT [FK_WorkFlowOperation_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowOperation_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowOperation]'))
ALTER TABLE [dbo].[WorkFlowOperation] CHECK CONSTRAINT [FK_WorkFlowOperation_Tenant]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowOperation_WorkFlow]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowOperation]'))
ALTER TABLE [dbo].[WorkFlowOperation]  WITH CHECK ADD  CONSTRAINT [FK_WorkFlowOperation_WorkFlow] FOREIGN KEY([WorkFlowId])
REFERENCES [dbo].[WorkFlow] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowOperation_WorkFlow]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowOperation]'))
ALTER TABLE [dbo].[WorkFlowOperation] CHECK CONSTRAINT [FK_WorkFlowOperation_WorkFlow]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowProcess_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowProcess]'))
ALTER TABLE [dbo].[WorkFlowProcess]  WITH CHECK ADD  CONSTRAINT [FK_WorkFlowProcess_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowProcess_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowProcess]'))
ALTER TABLE [dbo].[WorkFlowProcess] CHECK CONSTRAINT [FK_WorkFlowProcess_Tenant]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowProcess_WorkFlow]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowProcess]'))
ALTER TABLE [dbo].[WorkFlowProcess]  WITH CHECK ADD  CONSTRAINT [FK_WorkFlowProcess_WorkFlow] FOREIGN KEY([WorkFlowId])
REFERENCES [dbo].[WorkFlow] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowProcess_WorkFlow]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowProcess]'))
ALTER TABLE [dbo].[WorkFlowProcess] CHECK CONSTRAINT [FK_WorkFlowProcess_WorkFlow]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowProcessTask_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowProcessTask]'))
ALTER TABLE [dbo].[WorkFlowProcessTask]  WITH CHECK ADD  CONSTRAINT [FK_WorkFlowProcessTask_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowProcessTask_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowProcessTask]'))
ALTER TABLE [dbo].[WorkFlowProcessTask] CHECK CONSTRAINT [FK_WorkFlowProcessTask_Tenant]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowProcessTask_WorkFlow]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowProcessTask]'))
ALTER TABLE [dbo].[WorkFlowProcessTask]  WITH CHECK ADD  CONSTRAINT [FK_WorkFlowProcessTask_WorkFlow] FOREIGN KEY([WorkFlowId])
REFERENCES [dbo].[WorkFlow] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowProcessTask_WorkFlow]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowProcessTask]'))
ALTER TABLE [dbo].[WorkFlowProcessTask] CHECK CONSTRAINT [FK_WorkFlowProcessTask_WorkFlow]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowProcessTask_WorkFlowProcess]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowProcessTask]'))
ALTER TABLE [dbo].[WorkFlowProcessTask]  WITH CHECK ADD  CONSTRAINT [FK_WorkFlowProcessTask_WorkFlowProcess] FOREIGN KEY([WorkFlowProcessId])
REFERENCES [dbo].[WorkFlowProcess] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowProcessTask_WorkFlowProcess]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowProcessTask]'))
ALTER TABLE [dbo].[WorkFlowProcessTask] CHECK CONSTRAINT [FK_WorkFlowProcessTask_WorkFlowProcess]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowRole_Role]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowRole]'))
ALTER TABLE [dbo].[WorkFlowRole]  WITH CHECK ADD  CONSTRAINT [FK_WorkFlowRole_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowRole_Role]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowRole]'))
ALTER TABLE [dbo].[WorkFlowRole] CHECK CONSTRAINT [FK_WorkFlowRole_Role]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowRole_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowRole]'))
ALTER TABLE [dbo].[WorkFlowRole]  WITH CHECK ADD  CONSTRAINT [FK_WorkFlowRole_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowRole_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowRole]'))
ALTER TABLE [dbo].[WorkFlowRole] CHECK CONSTRAINT [FK_WorkFlowRole_Tenant]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowRole_WorkFlow]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowRole]'))
ALTER TABLE [dbo].[WorkFlowRole]  WITH CHECK ADD  CONSTRAINT [FK_WorkFlowRole_WorkFlow] FOREIGN KEY([WorkFlowId])
REFERENCES [dbo].[WorkFlow] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowRole_WorkFlow]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowRole]'))
ALTER TABLE [dbo].[WorkFlowRole] CHECK CONSTRAINT [FK_WorkFlowRole_WorkFlow]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowStep_WorkFlowRole]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowRole]'))
ALTER TABLE [dbo].[WorkFlowRole]  WITH CHECK ADD  CONSTRAINT [FK_WorkFlowStep_WorkFlowRole] FOREIGN KEY([WorkFlowStepId])
REFERENCES [dbo].[WorkFlowStep] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowStep_WorkFlowRole]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowRole]'))
ALTER TABLE [dbo].[WorkFlowRole] CHECK CONSTRAINT [FK_WorkFlowStep_WorkFlowRole]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowStep_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowStep]'))
ALTER TABLE [dbo].[WorkFlowStep]  WITH CHECK ADD  CONSTRAINT [FK_WorkFlowStep_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowStep_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowStep]'))
ALTER TABLE [dbo].[WorkFlowStep] CHECK CONSTRAINT [FK_WorkFlowStep_Tenant]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowStep_WorkFlow]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowStep]'))
ALTER TABLE [dbo].[WorkFlowStep]  WITH CHECK ADD  CONSTRAINT [FK_WorkFlowStep_WorkFlow] FOREIGN KEY([WorkFlowId])
REFERENCES [dbo].[WorkFlow] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowStep_WorkFlow]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowStep]'))
ALTER TABLE [dbo].[WorkFlowStep] CHECK CONSTRAINT [FK_WorkFlowStep_WorkFlow]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowSteps_WorkFlowTransitionHistory]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowTransitionHistory]'))
ALTER TABLE [dbo].[WorkFlowTransitionHistory]  WITH CHECK ADD  CONSTRAINT [FK_WorkFlowSteps_WorkFlowTransitionHistory] FOREIGN KEY([WorkFlowStepId])
REFERENCES [dbo].[WorkFlowStep] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowSteps_WorkFlowTransitionHistory]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowTransitionHistory]'))
ALTER TABLE [dbo].[WorkFlowTransitionHistory] CHECK CONSTRAINT [FK_WorkFlowSteps_WorkFlowTransitionHistory]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowTransitionHistory_CreatedBy]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowTransitionHistory]'))
ALTER TABLE [dbo].[WorkFlowTransitionHistory]  WITH CHECK ADD  CONSTRAINT [FK_WorkFlowTransitionHistory_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowTransitionHistory_CreatedBy]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowTransitionHistory]'))
ALTER TABLE [dbo].[WorkFlowTransitionHistory] CHECK CONSTRAINT [FK_WorkFlowTransitionHistory_CreatedBy]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowTransitionHistory_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowTransitionHistory]'))
ALTER TABLE [dbo].[WorkFlowTransitionHistory]  WITH CHECK ADD  CONSTRAINT [FK_WorkFlowTransitionHistory_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowTransitionHistory_Tenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowTransitionHistory]'))
ALTER TABLE [dbo].[WorkFlowTransitionHistory] CHECK CONSTRAINT [FK_WorkFlowTransitionHistory_Tenant]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowTransitionHistory_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowTransitionHistory]'))
ALTER TABLE [dbo].[WorkFlowTransitionHistory]  WITH CHECK ADD  CONSTRAINT [FK_WorkFlowTransitionHistory_User] FOREIGN KEY([AssignedUserId])
REFERENCES [dbo].[User] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WorkFlowTransitionHistory_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[WorkFlowTransitionHistory]'))
ALTER TABLE [dbo].[WorkFlowTransitionHistory] CHECK CONSTRAINT [FK_WorkFlowTransitionHistory_User]
GO



/****** Object:  StoredProcedure [dbo].[WorkFlowTransition_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowTransition_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WorkFlowTransition_Update]
GO
/****** Object:  StoredProcedure [dbo].[WorkFlowTransition_GetByRefId]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowTransition_GetByRefId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WorkFlowTransition_GetByRefId]
GO
/****** Object:  StoredProcedure [dbo].[WorkFlowTransition_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowTransition_Create]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WorkFlowTransition_Create]
GO
/****** Object:  StoredProcedure [dbo].[WorkFlowSteps_UserCanAssign]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowSteps_UserCanAssign]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WorkFlowSteps_UserCanAssign]
GO
/****** Object:  StoredProcedure [dbo].[WorkFlowSteps_GetBy_UserCode]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowSteps_GetBy_UserCode]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WorkFlowSteps_GetBy_UserCode]
GO
/****** Object:  StoredProcedure [dbo].[WorkFlowSteps_Get_WorkFlowIds]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowSteps_Get_WorkFlowIds]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WorkFlowSteps_Get_WorkFlowIds]
GO
/****** Object:  StoredProcedure [dbo].[WorkFlowStep_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowStep_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WorkFlowStep_Update]
GO
/****** Object:  StoredProcedure [dbo].[WorkFlowStep_MoveUpDown]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowStep_MoveUpDown]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WorkFlowStep_MoveUpDown]
GO
/****** Object:  StoredProcedure [dbo].[WorkFlowStep_Get]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowStep_Get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WorkFlowStep_Get]
GO
/****** Object:  StoredProcedure [dbo].[WorkFlowStep_Delete]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowStep_Delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WorkFlowStep_Delete]
GO
/****** Object:  StoredProcedure [dbo].[WorkFlowStep_Create_Xml]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowStep_Create_Xml]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WorkFlowStep_Create_Xml]
GO
/****** Object:  StoredProcedure [dbo].[WorkFlowStep_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowStep_Create]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WorkFlowStep_Create]
GO
/****** Object:  StoredProcedure [dbo].[WorkFlows_GetBy_Ids]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlows_GetBy_Ids]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WorkFlows_GetBy_Ids]
GO
/****** Object:  StoredProcedure [dbo].[WorkFlows_GetBy_EntityIds]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlows_GetBy_EntityIds]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WorkFlows_GetBy_EntityIds]
GO
/****** Object:  StoredProcedure [dbo].[WorkFlowRole_Get_WorkFlowIds]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowRole_Get_WorkFlowIds]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WorkFlowRole_Get_WorkFlowIds]
GO
/****** Object:  StoredProcedure [dbo].[WorkFlowRole_Get_StepIds]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowRole_Get_StepIds]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WorkFlowRole_Get_StepIds]
GO
/****** Object:  StoredProcedure [dbo].[WorkFlowRole_Get]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowRole_Get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WorkFlowRole_Get]
GO
/****** Object:  StoredProcedure [dbo].[WorkFlowRole_Delete]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowRole_Delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WorkFlowRole_Delete]
GO
/****** Object:  StoredProcedure [dbo].[WorkFlowRole_Create_Xml]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowRole_Create_Xml]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WorkFlowRole_Create_Xml]
GO
/****** Object:  StoredProcedure [dbo].[WorkFlowRole_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowRole_Create]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WorkFlowRole_Create]
GO
/****** Object:  StoredProcedure [dbo].[WorkFlowProcessTask_MoveUpDown]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowProcessTask_MoveUpDown]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WorkFlowProcessTask_MoveUpDown]
GO
/****** Object:  StoredProcedure [dbo].[WorkFlowProcessTask_GetBy_WorkFlowIds]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowProcessTask_GetBy_WorkFlowIds]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WorkFlowProcessTask_GetBy_WorkFlowIds]
GO
/****** Object:  StoredProcedure [dbo].[WorkFlowProcessTask_Get_ByProcessIds]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowProcessTask_Get_ByProcessIds]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WorkFlowProcessTask_Get_ByProcessIds]
GO
/****** Object:  StoredProcedure [dbo].[WorkFlowProcessTask_Get_ByInnerStepId]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowProcessTask_Get_ByInnerStepId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WorkFlowProcessTask_Get_ByInnerStepId]
GO
/****** Object:  StoredProcedure [dbo].[WorkFlowProcessTask_Get]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowProcessTask_Get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WorkFlowProcessTask_Get]
GO
/****** Object:  StoredProcedure [dbo].[WorkFlowProcessTask_Delete]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowProcessTask_Delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WorkFlowProcessTask_Delete]
GO
/****** Object:  StoredProcedure [dbo].[WorkFlowProcessTask_Create_Xml]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowProcessTask_Create_Xml]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WorkFlowProcessTask_Create_Xml]
GO
/****** Object:  StoredProcedure [dbo].[WorkFlowProcessTask_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowProcessTask_Create]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WorkFlowProcessTask_Create]
GO
/****** Object:  StoredProcedure [dbo].[WorkFlowProcess_GetByW_WorkFlowIds]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowProcess_GetByW_WorkFlowIds]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WorkFlowProcess_GetByW_WorkFlowIds]
GO
/****** Object:  StoredProcedure [dbo].[WorkFlowProcess_GetBy_OperationOrTransitionIds]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowProcess_GetBy_OperationOrTransitionIds]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WorkFlowProcess_GetBy_OperationOrTransitionIds]
GO
/****** Object:  StoredProcedure [dbo].[WorkFlowProcess_Get]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowProcess_Get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WorkFlowProcess_Get]
GO
/****** Object:  StoredProcedure [dbo].[WorkFlowProcess_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowProcess_Create]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WorkFlowProcess_Create]
GO
/****** Object:  StoredProcedure [dbo].[WorkFlowOperation_GetBy_WorkFlowIds]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowOperation_GetBy_WorkFlowIds]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WorkFlowOperation_GetBy_WorkFlowIds]
GO
/****** Object:  StoredProcedure [dbo].[WorkFlowOperation_Get]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowOperation_Get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WorkFlowOperation_Get]
GO
/****** Object:  StoredProcedure [dbo].[WorkFlowOperation_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowOperation_Create]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WorkFlowOperation_Create]
GO
/****** Object:  StoredProcedure [dbo].[WorkFlowInnerStep_MoveUpDown]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowInnerStep_MoveUpDown]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WorkFlowInnerStep_MoveUpDown]
GO
/****** Object:  StoredProcedure [dbo].[WorkFlowInnerStep_GetByWorkFlowIds]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowInnerStep_GetByWorkFlowIds]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WorkFlowInnerStep_GetByWorkFlowIds]
GO
/****** Object:  StoredProcedure [dbo].[WorkFlowInnerStep_GetByStepTransactionType]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowInnerStep_GetByStepTransactionType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WorkFlowInnerStep_GetByStepTransactionType]
GO
/****** Object:  StoredProcedure [dbo].[WorkFlowInnerStep_GetByStepIds]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowInnerStep_GetByStepIds]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WorkFlowInnerStep_GetByStepIds]
GO
/****** Object:  StoredProcedure [dbo].[WorkFlowInnerStep_Get]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowInnerStep_Get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WorkFlowInnerStep_Get]
GO
/****** Object:  StoredProcedure [dbo].[WorkFlowInnerStep_Delete]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowInnerStep_Delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WorkFlowInnerStep_Delete]
GO
/****** Object:  StoredProcedure [dbo].[WorkFlowInnerStep_Create_Xml]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowInnerStep_Create_Xml]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WorkFlowInnerStep_Create_Xml]
GO
/****** Object:  StoredProcedure [dbo].[WorkFlowInnerStep_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowInnerStep_Create]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WorkFlowInnerStep_Create]
GO
/****** Object:  StoredProcedure [dbo].[WorkFlow_Get]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlow_Get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WorkFlow_Get]
GO
/****** Object:  StoredProcedure [dbo].[WorkFlow_Create_Xml]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlow_Create_Xml]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WorkFlow_Create_Xml]
GO
/****** Object:  StoredProcedure [dbo].[WorkFlow_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlow_Create]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WorkFlow_Create]
GO
/****** Object:  StoredProcedure [dbo].[WorkFlow_All_Get]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlow_All_Get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WorkFlow_All_Get]
GO
/****** Object:  StoredProcedure [dbo].[UserCredential_UpdateLockedStatus]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserCredential_UpdateLockedStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UserCredential_UpdateLockedStatus]
GO
/****** Object:  StoredProcedure [dbo].[UserCredential_Get]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserCredential_Get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UserCredential_Get]
GO
/****** Object:  StoredProcedure [dbo].[User_GetBy_Id]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User_GetBy_Id]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[User_GetBy_Id]
GO
/****** Object:  StoredProcedure [dbo].[TenantSubscriptionEntityDetail_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TenantSubscriptionEntityDetail_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[TenantSubscriptionEntityDetail_Update]
GO
/****** Object:  StoredProcedure [dbo].[TenantSubscriptionEntityDetail_GetAll]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TenantSubscriptionEntityDetail_GetAll]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[TenantSubscriptionEntityDetail_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[TenantSubscriptionEntityDetail_Get]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TenantSubscriptionEntityDetail_Get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[TenantSubscriptionEntityDetail_Get]
GO
/****** Object:  StoredProcedure [dbo].[TenantSubscriptionEntityDetail_DeleteBySubsEntityId]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TenantSubscriptionEntityDetail_DeleteBySubsEntityId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[TenantSubscriptionEntityDetail_DeleteBySubsEntityId]
GO
/****** Object:  StoredProcedure [dbo].[TenantSubscriptionEntityDetail_Delete]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TenantSubscriptionEntityDetail_Delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[TenantSubscriptionEntityDetail_Delete]
GO
/****** Object:  StoredProcedure [dbo].[TenantSubscriptionEntityDetail_Create_Xml]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TenantSubscriptionEntityDetail_Create_Xml]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[TenantSubscriptionEntityDetail_Create_Xml]
GO
/****** Object:  StoredProcedure [dbo].[TenantSubscriptionEntityDetail_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TenantSubscriptionEntityDetail_Create]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[TenantSubscriptionEntityDetail_Create]
GO
/****** Object:  StoredProcedure [dbo].[TenantSubscriptionEntity_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TenantSubscriptionEntity_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[TenantSubscriptionEntity_Update]
GO
/****** Object:  StoredProcedure [dbo].[TenantSubscriptionEntity_GetAll]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TenantSubscriptionEntity_GetAll]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[TenantSubscriptionEntity_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[TenantSubscriptionEntity_Get]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TenantSubscriptionEntity_Get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[TenantSubscriptionEntity_Get]
GO
/****** Object:  StoredProcedure [dbo].[TenantSubscriptionEntity_Delete]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TenantSubscriptionEntity_Delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[TenantSubscriptionEntity_Delete]
GO
/****** Object:  StoredProcedure [dbo].[TenantSubscriptionEntity_Create_Xml]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TenantSubscriptionEntity_Create_Xml]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[TenantSubscriptionEntity_Create_Xml]
GO
/****** Object:  StoredProcedure [dbo].[TenantSubscriptionEntity_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TenantSubscriptionEntity_Create]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[TenantSubscriptionEntity_Create]
GO
/****** Object:  StoredProcedure [dbo].[TenantSubscription_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TenantSubscription_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[TenantSubscription_Update]
GO
/****** Object:  StoredProcedure [dbo].[TenantSubscription_Status]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TenantSubscription_Status]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[TenantSubscription_Status]
GO
/****** Object:  StoredProcedure [dbo].[TenantSubscription_GetAll]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TenantSubscription_GetAll]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[TenantSubscription_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[TenantSubscription_Get]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TenantSubscription_Get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[TenantSubscription_Get]
GO
/****** Object:  StoredProcedure [dbo].[TenantSubscription_Delete]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TenantSubscription_Delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[TenantSubscription_Delete]
GO
/****** Object:  StoredProcedure [dbo].[TenantSubscription_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TenantSubscription_Create]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[TenantSubscription_Create]
GO
/****** Object:  StoredProcedure [dbo].[Tenant_GetTenantId]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tenant_GetTenantId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Tenant_GetTenantId]
GO
/****** Object:  StoredProcedure [dbo].[Tenant_GetDefaultLanguageDetails_BCK_24062019]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tenant_GetDefaultLanguageDetails_BCK_24062019]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Tenant_GetDefaultLanguageDetails_BCK_24062019]
GO
/****** Object:  StoredProcedure [dbo].[Tenant_GetDefaultLanguageDetails]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tenant_GetDefaultLanguageDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Tenant_GetDefaultLanguageDetails]
GO
/****** Object:  StoredProcedure [dbo].[Setting_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Setting_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Setting_Update]
GO
/****** Object:  StoredProcedure [dbo].[Setting_GetById]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Setting_GetById]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Setting_GetById]
GO
/****** Object:  StoredProcedure [dbo].[Setting_GetByContextType]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Setting_GetByContextType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Setting_GetByContextType]
GO
/****** Object:  StoredProcedure [dbo].[Setting_DeleteById]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Setting_DeleteById]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Setting_DeleteById]
GO
/****** Object:  StoredProcedure [dbo].[Setting_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Setting_Create]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Setting_Create]
GO
/****** Object:  StoredProcedure [dbo].[SchedulerYearly_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchedulerYearly_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SchedulerYearly_Update]
GO
/****** Object:  StoredProcedure [dbo].[SchedulerYearly_GetBy_SchedulerId]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchedulerYearly_GetBy_SchedulerId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SchedulerYearly_GetBy_SchedulerId]
GO
/****** Object:  StoredProcedure [dbo].[SchedulerYearly_Delete]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchedulerYearly_Delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SchedulerYearly_Delete]
GO
/****** Object:  StoredProcedure [dbo].[SchedulerYearly_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchedulerYearly_Create]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SchedulerYearly_Create]
GO
/****** Object:  StoredProcedure [dbo].[SchedulerWeekly_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchedulerWeekly_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SchedulerWeekly_Update]
GO
/****** Object:  StoredProcedure [dbo].[SchedulerWeekly_GetBy_SchedulerId]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchedulerWeekly_GetBy_SchedulerId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SchedulerWeekly_GetBy_SchedulerId]
GO
/****** Object:  StoredProcedure [dbo].[SchedulerWeekly_Delete]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchedulerWeekly_Delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SchedulerWeekly_Delete]
GO
/****** Object:  StoredProcedure [dbo].[SchedulerWeekly_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchedulerWeekly_Create]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SchedulerWeekly_Create]
GO
/****** Object:  StoredProcedure [dbo].[SchedulerMonthly_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchedulerMonthly_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SchedulerMonthly_Update]
GO
/****** Object:  StoredProcedure [dbo].[SchedulerMonthly_GetBy_SchedulerId]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchedulerMonthly_GetBy_SchedulerId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SchedulerMonthly_GetBy_SchedulerId]
GO
/****** Object:  StoredProcedure [dbo].[SchedulerMonthly_Delete]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchedulerMonthly_Delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SchedulerMonthly_Delete]
GO
/****** Object:  StoredProcedure [dbo].[SchedulerMonthly_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchedulerMonthly_Create]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SchedulerMonthly_Create]
GO
/****** Object:  StoredProcedure [dbo].[SchedulerDaily_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchedulerDaily_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SchedulerDaily_Update]
GO
/****** Object:  StoredProcedure [dbo].[SchedulerDaily_GetBySchedulerId]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchedulerDaily_GetBySchedulerId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SchedulerDaily_GetBySchedulerId]
GO
/****** Object:  StoredProcedure [dbo].[SchedulerDaily_Delete]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchedulerDaily_Delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SchedulerDaily_Delete]
GO
/****** Object:  StoredProcedure [dbo].[SchedulerDaily_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchedulerDaily_Create]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SchedulerDaily_Create]
GO
/****** Object:  StoredProcedure [dbo].[Scheduler_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Scheduler_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Scheduler_Update]
GO
/****** Object:  StoredProcedure [dbo].[Scheduler_GetBy_BatchId]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Scheduler_GetBy_BatchId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Scheduler_GetBy_BatchId]
GO
/****** Object:  StoredProcedure [dbo].[Scheduler_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Scheduler_Create]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Scheduler_Create]
GO
/****** Object:  StoredProcedure [dbo].[Rule_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Rule_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Rule_Update]
GO
/****** Object:  StoredProcedure [dbo].[Rule_GetById]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Rule_GetById]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Rule_GetById]
GO
/****** Object:  StoredProcedure [dbo].[Rule_GetAll]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Rule_GetAll]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Rule_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[Rule_Delete]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Rule_Delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Rule_Delete]
GO
/****** Object:  StoredProcedure [dbo].[Rule_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Rule_Create]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Rule_Create]
GO
/****** Object:  StoredProcedure [dbo].[Roles_GetAll_ById]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Roles_GetAll_ById]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Roles_GetAll_ById]
GO
/****** Object:  StoredProcedure [dbo].[Role_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Role_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Role_Update]
GO
/****** Object:  StoredProcedure [dbo].[Role_GetBy_Id]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Role_GetBy_Id]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Role_GetBy_Id]
GO
/****** Object:  StoredProcedure [dbo].[Role_GetAll]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Role_GetAll]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Role_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[Role_Get]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Role_Get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Role_Get]
GO
/****** Object:  StoredProcedure [dbo].[Role_Delete]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Role_Delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Role_Delete]
GO
/****** Object:  StoredProcedure [dbo].[Role_Create_Xml]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Role_Create_Xml]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Role_Create_Xml]
GO
/****** Object:  StoredProcedure [dbo].[Role_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Role_Create]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Role_Create]
GO
/****** Object:  StoredProcedure [dbo].[Resource_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Resource_Update]
GO
/****** Object:  StoredProcedure [dbo].[Resource_SaveorUpdateorDelete]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource_SaveorUpdateorDelete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Resource_SaveorUpdateorDelete]
GO
/****** Object:  StoredProcedure [dbo].[Resource_GetKeyFromLanguage]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource_GetKeyFromLanguage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Resource_GetKeyFromLanguage]
GO
/****** Object:  StoredProcedure [dbo].[Resource_GetByKeyAndLanguage]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource_GetByKeyAndLanguage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Resource_GetByKeyAndLanguage]
GO
/****** Object:  StoredProcedure [dbo].[Resource_GetALL_BKP_20062019]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource_GetALL_BKP_20062019]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Resource_GetALL_BKP_20062019]
GO
/****** Object:  StoredProcedure [dbo].[Resource_GetALL]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource_GetALL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Resource_GetALL]
GO
/****** Object:  StoredProcedure [dbo].[Resource_Get]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource_Get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Resource_Get]
GO
/****** Object:  StoredProcedure [dbo].[Resource_DeleteByKey]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource_DeleteByKey]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Resource_DeleteByKey]
GO
/****** Object:  StoredProcedure [dbo].[Resource_Delete]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource_Delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Resource_Delete]
GO
/****** Object:  StoredProcedure [dbo].[Resource_Create_Xml]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource_Create_Xml]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Resource_Create_Xml]
GO
/****** Object:  StoredProcedure [dbo].[Resource_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource_Create]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Resource_Create]
GO
/****** Object:  StoredProcedure [dbo].[Resource_CheckDuplicateKey]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource_CheckDuplicateKey]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Resource_CheckDuplicateKey]
GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_Update_Status]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_Update_Status]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PicklistValue_Update_Status]
GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PicklistValue_Update]
GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_Timezone_ExtendedValue_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_Timezone_ExtendedValue_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PicklistValue_Timezone_ExtendedValue_Update]
GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_Timezone_ExtendedValue_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_Timezone_ExtendedValue_Create]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PicklistValue_Timezone_ExtendedValue_Create]
GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_Timezone_Clone]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_Timezone_Clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PicklistValue_Timezone_Clone]
GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_State_ExtendedValue_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_State_ExtendedValue_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PicklistValue_State_ExtendedValue_Update]
GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_State_ExtendedValue_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_State_ExtendedValue_Create]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PicklistValue_State_ExtendedValue_Create]
GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_State_Clone]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_State_Clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PicklistValue_State_Clone]
GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_SoftDelete]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_SoftDelete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PicklistValue_SoftDelete]
GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_SecurityFunction_Clone]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_SecurityFunction_Clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PicklistValue_SecurityFunction_Clone]
GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_Municipality_ExtendedValue_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_Municipality_ExtendedValue_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PicklistValue_Municipality_ExtendedValue_Update]
GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_Municipality_ExtendedValue_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_Municipality_ExtendedValue_Create]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PicklistValue_Municipality_ExtendedValue_Create]
GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_Municipality_Clone]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_Municipality_Clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PicklistValue_Municipality_Clone]
GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_MenuGroup_Clone]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_MenuGroup_Clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PicklistValue_MenuGroup_Clone]
GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_Language_ExtendedValue_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_Language_ExtendedValue_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PicklistValue_Language_ExtendedValue_Update]
GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_Language_ExtendedValue_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_Language_ExtendedValue_Create]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PicklistValue_Language_ExtendedValue_Create]
GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_Language_Clone]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_Language_Clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PicklistValue_Language_Clone]
GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_GetFavourite]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_GetFavourite]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PicklistValue_GetFavourite]
GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_GetBy_Id]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_GetBy_Id]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PicklistValue_GetBy_Id]
GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_GetAll]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_GetAll]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PicklistValue_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_Favourite_Delete]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_Favourite_Delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PicklistValue_Favourite_Delete]
GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_Delete]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_Delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PicklistValue_Delete]
GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_Currency_ExtendedValue_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_Currency_ExtendedValue_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PicklistValue_Currency_ExtendedValue_Update]
GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_Currency_ExtendedValue_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_Currency_ExtendedValue_Create]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PicklistValue_Currency_ExtendedValue_Create]
GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_Currency_Clone]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_Currency_Clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PicklistValue_Currency_Clone]
GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_Create_Favourite]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_Create_Favourite]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PicklistValue_Create_Favourite]
GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_Create]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PicklistValue_Create]
GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_Country_ExtendedValue_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_Country_ExtendedValue_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PicklistValue_Country_ExtendedValue_Update]
GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_Country_ExtendedValue_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_Country_ExtendedValue_Create]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PicklistValue_Country_ExtendedValue_Create]
GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_Country_Clone]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_Country_Clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PicklistValue_Country_Clone]
GO
/****** Object:  StoredProcedure [dbo].[PickListValue_Clone]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PickListValue_Clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PickListValue_Clone]
GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_City_Clone]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_City_Clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PicklistValue_City_Clone]
GO
/****** Object:  StoredProcedure [dbo].[PicklistLayouts_Clone]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistLayouts_Clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PicklistLayouts_Clone]
GO
/****** Object:  StoredProcedure [dbo].[PicklistLayout_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistLayout_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PicklistLayout_Update]
GO
/****** Object:  StoredProcedure [dbo].[PicklistLayout_Set_Default]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistLayout_Set_Default]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PicklistLayout_Set_Default]
GO
/****** Object:  StoredProcedure [dbo].[PicklistLayout_GetDefaultBy_Type]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistLayout_GetDefaultBy_Type]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PicklistLayout_GetDefaultBy_Type]
GO
/****** Object:  StoredProcedure [dbo].[PicklistLayout_GetBy_Id]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistLayout_GetBy_Id]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PicklistLayout_GetBy_Id]
GO
/****** Object:  StoredProcedure [dbo].[PicklistLayout_Delete]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistLayout_Delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PicklistLayout_Delete]
GO
/****** Object:  StoredProcedure [dbo].[PicklistLayout_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistLayout_Create]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PicklistLayout_Create]
GO
/****** Object:  StoredProcedure [dbo].[Picklist_Update_Status]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Picklist_Update_Status]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Picklist_Update_Status]
GO
/****** Object:  StoredProcedure [dbo].[Picklist_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Picklist_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Picklist_Update]
GO
/****** Object:  StoredProcedure [dbo].[Picklist_SoftDelete]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Picklist_SoftDelete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Picklist_SoftDelete]
GO
/****** Object:  StoredProcedure [dbo].[Picklist_GetBy_Type]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Picklist_GetBy_Type]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Picklist_GetBy_Type]
GO
/****** Object:  StoredProcedure [dbo].[Picklist_GetBy_Id]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Picklist_GetBy_Id]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Picklist_GetBy_Id]
GO
/****** Object:  StoredProcedure [dbo].[Picklist_GetAll]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Picklist_GetAll]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Picklist_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[Picklist_Delete]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Picklist_Delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Picklist_Delete]
GO
/****** Object:  StoredProcedure [dbo].[Picklist_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Picklist_Create]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Picklist_Create]
GO
/****** Object:  StoredProcedure [dbo].[Menu_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Menu_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Menu_Update]
GO
/****** Object:  StoredProcedure [dbo].[Menu_GetBy_Id]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Menu_GetBy_Id]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Menu_GetBy_Id]
GO
/****** Object:  StoredProcedure [dbo].[Menu_GetAll]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Menu_GetAll]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Menu_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[Menu_Delete]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Menu_Delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Menu_Delete]
GO
/****** Object:  StoredProcedure [dbo].[Menu_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Menu_Create]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Menu_Create]
GO
/****** Object:  StoredProcedure [dbo].[Menu_Clone]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Menu_Clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Menu_Clone]
GO
/****** Object:  StoredProcedure [dbo].[ListLayout_Set_Default]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ListLayout_Set_Default]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ListLayout_Set_Default]
GO
/****** Object:  StoredProcedure [dbo].[ListLayout_Delete]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ListLayout_Delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ListLayout_Delete]
GO
/****** Object:  StoredProcedure [dbo].[Layout_GetBy_Type]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Layout_GetBy_Type]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Layout_GetBy_Type]
GO
/****** Object:  StoredProcedure [dbo].[GetSubscriptionsByTenantId]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetSubscriptionsByTenantId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetSubscriptionsByTenantId]
GO
/****** Object:  StoredProcedure [dbo].[GetRootTenantLayouts]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetRootTenantLayouts]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetRootTenantLayouts]
GO
/****** Object:  StoredProcedure [dbo].[GetRootTenantCode]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetRootTenantCode]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetRootTenantCode]
GO
/****** Object:  StoredProcedure [dbo].[FunctionSecurity_GetAll_ByUserId]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FunctionSecurity_GetAll_ByUserId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[FunctionSecurity_GetAll_ByUserId]
GO
/****** Object:  StoredProcedure [dbo].[FunctionSecurity_GetAll_ById]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FunctionSecurity_GetAll_ById]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[FunctionSecurity_GetAll_ById]
GO
/****** Object:  StoredProcedure [dbo].[EntitySecurity_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EntitySecurity_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[EntitySecurity_Update]
GO
/****** Object:  StoredProcedure [dbo].[EntitySecurity_GetAll_ByUserId]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EntitySecurity_GetAll_ByUserId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[EntitySecurity_GetAll_ByUserId]
GO
/****** Object:  StoredProcedure [dbo].[EntitySecurity_GetAll_ById]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EntitySecurity_GetAll_ById]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[EntitySecurity_GetAll_ById]
GO
/****** Object:  StoredProcedure [dbo].[EntitySecurity_Get]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EntitySecurity_Get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[EntitySecurity_Get]
GO
/****** Object:  StoredProcedure [dbo].[EntitySecurity_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EntitySecurity_Create]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[EntitySecurity_Create]
GO
/****** Object:  StoredProcedure [dbo].[EntityLayouts_Clone]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EntityLayouts_Clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[EntityLayouts_Clone]
GO
/****** Object:  StoredProcedure [dbo].[EntityLayout_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EntityLayout_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[EntityLayout_Update]
GO
/****** Object:  StoredProcedure [dbo].[EntityLayout_GetBy_Type]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EntityLayout_GetBy_Type]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[EntityLayout_GetBy_Type]
GO
/****** Object:  StoredProcedure [dbo].[EntityLayout_GetBy_PicklistId]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EntityLayout_GetBy_PicklistId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[EntityLayout_GetBy_PicklistId]
GO
/****** Object:  StoredProcedure [dbo].[EntityLayout_GetBy_Id]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EntityLayout_GetBy_Id]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[EntityLayout_GetBy_Id]
GO
/****** Object:  StoredProcedure [dbo].[EntityLayout_GetBy_EntityId]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EntityLayout_GetBy_EntityId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[EntityLayout_GetBy_EntityId]
GO
/****** Object:  StoredProcedure [dbo].[EntityLayout_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EntityLayout_Create]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[EntityLayout_Create]
GO
/****** Object:  StoredProcedure [dbo].[CredentialHistory_Get]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CredentialHistory_Get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CredentialHistory_Get]
GO
/****** Object:  StoredProcedure [dbo].[Credential_Validate_UserName]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Credential_Validate_UserName]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Credential_Validate_UserName]
GO
/****** Object:  StoredProcedure [dbo].[Credential_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Credential_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Credential_Update]
GO
/****** Object:  StoredProcedure [dbo].[Credential_SetIsNew]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Credential_SetIsNew]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Credential_SetIsNew]
GO
/****** Object:  StoredProcedure [dbo].[Credential_Get_Password]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Credential_Get_Password]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Credential_Get_Password]
GO
/****** Object:  StoredProcedure [dbo].[Credential_Get]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Credential_Get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Credential_Get]
GO
/****** Object:  StoredProcedure [dbo].[Credential_Delete]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Credential_Delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Credential_Delete]
GO
/****** Object:  StoredProcedure [dbo].[Credential_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Credential_Create]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Credential_Create]
GO
/****** Object:  StoredProcedure [dbo].[Counter_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Counter_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Counter_Update]
GO
/****** Object:  StoredProcedure [dbo].[Counter_GetById]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Counter_GetById]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Counter_GetById]
GO
/****** Object:  StoredProcedure [dbo].[Counter_GetAll]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Counter_GetAll]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Counter_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[Counter_Delete]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Counter_Delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Counter_Delete]
GO
/****** Object:  StoredProcedure [dbo].[Counter_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Counter_Create]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Counter_Create]
GO
/****** Object:  StoredProcedure [dbo].[Copy_TenantResources]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Copy_TenantResources]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Copy_TenantResources]
GO
/****** Object:  StoredProcedure [dbo].[BatchType_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BatchType_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BatchType_Update]
GO
/****** Object:  StoredProcedure [dbo].[BatchType_Status]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BatchType_Status]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BatchType_Status]
GO
/****** Object:  StoredProcedure [dbo].[BatchType_GetById]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BatchType_GetById]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BatchType_GetById]
GO
/****** Object:  StoredProcedure [dbo].[BatchType_GetAllEnabled]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BatchType_GetAllEnabled]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BatchType_GetAllEnabled]
GO
/****** Object:  StoredProcedure [dbo].[BatchType_GetAll]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BatchType_GetAll]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BatchType_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[BatchType_Delete]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BatchType_Delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BatchType_Delete]
GO
/****** Object:  StoredProcedure [dbo].[BatchType_Create_Xml]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BatchType_Create_Xml]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BatchType_Create_Xml]
GO
/****** Object:  StoredProcedure [dbo].[BatchType_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BatchType_Create]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BatchType_Create]
GO
/****** Object:  StoredProcedure [dbo].[20002_Picklist_Intialisation]    Script Date: 15-Jul-19 15:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[20002_Picklist_Intialisation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[20002_Picklist_Intialisation]
GO
/****** Object:  StoredProcedure [dbo].[20002_Picklist_Intialisation]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[20002_Picklist_Intialisation]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[20002_Picklist_Intialisation] AS' 
END
GO
ALTER PROCEDURE [dbo].[20002_Picklist_Intialisation]
(
    @guidRootTenantId UNIQUEIDENTIFIER,
    @guidInitTenantId UNIQUEIDENTIFIER,
    @guidUserId UNIQUEIDENTIFIER
)
AS
SET NOCOUNT ON;
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;


--DECLARE @viewTenant UNIQUEIDENTIFIER = '1C083115-7DB3-4B92-A449-D57FD1D2A52A';
--DECLARE @novelTenant UNIQUEIDENTIFIER = 'E15516DF-CFAB-4597-97D2-0BE5F8E16734';
--DECLARE @WORKFLOW9 UNIQUEIDENTIFIER = 'fd968a85-822b-4d5d-abed-6958b2f07526';--'8531C109-000B-4493-8F02-061138FC9762';

--DECLARE @guidRootTenantId UNIQUEIDENTIFIER = @novelTenant;
--DECLARE @guidInitTenantId UNIQUEIDENTIFIER = '0F87897B-6406-44C0-BA9E-3E58A5C19B1D';

DECLARE @picklistId SMALLINT = '20002';
DECLARE @currencyContext SMALLINT = '20001';
DECLARE @languageContext SMALLINT = '20003';
DECLARE @timezone SMALLINT = '20004';



DECLARE @tmpCountryRelatedValue TABLE
(
    uniqueId UNIQUEIDENTIFIER,
    countryId UNIQUEIDENTIFIER,
    countryContext SMALLINT,
    countryKey NVARCHAR(25),
    countryText NVARCHAR(100),
    countryActive BIT,
    countrydeleted BIT,
    countryFlagged BIT,
    countryIsoCode NVARCHAR(25),
    countryNationality NVARCHAR(25),
    currencyUniqueId UNIQUEIDENTIFIER,
    currencyId SMALLINT,
    currencyKey NVARCHAR(100),
    currecnyText NVARCHAR(100),
    languageUniqueId UNIQUEIDENTIFIER,
    languageId SMALLINT,
    languageKey NVARCHAR(100),
    languageText NVARCHAR(100),
    timezoneUniqueId UNIQUEIDENTIFIER,
    timeZoneId SMALLINT,
    timeZoneKey NVARCHAR(100),
    timeZoneText NVARCHAR(100)
);

INSERT INTO @tmpCountryRelatedValue
(
    uniqueId,
    countryId,
    countryContext,
    countryKey,
    countryText,
    countryActive,
    countrydeleted,
    countryFlagged,
    countryIsoCode,
    countryNationality,
    currencyUniqueId,
    currencyId,
    currencyKey,
    currecnyText,
    languageUniqueId,
    languageId,
    languageKey,
    languageText,
    timezoneUniqueId,
    timeZoneId,
    timeZoneKey,
    timeZoneText
)
SELECT NEWID(),
       pv.Id,
       pv.PickListId,
       pv.[Key],
       pv.[Text],
       pv.[Active],
       pv.[IsDeletetd],
       pv.[Flagged],
       pvc.IsoCode,
       pvc.Nationality,
       NEWID(),
       cur.PickListId AS currency,
       cur.[Key],
       cur.[Text],
       NEWID(),
       lan.PickListId AS language,
       lan.[Key],
       lan.[Text],
       NEWID(),
       tim.PickListId AS timezone,
       tim.[Key],
       tim.[Text]
FROM dbo.PickListValue AS pv
    LEFT JOIN dbo.PickListValueForCountry pvc
        ON pvc.PickListValueId = pv.Id
    LEFT JOIN dbo.PickListValue AS cur
        ON cur.Id = pvc.Currency
           AND cur.PickListId = @currencyContext
    LEFT JOIN dbo.PickListValue AS lan
        ON lan.Id = pvc.Language
           AND lan.PickListId = @languageContext
    LEFT JOIN dbo.PickListValue AS tim
        ON tim.Id = pvc.Timezone
           AND tim.PickListId = @timezone
WHERE pv.PickListId = @picklistId
      AND pv.TenantId = @guidRootTenantId
      AND NOT EXISTS
(
    SELECT Id
    FROM dbo.PickListValue AS addPicklist
    WHERE addPicklist.TenantId = @guidInitTenantId
          AND addPicklist.PickListId = @picklistId
          AND addPicklist.[Key] = pv.[Key]
          AND addPicklist.[Text] = pv.[Text]
);

DECLARE @ErrorMessage NVARCHAR(4000),
        @ErrorNumber INT,
        @ErrorSeverity INT,
        @ErrorState INT,
        @ErrorLine INT,
        @ErrorProcedure NVARCHAR(200);
BEGIN TRY
    BEGIN TRAN;
    INSERT INTO [dbo].[PickListValue]
    (
        [TenantId],
        [Id],
        [PickListId],
        [Key],
        [Text],
        [Active],
        [IsDeletetd],
        [Flagged],
        [UpdatedBy],
        [UpdatedDate]
    )
    SELECT @guidInitTenantId,
           uniqueId,
           countryContext,
           countryKey,
           countryText,
           countryActive,
           countrydeleted,
           countryFlagged,
           @guidUserId,
           GETUTCDATE()
    FROM @tmpCountryRelatedValue;

    -----INSERT CURRENCY
    INSERT INTO [dbo].[PickListValue]
    (
        [TenantId],
        [Id],
        [PickListId],
        [Key],
        [Text],
        [Active],
        [IsDeletetd],
        [Flagged],
        [UpdatedBy],
        [UpdatedDate]
    )
    SELECT @guidInitTenantId,
           currencyUniqueId,
           currencyId,
           currencyKey,
           currecnyText,
           1,
           0,
           1,
           @guidUserId, --it should be dynamic
           GETUTCDATE()
    FROM @tmpCountryRelatedValue
    WHERE currencyId IS NOT NULL;



    -----INSERT TIMEZONE
    INSERT INTO [dbo].[PickListValue]
    (
        [TenantId],
        [Id],
        [PickListId],
        [Key],
        [Text],
        [Active],
        [IsDeletetd],
        [Flagged],
        [UpdatedBy],
        [UpdatedDate]
    )
    SELECT @guidInitTenantId,
           timezoneUniqueId,
           timeZoneId,
           timeZoneKey,
           timeZoneText,
           1,
           0,
           1,
           @guidUserId, --it should be dynamic
           GETUTCDATE()
    FROM @tmpCountryRelatedValue
    WHERE timeZoneId IS NOT NULL;


    -----INSERT language
    INSERT INTO [dbo].[PickListValue]
    (
        [TenantId],
        [Id],
        [PickListId],
        [Key],
        [Text],
        [Active],
        [IsDeletetd],
        [Flagged],
        [UpdatedBy],
        [UpdatedDate]
    )
    SELECT @guidInitTenantId,
           languageUniqueId,
           languageId,
           languageKey,
           languageKey,
           1,
           0,
           1,
           @guidUserId, --it should be dynamic
           GETUTCDATE()
    FROM @tmpCountryRelatedValue
    WHERE languageId IS NOT NULL;


    -----add relations...

    INSERT INTO [dbo].[PickListValueForCountry]
    (
        [TenantId],
        [Id],
        [PickListValueId],
        [Currency],
        [Language],
        [Timezone],
        [IsoCode],
        [Nationality]
    )
    SELECT @guidInitTenantId,
           NEWID(),
           uniqueId,
           currencyUniqueId,
           languageUniqueId,
           timezoneUniqueId,
           countryIsoCode,
           countryNationality
    FROM @tmpCountryRelatedValue;
    COMMIT TRAN;
END TRY
BEGIN CATCH
    SELECT @ErrorMessage = ERROR_MESSAGE(),
           @ErrorNumber = ERROR_NUMBER(),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE(),
           @ErrorLine = ERROR_LINE(),
           @ErrorProcedure = ISNULL(ERROR_PROCEDURE(), '-');

    ROLLBACK TRAN;
    RAISERROR(@ErrorMessage, @ErrorSeverity, 1, @ErrorNumber, @ErrorSeverity, @ErrorState, @ErrorProcedure, @ErrorLine);
END CATCH;


GO
/****** Object:  StoredProcedure [dbo].[BatchType_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BatchType_Create]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[BatchType_Create] AS' 
END
GO


ALTER PROCEDURE [dbo].[BatchType_Create]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidBatchTypeId UNIQUEIDENTIFIER,
    @strContext [dbo].[xSmallText],
    @intPriority INT = NULL,
    @intIdleTime INT = NULL,
    @bStatus BIT
)
AS
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    IF EXISTS
    (
        SELECT *
        FROM [dbo].[BatchTypes]
        WHERE [TenantId] = @guidTenantId
              AND [Context] = @strContext
    )
    BEGIN

        RETURN 2;
    END;

    INSERT INTO [dbo].[BatchTypes]
    (
        [TenantId],
        [Id],
        [Context],
        [Status],
        [Priority],
        [IdleTime]
    )
    VALUES
    (
		@guidTenantId, @guidBatchTypeId, @strContext, @bStatus, @intPriority, @intIdleTime
	);

    RETURN 0;
END;

GO
/****** Object:  StoredProcedure [dbo].[BatchType_Create_Xml]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BatchType_Create_Xml]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[BatchType_Create_Xml] AS' 
END
GO

ALTER PROCEDURE [dbo].[BatchType_Create_Xml]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @xmlBatchTypes XML
)
AS
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    DECLARE @DATA TABLE
    (
        idx INT IDENTITY(1, 1),
        BatchTypeId1 UNIQUEIDENTIFIER NOT NULL,
        Context1 [dbo].[mediumText] NULL,
        Priority1 INT NULL,
        IdleTime1 INT NULL,
		Status1 BIT
    );

    INSERT INTO @DATA
    SELECT ref.value('./@BatchTypeId', 'uniqueidentifier') AS BatchTypeId,
           ref.value('./@Context', '[dbo].[mediumText]') AS Context,
           ref.value('./@Priority', 'int') AS Priority1,
           ref.value('./@IdleTime', 'int') AS IdleTime,
		   ref.value('./@bStatus', 'BIT') AS bStatus
    FROM @xmlBatchTypes.nodes('/BatchTypes/BatchType') AS T(ref);

    INSERT INTO [dbo].[BatchTypes]
    (
        [TenantId],
        [Id],
        [Context],
        [Priority],
        [IdleTime],
        [Status]
    )
    SELECT @guidTenantId,
           DT.BatchTypeId1,
           DT.Context1,
           DT.Priority1,
           DT.IdleTime1,
           DT.Status1
    FROM @DATA DT
       


    IF @@ERROR <> 0
    BEGIN
        RETURN 1;
    END;
    RETURN 0;
END;

GO
/****** Object:  StoredProcedure [dbo].[BatchType_Delete]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BatchType_Delete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[BatchType_Delete] AS' 
END
GO


ALTER PROCEDURE [dbo].[BatchType_Delete]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidBatchTypeId UNIQUEIDENTIFIER
)
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM [dbo].[BatchTypes]
    WHERE [TenantId] = @guidTenantId
          AND Id = @guidBatchTypeId;

    RETURN 0;
END;

GO
/****** Object:  StoredProcedure [dbo].[BatchType_GetAll]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BatchType_GetAll]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[BatchType_GetAll] AS' 
END
GO


ALTER PROCEDURE [dbo].[BatchType_GetAll] 
(
	@guidTenantId UNIQUEIDENTIFIER
)
AS
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT [Id],
           [Context],
           [Priority],
           [IdleTime],
           [Status]
    FROM [dbo].[BatchTypes]
    WHERE [TenantId] = @guidTenantId;

    RETURN 0;
END;

GO
/****** Object:  StoredProcedure [dbo].[BatchType_GetAllEnabled]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BatchType_GetAllEnabled]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[BatchType_GetAllEnabled] AS' 
END
GO


ALTER PROCEDURE [dbo].[BatchType_GetAllEnabled]
AS
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT [TenantId],
           [Id],
           [Context],
           [Priority],
           [IdleTime],
           [Status]
    FROM [dbo].[BatchTypes]
    WHERE Status = 1  
--and (
----TenantId='1C083115-7DB3-4B92-A449-D57FD1D2A52A' or 
--TenantId='E15516DF-CFAB-4597-97D2-0BE5F8E16734' --or 
----TenantId='85818312-A31C-413B-AB37-2A4E815E2272'
--)

    RETURN 0;
END;

GO
/****** Object:  StoredProcedure [dbo].[BatchType_GetById]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BatchType_GetById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[BatchType_GetById] AS' 
END
GO


ALTER PROCEDURE [dbo].[BatchType_GetById]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidBatchTypeId UNIQUEIDENTIFIER
)
AS
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT [Id],
           [Context],
           [Priority],
           [IdleTime],
           [Status]
    FROM [dbo].[BatchTypes]
    WHERE [TenantId] = @guidTenantId
          AND [Id] = @guidBatchTypeId;

    RETURN 0;
END;

GO
/****** Object:  StoredProcedure [dbo].[BatchType_Status]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BatchType_Status]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[BatchType_Status] AS' 
END
GO


ALTER PROCEDURE [dbo].[BatchType_Status]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidBatchTypeId UNIQUEIDENTIFIER
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[BatchTypes]
    SET [Status] = (CASE
                        WHEN [Status] = 1 THEN
                            0
                        ELSE
                            1
                    END
                   )
    WHERE [TenantId] = @guidTenantId
          AND Id = @guidBatchTypeId;

    RETURN 0;
END;

GO
/****** Object:  StoredProcedure [dbo].[BatchType_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BatchType_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[BatchType_Update] AS' 
END
GO


ALTER PROCEDURE [dbo].[BatchType_Update]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidBatchTypeId UNIQUEIDENTIFIER,
    @strContext [dbo].[xSmallText],
    @intPriority AS INT = NULL,
    @intIdleTime AS INT = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[BatchTypes]
    SET [Priority] = @intPriority,
        [IdleTime] = @intIdleTime
    WHERE [TenantId] = @guidTenantId
          AND [Id] = @guidBatchTypeId
          AND [Context] = @strContext;

    RETURN 0;
END;

GO
/****** Object:  StoredProcedure [dbo].[Copy_TenantResources]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Copy_TenantResources]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Copy_TenantResources] AS' 
END
GO

ALTER PROCEDURE [dbo].[Copy_TenantResources]
(
    @guidRootTenantId UNIQUEIDENTIFIER,
    @guidToTenantId UNIQUEIDENTIFIER
)
AS
BEGIN

    SET NOCOUNT ON;

    INSERT INTO [dbo].[Resource]
    (
        TenantId,
        [Key],
        [Value],
        [Language]
    )
    SELECT @guidToTenantId,
           [Key],
           [Value],
           [Language]
    FROM [dbo].[Resource]
    WHERE TenantId = @guidRootTenantId;

END;

GO
/****** Object:  StoredProcedure [dbo].[Counter_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Counter_Create]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Counter_Create] AS' 
END
GO

ALTER PROCEDURE [dbo].[Counter_Create]
    @guidTenantId UNIQUEIDENTIFIER,
    @guidCounterId UNIQUEIDENTIFIER,
    @strText [dbo].[mediumText],
    @strDescription [dbo].[mediumText],
    @intCounterN INT,
    @intCounterO INT,
    @intCounterP INT,
    @intResetCounterN INT,
    @intResetCounterO INT,
    @intResetCounterP INT,
    @dateUpdatedOn DATETIME,
    @guidUpdatedBy UNIQUEIDENTIFIER,
    @strEntityId xSmallText
AS
BEGIN
    INSERT INTO [dbo].[Counter]
    (
        TenantId,
        Id,
        Description,
        Text,
        CounterN,
        CounterO,
        CounterP,
        ResetCounterN,
        ResetCounterO,
        ResetCounterP,
        UpdatedOn,
        UpdatedBy,
        EntityId
    )
    VALUES
    (@guidTenantId,
     @guidCounterId,
     @strDescription,
     @strText,
     @intCounterN,
     @intCounterO,
     @intCounterP,
     @intResetCounterN,
     @intResetCounterO,
     @intResetCounterP,
     @dateUpdatedOn,
     @guidUpdatedBy,
     @strEntityId
    );
END;
GO
/****** Object:  StoredProcedure [dbo].[Counter_Delete]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Counter_Delete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Counter_Delete] AS' 
END
GO

ALTER PROCEDURE [dbo].[Counter_Delete]
    @guidTenantId UNIQUEIDENTIFIER,
    @guidCounterId UNIQUEIDENTIFIER
AS
BEGIN

    DELETE FROM [dbo].[Counter]
    WHERE Id = @guidCounterId
          AND TenantId = @guidTenantId;
END;
GO
/****** Object:  StoredProcedure [dbo].[Counter_GetAll]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Counter_GetAll]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Counter_GetAll] AS' 
END
GO

ALTER PROCEDURE [dbo].[Counter_GetAll]
    @guidTenantId UNIQUEIDENTIFIER,
    @guidEntityId xSmallText
AS
BEGIN

    SELECT Id,
           Text,
           Description,
           CounterN,
           CounterO,
           CounterP,
           ResetCounterN,
           ResetCounterO,
           ResetCounterP,
           UpdatedOn,
           UpdatedBy,
           EntityId
    FROM [dbo].[Counter] c
    WHERE TenantId = @guidTenantId
          AND EntityId = @guidEntityId;

END;
GO
/****** Object:  StoredProcedure [dbo].[Counter_GetById]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Counter_GetById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Counter_GetById] AS' 
END
GO
ALTER PROCEDURE [dbo].[Counter_GetById]
    @guidTenantId UNIQUEIDENTIFIER,
    @guidCounterId UNIQUEIDENTIFIER
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT Id,
           Text,
           Description,
           CounterN,
           CounterO,
           CounterP,
           ResetCounterN,
           ResetCounterO,
           ResetCounterP,
           UpdatedOn,
           UpdatedBy,
           EntityId
    FROM [dbo].[Counter]
    WHERE Id = @guidCounterId
          AND TenantId = @guidTenantId;
END;
GO
/****** Object:  StoredProcedure [dbo].[Counter_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Counter_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Counter_Update] AS' 
END
GO

ALTER PROCEDURE [dbo].[Counter_Update]
    @guidTenantId UNIQUEIDENTIFIER,
    @guidCounterId UNIQUEIDENTIFIER,
    @strText [dbo].[mediumText],
    @strDescription [dbo].[mediumText],
    @intCounterN INT,
    @intCounterO INT,
    @intCounterP INT,
    @intResetCounterN INT,
    @intResetCounterO INT,
    @intResetCounterP INT,
    @dateUpdatedOn DATETIME,
    @guidUpdatedBy UNIQUEIDENTIFIER,
    @strEntityId xSmallText
AS
BEGIN

    UPDATE [dbo].[Counter]
    SET Text = @strText,
        Description = @strDescription,
        CounterN = @intCounterN,
        CounterO = @intCounterO,
        CounterP = @intCounterP,
        ResetCounterN = @intResetCounterN,
        ResetCounterO = @intResetCounterO,
        ResetCounterP = @intResetCounterP,
        UpdatedOn = @dateUpdatedOn,
        UpdatedBy = @guidUpdatedBy,
        EntityId = @strEntityId
    WHERE Id = @guidCounterId
          AND TenantId = @guidTenantId;
END;
GO
/****** Object:  StoredProcedure [dbo].[Credential_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Credential_Create]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Credential_Create] AS' 
END
GO

ALTER PROCEDURE [dbo].[Credential_Create]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidCredentialId UNIQUEIDENTIFIER,
    @guidParentId UNIQUEIDENTIFIER,
    @strUserName [dbo].[mediumText],
    @strPasswordHash [dbo].[mediumText],
    @strPasswordSalt [dbo].[mediumText],
    @bitIsNew BIT,
    @currentdate DATETIME
)
AS
BEGIN
    SET NOCOUNT ON;

	DECLARE	@strErrorMessage	NVARCHAR(MAX),
			@intErrorSeverity	INT,
			@intErrorState		INT

	BEGIN TRY
		BEGIN TRAN

		INSERT INTO [dbo].[Credential]
		(
			[TenantId],
			[Id],
			[UserName],
			[PasswordHash],
			[PasswordSalt],
			[IsNew],
			PasswordChangedOn
		)
		VALUES
		(
			@guidTenantId, @guidCredentialId, @strUserName, @strPasswordHash, @strPasswordSalt, @bitIsNew, @currentdate
		);

		INSERT INTO [dbo].[CredentialHistory]
		(
			TenantId,
			Id,
			RefId,
			UserName,
			PasswordHash,
			PasswordSalt,
			CreatedDate
		)
		VALUES
		(
			@guidTenantId, NEWID(), @guidParentId, @strUserName, @strPasswordHash, @strPasswordSalt, @currentdate
		);

		UPDATE	[dbo].[User]
		SET		UserCredentialId = @guidCredentialId
		WHERE	Id = @guidParentId;

		COMMIT TRAN
	END TRY
	BEGIN CATCH
		SELECT	@strErrorMessage = ERROR_MESSAGE(),
				@intErrorSeverity = ERROR_SEVERITY(),
				@intErrorState = ERROR_STATE()

		ROLLBACK TRAN

		RAISERROR(@strErrorMessage, @intErrorSeverity, @intErrorState);

	END CATCH

    RETURN 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[Credential_Delete]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Credential_Delete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Credential_Delete] AS' 
END
GO


ALTER PROCEDURE [dbo].[Credential_Delete]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidCredentialId UNIQUEIDENTIFIER,
    @guidParentId UNIQUEIDENTIFIER
)
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM [dbo].[Credential]
    WHERE [TenantId] = @guidTenantId
          AND [Id] = @guidCredentialId;

    RETURN 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[Credential_Get]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Credential_Get]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Credential_Get] AS' 
END
GO
ALTER PROCEDURE [dbo].[Credential_Get]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidRefId UNIQUEIDENTIFIER
)
AS
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT CRD.[Id],
           UR.[Id],
           [UserName],
           [IsNew],
           InvalidAttemptCount,
           IsLocked,
           LockedOn,
           PasswordChangedOn
    FROM dbo.[User] UR
        INNER JOIN [dbo].[Credential] CRD
            ON CRD.Id = UR.UserCredentialId
    WHERE UR.[TenantId] = @guidTenantId
          AND UR.[Id] = @guidRefId;
    RETURN 0;
END;


GO
/****** Object:  StoredProcedure [dbo].[Credential_Get_Password]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Credential_Get_Password]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Credential_Get_Password] AS' 
END
GO


ALTER PROCEDURE [dbo].[Credential_Get_Password]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @strUserName [dbo].[mediumText]
)
AS
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT [PasswordHash],
           [PasswordSalt]
    FROM [dbo].[Credential]
    WHERE [TenantId] = @guidTenantId
          AND [UserName] = @strUserName;
    RETURN 0;
END;

GO
/****** Object:  StoredProcedure [dbo].[Credential_SetIsNew]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Credential_SetIsNew]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Credential_SetIsNew] AS' 
END
GO
  
    
ALTER PROCEDURE [dbo].[Credential_SetIsNew]    
(    
    @guidTenantId UNIQUEIDENTIFIER,    
    @guidCredentialId UNIQUEIDENTIFIER,    
    @guidParentId UNIQUEIDENTIFIER,    
    @bitIsNew BIT,   
    @datecurrentdate DATETIME     
)    
AS    
BEGIN    
    SET NOCOUNT ON;    
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;    
      
    
    
 if @bitIsNew=0    
 begin    
        UPDATE [dbo].[Credential]    
    SET [IsNew] = @bitIsNew,  
 PasswordChangedOn = @datecurrentdate        
    WHERE [TenantId] = @guidTenantId    
          AND [Id] = @guidCredentialId;   
  INSERT INTO CredentialHistory      
  (      
  TenantId,      
  Id,      
  RefId,      
  UserName,      
  PasswordHash,      
  PasswordSalt,      
  CreatedDate      
  )     
  select @guidTenantId,NEWID(), @guidParentId, UserName, PasswordHash, PasswordSalt, @datecurrentdate      
  from [dbo].[Credential] where id= @guidCredentialId   
       
 end 
 else
 begin
  UPDATE [dbo].[Credential]    
    SET [IsNew] = @bitIsNew     
    WHERE [TenantId] = @guidTenantId    
          AND [Id] = @guidCredentialId;  

 end   
    RETURN 0;    
END
GO
/****** Object:  StoredProcedure [dbo].[Credential_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Credential_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Credential_Update] AS' 
END
GO
    
ALTER PROCEDURE [dbo].[Credential_Update]    
(    
    @guidTenantId UNIQUEIDENTIFIER,    
    @guidCredentialId UNIQUEIDENTIFIER,    
    @guidParentId UNIQUEIDENTIFIER,    
    @strPasswordHash [dbo].[mediumText],    
    @strPasswordSalt [dbo].[mediumText],    
    @bitIsNew BIT,    
    @currentdate DATETIME    
)    
AS    
BEGIN    
    SET NOCOUNT ON;    
    
 DECLARE @strErrorMessage NVARCHAR(MAX),    
   @intErrorSeverity INT,    
   @intErrorState  INT,    
   @username   [dbo].[mediumText]    
        
 BEGIN TRY    
  BEGIN TRAN       
   
  if @bitIsNew=0  
  begin  
    UPDATE [dbo].[Credential]    
  SET [PasswordHash] = @strPasswordHash,    
   [PasswordSalt] = @strPasswordSalt,    
   [IsNew] = @bitIsNew,    
   PasswordChangedOn = @currentdate    
  WHERE [TenantId] = @guidTenantId    
     AND [Id] = @guidCredentialId; 
  SET @username =    
  (    
   SELECT UserName    
   FROM [dbo].[Credential]    
   WHERE [TenantId] = @guidTenantId    
      AND [Id] = @guidCredentialId    
  );    
    
  INSERT INTO CredentialHistory    
  (    
   TenantId,    
   Id,    
   RefId,    
   UserName,    
   PasswordHash,    
   PasswordSalt,    
   CreatedDate    
  )    
  VALUES    
  (    
   @guidTenantId, NEWID(), @guidParentId, @username, @strPasswordHash, @strPasswordSalt, @currentdate    
  );    
  end
  else
  begin
   UPDATE [dbo].[Credential]   
   SET [PasswordHash] = @strPasswordHash,  
   [PasswordSalt] = @strPasswordSalt,   
   [IsNew] = @bitIsNew
  WHERE [TenantId] = @guidTenantId    
     AND [Id] = @guidCredentialId;
  end  
    
  COMMIT TRAN    
 END TRY    
 BEGIN CATCH    
  SELECT @strErrorMessage = ERROR_MESSAGE(),    
    @intErrorSeverity = ERROR_SEVERITY(),    
    @intErrorState = ERROR_STATE()    
    
  ROLLBACK TRAN    
    
  RAISERROR(@strErrorMessage, @intErrorSeverity, @intErrorState);    
 END CATCH    
    
    RETURN 0;    
END;
GO
/****** Object:  StoredProcedure [dbo].[Credential_Validate_UserName]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Credential_Validate_UserName]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Credential_Validate_UserName] AS' 
END
GO


ALTER PROCEDURE [dbo].[Credential_Validate_UserName]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @strUserName [dbo].[mediumText]
)
AS
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT UR.[Id]
    FROM dbo.[User] UR
        INNER JOIN [dbo].[Credential] CRD
            ON CRD.Id = UR.UserCredentialId
    WHERE UR.[TenantId] = @guidTenantId
          AND CRD.[UserName] = @strUserName;
    RETURN 0;
END;



GO
/****** Object:  StoredProcedure [dbo].[CredentialHistory_Get]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CredentialHistory_Get]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[CredentialHistory_Get] AS' 
END
GO
ALTER PROCEDURE [dbo].[CredentialHistory_Get]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidRefId UNIQUEIDENTIFIER,
    @intCount INT
)
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT TOP (@intCount)
        [Id],
        [RefId],
        [UserName],
        [PasswordHash],
        [PasswordSalt],
        [CreatedDate]
    FROM CredentialHistory
    WHERE [TenantId] = @guidTenantId
          AND [RefId] = @guidRefId
    ORDER BY CreatedDate DESC;

END;
GO
/****** Object:  StoredProcedure [dbo].[EntityLayout_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EntityLayout_Create]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[EntityLayout_Create] AS' 
END
GO
ALTER PROCEDURE [dbo].[EntityLayout_Create]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidId UNIQUEIDENTIFIER,
    @strEntityId xSmallText,
    @strName mediumText,
    @intType INT,
    @strSubType smallText = NULL,
    @intLayoutContext INT = NULL,
    @guidUpdatedBy UNIQUEIDENTIFIER,
	@defaultLayout BIT = 0,
	@layoutStr xLargeText = null
)
AS
BEGIN
    DECLARE @ErrorMessage NVARCHAR(4000),
            @ErrorNumber INT,
            @ErrorSeverity INT,
            @ErrorState INT,
            @ErrorLine INT,
            @ErrorProcedure NVARCHAR(200);

   

    BEGIN TRY
        BEGIN TRAN;

		IF @layoutStr IS NULL
		BEGIN
			IF @intType = 1
			BEGIN
				SET @layoutStr
					= '{"fields":[{"name":"InternalId","sequence":1,"hidden":true,"dataType":"Guid","refId":null,"defaultValue":null,"properties":null,"values":null,"clickable":false}],"defaultSortOrder":{"name":"","value":""},"actions":[]}';
			END;

			IF @intType = 3
			BEGIN
				SET @layoutStr
					= '{"fields":[{"name":"InternalId","sequence":1,"hidden":true,"dataType":"Guid","refId":null,"defaultValue":null,"properties":null,"values":null,"clickable":false,"defaultView":null},{"name":"SubType","sequence":2,"hidden":false,"dataType":"Text","refId":null,"defaultValue":null,"properties":null,"values":null,"clickable":false,"defaultView":null}],"defaultSortOrder":{"name":"","value":""},"defaultGroupBy":"","maxResult":0,"searchProperties":[{"name":"FreeTextSearch","properties":[]},{"name":"SimpleSearch","properties":[]},{"name":"AdvanceSearch","properties":[]}],"actions":[],"toolbar":[]}';
			END;


		END

        


        INSERT INTO [dbo].[EntityLayout]
        (
            [TenantId],
            [Id],
            [EntityId],
            [Name],
            [Type],
            [SubType],
            [LayoutContext],
            [Layout],
            [UpdatedOn],
            [UpdatedBy],
            [Default]
        )
        VALUES
        (@guidTenantId,
         @guidId,
         @strEntityId,
         @strName,
         @intType,
         @strSubType,
         @intLayoutContext,
         @layoutStr,
         GETUTCDATE(),
         @guidUpdatedBy,
         @defaultLayout
        );

        IF NOT EXISTS
        (
            SELECT *
            FROM [dbo].[EntityLayout]
            WHERE [EntityId] = @strEntityId
                  AND [Type] = @intType
                  AND ISNULL([SubType], 0) = ISNULL(@strSubType, 0)
                  AND ISNULL([LayoutContext], 0) = ISNULL(@intLayoutContext, 0)
                  AND [Default] = 1
				  AND TenantId = @guidTenantId
        )
        BEGIN
            UPDATE [dbo].[EntityLayout]
            SET [Default] = 1
            WHERE [TenantId] = @guidTenantId
                  AND [Id] = @guidId;
        END;

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        SELECT @ErrorMessage = ERROR_MESSAGE(),
               @ErrorNumber = ERROR_NUMBER(),
               @ErrorSeverity = ERROR_SEVERITY(),
               @ErrorState = ERROR_STATE(),
               @ErrorLine = ERROR_LINE(),
               @ErrorProcedure = ISNULL(ERROR_PROCEDURE(), '-');

        ROLLBACK TRAN;
        RAISERROR(
                     @ErrorMessage,
                     @ErrorSeverity,
                     1,
                     @ErrorNumber,
                     @ErrorSeverity,
                     @ErrorState,
                     @ErrorProcedure,
                     @ErrorLine
                 );
    END CATCH;

END;

GO
/****** Object:  StoredProcedure [dbo].[EntityLayout_GetBy_EntityId]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EntityLayout_GetBy_EntityId]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[EntityLayout_GetBy_EntityId] AS' 
END
GO



ALTER PROCEDURE [dbo].[EntityLayout_GetBy_EntityId]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @strEntityId xSmallText
)
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT [Id],
           [EntityId],
           [Name],
           [Type],
           [SubType],
           [LayoutContext],
           [UpdatedOn],
           --,IT2.IT_Name ModifiedBy  
           '',
           [Layout],
           [Default]
    FROM [dbo].[EntityLayout] EN
    --INNER JOIN IT_Item IT1 ON IT1.IT_Guid = EN.CreatedBy  
    --LEFT JOIN IT_Item IT2 ON IT2.IT_Guid = EN.ModifiedBy  
    WHERE TenantId = @guidTenantId
          AND EntityId = @strEntityId
    ORDER BY [Name] DESC;
END;

GO
/****** Object:  StoredProcedure [dbo].[EntityLayout_GetBy_Id]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EntityLayout_GetBy_Id]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[EntityLayout_GetBy_Id] AS' 
END
GO
ALTER PROCEDURE [dbo].[EntityLayout_GetBy_Id]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidId UNIQUEIDENTIFIER
)
AS
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT [Id],
           [EntityId],
           [Name],
           [Type],
           [SubType],
           [LayoutContext],
           [Layout],
           [UpdatedOn],
           [Default]
    FROM [dbo].[EntityLayout]
    WHERE [TenantId] = @guidTenantId
          AND [Id] = @guidId;
END;

GO
/****** Object:  StoredProcedure [dbo].[EntityLayout_GetBy_PicklistId]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EntityLayout_GetBy_PicklistId]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[EntityLayout_GetBy_PicklistId] AS' 
END
GO

--  ▄████  ▄▄▄       █    ██  ██▀███   ▄▄▄       ▄▄▄▄   
-- ██▒ ▀█▒▒████▄     ██  ▓██▒▓██ ▒ ██▒▒████▄    ▓█████▄ 
--▒██░▄▄▄░▒██  ▀█▄  ▓██  ▒██░▓██ ░▄█ ▒▒██  ▀█▄  ▒██▒ ▄██
--░▓█  ██▓░██▄▄▄▄██ ▓▓█  ░██░▒██▀▀█▄  ░██▄▄▄▄██ ▒██░█▀  
--░▒▓███▀▒ ▓█   ▓██▒▒▒█████▓ ░██▓ ▒██▒ ▓█   ▓██▒░▓█  ▀█▓
-- ░▒   ▒  ▒▒   ▓▒█░░▒▓▒ ▒ ▒ ░ ▒▓ ░▒▓░ ▒▒   ▓▒█░░▒▓███▀▒
--  ░   ░   ▒   ▒▒ ░░░▒░ ░ ░   ░▒ ░ ▒░  ▒   ▒▒ ░▒░▒   ░ 
--░ ░   ░   ░   ▒    ░░░ ░ ░   ░░   ░   ░   ▒    ░    ░ 
--      ░       ░  ░   ░        ░           ░  ░ ░      
--                                                    ░ 


ALTER PROCEDURE [dbo].[EntityLayout_GetBy_PicklistId]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @strPicklistId xSmallText
)
AS
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT Id,
           PicklistId,
           Name,
           [Type],
           LayoutContext,
           UpdatedOn,
           '',
           Layout,
           [Default]
    FROM [dbo].[PicklistLayout]
    WHERE TenantId = @guidTenantId
          AND PicklistId = @strPicklistId;
END;

GO
/****** Object:  StoredProcedure [dbo].[EntityLayout_GetBy_Type]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EntityLayout_GetBy_Type]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[EntityLayout_GetBy_Type] AS' 
END
GO
ALTER PROCEDURE [dbo].[EntityLayout_GetBy_Type]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @strEntityId xSmallText,
    @intType INT
)
AS
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT [Id],
           [EntityId],
           [Name],
           [Type],
           [LayoutContext],
           [Layout],
           [UpdatedOn],
           [UpdatedBy],
           [Default],
           [SubType]
    FROM [dbo].[EntityLayout]
    WHERE [TenantId] = @guidTenantId
          AND [EntityId] = @strEntityId
          AND [Type] = @intType;

END;

GO
/****** Object:  StoredProcedure [dbo].[EntityLayout_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EntityLayout_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[EntityLayout_Update] AS' 
END
GO
ALTER PROCEDURE [dbo].[EntityLayout_Update]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidId UNIQUEIDENTIFIER,
    @strLayout xLargeText,
    @guidUpdatedBy UNIQUEIDENTIFIER,
    @strName mediumText
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[EntityLayout]
    SET [Layout] = @strLayout,
        [UpdatedOn] = GETUTCDATE(),
        [UpdatedBy] = @guidUpdatedBy,
        [Name] = @strName
    WHERE TenantId = @guidTenantId
          AND Id = @guidId;

END;

GO
/****** Object:  StoredProcedure [dbo].[EntityLayouts_Clone]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EntityLayouts_Clone]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[EntityLayouts_Clone] AS' 
END
GO
ALTER PROCEDURE [dbo].[EntityLayouts_Clone]
(
    @rootTenantId UNIQUEIDENTIFIER,
    @targetTenantId UNIQUEIDENTIFIER,
    @xmlIds XML
)
AS
BEGIN

    DECLARE @resources TABLE (Id NVARCHAR(50) NOT NULL);
    
	INSERT INTO @resources
    SELECT DISTINCT
        ref.value('./@value', 'NVARCHAR(50)') AS type
    FROM @xmlIds.nodes('/items/item') AS T(ref);

    INSERT INTO dbo.EntityLayout
    (
        TenantId,
        Id,
        EntityId,
        Name,
        Type,
        SubType,
        LayoutContext,
        Layout,
        UpdatedOn,
        UpdatedBy,
        [Default]
    )
    SELECT @targetTenantId,
           NEWID(),
           LA.EntityId,
           LA.Name,
           LA.Type,
           LA.SubType,
           LA.LayoutContext,
           LA.Layout,
           GETUTCDATE(),
           @rootTenantId,
           LA.[Default]
    FROM @resources RE
        INNER JOIN dbo.EntityLayout LA
            ON LA.[EntityId] = RE.Id
    WHERE LA.[Default] = 1
          AND LA.[TenantId] = @rootTenantId;
END;

GO
/****** Object:  StoredProcedure [dbo].[EntitySecurity_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EntitySecurity_Create]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[EntitySecurity_Create] AS' 
END
GO


ALTER PROCEDURE [dbo].[EntitySecurity_Create]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidEntitySecurityId AS UNIQUEIDENTIFIER,
    @strEntityId [dbo].[xSmallText],
    @guidRoleId UNIQUEIDENTIFIER,
    @intSecurityCode INT,
    @guidFunctionContext UNIQUEIDENTIFIER = NULL
)
AS
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    INSERT INTO [dbo].[EntitySecurity]
    (
        [TenantId],
        [Id],
        [EntityId],
        [RoleId],
        [SecurityCode],
        [FunctionContext]
    )
    VALUES
    (
		@guidTenantId, @guidEntitySecurityId, @strEntityId, @guidRoleId, @intSecurityCode, @guidFunctionContext
	);

    RETURN 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[EntitySecurity_Get]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EntitySecurity_Get]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[EntitySecurity_Get] AS' 
END
GO


ALTER PROCEDURE [dbo].[EntitySecurity_Get]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @strEntityId [dbo].[xSmallText],
    @guidRoleId UNIQUEIDENTIFIER
)
AS
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT [Id],
           [EntityId],
           [RoleId],
           [SecurityCode],
           [FunctionContext]
    FROM [dbo].[EntitySecurity]
    WHERE [TenantId] = @guidTenantId
          AND [RoleId] = @guidRoleId
          AND EntityId = @strEntityId
          AND [FunctionContext] IS NULL;

    RETURN 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[EntitySecurity_GetAll_ById]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EntitySecurity_GetAll_ById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[EntitySecurity_GetAll_ById] AS' 
END
GO


ALTER PROCEDURE [dbo].[EntitySecurity_GetAll_ById]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @strEntityId [dbo].[xSmallText],
    @guidRoleId UNIQUEIDENTIFIER = NULL
)
AS
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT [Id],
           [EntityId],
           [RoleId],
           [SecurityCode],
           [FunctionContext]
    FROM [dbo].[EntitySecurity]
    WHERE [TenantId] = @guidTenantId
          AND EntityId = @strEntityId
          AND [FunctionContext] IS NULL
          AND (
                  (
                      (@guidRoleId IS NOT NULL)
                      AND ([RoleId] = @guidRoleId)
                  )
                  OR (@guidRoleId IS NULL)
              );

    RETURN 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[EntitySecurity_GetAll_ByUserId]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EntitySecurity_GetAll_ByUserId]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[EntitySecurity_GetAll_ByUserId] AS' 
END
GO


ALTER PROCEDURE [dbo].[EntitySecurity_GetAll_ByUserId]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidUserId UNIQUEIDENTIFIER
)
AS
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT EN.[Id],
           EN.[EntityId],
           EN.[RoleId],
           EN.[SecurityCode],
           [FunctionContext]
    FROM [dbo].[EntitySecurity] EN
        INNER JOIN [dbo].[UserInRole] UIR
            ON UIR.RoleId = EN.RoleId
    WHERE EN.[TenantId] = @guidTenantId
          AND UIR.UserId = @guidUserId
          AND [FunctionContext] IS NULL;

    RETURN 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[EntitySecurity_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EntitySecurity_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[EntitySecurity_Update] AS' 
END
GO


ALTER PROCEDURE [dbo].[EntitySecurity_Update]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidEntitySecurityId AS UNIQUEIDENTIFIER,
    @strEntityId [dbo].[xSmallText],
    @guidRoleId UNIQUEIDENTIFIER,
    @intSecurityCode INT
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[EntitySecurity]
    SET [SecurityCode] = @intSecurityCode
    WHERE [TenantId] = @guidTenantId
          AND [Id] = @guidEntitySecurityId
          AND [EntityId] = @strEntityId
          AND [RoleId] = @guidRoleId;



    RETURN 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[FunctionSecurity_GetAll_ById]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FunctionSecurity_GetAll_ById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[FunctionSecurity_GetAll_ById] AS' 
END
GO


ALTER PROCEDURE [dbo].[FunctionSecurity_GetAll_ById]
    (
      @guidTenantId UNIQUEIDENTIFIER,     
	  @strEntityId [dbo].[xSmallText] ,
      @guidRoleId  UNIQUEIDENTIFIER=null
    )
AS 
    BEGIN
        SET NOCOUNT ON 
	    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

		SELECT [Id],[EntityId],[RoleId],[SecurityCode],[FunctionContext] FROM [dbo].[EntitySecurity]
		 WHERE  [TenantId]=@guidTenantId  AND EntityId=@strEntityId  AND [FunctionContext] IS not NULL
		 AND  (((@guidRoleId IS NOT NULL) and ([RoleId] = @guidRoleId)) or (@guidRoleId IS NULL))
       
		RETURN 0
    END
GO
/****** Object:  StoredProcedure [dbo].[FunctionSecurity_GetAll_ByUserId]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FunctionSecurity_GetAll_ByUserId]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[FunctionSecurity_GetAll_ByUserId] AS' 
END
GO


ALTER PROCEDURE [dbo].[FunctionSecurity_GetAll_ByUserId]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidUserId UNIQUEIDENTIFIER
)
AS
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT EN.[Id],
           EN.[EntityId],
           EN.[RoleId],
           EN.[SecurityCode],
           [FunctionContext]
    FROM [dbo].[EntitySecurity] EN
        INNER JOIN [dbo].[UserInRole] UIR
            ON UIR.RoleId = EN.RoleId
    WHERE EN.[TenantId] = @guidTenantId
          AND UIR.UserId = @guidUserId
          AND [FunctionContext] IS NOT NULL;

    RETURN 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetRootTenantCode]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetRootTenantCode]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetRootTenantCode] AS' 
END
GO

ALTER PROCEDURE [dbo].[GetRootTenantCode]
AS
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT Id
    FROM dbo.Tenant
    WHERE IsSystemRoot = 1;
END;

GO
/****** Object:  StoredProcedure [dbo].[GetRootTenantLayouts]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetRootTenantLayouts]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetRootTenantLayouts] AS' 
END
GO


ALTER PROCEDURE [dbo].[GetRootTenantLayouts] 
(
	@guidTenantId UNIQUEIDENTIFIER
)
AS
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT EntityId AS Id,
           Name,
           [Type],
           SubType,
           LayoutContext,
           Layout,
           UpdatedOn,
           UpdatedBy,
           [Default],
           CONVERT(INT, 1) AS LayoutFor
    FROM dbo.EntityLayout
    WHERE TenantId = @guidTenantId
    UNION ALL
    SELECT PicklistId AS Id,
           Name,
           [Type],
           '' AS SubType,
           LayoutContext,
           Layout,
           UpdatedOn,
           UpdatedBy,
           [Default],
           CONVERT(INT, 2) AS LayoutFor
    FROM dbo.PicklistLayout
    WHERE TenantId = @guidTenantId;
END;

GO
/****** Object:  StoredProcedure [dbo].[GetSubscriptionsByTenantId]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetSubscriptionsByTenantId]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetSubscriptionsByTenantId] AS' 
END
GO


ALTER PROCEDURE [dbo].[GetSubscriptionsByTenantId] 
(
	@guidTenantId UNIQUEIDENTIFIER
)
AS
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;


    SELECT ts.Id,
           ts.TenantSubscriptionId,
           ts.EntityId,
           ts.LimitNumber,
           ts.LimitType
    FROM [dbo].[TenantSubscriptionEntity] AS ts
        INNER JOIN dbo.Tenant AS tt
            ON tt.Id = ts.TenantId
    WHERE ts.TenantId = @guidTenantId;

END;



GO
/****** Object:  StoredProcedure [dbo].[Layout_GetBy_Type]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Layout_GetBy_Type]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Layout_GetBy_Type] AS' 
END
GO


ALTER PROCEDURE [dbo].[Layout_GetBy_Type]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @strEntityId xSmallText,
    @intType INT,
    @strSubType smallText = NULL,
    @LayoutContext INT = NULL
)
AS
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
    
	SELECT Id,
           EntityId,
           Name,
           [Type],
           SubType,
           LayoutContext,
           Layout,
           UpdatedOn,
           UpdatedBy,
           [Default]
    FROM [dbo].[EntityLayout]
    WHERE TenantId = @guidTenantId
          AND EntityId = @strEntityId
          AND [Type] = @intType
          AND [Default] = 1
          AND (
                  (
                      SubType IS NOT NULL
                      AND SubType = @strSubType
                  )
                  OR (@strSubType IS NULL)
              )
          AND (
                  (
                      LayoutContext IS NOT NULL
                      AND LayoutContext = @LayoutContext
                  )
                  OR (@LayoutContext IS NULL)
              );
END;

GO
/****** Object:  StoredProcedure [dbo].[ListLayout_Delete]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ListLayout_Delete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ListLayout_Delete] AS' 
END
GO
ALTER PROCEDURE [dbo].[ListLayout_Delete]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidId UNIQUEIDENTIFIER
)
AS
BEGIN
    DELETE dbo.EntityLayout
    WHERE TenantId = @guidTenantId
          AND Id = @guidId;
END;

GO
/****** Object:  StoredProcedure [dbo].[ListLayout_Set_Default]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ListLayout_Set_Default]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ListLayout_Set_Default] AS' 
END
GO
ALTER PROCEDURE [dbo].[ListLayout_Set_Default]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidId UNIQUEIDENTIFIER,
    @strListId xSmallText,
    @intType INT,
    @guidUpdatedBy UNIQUEIDENTIFIER,
    @strSubTypeId smallText,
    @intContext INT
)
AS
BEGIN

    DECLARE @ErrorMessage NVARCHAR(4000),
            @ErrorNumber INT,
            @ErrorSeverity INT,
            @ErrorState INT,
            @ErrorLine INT,
            @ErrorProcedure NVARCHAR(200);

    BEGIN TRY
        BEGIN TRAN;
        
		IF @intType = 2
        BEGIN
            UPDATE dbo.EntityLayout
            SET [Default] = 0
            WHERE TenantId = @guidTenantId
                  AND EntityId = @strListId
                  AND [Type] = @intType
                  AND [SubType] = @strSubTypeId
                  AND [LayoutContext] = @intContext;
        END;
        ELSE
        BEGIN
            UPDATE dbo.EntityLayout
            SET [Default] = 0
            WHERE TenantId = @guidTenantId
                  AND EntityId = @strListId
                  AND [Type] = @intType;
        END;

        UPDATE dbo.EntityLayout
        SET [Default] = 1
        WHERE [TenantId] = @guidTenantId
              AND [Id] = @guidId;

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        SELECT @ErrorMessage = ERROR_MESSAGE(),
               @ErrorNumber = ERROR_NUMBER(),
               @ErrorSeverity = ERROR_SEVERITY(),
               @ErrorState = ERROR_STATE(),
               @ErrorLine = ERROR_LINE(),
               @ErrorProcedure = ISNULL(ERROR_PROCEDURE(), '-');

        ROLLBACK TRAN;
        RAISERROR(
                     @ErrorMessage,
                     @ErrorSeverity,
                     1,
                     @ErrorNumber,
                     @ErrorSeverity,
                     @ErrorState,
                     @ErrorProcedure,
                     @ErrorLine
                 );
    END CATCH;

END;

GO
/****** Object:  StoredProcedure [dbo].[Menu_Clone]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Menu_Clone]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Menu_Clone] AS' 
END
GO
ALTER PROCEDURE [dbo].[Menu_Clone]
(
    @rootTenantId UNIQUEIDENTIFIER,
    @initilizedTenantId UNIQUEIDENTIFIER
)
AS
BEGIN

    DECLARE @PickListId SMALLINT;
    SET @PickListId = 10015;

    INSERT INTO dbo.Menu
    (
        [TenantId],
        [Id],
        [GroupId],
        [Name],
        [MenuTypeId],
        [ReferenceEntityId],
        [ActionTypeId],
        [LayoutId],
        [WellKnownLink],
        [Link],
        [UpdatedOn],
        [UpdatedBy]
    )
    SELECT @initilizedTenantId,
           NEWID(),
           (
               SELECT TOP (1)
                   Id
               FROM dbo.PickListValue
               WHERE TenantId = @initilizedTenantId
                     AND PickListId = pv.PickListId
                     AND [Key] = pv.[Key]
                     AND [Text] = pv.[Text]
           ) AS GroupId,
           [Name],
           [MenuTypeId],
           [ReferenceEntityId],
           [ActionTypeId],
           [LayoutId],
           [WellKnownLink],
           [Link],
           GETUTCDATE(),
           @rootTenantId
    FROM dbo.[Menu] ME
        INNER JOIN PickListValue pv
            ON ME.GroupId = pv.Id
               AND pv.PickListId = @PickListId
               AND pv.TenantId = @rootTenantId
               AND ME.TenantId = @rootTenantId;

END;

GO
/****** Object:  StoredProcedure [dbo].[Menu_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Menu_Create]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Menu_Create] AS' 
END
GO
ALTER PROCEDURE [dbo].[Menu_Create]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidId UNIQUEIDENTIFIER,
    @guidGroupId UNIQUEIDENTIFIER,
    @strName mediumText,
    @intMenuType INT,
    @strReferenceEntity smallText = NULL,
    @intActionTypeId INT,
    @strWellKnownLink mediumText,
    @guidLayoutId UNIQUEIDENTIFIER,
    @guidUpdatedBy UNIQUEIDENTIFIER
)
AS
BEGIN

    INSERT INTO [dbo].[Menu]
    (
        [TenantId],
        [Id],
        [GroupId],
        [Name],
        [MenuTypeId],
        [ReferenceEntityId],
        [ActionTypeId],
        [LayoutId],
        [WellKnownLink],
        [UpdatedOn],
        [UpdatedBy]
    )
    VALUES
    (@guidTenantId,
     @guidId,
     @guidGroupId,
     @strName,
     @intMenuType,
     @strReferenceEntity,
     @intActionTypeId,
     @guidLayoutId,
     @strWellKnownLink,
     GETUTCDATE(),
     @guidUpdatedBy
    );

END;

GO
/****** Object:  StoredProcedure [dbo].[Menu_Delete]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Menu_Delete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Menu_Delete] AS' 
END
GO
ALTER PROCEDURE [dbo].[Menu_Delete]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidId UNIQUEIDENTIFIER
)
AS
BEGIN
    DELETE dbo.Menu
    WHERE TenantId = @guidTenantId
          AND [Id] = @guidId;
END;

GO
/****** Object:  StoredProcedure [dbo].[Menu_GetAll]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Menu_GetAll]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Menu_GetAll] AS' 
END
GO



ALTER PROCEDURE [dbo].[Menu_GetAll]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @intPageIndex INT,
    @intPageSize INT,
    @strGroupName mediumText = NULL
)
AS
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

	DECLARE @PicklistId AS smallint = 10015;

    IF ISNULL(@strGroupName, '') != ''
    BEGIN
        SELECT ME.[Id],
               [GroupId],
               [Name],
               [MenuTypeId],
               [ReferenceEntityId],
               [ActionTypeId],
               [LayoutId],
               [Link],
               [UpdatedOn],
               ME.[UpdatedBy],
               PL.Text,
               [WellKnownLink]
        FROM [dbo].[Menu] ME
            INNER JOIN [dbo].[PickListValue] PL
                ON ME.GroupId = PL.Id AND PL.TenantId = @guidTenantId
        WHERE ME.[TenantId] = @guidTenantId
              AND PL.PickListId = @PicklistId
              AND PL.[Text] = @strGroupName
        ORDER BY [Name] OFFSET @intPageSize * (@intPageIndex - 1) ROWS FETCH NEXT @intPageSize ROWS ONLY
        OPTION (RECOMPILE);
    END;
    ELSE
    BEGIN
        SELECT ME.[Id],
               [GroupId],
               [Name],
               [MenuTypeId],
               [ReferenceEntityId],
               [ActionTypeId],
               [LayoutId],
               [Link],
               [UpdatedOn],
               ME.[UpdatedBy],
               PL.Text,
               [WellKnownLink]
        FROM [dbo].[Menu] ME
            INNER JOIN [dbo].[PickListValue] PL
                ON ME.GroupId = PL.Id 
				AND PL.TenantId = @guidTenantId
        WHERE ME.[TenantId] = @guidTenantId
              AND PL.PickListId = @PicklistId
        ORDER BY [Name] OFFSET @intPageSize * (@intPageIndex - 1) ROWS FETCH NEXT @intPageSize ROWS ONLY
        OPTION (RECOMPILE);
    END;


END;

GO
/****** Object:  StoredProcedure [dbo].[Menu_GetBy_Id]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Menu_GetBy_Id]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Menu_GetBy_Id] AS' 
END
GO
ALTER PROCEDURE [dbo].[Menu_GetBy_Id]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidId UNIQUEIDENTIFIER
)
AS
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT [Id],
           [GroupId],
           [Name],
           [MenuTypeId],
           [ReferenceEntityId],
           [ActionTypeId],
           [LayoutId],
           [Link],
           [UpdatedOn],
           [UpdatedBy],
           [WellKnownLink]
    FROM [dbo].[Menu]
    WHERE [TenantId] = @guidTenantId
          AND [Id] = @guidId;
END;

GO
/****** Object:  StoredProcedure [dbo].[Menu_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Menu_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Menu_Update] AS' 
END
GO
ALTER PROCEDURE [dbo].[Menu_Update]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidId UNIQUEIDENTIFIER,
    @guidGroupId UNIQUEIDENTIFIER,
    @strName mediumText,
    @intMenuType INT,
    @strReferenceEntity smalltext,
    @intActionTypeId INT,
    @strWellKnownLink mediumText,
    @guidLayoutId UNIQUEIDENTIFIER,
    @guidUpdatedBy UNIQUEIDENTIFIER
)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN

        UPDATE [dbo].[Menu]
        SET [GroupId] = @guidGroupId,
            [Name] = @strName,
            [MenuTypeId] = @intMenuType,
            [ReferenceEntityId] = @strReferenceEntity,
            [ActionTypeId] = @intActionTypeId,
            [LayoutId] = @guidLayoutId,
            [WellKnownLink] = @strWellKnownLink,
            [UpdatedOn] = GETUTCDATE(),
            [UpdatedBy] = @guidUpdatedBy
        WHERE TenantId = @guidTenantId
              AND [Id] = @guidId;

    END;
END;
GO
/****** Object:  StoredProcedure [dbo].[Picklist_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Picklist_Create]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Picklist_Create] AS' 
END
GO

ALTER PROCEDURE [dbo].[Picklist_Create]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @intId SMALLINT,
    @strName smallText,
    @bitIsStandard BIT,
    @intEntityId SMALLINT = NULL,
    @bitFixedValueList BIT,
    @bitCustomizeValue BIT,
    @bitIsKeyValueType BIT,
    @bitActive BIT,
    @bitIsDeletetd BIT,
    @guidUpdatedBy UNIQUEIDENTIFIER,
    @intReturn INT OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS
    (
        SELECT *
        FROM dbo.PickList
        WHERE Id = @intId
              AND TenantId = @guidTenantId
              AND IsDeletetd = 0
    )
    BEGIN
        SET @intReturn = 1;
    END;
    ELSE
    BEGIN
        INSERT INTO dbo.PickList
        (
            TenantId,
            [Id],
            [Name],
            IsStandard,
            EntityId,
            FixedValueList,
            CustomizeValue,
            IsKeyValueType,
            Active,
            IsDeletetd,
            UpdatedBy,
            UpdatedDate
        )
        VALUES
        (@guidTenantId,
         @intId,
         @strName,
         @bitIsStandard,
         @intEntityId,
         @bitFixedValueList,
         @bitCustomizeValue,
         @bitIsKeyValueType,
         @bitActive,
         @bitIsDeletetd,
         @guidUpdatedBy,
         GETUTCDATE()
        );

        SET @intReturn = 0;
    END;
END;

GO
/****** Object:  StoredProcedure [dbo].[Picklist_Delete]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Picklist_Delete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Picklist_Delete] AS' 
END
GO

ALTER PROCEDURE [dbo].[Picklist_Delete]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @intId SMALLINT
)
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM dbo.PickList
    WHERE Id = @intId
          AND TenantId = @guidTenantId;
END;

GO
/****** Object:  StoredProcedure [dbo].[Picklist_GetAll]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Picklist_GetAll]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Picklist_GetAll] AS' 
END
GO

ALTER PROCEDURE [dbo].[Picklist_GetAll]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @intPageNumber INT,
    @intPageSize INT,
    @strSearchText smallText = NULL,
    @bitIsdeleted BIT = NULL
)
AS
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT Id,
           Name,
           IsStandard,
           EntityId,
           FixedValueList,
           CustomizeValue,
           IsKeyValueType,
           Active,
           IsDeletetd,
           UpdatedBy,
           UpdatedDate
    FROM dbo.PickList
    WHERE IsDeletetd = @bitIsdeleted
    ORDER BY Name OFFSET @intPageSize * (@intPageNumber - 1) ROWS FETCH NEXT @intPageSize ROWS ONLY
    OPTION (RECOMPILE);
END;

GO
/****** Object:  StoredProcedure [dbo].[Picklist_GetBy_Id]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Picklist_GetBy_Id]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Picklist_GetBy_Id] AS' 
END
GO

ALTER PROCEDURE [dbo].[Picklist_GetBy_Id]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @intId SMALLINT
)
AS
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT Id,
           Name,
           IsStandard,
           EntityId,
           FixedValueList,
           CustomizeValue,
           IsKeyValueType,
           Active,
           IsDeletetd,
           UpdatedBy,
           UpdatedDate
    FROM dbo.PickList
    WHERE Id = @intId
          AND TenantId = @guidTenantId;
END;

GO
/****** Object:  StoredProcedure [dbo].[Picklist_GetBy_Type]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Picklist_GetBy_Type]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Picklist_GetBy_Type] AS' 
END
GO
ALTER PROCEDURE [dbo].[Picklist_GetBy_Type]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @strEntityId xSmallText,
    @intType INT,
    @intLayoutContext INT = NULL
)
AS
BEGIN

    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT [Id],
           [PicklistId],
           [Name],
           [Type],
           [LayoutContext],
           [Layout],
           [UpdatedOn],
           [UpdatedBy],
           [Default]
    FROM [dbo].[PicklistLayout]
    WHERE [TenantId] = @guidTenantId
          AND [PicklistId] = @strEntityId
          AND [Type] = @intType;
--AND (@intLayoutContext is not null AND LayoutContext = @intLayoutContext) 

END;

GO
/****** Object:  StoredProcedure [dbo].[Picklist_SoftDelete]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Picklist_SoftDelete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Picklist_SoftDelete] AS' 
END
GO

ALTER PROCEDURE [dbo].[Picklist_SoftDelete]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @intId SMALLINT,
    @guidUpdatedBy UNIQUEIDENTIFIER
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.PickList
    SET IsDeletetd = 1,
        UpdatedBy = @guidUpdatedBy,
        UpdatedDate = GETUTCDATE()
    WHERE Id = @intId
          AND TenantId = @guidTenantId;
END;

GO
/****** Object:  StoredProcedure [dbo].[Picklist_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Picklist_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Picklist_Update] AS' 
END
GO

ALTER PROCEDURE [dbo].[Picklist_Update]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @intId SMALLINT,
    @strName smallText,
    @bitIsStandard BIT,
    @intEntityId SMALLINT = NULL,
    @bitFixedValueList BIT,
    @bitCustomizeValue BIT,
    @bitIsKeyValueType BIT,
    @bitActive BIT,
    @bitIsDeletetd BIT,
    @guidUpdatedBy UNIQUEIDENTIFIER
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.PickList
    SET Name = @strName,
        IsStandard = @bitIsStandard,
        EntityId = @intEntityId,
        FixedValueList = @bitFixedValueList,
        CustomizeValue = @bitCustomizeValue,
        IsKeyValueType = @bitIsKeyValueType,
        Active = @bitActive,
        IsDeletetd = @bitIsDeletetd,
        UpdatedBy = @guidUpdatedBy,
        UpdatedDate = GETUTCDATE()
    WHERE Id = @intId
          AND TenantId = @guidTenantId;
END;

GO
/****** Object:  StoredProcedure [dbo].[Picklist_Update_Status]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Picklist_Update_Status]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Picklist_Update_Status] AS' 
END
GO

ALTER PROCEDURE [dbo].[Picklist_Update_Status]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @intId SMALLINT,
    @guidUpdatedBy UNIQUEIDENTIFIER
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.PickList
    SET Active = ~ Active,
        UpdatedBy = @guidUpdatedBy,
        UpdatedDate = GETUTCDATE()
    WHERE Id = @intId
          AND TenantId = @guidTenantId;
END;

GO
/****** Object:  StoredProcedure [dbo].[PicklistLayout_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistLayout_Create]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PicklistLayout_Create] AS' 
END
GO

ALTER PROCEDURE [dbo].[PicklistLayout_Create]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidId UNIQUEIDENTIFIER,
    @strPicklistId xSmallText,
    @strName mediumText,
    @intType INT,
    @intLayoutContext INT = NULL,
    @guidUpdatedBy UNIQUEIDENTIFIER,
    @defaultLayout xLargeText = NULL,
    @isDefault BIT = 0
)
AS
BEGIN

    DECLARE @ErrorMessage NVARCHAR(4000),
            @ErrorNumber INT,
            @ErrorSeverity INT,
            @ErrorState INT,
            @ErrorLine INT,
            @ErrorProcedure NVARCHAR(200);
    --,
    --@defaultLayout xLargeText; 
    BEGIN TRY
        BEGIN TRAN;

        IF @defaultLayout IS NULL
        BEGIN
            IF @intType = 1
            BEGIN
                SET @defaultLayout
                    = '{"fields":[{"name":"InternalId","sequence":1,"hidden":true,"dataType":"Guid","refId":null,"defaultValue":null,"properties":null,"values":null,"clickable":false}],"defaultSortOrder":{"name":"","value":""}}';
            END;

            IF @intType = 3
            BEGIN
                SET @defaultLayout
                    = '{"fields":[{"name":"InternalId","sequence":1,"hidden":true,"dataType":"Guid","refId":null,"defaultValue":null,"properties":null,"values":null,"clickable":false}],"defaultSortOrder":{"name":"","value":""},"defaultGroupBy":"","maxResult":0,"searchProperties":[{"name":"FreeTextSearch","properties":[]},{"name":"SimpleSearch","properties":[]},{"name":"AdvanceSearch","properties":[]}],"actions":[],"toolbar":[]}';
            END;
        END;



        INSERT INTO [dbo].[PicklistLayout]
        (
            [TenantId],
            [Id],
            [PicklistId],
            [Name],
            [Type],
            [LayoutContext],
            [UpdatedOn],
            [UpdatedBy],
            [Default],
            Layout
        )
        VALUES
        (@guidTenantId,
         @guidId,
         @strPicklistId,
         @strName,
         @intType,
         @intLayoutContext,
         GETDATE(),
         @guidUpdatedBy,
         @isDefault,
         @defaultLayout
        );

        IF NOT EXISTS
        (
            SELECT *
            FROM PicklistLayout
            WHERE [PicklistId] = @strPicklistId
                  AND [Type] = @intType
                  AND ISNULL(LayoutContext, 0) = ISNULL(@intLayoutContext, 0)
                  AND [Default] = 1
				  AND TenantId = @guidTenantId
        )
        BEGIN
            UPDATE dbo.PicklistLayout
            SET [Default] = 1
            WHERE [TenantId] = @guidTenantId
                  AND [Id] = @guidId;
        END;
        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        SELECT @ErrorMessage = ERROR_MESSAGE(),
               @ErrorNumber = ERROR_NUMBER(),
               @ErrorSeverity = ERROR_SEVERITY(),
               @ErrorState = ERROR_STATE(),
               @ErrorLine = ERROR_LINE(),
               @ErrorProcedure = ISNULL(ERROR_PROCEDURE(), '-');

        ROLLBACK TRAN;
        RAISERROR(
                     @ErrorMessage,
                     @ErrorSeverity,
                     1,
                     @ErrorNumber,
                     @ErrorSeverity,
                     @ErrorState,
                     @ErrorProcedure,
                     @ErrorLine
                 );
    END CATCH;



END;


GO
/****** Object:  StoredProcedure [dbo].[PicklistLayout_Delete]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistLayout_Delete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PicklistLayout_Delete] AS' 
END
GO
ALTER PROCEDURE [dbo].[PicklistLayout_Delete]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidId UNIQUEIDENTIFIER
)
AS
BEGIN
    DELETE dbo.PicklistLayout
    WHERE TenantId = @guidTenantId
          AND Id = @guidId;
END;

GO
/****** Object:  StoredProcedure [dbo].[PicklistLayout_GetBy_Id]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistLayout_GetBy_Id]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PicklistLayout_GetBy_Id] AS' 
END
GO

ALTER PROCEDURE [dbo].[PicklistLayout_GetBy_Id]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidId UNIQUEIDENTIFIER
)
AS
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT [Id],
           [PicklistId],
           [Name],
           [Type],
           [LayoutContext],
           [Layout],
           [UpdatedOn],
           [Default]
    FROM [dbo].[PicklistLayout]
    WHERE [TenantId] = @guidTenantId
          AND [Id] = @guidId;
END;

GO
/****** Object:  StoredProcedure [dbo].[PicklistLayout_GetDefaultBy_Type]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistLayout_GetDefaultBy_Type]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PicklistLayout_GetDefaultBy_Type] AS' 
END
GO


ALTER PROCEDURE [dbo].[PicklistLayout_GetDefaultBy_Type]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @strEntityId xSmallText,
    @intType INT,
    @LayoutContext INT = NULL
)
AS
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT Id,
           PicklistId,
           Name,
           [Type],
           LayoutContext,
           Layout,
           UpdatedOn,
           UpdatedBy,
           [Default]
    FROM [dbo].[PicklistLayout]
    WHERE TenantId = @guidTenantId
          AND PicklistId = @strEntityId
          AND [Type] = @intType
          AND [Default] = 1
          AND (
                  (
                      LayoutContext IS NOT NULL
                      AND LayoutContext = @LayoutContext
                  )
                  OR (@LayoutContext IS NULL)
              );
END;

GO
/****** Object:  StoredProcedure [dbo].[PicklistLayout_Set_Default]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistLayout_Set_Default]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PicklistLayout_Set_Default] AS' 
END
GO
ALTER PROCEDURE [dbo].[PicklistLayout_Set_Default]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidId UNIQUEIDENTIFIER,
    @strPicklistId xSmallText,
    @intType INT,
    @intContext INT,
    @guidUpdatedBy UNIQUEIDENTIFIER
)
AS
BEGIN

    DECLARE @ErrorMessage NVARCHAR(4000),
            @ErrorNumber INT,
            @ErrorSeverity INT,
            @ErrorState INT,
            @ErrorLine INT,
            @ErrorProcedure NVARCHAR(200);
    BEGIN TRY
        BEGIN TRAN;
        IF @intType = 2
        BEGIN
            UPDATE dbo.PicklistLayout
            SET [Default] = 0
            WHERE TenantId = @guidTenantId
                  AND PicklistId = @strPicklistId
                  AND Type = @intType
                  AND LayoutContext = @intContext;
        END;
        ELSE
        BEGIN
            UPDATE dbo.PicklistLayout
            SET [Default] = 0
            WHERE TenantId = @guidTenantId
                  AND PicklistId = @strPicklistId
                  AND Type = @intType;
        END;

        UPDATE dbo.PicklistLayout
        SET [Default] = 1
        WHERE [TenantId] = @guidTenantId
              AND [Id] = @guidId;

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        SELECT @ErrorMessage = ERROR_MESSAGE(),
               @ErrorNumber = ERROR_NUMBER(),
               @ErrorSeverity = ERROR_SEVERITY(),
               @ErrorState = ERROR_STATE(),
               @ErrorLine = ERROR_LINE(),
               @ErrorProcedure = ISNULL(ERROR_PROCEDURE(), '-');

        ROLLBACK TRAN;
        RAISERROR(
                     @ErrorMessage,
                     @ErrorSeverity,
                     1,
                     @ErrorNumber,
                     @ErrorSeverity,
                     @ErrorState,
                     @ErrorProcedure,
                     @ErrorLine
                 );
    END CATCH;



END;

GO
/****** Object:  StoredProcedure [dbo].[PicklistLayout_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistLayout_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PicklistLayout_Update] AS' 
END
GO

ALTER PROCEDURE [dbo].[PicklistLayout_Update]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidId UNIQUEIDENTIFIER,
    @strLayout xLargeText,
    @guidUpdatedBy UNIQUEIDENTIFIER,
    @strName mediumText
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[PicklistLayout]
    SET [Layout] = @strLayout,
        [UpdatedOn] = GETUTCDATE(),
        [UpdatedBy] = @guidUpdatedBy,
        Name = @strName
    WHERE TenantId = @guidTenantId
          AND Id = @guidId;

END;

GO
/****** Object:  StoredProcedure [dbo].[PicklistLayouts_Clone]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistLayouts_Clone]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PicklistLayouts_Clone] AS' 
END
GO
ALTER PROCEDURE [dbo].[PicklistLayouts_Clone]
(
    @rootTenantId UNIQUEIDENTIFIER,
    @targetTenantId UNIQUEIDENTIFIER,
    @xmlIds XML
)
AS
BEGIN

    DECLARE @resources TABLE (Id NVARCHAR(50) NOT NULL);

    INSERT INTO @resources
    SELECT DISTINCT
        ref.value('./@value', 'NVARCHAR(50)') AS type
    FROM @xmlIds.nodes('/items/item') AS T(ref);

    INSERT INTO dbo.PicklistLayout
    (
        TenantId,
        Id,
        PicklistId,
        Name,
        Type,
        LayoutContext,
        Layout,
        UpdatedOn,
        UpdatedBy,
        [Default]
    )
    SELECT @targetTenantId,
           NEWID(),
           LA.PicklistId,
           LA.Name,
           LA.Type,
           LA.LayoutContext,
           LA.Layout,
           GETUTCDATE(),
           @rootTenantId,
           LA.[Default]
    FROM @resources RE
        INNER JOIN dbo.PicklistLayout LA
            ON LA.[PicklistId] = RE.Id
    WHERE LA.[Default] = 1
          AND LA.[TenantId] = @rootTenantId;
END;

GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_City_Clone]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_City_Clone]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PicklistValue_City_Clone] AS' 
END
GO
ALTER PROCEDURE [dbo].[PicklistValue_City_Clone]
(
    @picklistId xSmallText,
    @rootTenantId UNIQUEIDENTIFIER,
    @initilizedTenantId UNIQUEIDENTIFIER
)
AS
BEGIN

    DECLARE @countryContext SMALLINT = '20002';
    DECLARE @stateContext SMALLINT = '20005';
    DECLARE @municiplityContext SMALLINT = '20007';

    INSERT INTO dbo.PickListValueForCity
    (
        TenantId,
        Id,
        PickListValueId,
        CountryId,
        StateId,
        MunicipalityId
    )
    SELECT @initilizedTenantId,
           NEWID(),
           (
               SELECT TOP (1)
                   Id
               FROM dbo.PickListValue
               WHERE TenantId = @initilizedTenantId
                     AND PickListId = pv.PickListId
                     AND [Key] = pv.[Key]
                     AND [Text] = pv.[Text]
           ), --get id from intiliast id of city
           (
               SELECT TOP (1)
                   Id
               FROM dbo.PickListValue
               WHERE TenantId = @initilizedTenantId
                     AND PickListId = cou.PickListId
                     AND [Key] = cou.[Key]
                     AND [Text] = cou.[Text]
           ), --NEWID(), --get id from intiliast id of country
           (
               SELECT TOP (1)
                   Id
               FROM dbo.PickListValue
               WHERE TenantId = @initilizedTenantId
                     AND PickListId = sta.PickListId
                     AND [Key] = sta.[Key]
                     AND [Text] = sta.[Text]
           ), -- NEWID(), --get id from intiliast id of state
           (
               SELECT TOP (1)
                   Id
               FROM dbo.PickListValue
               WHERE TenantId = @initilizedTenantId
                     AND PickListId = mun.PickListId
                     AND [Key] = mun.[Key]
                     AND [Text] = mun.[Text]
           )  -- NEWID(), --get id from intiliast id of municipality
    FROM dbo.PickListValueForCity AS pvc
        INNER JOIN dbo.PickListValue AS pv
            ON pvc.PickListValueId = pv.Id
        INNER JOIN dbo.PickListValue AS cou
            ON cou.Id = pvc.CountryId
               AND cou.PickListId = @countryContext
        LEFT JOIN dbo.PickListValue AS sta
            ON sta.Id = pvc.StateId
               AND sta.PickListId = @stateContext
        LEFT JOIN dbo.PickListValue AS mun
            ON mun.Id = pvc.MunicipalityId
               AND mun.PickListId = @municiplityContext
    WHERE pv.PickListId = @picklistId
          AND pv.TenantId = @rootTenantId;

END;

GO
/****** Object:  StoredProcedure [dbo].[PickListValue_Clone]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PickListValue_Clone]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PickListValue_Clone] AS' 
END
GO
ALTER PROCEDURE [dbo].[PickListValue_Clone]
(
    @rootTenantId UNIQUEIDENTIFIER,
    @targetTenantId UNIQUEIDENTIFIER,
    --- @xmlIds XML,
    @guidUserId UNIQUEIDENTIFIER
)
AS
BEGIN

    --DECLARE @resources TABLE
    --(
    --    Id NVARCHAR(50) NOT NULL
    --);
    --INSERT INTO @resources
    --SELECT DISTINCT
    --       ref.value('./@value', 'NVARCHAR(50)') AS type
    --FROM @xmlIds.nodes('/items/item') AS T(ref);

    INSERT INTO [dbo].[PickListValue]
    (
        [TenantId],
        [Id],
        [PickListId],
        [Key],
        [Text],
        [Active],
        [IsDeletetd],
        [Flagged],
        [UpdatedBy],
        [UpdatedDate]
    )
    SELECT @targetTenantId,
           NEWID(),
           [PickListId],
           [Key],
           [Text],
           [Active],
           [IsDeletetd],
           [Flagged],
           @guidUserId,
           GETUTCDATE()
    FROM dbo.PickListValue
    -- FROM @resources RE
    --INNER JOIN PickListValue LA ON LA.PickListId = RE.Id
    WHERE [TenantId] = @rootTenantId;



END;

GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_Country_Clone]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_Country_Clone]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PicklistValue_Country_Clone] AS' 
END
GO
ALTER PROCEDURE [dbo].[PicklistValue_Country_Clone]
(
    @picklistId xSmallText,
    @rootTenantId UNIQUEIDENTIFIER,
    @initilizedTenantId UNIQUEIDENTIFIER
)
AS
BEGIN

    DECLARE @currencyContext SMALLINT = '20001';
    DECLARE @languageContext SMALLINT = '20003';
    DECLARE @timezone SMALLINT = '20004';

    INSERT INTO dbo.PickListValueForCountry
    (
        TenantId,
        Id,
        PickListValueId,
        Currency,
        Language,
        Timezone,
        IsoCode,
        Nationality
    )
    SELECT @initilizedTenantId,
           NEWID(),
           (
               SELECT TOP (1)
                   Id
               FROM dbo.PickListValue
               WHERE TenantId = @initilizedTenantId
                     AND PickListId = pv.PickListId
                     AND [Key] = pv.[Key]
                     AND [Text] = pv.[Text]
           ), --get id from intiliast id of country 
           (
               SELECT TOP (1)
                   Id
               FROM dbo.PickListValue
               WHERE TenantId = @initilizedTenantId
                     AND PickListId = cur.PickListId
                     AND [Key] = cur.[Key]
                     AND [Text] = cur.[Text]
           ), --NEWID(), --get id from intiliast id of currency
           (
               SELECT TOP (1)
                   Id
               FROM dbo.PickListValue
               WHERE TenantId = @initilizedTenantId
                     AND PickListId = lan.PickListId
                     AND [Key] = lan.[Key]
                     AND [Text] = lan.[Text]
           ), -- NEWID(), --get id from intiliast id of language
           (
               SELECT TOP (1)
                   Id
               FROM dbo.PickListValue
               WHERE TenantId = @initilizedTenantId
                     AND PickListId = tim.PickListId
                     AND [Key] = tim.[Key]
                     AND [Text] = tim.[Text]
           ), -- NEWID(), --get id from intiliast id of timezone
           pvc.IsoCode,
           pvc.Nationality
    FROM dbo.PickListValueForCountry AS pvc
        INNER JOIN dbo.PickListValue AS pv
            ON pvc.PickListValueId = pv.Id
        LEFT JOIN dbo.PickListValue AS cur
            ON cur.Id = pvc.Currency
               AND cur.PickListId = @currencyContext
        LEFT JOIN dbo.PickListValue AS lan
            ON lan.Id = pvc.Language
               AND lan.PickListId = @languageContext
        LEFT JOIN dbo.PickListValue AS tim
            ON tim.Id = pvc.Timezone
               AND tim.PickListId = @timezone
    WHERE pv.PickListId = @picklistId
          AND pv.TenantId = @rootTenantId;

END;

GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_Country_ExtendedValue_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_Country_ExtendedValue_Create]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PicklistValue_Country_ExtendedValue_Create] AS' 
END
GO

ALTER PROCEDURE [dbo].[PicklistValue_Country_ExtendedValue_Create]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidId UNIQUEIDENTIFIER,
    @guidPicklistValueId UNIQUEIDENTIFIER,
    @guidCurrency UNIQUEIDENTIFIER,
    @guidLanguage UNIQUEIDENTIFIER,
    @guidTimezone UNIQUEIDENTIFIER,
    @strIsoCode smallText,
    @strNationality smallText
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.PickListValueForCountry
    (
        TenantId,
        Id,
        PickListValueId,
        Currency,
        Language,
        Timezone,
        IsoCode,
        Nationality
    )
    VALUES
    (   @guidTenantId,        -- TenantId - xSmallText
        @guidId,              -- Id - uniqueidentifier
        @guidPicklistValueId, -- PickListValueId - uniqueidentifier
        @guidCurrency,        -- Currency - uniqueidentifier
        @guidLanguage,        -- Language - uniqueidentifier
        @guidTimezone,        -- Timezone - uniqueidentifier
        @strIsoCode,          -- IsoCode - smallText
        @strNationality       -- Nationality - smallText
    );
END;

GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_Country_ExtendedValue_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_Country_ExtendedValue_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PicklistValue_Country_ExtendedValue_Update] AS' 
END
GO

ALTER PROCEDURE [dbo].[PicklistValue_Country_ExtendedValue_Update]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidId UNIQUEIDENTIFIER,
    @guidPicklistValueId UNIQUEIDENTIFIER,
    @guidCurrency UNIQUEIDENTIFIER,
    @guidLanguage UNIQUEIDENTIFIER,
    @guidTimezone UNIQUEIDENTIFIER,
    @strIsoCode smallText,
    @strNationality smallText
)
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE dbo.PickListValueForCountry
    SET Currency = @guidCurrency,
        [Language] = @guidLanguage,
        Timezone = @guidTimezone,
        IsoCode = @strIsoCode,
        Nationality = @strNationality
    WHERE TenantId = @guidTenantId
          AND Id = @guidId
          AND PickListValueId = @guidPicklistValueId;
END;

GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_Create]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PicklistValue_Create] AS' 
END
GO

ALTER PROCEDURE [dbo].[PicklistValue_Create]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidId UNIQUEIDENTIFIER,
    @intPicklistId SMALLINT,
    @strKey mediumText,
    @strText mediumText,
    @bitActive BIT,
    @bitIsDeletetd BIT,
    @bitFlagged BIT,
    @guidUpdatedBy UNIQUEIDENTIFIER,
    @intReturn INT OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    IF EXISTS
    (
        SELECT *
        FROM dbo.PickListValue
        WHERE PickListId = @intPicklistId
              AND TenantId = @guidTenantId
              AND [Key] = @strKey
              AND IsDeletetd = 0
    )
    BEGIN
        SET @intReturn = 1;
    END;
    ELSE
    BEGIN
        INSERT INTO dbo.PickListValue
        (
            TenantId,
            Id,
            PickListId,
            [Key],
            [Text],
            Active,
            IsDeletetd,
            Flagged,
            UpdatedBy,
            UpdatedDate
        )
        VALUES
        (@guidTenantId,
         @guidId,
         @intPicklistId,
         @strKey,
         @strText,
         @bitActive,
         @bitIsDeletetd,
         @bitFlagged,
         @guidUpdatedBy,
         GETUTCDATE()
        );
        SET @intReturn = 0;
    END;
END;

GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_Create_Favourite]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_Create_Favourite]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PicklistValue_Create_Favourite] AS' 
END
GO

ALTER PROCEDURE [dbo].[PicklistValue_Create_Favourite]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @intPicklistId SMALLINT,
    @guidPicklistValueId UNIQUEIDENTIFIER,
    @guidUserId UNIQUEIDENTIFIER
)
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO dbo.PickListValueFavourite
    (
        TenantId,
        PickListId,
        PickListValueId,
        [User]
    )
    VALUES
    (   @guidTenantId,        -- TenantId - xSmallText
        @intPicklistId,       -- PickListId - smallint
        @guidPicklistValueId, -- PickListValueId - uniqueidentifier
        @guidUserId           -- User - uniqueidentifier
    );
END;

GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_Currency_Clone]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_Currency_Clone]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PicklistValue_Currency_Clone] AS' 
END
GO
ALTER PROCEDURE [dbo].[PicklistValue_Currency_Clone]
(
    @picklistId xSmallText,
    @rootTenantId UNIQUEIDENTIFIER,
    @initilizedTenantId UNIQUEIDENTIFIER
)
AS
BEGIN

    INSERT INTO dbo.PickListValueForCurrency
    (
        TenantId,
        Id,
        PickListValueId,
        DecimalPrecision,
        DecimalVisualization,
        IsoCode
    )
    SELECT @initilizedTenantId,
           NEWID(),
           (
               SELECT TOP (1)
                   Id
               FROM dbo.PickListValue
               WHERE TenantId = @initilizedTenantId
                     AND PickListId = pv.PickListId
                     AND [Key] = pv.[Key]
                     AND [Text] = pv.[Text]
           ), --get id from intiliast id of currency	
           pvc.DecimalPrecision,
           pvc.DecimalVisualization,
           pvc.IsoCode
    FROM dbo.PickListValueForCurrency AS pvc
        INNER JOIN dbo.PickListValue AS pv
            ON pvc.PickListValueId = pv.Id
    WHERE pv.PickListId = @picklistId
          AND pv.TenantId = @rootTenantId;



END;

GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_Currency_ExtendedValue_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_Currency_ExtendedValue_Create]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PicklistValue_Currency_ExtendedValue_Create] AS' 
END
GO


ALTER PROCEDURE [dbo].[PicklistValue_Currency_ExtendedValue_Create]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidId UNIQUEIDENTIFIER,
    @guidPicklistValueId UNIQUEIDENTIFIER,
    @intDecimalPrecision TINYINT,
    @intDecimalVisualization TINYINT,
    @strIsoCode smallText
)
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO dbo.PickListValueForCurrency
    (
        TenantId,
        Id,
        PickListValueId,
        DecimalPrecision,
        DecimalVisualization,
        IsoCode
    )
    VALUES
    (   @guidTenantId,            -- TenantId - xSmallText
        @guidId,                  -- Id - uniqueidentifier
        @guidPicklistValueId,     -- PickListValueId - uniqueidentifier
        @intDecimalPrecision,     -- DecimalPrecision - tinyint
        @intDecimalVisualization, -- DecimalVisualization - tinyint
        @strIsoCode               -- IsoCode - smallText
    );
END;

GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_Currency_ExtendedValue_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_Currency_ExtendedValue_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PicklistValue_Currency_ExtendedValue_Update] AS' 
END
GO

ALTER PROCEDURE [dbo].[PicklistValue_Currency_ExtendedValue_Update]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidId UNIQUEIDENTIFIER,
    @guidPicklistValueId UNIQUEIDENTIFIER,
    @intDecimalPrecision TINYINT,
    @intDecimalVisualization TINYINT,
    @strIsoCode smallText
)
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE dbo.PickListValueForCurrency
    SET [DecimalPrecision] = @intDecimalPrecision,
        [DecimalVisualization] = @intDecimalVisualization,
        IsoCode = @strIsoCode
    WHERE TenantId = @guidTenantId
          AND Id = @guidId
          AND PickListValueId = @guidPicklistValueId;
END;

GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_Delete]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_Delete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PicklistValue_Delete] AS' 
END
GO


ALTER PROCEDURE [dbo].[PicklistValue_Delete]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidId UNIQUEIDENTIFIER,
    @intPicklistId SMALLINT
)
AS
BEGIN
    SET NOCOUNT ON;
    
    DELETE FROM dbo.PickListValue
    WHERE Id = @guidId
          AND TenantId = @guidTenantId
          AND PickListId = @intPicklistId;
END;

GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_Favourite_Delete]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_Favourite_Delete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PicklistValue_Favourite_Delete] AS' 
END
GO

ALTER PROCEDURE [dbo].[PicklistValue_Favourite_Delete]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidPicklistValueId UNIQUEIDENTIFIER,
    @intPicklistId SMALLINT,
    @guidUserId UNIQUEIDENTIFIER
)
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM dbo.PickListValueFavourite
    WHERE PickListId = @intPicklistId
          AND PickListValueId = @guidPicklistValueId
          AND TenantId = @guidTenantId
          AND [User] = @guidUserId;
END;

GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_GetAll]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_GetAll]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PicklistValue_GetAll] AS' 
END
GO


ALTER PROCEDURE [dbo].[PicklistValue_GetAll]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @intPageNumber INT,
    @intPageSize INT,
    @strSearchText smallText = NULL,
    @bitIsdeleted BIT = NULL,
    @intPicklistId SMALLINT
)
AS
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT Id,
           PickListId,
           [Key],
           [Text],
           Active,
           IsDeletetd,
           Flagged,
           UpdatedBy,
           UpdatedDate
    FROM dbo.PickListValue
    WHERE IsDeletetd = @bitIsdeleted
    ORDER BY [Key] OFFSET @intPageSize * (@intPageNumber - 1) ROWS FETCH NEXT @intPageSize ROWS ONLY
    OPTION (RECOMPILE);
END;

GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_GetBy_Id]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_GetBy_Id]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PicklistValue_GetBy_Id] AS' 
END
GO


ALTER PROCEDURE [dbo].[PicklistValue_GetBy_Id]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidId UNIQUEIDENTIFIER,
    @intPicklistId SMALLINT
)
AS
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT Id,
           PickListId,
           [Key],
           Text,
           Active,
           IsDeletetd,
           Flagged,
           UpdatedBy,
           UpdatedDate
    FROM dbo.PickListValue
    WHERE Id = @guidId
          AND TenantId = @guidTenantId
          AND PickListId = @intPicklistId;
END;

GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_GetFavourite]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_GetFavourite]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PicklistValue_GetFavourite] AS' 
END
GO

ALTER PROCEDURE [dbo].[PicklistValue_GetFavourite]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @intPicklistId SMALLINT,
    @guidUserId UNIQUEIDENTIFIER
)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT PickListId,
           PickListValueId,
           [User]
    FROM dbo.PickListValueFavourite
    WHERE PickListId = @intPicklistId
          AND TenantId = @guidTenantId
          AND [User] = @guidUserId;
END;

GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_Language_Clone]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_Language_Clone]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PicklistValue_Language_Clone] AS' 
END
GO
ALTER PROCEDURE [dbo].[PicklistValue_Language_Clone]
(
    @picklistId xSmallText,
    @rootTenantId UNIQUEIDENTIFIER,
    @initilizedTenantId UNIQUEIDENTIFIER
)
AS
BEGIN

    INSERT INTO dbo.PickListValueForLanguage
    (
        TenantId,
        Id,
        PickListValueId,
        [DateFormat],
        IsoCode
    )
    SELECT @initilizedTenantId,
           NEWID(),
           (
               SELECT TOP (1)
                   Id
               FROM dbo.PickListValue
               WHERE TenantId = @initilizedTenantId
                     AND PickListId = pv.PickListId
                     AND [Key] = pv.[Key]
                     AND [Text] = pv.[Text]
           ), --get id from intiliast id of language
           pvl.[DateFormat],
           pvl.IsoCode
    FROM dbo.PickListValueForLanguage AS pvl
        INNER JOIN dbo.PickListValue AS pv
            ON pvl.PickListValueId = pv.Id
    WHERE pv.PickListId = @picklistId
          AND pv.TenantId = @rootTenantId;


END;

GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_Language_ExtendedValue_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_Language_ExtendedValue_Create]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PicklistValue_Language_ExtendedValue_Create] AS' 
END
GO


ALTER PROCEDURE [dbo].[PicklistValue_Language_ExtendedValue_Create]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidId UNIQUEIDENTIFIER,
    @guidPicklistValueId UNIQUEIDENTIFIER,
    @strDateFormat smallText,
    @strIsoCode smallText
)
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO dbo.PickListValueForLanguage
    (
        TenantId,
        Id,
        PickListValueId,
        DateFormat,
        IsoCode
    )
    VALUES
    (   @guidTenantId,        -- TenantId - xSmallText
        @guidId,              -- Id - uniqueidentifier
        @guidPicklistValueId, -- PickListValueId - uniqueidentifier
        @strDateFormat,       -- DateFormat - smallText
        @strIsoCode           -- IsoCode - smallText
    );

END;

GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_Language_ExtendedValue_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_Language_ExtendedValue_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PicklistValue_Language_ExtendedValue_Update] AS' 
END
GO

ALTER PROCEDURE [dbo].[PicklistValue_Language_ExtendedValue_Update]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidId UNIQUEIDENTIFIER,
    @guidPicklistValueId UNIQUEIDENTIFIER,
    @strDateFormat smallText,
    @strIsoCode smallText
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.PickListValueForLanguage
    SET [DateFormat] = @strDateFormat,
        [IsoCode] = @strIsoCode
    WHERE TenantId = @guidTenantId
          AND Id = @guidId
          AND PickListValueId = @guidPicklistValueId;
END;

GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_MenuGroup_Clone]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_MenuGroup_Clone]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PicklistValue_MenuGroup_Clone] AS' 
END
GO
ALTER PROCEDURE [dbo].[PicklistValue_MenuGroup_Clone]
(
    @picklistId xSmallText,
    @rootTenantId UNIQUEIDENTIFIER,
    @initilizedTenantId UNIQUEIDENTIFIER
)
AS
BEGIN

    INSERT INTO dbo.PickListValueForMenuGroup
    (
        TenantId,
        Id,
        PickListValueId,
        IconClass
    )
    SELECT @initilizedTenantId,
           NEWID(),
           (
               SELECT TOP (1)
                   Id
               FROM dbo.PickListValue
               WHERE TenantId = @initilizedTenantId
                     AND PickListId = pv.PickListId
                     AND [Key] = pv.[Key]
                     AND [Text] = pv.[Text]
           ), --get id from intiliast id of menugroup
           pvmg.IconClass
    FROM dbo.PickListValueForMenuGroup AS pvmg
        INNER JOIN dbo.PickListValue AS pv
            ON pvmg.PickListValueId = pv.Id
    WHERE pv.PickListId = @picklistId
          AND pv.TenantId = @rootTenantId;

END;

GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_Municipality_Clone]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_Municipality_Clone]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PicklistValue_Municipality_Clone] AS' 
END
GO
ALTER PROCEDURE [dbo].[PicklistValue_Municipality_Clone]
(
    @picklistId xSmallText,
    @rootTenantId UNIQUEIDENTIFIER,
    @initilizedTenantId UNIQUEIDENTIFIER
)
AS
BEGIN

    DECLARE @countryContext SMALLINT = '20002';
    DECLARE @stateContext SMALLINT = '20005';

    INSERT INTO dbo.PickListValueForMunicipality
    (
        TenantId,
        Id,
        PickListValueId,
        CountryId,
        StateId
    )
    SELECT @initilizedTenantId,
           NEWID(),
           (
               SELECT TOP (1)
                   Id
               FROM dbo.PickListValue
               WHERE TenantId = @initilizedTenantId
                     AND PickListId = pv.PickListId
                     AND [Key] = pv.[Key]
                     AND [Text] = pv.[Text]
           ), --get id from intiliast id of municipality
           (
               SELECT TOP (1)
                   Id
               FROM dbo.PickListValue
               WHERE TenantId = @initilizedTenantId
                     AND PickListId = cou.PickListId
                     AND [Key] = cou.[Key]
                     AND [Text] = cou.[Text]
           ), --get id from intiliast id of country
           (
               SELECT TOP (1)
                   Id
               FROM dbo.PickListValue
               WHERE TenantId = @initilizedTenantId
                     AND PickListId = sta.PickListId
                     AND [Key] = sta.[Key]
                     AND [Text] = sta.[Text]
           )  --get id from intiliast id of state
    FROM dbo.PickListValueForMunicipality AS pvm
        INNER JOIN dbo.PickListValue AS pv
            ON pvm.PickListValueId = pv.Id
        LEFT JOIN dbo.PickListValue AS cou
            ON cou.Id = pvm.CountryId
               AND cou.PickListId = @countryContext
        LEFT JOIN dbo.PickListValue AS sta
            ON sta.Id = pvm.StateId
               AND sta.PickListId = @stateContext
    WHERE pv.PickListId = @picklistId
          AND pv.TenantId = @rootTenantId;

END;

GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_Municipality_ExtendedValue_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_Municipality_ExtendedValue_Create]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PicklistValue_Municipality_ExtendedValue_Create] AS' 
END
GO


--  ▄████  ▄▄▄       █    ██  ██▀███   ▄▄▄       ▄▄▄▄   
-- ██▒ ▀█▒▒████▄     ██  ▓██▒▓██ ▒ ██▒▒████▄    ▓█████▄ 
--▒██░▄▄▄░▒██  ▀█▄  ▓██  ▒██░▓██ ░▄█ ▒▒██  ▀█▄  ▒██▒ ▄██
--░▓█  ██▓░██▄▄▄▄██ ▓▓█  ░██░▒██▀▀█▄  ░██▄▄▄▄██ ▒██░█▀  
--░▒▓███▀▒ ▓█   ▓██▒▒▒█████▓ ░██▓ ▒██▒ ▓█   ▓██▒░▓█  ▀█▓
-- ░▒   ▒  ▒▒   ▓▒█░░▒▓▒ ▒ ▒ ░ ▒▓ ░▒▓░ ▒▒   ▓▒█░░▒▓███▀▒
--  ░   ░   ▒   ▒▒ ░░░▒░ ░ ░   ░▒ ░ ▒░  ▒   ▒▒ ░▒░▒   ░ 
--░ ░   ░   ░   ▒    ░░░ ░ ░   ░░   ░   ░   ▒    ░    ░ 
--      ░       ░  ░   ░        ░           ░  ░ ░      
--                                                    ░ 



ALTER PROCEDURE [dbo].[PicklistValue_Municipality_ExtendedValue_Create]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidId UNIQUEIDENTIFIER,
    @guidPicklistValueId UNIQUEIDENTIFIER,
    @guidCountryId UNIQUEIDENTIFIER,
    @guidStateId UNIQUEIDENTIFIER
)
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO dbo.PickListValueForMunicipality
    (
        TenantId,
        Id,
        PickListValueId,
        CountryId,
        StateId
    )
    VALUES
    (   @guidTenantId,        -- TenantId - xSmallText
        @guidId,              -- Id - uniqueidentifier
        @guidPicklistValueId, -- PickListValueId - uniqueidentifier
        @guidCountryId,       -- CountryId - uniqueidentifier
        @guidStateId          -- StateId - uniqueidentifier
    );
END;

GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_Municipality_ExtendedValue_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_Municipality_ExtendedValue_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PicklistValue_Municipality_ExtendedValue_Update] AS' 
END
GO
--  ▄████  ▄▄▄       █    ██  ██▀███   ▄▄▄       ▄▄▄▄   
-- ██▒ ▀█▒▒████▄     ██  ▓██▒▓██ ▒ ██▒▒████▄    ▓█████▄ 
--▒██░▄▄▄░▒██  ▀█▄  ▓██  ▒██░▓██ ░▄█ ▒▒██  ▀█▄  ▒██▒ ▄██
--░▓█  ██▓░██▄▄▄▄██ ▓▓█  ░██░▒██▀▀█▄  ░██▄▄▄▄██ ▒██░█▀  
--░▒▓███▀▒ ▓█   ▓██▒▒▒█████▓ ░██▓ ▒██▒ ▓█   ▓██▒░▓█  ▀█▓
-- ░▒   ▒  ▒▒   ▓▒█░░▒▓▒ ▒ ▒ ░ ▒▓ ░▒▓░ ▒▒   ▓▒█░░▒▓███▀▒
--  ░   ░   ▒   ▒▒ ░░░▒░ ░ ░   ░▒ ░ ▒░  ▒   ▒▒ ░▒░▒   ░ 
--░ ░   ░   ░   ▒    ░░░ ░ ░   ░░   ░   ░   ▒    ░    ░ 
--      ░       ░  ░   ░        ░           ░  ░ ░      
--                                                    ░ 


ALTER PROCEDURE [dbo].[PicklistValue_Municipality_ExtendedValue_Update]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidId UNIQUEIDENTIFIER,
    @guidPicklistValueId UNIQUEIDENTIFIER,
    @guidCountryId UNIQUEIDENTIFIER,
    @guidStateId UNIQUEIDENTIFIER
)
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE dbo.PickListValueForMunicipality
    SET [CountryId] = @guidCountryId,
        [StateId] = @guidStateId
    WHERE TenantId = @guidTenantId
          AND Id = @guidId
          AND PickListValueId = @guidPicklistValueId;
END;

GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_SecurityFunction_Clone]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_SecurityFunction_Clone]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PicklistValue_SecurityFunction_Clone] AS' 
END
GO
ALTER PROCEDURE [dbo].[PicklistValue_SecurityFunction_Clone]
(
    @picklistId xSmallText,
    @rootTenantId UNIQUEIDENTIFIER,
    @initilizedTenantId UNIQUEIDENTIFIER
)
AS
BEGIN

    INSERT INTO dbo.PickListValueForSecurityFunction
    (
        TenantId,
        Id,
        PickListValueId,
        scopeEntityId
    )
    SELECT @initilizedTenantId,
           NEWID(),
           (
               SELECT TOP (1)
                   Id
               FROM dbo.PickListValue
               WHERE TenantId = @initilizedTenantId
                     AND PickListId = pv.PickListId
                     AND [Key] = pv.[Key]
                     AND [Text] = pv.[Text]
           ), --get id from intiliast id of security function
           pvsf.scopeEntityId
    FROM dbo.PickListValueForSecurityFunction AS pvsf
        INNER JOIN dbo.PickListValue AS pv
            ON pvsf.PickListValueId = pv.Id
    WHERE pv.PickListId = @picklistId
          AND pv.TenantId = @rootTenantId;

END;

GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_SoftDelete]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_SoftDelete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PicklistValue_SoftDelete] AS' 
END
GO


ALTER PROCEDURE [dbo].[PicklistValue_SoftDelete]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidId UNIQUEIDENTIFIER,
    @guidUpdatedBy UNIQUEIDENTIFIER,
    @intPicklistId SMALLINT
)
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE dbo.PickListValue
    SET IsDeletetd = 1,
        UpdatedBy = @guidUpdatedBy,
        UpdatedDate = GETUTCDATE()
    WHERE Id = @guidId
          AND TenantId = @guidTenantId
          AND PickListId = @intPicklistId;
END;

GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_State_Clone]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_State_Clone]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PicklistValue_State_Clone] AS' 
END
GO
ALTER PROCEDURE [dbo].[PicklistValue_State_Clone]
(
    @picklistId xSmallText,
    @rootTenantId UNIQUEIDENTIFIER,
    @initilizedTenantId UNIQUEIDENTIFIER
)
AS
BEGIN

    DECLARE @countryContext SMALLINT = '20002';

    INSERT INTO dbo.PickListValueForState
    (
        TenantId,
        Id,
        PickListValueId,
        CountryId
    )
    SELECT @initilizedTenantId,
           NEWID(),
           (
               SELECT TOP (1)
                   Id
               FROM dbo.PickListValue
               WHERE TenantId = @initilizedTenantId
                     AND PickListId = pv.PickListId
                     AND [Key] = pv.[Key]
                     AND [Text] = pv.[Text]
           ), --get id from intiliast id of state
           (
               SELECT TOP (1)
                   Id
               FROM dbo.PickListValue
               WHERE TenantId = @initilizedTenantId
                     AND PickListId = cou.PickListId
                     AND [Key] = cou.[Key]
                     AND [Text] = cou.[Text]
           )  --get id from intiliast id of country	
    FROM dbo.PickListValueForState AS pvs
        INNER JOIN dbo.PickListValue AS pv
            ON pvs.PickListValueId = pv.Id
        LEFT JOIN dbo.PickListValue AS cou
            ON cou.Id = pvs.CountryId
               AND cou.PickListId = @countryContext
    WHERE pv.PickListId = @picklistId
          AND pv.TenantId = @rootTenantId;

END;

GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_State_ExtendedValue_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_State_ExtendedValue_Create]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PicklistValue_State_ExtendedValue_Create] AS' 
END
GO


--  ▄████  ▄▄▄       █    ██  ██▀███   ▄▄▄       ▄▄▄▄   
-- ██▒ ▀█▒▒████▄     ██  ▓██▒▓██ ▒ ██▒▒████▄    ▓█████▄ 
--▒██░▄▄▄░▒██  ▀█▄  ▓██  ▒██░▓██ ░▄█ ▒▒██  ▀█▄  ▒██▒ ▄██
--░▓█  ██▓░██▄▄▄▄██ ▓▓█  ░██░▒██▀▀█▄  ░██▄▄▄▄██ ▒██░█▀  
--░▒▓███▀▒ ▓█   ▓██▒▒▒█████▓ ░██▓ ▒██▒ ▓█   ▓██▒░▓█  ▀█▓
-- ░▒   ▒  ▒▒   ▓▒█░░▒▓▒ ▒ ▒ ░ ▒▓ ░▒▓░ ▒▒   ▓▒█░░▒▓███▀▒
--  ░   ░   ▒   ▒▒ ░░░▒░ ░ ░   ░▒ ░ ▒░  ▒   ▒▒ ░▒░▒   ░ 
--░ ░   ░   ░   ▒    ░░░ ░ ░   ░░   ░   ░   ▒    ░    ░ 
--      ░       ░  ░   ░        ░           ░  ░ ░      
--                                                    ░ 



ALTER PROCEDURE [dbo].[PicklistValue_State_ExtendedValue_Create]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidId UNIQUEIDENTIFIER,
    @guidPicklistValueId UNIQUEIDENTIFIER,
    @guidCountryId UNIQUEIDENTIFIER
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.PickListValueForState
    (
        TenantId,
        Id,
        PickListValueId,
        CountryId
    )
    VALUES
    (   @guidTenantId,        -- TenantId - xSmallText
        @guidId,              -- Id - uniqueidentifier
        @guidPicklistValueId, -- PickListValueId - uniqueidentifier
        @guidCountryId        -- CountryId - uniqueidentifier
    );



END;

GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_State_ExtendedValue_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_State_ExtendedValue_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PicklistValue_State_ExtendedValue_Update] AS' 
END
GO
--  ▄████  ▄▄▄       █    ██  ██▀███   ▄▄▄       ▄▄▄▄   
-- ██▒ ▀█▒▒████▄     ██  ▓██▒▓██ ▒ ██▒▒████▄    ▓█████▄ 
--▒██░▄▄▄░▒██  ▀█▄  ▓██  ▒██░▓██ ░▄█ ▒▒██  ▀█▄  ▒██▒ ▄██
--░▓█  ██▓░██▄▄▄▄██ ▓▓█  ░██░▒██▀▀█▄  ░██▄▄▄▄██ ▒██░█▀  
--░▒▓███▀▒ ▓█   ▓██▒▒▒█████▓ ░██▓ ▒██▒ ▓█   ▓██▒░▓█  ▀█▓
-- ░▒   ▒  ▒▒   ▓▒█░░▒▓▒ ▒ ▒ ░ ▒▓ ░▒▓░ ▒▒   ▓▒█░░▒▓███▀▒
--  ░   ░   ▒   ▒▒ ░░░▒░ ░ ░   ░▒ ░ ▒░  ▒   ▒▒ ░▒░▒   ░ 
--░ ░   ░   ░   ▒    ░░░ ░ ░   ░░   ░   ░   ▒    ░    ░ 
--      ░       ░  ░   ░        ░           ░  ░ ░      
--                                                    ░ 


ALTER PROCEDURE [dbo].[PicklistValue_State_ExtendedValue_Update]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidId UNIQUEIDENTIFIER,
    @guidPicklistValueId UNIQUEIDENTIFIER,
    @guidCountryId UNIQUEIDENTIFIER
)
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE dbo.PickListValueForState
    SET [CountryId] = @guidCountryId
    WHERE TenantId = @guidTenantId
          AND Id = @guidId
          AND PickListValueId = @guidPicklistValueId;
END;

GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_Timezone_Clone]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_Timezone_Clone]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PicklistValue_Timezone_Clone] AS' 
END
GO
ALTER PROCEDURE [dbo].[PicklistValue_Timezone_Clone]
(
    @picklistId xSmallText,
    @rootTenantId UNIQUEIDENTIFIER,
    @initilizedTenantId UNIQUEIDENTIFIER
)
AS
BEGIN

    INSERT INTO dbo.PickListValueForTimeZone
    (
        TenantId,
        Id,
        PickListValueId,
        GmtDeviation,
        SummerTimeStart,
        WinterTimeStart
    )
    SELECT @initilizedTenantId,
           NEWID(),
           (
               SELECT TOP (1)
                   Id
               FROM dbo.PickListValue
               WHERE TenantId = @initilizedTenantId
                     AND PickListId = pv.PickListId
                     AND [Key] = pv.[Key]
                     AND [Text] = pv.[Text]
           ), --get id from intiliast id of time zone
           pvt.GmtDeviation,
           pvt.SummerTimeStart,
           pvt.WinterTimeStart
    FROM dbo.PickListValueForTimeZone AS pvt
        INNER JOIN dbo.PickListValue AS pv
            ON pvt.PickListValueId = pv.Id
    WHERE pv.PickListId = @picklistId
          AND pv.TenantId = @rootTenantId;

END;

GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_Timezone_ExtendedValue_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_Timezone_ExtendedValue_Create]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PicklistValue_Timezone_ExtendedValue_Create] AS' 
END
GO


--  ▄████  ▄▄▄       █    ██  ██▀███   ▄▄▄       ▄▄▄▄   
-- ██▒ ▀█▒▒████▄     ██  ▓██▒▓██ ▒ ██▒▒████▄    ▓█████▄ 
--▒██░▄▄▄░▒██  ▀█▄  ▓██  ▒██░▓██ ░▄█ ▒▒██  ▀█▄  ▒██▒ ▄██
--░▓█  ██▓░██▄▄▄▄██ ▓▓█  ░██░▒██▀▀█▄  ░██▄▄▄▄██ ▒██░█▀  
--░▒▓███▀▒ ▓█   ▓██▒▒▒█████▓ ░██▓ ▒██▒ ▓█   ▓██▒░▓█  ▀█▓
-- ░▒   ▒  ▒▒   ▓▒█░░▒▓▒ ▒ ▒ ░ ▒▓ ░▒▓░ ▒▒   ▓▒█░░▒▓███▀▒
--  ░   ░   ▒   ▒▒ ░░░▒░ ░ ░   ░▒ ░ ▒░  ▒   ▒▒ ░▒░▒   ░ 
--░ ░   ░   ░   ▒    ░░░ ░ ░   ░░   ░   ░   ▒    ░    ░ 
--      ░       ░  ░   ░        ░           ░  ░ ░      
--                                                    ░ 



ALTER PROCEDURE [dbo].[PicklistValue_Timezone_ExtendedValue_Create]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidId UNIQUEIDENTIFIER,
    @guidPicklistValueId UNIQUEIDENTIFIER,
    @decGmtDeviation DECIMAL(18, 2),
    @strSummerTimeStart xLargeText = NULL,
    @strWinterTimeStart xLargeText = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.PickListValueForTimeZone
    (
        TenantId,
        Id,
        PickListValueId,
        GmtDeviation,
        SummerTimeStart,
        WinterTimeStart
    )
    VALUES
    (   @guidTenantId,        -- TenantId - xSmallText
        @guidId,              -- Id - uniqueidentifier
        @guidPicklistValueId, -- PickListValueId - uniqueidentifier
        @decGmtDeviation,     -- GmtDeviation - decimal(18, 2)
        @strSummerTimeStart,  -- SummerTimeStart - xLargeText
        @strWinterTimeStart   -- WinterTimeStart - xLargeText
    );


END;

GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_Timezone_ExtendedValue_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_Timezone_ExtendedValue_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PicklistValue_Timezone_ExtendedValue_Update] AS' 
END
GO
--  ▄████  ▄▄▄       █    ██  ██▀███   ▄▄▄       ▄▄▄▄   
-- ██▒ ▀█▒▒████▄     ██  ▓██▒▓██ ▒ ██▒▒████▄    ▓█████▄ 
--▒██░▄▄▄░▒██  ▀█▄  ▓██  ▒██░▓██ ░▄█ ▒▒██  ▀█▄  ▒██▒ ▄██
--░▓█  ██▓░██▄▄▄▄██ ▓▓█  ░██░▒██▀▀█▄  ░██▄▄▄▄██ ▒██░█▀  
--░▒▓███▀▒ ▓█   ▓██▒▒▒█████▓ ░██▓ ▒██▒ ▓█   ▓██▒░▓█  ▀█▓
-- ░▒   ▒  ▒▒   ▓▒█░░▒▓▒ ▒ ▒ ░ ▒▓ ░▒▓░ ▒▒   ▓▒█░░▒▓███▀▒
--  ░   ░   ▒   ▒▒ ░░░▒░ ░ ░   ░▒ ░ ▒░  ▒   ▒▒ ░▒░▒   ░ 
--░ ░   ░   ░   ▒    ░░░ ░ ░   ░░   ░   ░   ▒    ░    ░ 
--      ░       ░  ░   ░        ░           ░  ░ ░      
--                                                    ░ 


ALTER PROCEDURE [dbo].[PicklistValue_Timezone_ExtendedValue_Update]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidId UNIQUEIDENTIFIER,
    @guidPicklistValueId UNIQUEIDENTIFIER,
    @decGmtDeviation DECIMAL(18, 2),
    @strSummerTimeStart xLargeText = NULL,
    @strWinterTimeStart xLargeText = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.PickListValueForTimeZone
    SET [GmtDeviation] = @decGmtDeviation,
        [SummerTimeStart] = @strSummerTimeStart,
        [WinterTimeStart] = @strWinterTimeStart
    WHERE TenantId = @guidTenantId
          AND Id = @guidId
          AND PickListValueId = @guidPicklistValueId;
END;

GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PicklistValue_Update] AS' 
END
GO

ALTER PROCEDURE [dbo].[PicklistValue_Update]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidId UNIQUEIDENTIFIER,
    @strKey mediumText,
    @strText mediumText,
    @bitActive BIT,
    @bitIsDeletetd BIT,
    @bitFlagged BIT,
    @guidUpdatedBy UNIQUEIDENTIFIER
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.PickListValue
    SET [Key] = @strKey,
        [Text] = @strText,
        Active = @bitActive,
        IsDeletetd = @bitIsDeletetd,
        Flagged = @bitFlagged,
        UpdatedBy = @guidUpdatedBy,
        UpdatedDate = GETUTCDATE()
    WHERE Id = @guidId
          AND TenantId = @guidTenantId;
END;

GO
/****** Object:  StoredProcedure [dbo].[PicklistValue_Update_Status]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PicklistValue_Update_Status]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PicklistValue_Update_Status] AS' 
END
GO

ALTER PROCEDURE [dbo].[PicklistValue_Update_Status]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidId UNIQUEIDENTIFIER,
    @guidUpdatedBy UNIQUEIDENTIFIER,
    @intPicklistId SMALLINT
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.PickListValue
    SET Active = ~ Active,
        UpdatedBy = @guidUpdatedBy,
        UpdatedDate = GETUTCDATE()
    WHERE Id = @guidId
          AND TenantId = @guidTenantId
          AND PickListId = @intPicklistId;
END;

GO
/****** Object:  StoredProcedure [dbo].[Resource_CheckDuplicateKey]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource_CheckDuplicateKey]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Resource_CheckDuplicateKey] AS' 
END
GO
 
 
ALTER PROCEDURE [dbo].[Resource_CheckDuplicateKey]  
(  
	@guidTenantId UNIQUEIDENTIFIER, 
	@strKey [dbo].[mediumText],
    @strLanguage [dbo].[xSmallText]
)  
AS   
    SET NOCOUNT ON   
     
    BEGIN  


					SELECT       Id, [Key], [Value], [Language] 
					FROM           dbo.[Resource] 
					WHERE          TenantId=@guidTenantId AND  lower([Key])= lower(@strKey) AND lower([Language])!= lower(@strLanguage)
					ORDER BY Id

		
    END  

GO
/****** Object:  StoredProcedure [dbo].[Resource_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource_Create]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Resource_Create] AS' 
END
GO
 
 
ALTER PROCEDURE [dbo].[Resource_Create]  
(  
	@guidTenantId UNIQUEIDENTIFIER, 
    @strKey [dbo].[mediumText] = null,
	@strValue [dbo].[xLargeText] = null,
	@strLanguage [dbo].[xSmallText] = null		
	,@strMessage varchar(100) output
)  
AS   
    SET NOCOUNT ON   
     
BEGIN  

			IF EXISTS (SELECT * FROM [dbo].[Resource] WHERE TenantId = @guidTenantId AND [Key] = @strKey AND [Language] = @strLanguage)
			BEGIN
					SET @strMessage = 'Resource already exits'
			END
			ELSE
			BEGIN
					INSERT INTO [dbo].[Resource] (TenantId, [Key], [Value], [Language])
					VALUES (@guidTenantId,@strKey,@strValue,@strLanguage)
			END

	


END  

GO
/****** Object:  StoredProcedure [dbo].[Resource_Create_Xml]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource_Create_Xml]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Resource_Create_Xml] AS' 
END
GO

ALTER PROCEDURE [dbo].[Resource_Create_Xml]
(	

	@guidrootTenantId uniqueidentifier,
	@XmlForResources as xml

) 

AS

	SET NOCOUNT ON
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	DECLARE @DATA TABLE
	(
		Id INT IDENTITY(1,1),
		TenantId uniqueidentifier NOT NULL,
		[Key] [dbo].[mediumText] NOT NULL,
		[Value] [dbo].[xLargeText] NOT NULL
		
	)
	
	Declare @language [dbo].[xSmallText] 
	set @language = 
	( 
	  SELECT top 1 PickListValue.[Key] 	FROM   Tenant 	
	  Left JOIN  PickListValue ON Tenant.Id = PickListValue.TenantId AND Tenant.PreferredLanguageId = PickListValue.Id
	  WHERE Tenant.Id = @guidrootTenantId
	)
	if @language is null set @language = 'en-US'
	INSERT INTO @DATA  
	SELECT 
	ref.value( './@TenantId', 'uniqueidentifier' ) as TenantId,
	ref.value( './@Key', '[dbo].[mediumText]' ) as [Key],
	ref.value( './@Value', '[dbo].[xLargeText]' ) as [Value]	
	FROM @XmlForResources.nodes('/Resources/Resource') as T(ref)

	INSERT INTO [dbo].[Resource] (TenantId, [Key], [Value], [Language])
	SELECT   TenantId,[Key],[Value],@language FROM @DATA



IF @@ERROR <> 0
BEGIN
RETURN 1
END
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[Resource_Delete]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource_Delete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Resource_Delete] AS' 
END
GO
 
 
ALTER PROCEDURE [dbo].[Resource_Delete]  
(  
	@guidTenantId UNIQUEIDENTIFIER,  
	@intId bigint
	
)  
AS   
    SET NOCOUNT ON   
     
BEGIN  

			DELETE FROM 	[dbo].[Resource]		
			WHERE TenantId = @guidTenantId AND Id = @intId
	


END  

GO
/****** Object:  StoredProcedure [dbo].[Resource_DeleteByKey]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource_DeleteByKey]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Resource_DeleteByKey] AS' 
END
GO
 
 
ALTER PROCEDURE [dbo].[Resource_DeleteByKey]  
(  
	@guidTenantId UNIQUEIDENTIFIER,  
	@strKey [dbo].[mediumText]
	
)  
AS   
    SET NOCOUNT ON   
     
BEGIN  

			DELETE FROM 	[dbo].[Resource]		
			WHERE TenantId = @guidTenantId AND [Key] = @strKey
	


END  

GO
/****** Object:  StoredProcedure [dbo].[Resource_Get]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource_Get]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Resource_Get] AS' 
END
GO
 
 
ALTER PROCEDURE [dbo].[Resource_Get]  
(  
	@guidTenantId UNIQUEIDENTIFIER, 
    @strLanguage [dbo].[xSmallText] = null
)  
AS   
    SET NOCOUNT ON   
     
    BEGIN  

		DECLARE @defaultLanguageTable AS TABLE 
		(Tenant_Code  uniqueidentifier not null, Tenent_Id uniqueidentifier not null, OrgNo varchar(50) null,
		PickListValue_Id uniqueidentifier null, PickListId int null, [Key] varchar(50) null, [Text] varchar(50) null, 
		PreferredLanguageId uniqueidentifier null)

		INSERT INTO @defaultLanguageTable(Tenant_Code, Tenent_Id, OrgNo, PickListValue_Id, PickListId, [Key], [Text], PreferredLanguageId)
		EXEC [dbo].[Tenant_GetDefaultLanguageDetails] @guidTenantId



		IF @strLanguage IS NOT NULL
		BEGIN
					DECLARE @strLanguageKey varchar(50)

					SET @strLanguageKey = (SELECT  top 1 [Key] FROM [dbo].[PickListValue]  
					where ([Text] like @strLanguage + '%' or [Key] like @strLanguage + '%') and [TenantId] =  @guidTenantId)

					IF @strLanguageKey IS NOT NULL
					SELECT       Id, [Key], [Value], [Language]
					FROM          dbo.[Resource]
					WHERE         TenantId=@guidTenantId AND [Language] = @strLanguageKey
					ORDER BY Id

					ELSE 
					BEGIN
								--SET DEFAULT LANGUAGE FROM TENANT TABLE-----					
								SET @strLanguage = (select top 1 [Key] from @defaultLanguageTable)
								--END SET DEFAULT LANGUAGE FROM TENANT TABLE--

								IF @strLanguage IS NOT NULL 
								SELECT       Id, [Key], [Value], [Language] 
								FROM           dbo.[Resource] 
								WHERE          TenantId=@guidTenantId AND [Language]=@strLanguage
								ORDER BY Id

								ELSE 
								SELECT       Id, [Key], [Value], [Language] 
								FROM           dbo.[Resource] 
								WHERE          TenantId=@guidTenantId
								ORDER BY Id
					END

		END
		ELSE 
		BEGIN
					--SET DEFAULT LANGUAGE FROM TENANT TABLE-----					
					SET @strLanguage = (select top 1 [Key] from @defaultLanguageTable)
					--END SET DEFAULT LANGUAGE FROM TENANT TABLE--

					IF @strLanguage IS NOT NULL 
					SELECT       Id, [Key], [Value], [Language] 
					FROM           dbo.[Resource] 
					WHERE          TenantId=@guidTenantId AND [Language]=@strLanguage
					ORDER BY Id

					ELSE 
					SELECT       Id, [Key], [Value], [Language] 
					FROM           dbo.[Resource] 
					WHERE          TenantId=@guidTenantId
					ORDER BY Id

		END
    END  

GO
/****** Object:  StoredProcedure [dbo].[Resource_GetALL]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource_GetALL]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Resource_GetALL] AS' 
END
GO
 
 

 
ALTER PROCEDURE [dbo].[Resource_GetALL]  
(  
	@guidTenantId UNIQUEIDENTIFIER,    
	@intPageIndex INT = null,
    @intPageSize INT = null,
	@strOrderBy varchar(50) = null
	,@strLanguage [dbo].[xSmallText] = null
)  
AS   
    SET NOCOUNT ON   
     
    BEGIN  

		DECLARE @SQL NVARCHAR(MAX) 
		DECLARE @strLanguageKey varchar(50)
		DECLARE @defaultLanguageTable AS TABLE 
		(Tenant_Code  uniqueidentifier not null, Tenent_Id uniqueidentifier not null, OrgNo varchar(50) null,
		PickListValue_Id uniqueidentifier null, PickListId int null, [Key] varchar(50) null, [Text] varchar(50) null, 
		PreferredLanguageId uniqueidentifier null)

		INSERT INTO @defaultLanguageTable(Tenant_Code, Tenent_Id, OrgNo, PickListValue_Id, PickListId, [Key], [Text], PreferredLanguageId)
		EXEC [dbo].[Tenant_GetDefaultLanguageDetails] @guidTenantId

		IF @strOrderBy IS NOT NULL
		BEGIN

						IF @strOrderBy  = ''
						SET @strOrderBy = 'Id'

						IF TRIM(LOWER(@strOrderBy)) = 'id' 
						SET @strOrderBy = 'Id'

						IF TRIM(LOWER(@strOrderBy)) = 'key' 
						SET @strOrderBy = '[Key]'

						IF TRIM(LOWER(@strOrderBy)) = 'value' 
						SET @strOrderBy = '[Value]'

						IF TRIM(LOWER(@strOrderBy)) = 'language' 
						SET @strOrderBy = '[Language]'

						
		END
		IF @strOrderBy IS NULL
		BEGIN
				SET @strOrderBy = 'Id'
		END

		IF @strLanguage = ''
		SET @strLanguage = NULL

		IF @intPageIndex IS NOT NULL AND @intPageSize IS NOT NULL
		BEGIN
				IF @intPageIndex = 0 AND @intPageSize = 0
				BEGIN
						SET @intPageIndex = NULL
						SET @intPageSize = NULL
				END
		END

		IF @intPageIndex IS NOT NULL AND @intPageSize IS NOT NULL
		BEGIN

				
				IF @strLanguage IS NOT NULL
				BEGIN
					SET @strLanguageKey = (SELECT  top 1 [Key] FROM [dbo].[PickListValue]  
					where ([Text] like @strLanguage + '%' or [Key] like @strLanguage + '%') and [TenantId] =  @guidTenantId)

					IF @strLanguageKey IS NOT NULL
					BEGIN
								--SELECT       Id, [Key], [Value], [Language]
								--FROM          dbo.[Resource]
								--WHERE         TenantId=@guidTenantId AND [Language] = @strLanguageKey
								--ORDER BY Id OFFSET @intPageSize * (@intPageIndex - 1) ROWS FETCH NEXT @intPageSize ROWS ONLY
								--OPTION (RECOMPILE);

										IF @strOrderBy = 'Id'											
										SELECT       Resrc.Id, Resrc.[Key], Resrc.[Value], Resrc.[Language], 
										(select top 1 [Text]  FROM [dbo].[PickListValue] where [Key] =   Resrc.[Language]) AS LanguageName
										FROM           dbo.[Resource] as Resrc
										WHERE          TenantId=@guidTenantId AND Resrc.[Language]=@strLanguageKey
										ORDER BY Resrc.Id OFFSET @intPageSize * (@intPageIndex - 1) ROWS FETCH NEXT @intPageSize ROWS ONLY
										OPTION (RECOMPILE);

										ELSE IF @strOrderBy = '[Key]'	
										SELECT       Resrc.Id, Resrc.[Key], Resrc.[Value], Resrc.[Language], 
										(select top 1 [Text]  FROM [dbo].[PickListValue] where [Key] =   Resrc.[Language]) AS LanguageName
										FROM           dbo.[Resource] as Resrc
										WHERE          TenantId=@guidTenantId AND Resrc.[Language]=@strLanguageKey
										ORDER BY Resrc.[Key]  OFFSET @intPageSize * (@intPageIndex - 1) ROWS FETCH NEXT @intPageSize ROWS ONLY
										OPTION (RECOMPILE);

										ELSE IF @strOrderBy = '[Value]'	
										SELECT       Resrc.Id, Resrc.[Key], Resrc.[Value], Resrc.[Language], 
										(select top 1 [Text]  FROM [dbo].[PickListValue] where [Key] =   Resrc.[Language]) AS LanguageName
										FROM           dbo.[Resource] as Resrc
										WHERE          TenantId=@guidTenantId AND Resrc.[Language]=@strLanguageKey
										ORDER BY Resrc.[Value]  OFFSET @intPageSize * (@intPageIndex - 1) ROWS FETCH NEXT @intPageSize ROWS ONLY
										OPTION (RECOMPILE);

										ELSE IF @strOrderBy = '[Language]'	
										SELECT       Resrc.Id, Resrc.[Key], Resrc.[Value], Resrc.[Language], 
										(select top 1 [Text]  FROM [dbo].[PickListValue] where [Key] =   Resrc.[Language]) AS LanguageName
										FROM           dbo.[Resource] as Resrc
										WHERE          TenantId=@guidTenantId AND Resrc.[Language]=@strLanguageKey
										ORDER BY Resrc.[Language]  OFFSET @intPageSize * (@intPageIndex - 1) ROWS FETCH NEXT @intPageSize ROWS ONLY
										OPTION (RECOMPILE);


					END
					ELSE 
					BEGIN
								--SET DEFAULT LANGUAGE FROM TENANT TABLE-----					
								SET @strLanguage = (select top 1 [Key] from @defaultLanguageTable)
								--END SET DEFAULT LANGUAGE FROM TENANT TABLE--

								IF @strLanguage IS NOT NULL 
								BEGIN
									
										IF @strOrderBy = 'Id'
										SELECT       Resrc.Id, Resrc.[Key], Resrc.[Value], Resrc.[Language], 
										(select top 1 [Text]  FROM [dbo].[PickListValue] where [Key] =   Resrc.[Language]) AS LanguageName
										FROM           dbo.[Resource] as Resrc
										WHERE          TenantId=@guidTenantId AND Resrc.[Language]=@strLanguage
										ORDER BY Resrc.Id  OFFSET @intPageSize * (@intPageIndex - 1) ROWS FETCH NEXT @intPageSize ROWS ONLY
										OPTION (RECOMPILE);	
										

										ELSE IF @strOrderBy = '[Key]'	
										SELECT       Resrc.Id, Resrc.[Key], Resrc.[Value], Resrc.[Language], 
										(select top 1 [Text]  FROM [dbo].[PickListValue] where [Key] =   Resrc.[Language]) AS LanguageName
										FROM           dbo.[Resource] as Resrc
										WHERE          TenantId=@guidTenantId AND Resrc.[Language]=@strLanguage
										ORDER BY Resrc.[Key]  OFFSET @intPageSize * (@intPageIndex - 1) ROWS FETCH NEXT @intPageSize ROWS ONLY
										OPTION (RECOMPILE);	

										ELSE IF @strOrderBy = '[Value]'	
										SELECT       Resrc.Id, Resrc.[Key], Resrc.[Value], Resrc.[Language], 
										(select top 1 [Text]  FROM [dbo].[PickListValue] where [Key] =   Resrc.[Language]) AS LanguageName
										FROM           dbo.[Resource] as Resrc
										WHERE          TenantId=@guidTenantId AND Resrc.[Language]=@strLanguage
										ORDER BY Resrc.[Value]  OFFSET @intPageSize * (@intPageIndex - 1) ROWS FETCH NEXT @intPageSize ROWS ONLY
										OPTION (RECOMPILE);	

										ELSE IF @strOrderBy = '[Language]'	
										SELECT       Resrc.Id, Resrc.[Key], Resrc.[Value], Resrc.[Language], 
										(select top 1 [Text]  FROM [dbo].[PickListValue] where [Key] =   Resrc.[Language]) AS LanguageName
										FROM           dbo.[Resource] as Resrc
										WHERE          TenantId=@guidTenantId AND Resrc.[Language]=@strLanguage
										ORDER BY Resrc.[Language]  OFFSET @intPageSize * (@intPageIndex - 1) ROWS FETCH NEXT @intPageSize ROWS ONLY
										OPTION (RECOMPILE);	



								END
								ELSE 
								BEGIN

										IF @strOrderBy = 'Id'	
										SELECT       Resrc.Id, Resrc.[Key], Resrc.[Value], Resrc.[Language], 
										(select top 1 [Text]  FROM [dbo].[PickListValue] where [Key] =   Resrc.[Language]) AS LanguageName
										FROM           dbo.[Resource] as Resrc
										WHERE          TenantId=@guidTenantId 
										ORDER BY Resrc.Id  OFFSET @intPageSize * (@intPageIndex - 1) ROWS FETCH NEXT @intPageSize ROWS ONLY
										OPTION (RECOMPILE);		

										ELSE IF @strOrderBy = '[Key]'	
										SELECT       Resrc.Id, Resrc.[Key], Resrc.[Value], Resrc.[Language], 
										(select top 1 [Text]  FROM [dbo].[PickListValue] where [Key] =   Resrc.[Language]) AS LanguageName
										FROM           dbo.[Resource] as Resrc
										WHERE          TenantId=@guidTenantId 
										ORDER BY Resrc.[Key]  OFFSET @intPageSize * (@intPageIndex - 1) ROWS FETCH NEXT @intPageSize ROWS ONLY
										OPTION (RECOMPILE);

										ELSE IF @strOrderBy = '[Value]'	
										SELECT       Resrc.Id, Resrc.[Key], Resrc.[Value], Resrc.[Language], 
										(select top 1 [Text]  FROM [dbo].[PickListValue] where [Key] =   Resrc.[Language]) AS LanguageName
										FROM           dbo.[Resource] as Resrc
										WHERE          TenantId=@guidTenantId 
										ORDER BY Resrc.[Value]  OFFSET @intPageSize * (@intPageIndex - 1) ROWS FETCH NEXT @intPageSize ROWS ONLY
										OPTION (RECOMPILE);

										ELSE IF @strOrderBy = '[Language]'	
										SELECT       Resrc.Id, Resrc.[Key], Resrc.[Value], Resrc.[Language], 
										(select top 1 [Text]  FROM [dbo].[PickListValue] where [Key] =   Resrc.[Language]) AS LanguageName
										FROM           dbo.[Resource] as Resrc
										WHERE          TenantId=@guidTenantId 
										ORDER BY Resrc.[Language]  OFFSET @intPageSize * (@intPageIndex - 1) ROWS FETCH NEXT @intPageSize ROWS ONLY
										OPTION (RECOMPILE);

								END
					END

				END
				ELSE
				BEGIN
						--SET DEFAULT LANGUAGE FROM TENANT TABLE-----					
						SET @strLanguage = (select top 1 [Key] from @defaultLanguageTable)
						--END SET DEFAULT LANGUAGE FROM TENANT TABLE--

						IF @strLanguage IS NOT NULL 
						BEGIN
										IF @strOrderBy = 'Id'
										SELECT       Resrc.Id, Resrc.[Key], Resrc.[Value], Resrc.[Language], 
										(select top 1 [Text]  FROM [dbo].[PickListValue] where [Key] =   Resrc.[Language]) AS LanguageName
										FROM           dbo.[Resource] as Resrc
										WHERE          TenantId=@guidTenantId AND Resrc.[Language]=@strLanguage
										ORDER BY Resrc.Id  OFFSET @intPageSize * (@intPageIndex - 1) ROWS FETCH NEXT @intPageSize ROWS ONLY
										OPTION (RECOMPILE);	
										

										ELSE IF @strOrderBy = '[Key]'	
										SELECT       Resrc.Id, Resrc.[Key], Resrc.[Value], Resrc.[Language], 
										(select top 1 [Text]  FROM [dbo].[PickListValue] where [Key] =   Resrc.[Language]) AS LanguageName
										FROM           dbo.[Resource] as Resrc
										WHERE          TenantId=@guidTenantId AND Resrc.[Language]=@strLanguage
										ORDER BY Resrc.[Key]  OFFSET @intPageSize * (@intPageIndex - 1) ROWS FETCH NEXT @intPageSize ROWS ONLY
										OPTION (RECOMPILE);	

										ELSE IF @strOrderBy = '[Value]'	
										SELECT       Resrc.Id, Resrc.[Key], Resrc.[Value], Resrc.[Language], 
										(select top 1 [Text]  FROM [dbo].[PickListValue] where [Key] =   Resrc.[Language]) AS LanguageName
										FROM           dbo.[Resource] as Resrc
										WHERE          TenantId=@guidTenantId AND Resrc.[Language]=@strLanguage
										ORDER BY Resrc.[Value]  OFFSET @intPageSize * (@intPageIndex - 1) ROWS FETCH NEXT @intPageSize ROWS ONLY
										OPTION (RECOMPILE);	

										ELSE IF @strOrderBy = '[Language]'	
										SELECT       Resrc.Id, Resrc.[Key], Resrc.[Value], Resrc.[Language], 
										(select top 1 [Text]  FROM [dbo].[PickListValue] where [Key] =   Resrc.[Language]) AS LanguageName
										FROM           dbo.[Resource] as Resrc
										WHERE          TenantId=@guidTenantId AND Resrc.[Language]=@strLanguage
										ORDER BY Resrc.[Language]  OFFSET @intPageSize * (@intPageIndex - 1) ROWS FETCH NEXT @intPageSize ROWS ONLY
										OPTION (RECOMPILE);	

						END
						ELSE 
						BEGIN
										IF @strOrderBy = 'Id'	
										SELECT       Resrc.Id, Resrc.[Key], Resrc.[Value], Resrc.[Language], 
										(select top 1 [Text]  FROM [dbo].[PickListValue] where [Key] =   Resrc.[Language]) AS LanguageName
										FROM           dbo.[Resource] as Resrc
										WHERE          TenantId=@guidTenantId 
										ORDER BY Resrc.Id  OFFSET @intPageSize * (@intPageIndex - 1) ROWS FETCH NEXT @intPageSize ROWS ONLY
										OPTION (RECOMPILE);		

										ELSE IF @strOrderBy = '[Key]'	
										SELECT       Resrc.Id, Resrc.[Key], Resrc.[Value], Resrc.[Language], 
										(select top 1 [Text]  FROM [dbo].[PickListValue] where [Key] =   Resrc.[Language]) AS LanguageName
										FROM           dbo.[Resource] as Resrc
										WHERE          TenantId=@guidTenantId 
										ORDER BY Resrc.[Key]  OFFSET @intPageSize * (@intPageIndex - 1) ROWS FETCH NEXT @intPageSize ROWS ONLY
										OPTION (RECOMPILE);

										ELSE IF @strOrderBy = '[Value]'	
										SELECT       Resrc.Id, Resrc.[Key], Resrc.[Value], Resrc.[Language], 
										(select top 1 [Text]  FROM [dbo].[PickListValue] where [Key] =   Resrc.[Language]) AS LanguageName
										FROM           dbo.[Resource] as Resrc
										WHERE          TenantId=@guidTenantId 
										ORDER BY Resrc.[Value]  OFFSET @intPageSize * (@intPageIndex - 1) ROWS FETCH NEXT @intPageSize ROWS ONLY
										OPTION (RECOMPILE);

										ELSE IF @strOrderBy = '[Language]'	
										SELECT       Resrc.Id, Resrc.[Key], Resrc.[Value], Resrc.[Language], 
										(select top 1 [Text]  FROM [dbo].[PickListValue] where [Key] =   Resrc.[Language]) AS LanguageName
										FROM           dbo.[Resource] as Resrc
										WHERE          TenantId=@guidTenantId 
										ORDER BY Resrc.[Language]  OFFSET @intPageSize * (@intPageIndex - 1) ROWS FETCH NEXT @intPageSize ROWS ONLY
										OPTION (RECOMPILE);
							END
				END

		
		END		
		ELSE 
		BEGIN

				IF @strLanguage IS NOT NULL
				BEGIN
					SET @strLanguageKey = (SELECT  top 1 [Key] FROM [dbo].[PickListValue]  
					where ([Text] like @strLanguage + '%' or [Key] like @strLanguage + '%') and [TenantId] =  @guidTenantId)

					IF @strLanguageKey IS NOT NULL
					BEGIN
									--SELECT       Id, [Key], [Value], [Language]
									--FROM          dbo.[Resource]
									--WHERE         TenantId=@guidTenantId AND [Language] = @strLanguageKey
									--ORDER BY Id

										IF @strOrderBy = 'Id'											
										SELECT       Resrc.Id, Resrc.[Key], Resrc.[Value], Resrc.[Language], 
										(select top 1 [Text]  FROM [dbo].[PickListValue] where [Key] =   Resrc.[Language]) AS LanguageName
										FROM           dbo.[Resource] as Resrc
										WHERE          TenantId=@guidTenantId AND Resrc.[Language]=@strLanguageKey
										ORDER BY Resrc.Id 

										ELSE IF @strOrderBy = '[Key]'	
										SELECT       Resrc.Id, Resrc.[Key], Resrc.[Value], Resrc.[Language], 
										(select top 1 [Text]  FROM [dbo].[PickListValue] where [Key] =   Resrc.[Language]) AS LanguageName
										FROM           dbo.[Resource] as Resrc
										WHERE          TenantId=@guidTenantId AND Resrc.[Language]=@strLanguageKey
										ORDER BY Resrc.[Key]  

										ELSE IF @strOrderBy = '[Value]'	
										SELECT       Resrc.Id, Resrc.[Key], Resrc.[Value], Resrc.[Language], 
										(select top 1 [Text]  FROM [dbo].[PickListValue] where [Key] =   Resrc.[Language]) AS LanguageName
										FROM           dbo.[Resource] as Resrc
										WHERE          TenantId=@guidTenantId AND Resrc.[Language]=@strLanguageKey
										ORDER BY Resrc.[Value]  

										ELSE IF @strOrderBy = '[Language]'	
										SELECT       Resrc.Id, Resrc.[Key], Resrc.[Value], Resrc.[Language], 
										(select top 1 [Text]  FROM [dbo].[PickListValue] where [Key] =   Resrc.[Language]) AS LanguageName
										FROM           dbo.[Resource] as Resrc
										WHERE          TenantId=@guidTenantId AND Resrc.[Language]=@strLanguageKey
										ORDER BY Resrc.[Language] 


					END
					ELSE 
					BEGIN
								--SET DEFAULT LANGUAGE FROM TENANT TABLE-----					
								SET @strLanguage = (select top 1 [Key] from @defaultLanguageTable)
								--END SET DEFAULT LANGUAGE FROM TENANT TABLE--

								IF @strLanguage IS NOT NULL 
								BEGIN
										IF @strOrderBy = 'Id'											
										SELECT       Resrc.Id, Resrc.[Key], Resrc.[Value], Resrc.[Language], 
										(select top 1 [Text]  FROM [dbo].[PickListValue] where [Key] =   Resrc.[Language]) AS LanguageName
										FROM           dbo.[Resource] as Resrc
										WHERE          TenantId=@guidTenantId AND Resrc.[Language]=@strLanguage
										ORDER BY Resrc.Id 

										ELSE IF @strOrderBy = '[Key]'	
										SELECT       Resrc.Id, Resrc.[Key], Resrc.[Value], Resrc.[Language], 
										(select top 1 [Text]  FROM [dbo].[PickListValue] where [Key] =   Resrc.[Language]) AS LanguageName
										FROM           dbo.[Resource] as Resrc
										WHERE          TenantId=@guidTenantId AND Resrc.[Language]=@strLanguage
										ORDER BY Resrc.[Key]  

										ELSE IF @strOrderBy = '[Value]'	
										SELECT       Resrc.Id, Resrc.[Key], Resrc.[Value], Resrc.[Language], 
										(select top 1 [Text]  FROM [dbo].[PickListValue] where [Key] =   Resrc.[Language]) AS LanguageName
										FROM           dbo.[Resource] as Resrc
										WHERE          TenantId=@guidTenantId AND Resrc.[Language]=@strLanguage
										ORDER BY Resrc.[Value]  

										ELSE IF @strOrderBy = '[Language]'	
										SELECT       Resrc.Id, Resrc.[Key], Resrc.[Value], Resrc.[Language], 
										(select top 1 [Text]  FROM [dbo].[PickListValue] where [Key] =   Resrc.[Language]) AS LanguageName
										FROM           dbo.[Resource] as Resrc
										WHERE          TenantId=@guidTenantId AND Resrc.[Language]=@strLanguage
										ORDER BY Resrc.[Language] 
								END
								ELSE 
								BEGIN

										IF @strOrderBy = 'Id'											
										SELECT       Resrc.Id, Resrc.[Key], Resrc.[Value], Resrc.[Language], 
										(select top 1 [Text]  FROM [dbo].[PickListValue] where [Key] =   Resrc.[Language]) AS LanguageName
										FROM           dbo.[Resource] as Resrc
										WHERE          TenantId=@guidTenantId 
										ORDER BY Resrc.Id 

										ELSE IF @strOrderBy = '[Key]'	
										SELECT       Resrc.Id, Resrc.[Key], Resrc.[Value], Resrc.[Language], 
										(select top 1 [Text]  FROM [dbo].[PickListValue] where [Key] =   Resrc.[Language]) AS LanguageName
										FROM           dbo.[Resource] as Resrc
										WHERE          TenantId=@guidTenantId 
										ORDER BY Resrc.[Key]  

										ELSE IF @strOrderBy = '[Value]'	
										SELECT       Resrc.Id, Resrc.[Key], Resrc.[Value], Resrc.[Language], 
										(select top 1 [Text]  FROM [dbo].[PickListValue] where [Key] =   Resrc.[Language]) AS LanguageName
										FROM           dbo.[Resource] as Resrc
										WHERE          TenantId=@guidTenantId 
										ORDER BY Resrc.[Value]  

										ELSE IF @strOrderBy = '[Language]'	
										SELECT       Resrc.Id, Resrc.[Key], Resrc.[Value], Resrc.[Language], 
										(select top 1 [Text]  FROM [dbo].[PickListValue] where [Key] =   Resrc.[Language]) AS LanguageName
										FROM           dbo.[Resource] as Resrc
										WHERE          TenantId=@guidTenantId 
										ORDER BY Resrc.[Language] 

								END


					END

				END
				ELSE
				BEGIN
								--SET DEFAULT LANGUAGE FROM TENANT TABLE-----					
								SET @strLanguage = (select top 1 [Key] from @defaultLanguageTable)
								--END SET DEFAULT LANGUAGE FROM TENANT TABLE--

								IF @strLanguage IS NOT NULL 
								BEGIN
										IF @strOrderBy = 'Id'											
										SELECT       Resrc.Id, Resrc.[Key], Resrc.[Value], Resrc.[Language], 
										(select top 1 [Text]  FROM [dbo].[PickListValue] where [Key] =   Resrc.[Language]) AS LanguageName
										FROM           dbo.[Resource] as Resrc
										WHERE          TenantId=@guidTenantId AND Resrc.[Language]=@strLanguage
										ORDER BY Resrc.Id 

										ELSE IF @strOrderBy = '[Key]'	
										SELECT       Resrc.Id, Resrc.[Key], Resrc.[Value], Resrc.[Language], 
										(select top 1 [Text]  FROM [dbo].[PickListValue] where [Key] =   Resrc.[Language]) AS LanguageName
										FROM           dbo.[Resource] as Resrc
										WHERE          TenantId=@guidTenantId AND Resrc.[Language]=@strLanguage
										ORDER BY Resrc.[Key]  

										ELSE IF @strOrderBy = '[Value]'	
										SELECT       Resrc.Id, Resrc.[Key], Resrc.[Value], Resrc.[Language], 
										(select top 1 [Text]  FROM [dbo].[PickListValue] where [Key] =   Resrc.[Language]) AS LanguageName
										FROM           dbo.[Resource] as Resrc
										WHERE          TenantId=@guidTenantId AND Resrc.[Language]=@strLanguage
										ORDER BY Resrc.[Value]  

										ELSE IF @strOrderBy = '[Language]'	
										SELECT       Resrc.Id, Resrc.[Key], Resrc.[Value], Resrc.[Language], 
										(select top 1 [Text]  FROM [dbo].[PickListValue] where [Key] =   Resrc.[Language]) AS LanguageName
										FROM           dbo.[Resource] as Resrc
										WHERE          TenantId=@guidTenantId AND Resrc.[Language]=@strLanguage
										ORDER BY Resrc.[Language] 
								END
								ELSE 
								BEGIN

										IF @strOrderBy = 'Id'											
										SELECT       Resrc.Id, Resrc.[Key], Resrc.[Value], Resrc.[Language], 
										(select top 1 [Text]  FROM [dbo].[PickListValue] where [Key] =   Resrc.[Language]) AS LanguageName
										FROM           dbo.[Resource] as Resrc
										WHERE          TenantId=@guidTenantId 
										ORDER BY Resrc.Id 

										ELSE IF @strOrderBy = '[Key]'	
										SELECT       Resrc.Id, Resrc.[Key], Resrc.[Value], Resrc.[Language], 
										(select top 1 [Text]  FROM [dbo].[PickListValue] where [Key] =   Resrc.[Language]) AS LanguageName
										FROM           dbo.[Resource] as Resrc
										WHERE          TenantId=@guidTenantId 
										ORDER BY Resrc.[Key]  

										ELSE IF @strOrderBy = '[Value]'	
										SELECT       Resrc.Id, Resrc.[Key], Resrc.[Value], Resrc.[Language], 
										(select top 1 [Text]  FROM [dbo].[PickListValue] where [Key] =   Resrc.[Language]) AS LanguageName
										FROM           dbo.[Resource] as Resrc
										WHERE          TenantId=@guidTenantId 
										ORDER BY Resrc.[Value]  

										ELSE IF @strOrderBy = '[Language]'	
										SELECT       Resrc.Id, Resrc.[Key], Resrc.[Value], Resrc.[Language], 
										(select top 1 [Text]  FROM [dbo].[PickListValue] where [Key] =   Resrc.[Language]) AS LanguageName
										FROM           dbo.[Resource] as Resrc
										WHERE          TenantId=@guidTenantId 
										ORDER BY Resrc.[Language] 

								END


					END
						
		END
    END  

GO
/****** Object:  StoredProcedure [dbo].[Resource_GetALL_BKP_20062019]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource_GetALL_BKP_20062019]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Resource_GetALL_BKP_20062019] AS' 
END
GO
 
 

 
ALTER PROCEDURE [dbo].[Resource_GetALL_BKP_20062019]  
(  
	@guidTenantId UNIQUEIDENTIFIER = NULL,    
	@intPageIndex INT = null,
    @intPageSize INT = null,
	@strOrderBy varchar(50) = null
	,@strLanguage [dbo].[xSmallText] = null
)  
AS   
    SET NOCOUNT ON   
     
    BEGIN  

	DECLARE @SQL NVARCHAR(MAX) 


		IF @intPageIndex IS NOT NULL AND @intPageSize IS NOT NULL
		BEGIN

				IF @strOrderBy IS NOT NULL
				BEGIN
						IF LOWER(@strOrderBy) = 'id' 
						SET @strOrderBy = 'Id'

						IF LOWER(@strOrderBy) = 'key' 
						SET @strOrderBy = '[Key]'

						IF LOWER(@strOrderBy) = 'value' 
						SET @strOrderBy = '[Value]'

						IF LOWER(@strOrderBy) = 'language' 
						SET @strOrderBy = '[Language]'

						IF @strOrderBy IS NULL
						BEGIN
								SET @strOrderBy = 'Id'
						END
						IF @strLanguage IS NULL
						BEGIN
								SET @SQL = 'SELECT       Id, [Key], [Value], [Language]
									FROM          dbo.[Resource]
									WHERE         TenantId= ''' + CONVERT(VARCHAR(255), @guidTenantId) + ''' 
									ORDER BY ' + @strOrderBy + ' OFFSET ' + CONVERT(VARCHAR(50), @intPageSize * (@intPageIndex - 1)) + 
									'ROWS FETCH NEXT ' + CONVERT(VARCHAR(50), @intPageSize) + ' ROWS ONLY  OPTION (RECOMPILE);'

								PRINT @SQL

								EXECUTE(@SQL);
						END
						ELSE IF @strLanguage IS NOT NULL
						BEGIN
								SET @SQL = 'SELECT       Id, [Key], [Value], [Language]
									FROM          dbo.[Resource]
									WHERE         TenantId= ''' + CONVERT(VARCHAR(255), @guidTenantId) + ''' 
									AND [Language] = ''' + @strLanguage + '''
									ORDER BY ' + @strOrderBy + ' OFFSET ' + CONVERT(VARCHAR(50), @intPageSize * (@intPageIndex - 1)) + 
									'ROWS FETCH NEXT ' + CONVERT(VARCHAR(50), @intPageSize) + ' ROWS ONLY  OPTION (RECOMPILE);'

								PRINT @SQL

								EXECUTE(@SQL);

						END

				END
				--ELSE IF @strOrderBy IS NULL AND @intPageIndex IS NOT NULL AND @intPageSize IS NOT NULL
				--BEGIN

				
				--				SET @strOrderBy = 'Id'
						
				--			SET @SQL = 'SELECT       Id, [Key], [Value], [Language]
				--			FROM          dbo.[Resource]
				--			WHERE         TenantId= ''' + CONVERT(VARCHAR(255), @guidTenantId) + '''
				--			ORDER BY ' + @strOrderBy + ' OFFSET ' + CONVERT(VARCHAR(50), @intPageSize * (@intPageIndex - 1)) + 
				--			'ROWS FETCH NEXT ' + CONVERT(VARCHAR(50), @intPageSize) + ' ROWS ONLY  OPTION (RECOMPILE);'
				--			PRINT @SQL

				--			EXECUTE(@SQL);
				--END
				ELSE 
				BEGIN

						IF @strLanguage IS NULL
						BEGIN

									SET @SQL = 'SELECT       Id, [Key], [Value], [Language]
									FROM          dbo.[Resource]
									WHERE         TenantId= ''' + CONVERT(VARCHAR(255), @guidTenantId) + ''''

									PRINT @SQL
									EXECUTE(@SQL);
						END
						ELSE IF @strLanguage IS NOT NULL
						BEGIN
									SET @SQL = 'SELECT       Id, [Key], [Value], [Language]
									FROM          dbo.[Resource]
									WHERE         TenantId= ''' + CONVERT(VARCHAR(255), @guidTenantId) + '''
									AND [Language] = ''' + @strLanguage + ''''

									PRINT @SQL
									EXECUTE(@SQL);

						END
				END

				
		
		END		
		ELSE 
		BEGIN

			

						IF @strLanguage IS NULL
						BEGIN

									SET @SQL = 'SELECT       Id, [Key], [Value], [Language]
									FROM          dbo.[Resource]
									WHERE         TenantId= ''' + CONVERT(VARCHAR(255), @guidTenantId) + ''''

									PRINT @SQL
									EXECUTE(@SQL);
						END
						ELSE IF @strLanguage IS NOT NULL
						BEGIN
									SET @SQL = 'SELECT       Id, [Key], [Value], [Language]
									FROM          dbo.[Resource]
									WHERE         TenantId= ''' + CONVERT(VARCHAR(255), @guidTenantId) + '''
									AND [Language] = ''' + @strLanguage + ''''

									PRINT @SQL
									EXECUTE(@SQL);

						END
		END
    END  

GO
/****** Object:  StoredProcedure [dbo].[Resource_GetByKeyAndLanguage]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource_GetByKeyAndLanguage]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Resource_GetByKeyAndLanguage] AS' 
END
GO
 
 
ALTER PROCEDURE [dbo].[Resource_GetByKeyAndLanguage]  
(  
	@guidTenantId UNIQUEIDENTIFIER, 
	@strKey [dbo].[mediumText],
	@strLanguage [dbo].[xSmallText]
)  
AS   
    SET NOCOUNT ON   
     
    BEGIN  

		SELECT       Resrc.Id, Resrc.[Key], Resrc.[Value], Resrc.[Language], 
		(select top 1 [Text]  FROM [dbo].[PickListValue] where [Key] =   Resrc.[Language]) AS LanguageName
		FROM           dbo.[Resource] as Resrc
		WHERE          TenantId=@guidTenantId AND Resrc.[Key]=@strKey AND Resrc.[Language]=@strLanguage
		ORDER BY Id
					
					
		
    END  

GO
/****** Object:  StoredProcedure [dbo].[Resource_GetKeyFromLanguage]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource_GetKeyFromLanguage]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Resource_GetKeyFromLanguage] AS' 
END
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE  [dbo].[Resource_GetKeyFromLanguage]
	@guidTenantId UNIQUEIDENTIFIER, 
    @strLanguage [dbo].[xSmallText] = null
	
AS
BEGIN
	SET NOCOUNT ON;
	
	IF @strLanguage is not null
					SELECT  top 1 [Key], [Text] FROM [dbo].[PickListValue]  
					where ([Text] like @strLanguage + '%' or [Key] like @strLanguage + '%') and [TenantId] =  @guidTenantId
    ELSE 
	BEGIN
		
		DECLARE @defaultLanguageTable AS TABLE 
		(Tenant_Code  uniqueidentifier not null, Tenent_Id uniqueidentifier not null, OrgNo varchar(50) null,
		PickListValue_Id uniqueidentifier null, PickListId int null, [Key] varchar(50) null, [Text] varchar(50) null, 
		PreferredLanguageId uniqueidentifier null)

		INSERT INTO @defaultLanguageTable(Tenant_Code, Tenent_Id, OrgNo, PickListValue_Id, PickListId, [Key], [Text], PreferredLanguageId)
		EXEC [dbo].[Tenant_GetDefaultLanguageDetails] @guidTenantId


		select top 1 [Key],[Text] from @defaultLanguageTable
	

	END

END

GO
/****** Object:  StoredProcedure [dbo].[Resource_SaveorUpdateorDelete]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource_SaveorUpdateorDelete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Resource_SaveorUpdateorDelete] AS' 
END
GO
 
 
ALTER PROCEDURE [dbo].[Resource_SaveorUpdateorDelete]  
(  
	@guidTenantId UNIQUEIDENTIFIER, 
    @strKey [dbo].[mediumText] = null,
	@strValue [dbo].[xLargeText] = null,
	@strLanguage [dbo].[xSmallText] = null,
	@intId bigint = null,
	@mode char(1) = null
	,@strMessage varchar(100) output
)  
AS   
    SET NOCOUNT ON   
     
BEGIN  

	IF @mode = 'S'
	BEGIN

			IF EXISTS (SELECT * FROM [dbo].[Resource] WHERE TenantId = @guidTenantId AND [Key] = @strKey AND [Language] = @strLanguage)
			BEGIN
					SET @strMessage = 'Resource already exits'
			END
			ELSE
			BEGIN
					INSERT INTO [dbo].[Resource] (TenantId, [Key], [Value], [Language])
					VALUES (@guidTenantId,@strKey,@strValue,@strLanguage)
			END
	END
	ELSE IF @mode = 'U'
	BEGIN

			IF EXISTS (SELECT * FROM [dbo].[Resource] WHERE TenantId = @guidTenantId AND [Key] = @strKey AND [Language] = @strLanguage AND Id <> @intId)
			BEGIN
					SET @strMessage = 'Resource already exits'
			END
			ELSE
			BEGIN

					UPDATE 	[dbo].[Resource]
					SET [Key] = @strKey,
					[Value] = @strValue,
					[Language] = @strLanguage
					WHERE TenantId = @guidTenantId AND Id = @intId

			END

	END
	ELSE IF @mode = 'D'
	BEGIN
			DELETE FROM 	[dbo].[Resource]		
			WHERE TenantId = @guidTenantId AND Id = @intId

	END


END  

GO
/****** Object:  StoredProcedure [dbo].[Resource_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Resource_Update] AS' 
END
GO
 
 
ALTER PROCEDURE [dbo].[Resource_Update]  
(  
	@guidTenantId UNIQUEIDENTIFIER, 
    @strKey [dbo].[mediumText],
	@strValue [dbo].[xLargeText],
	@strLanguage [dbo].[xSmallText]	
	,@intId bigint
	,@strMessage varchar(100) output
)  
AS   
    SET NOCOUNT ON   
     
BEGIN  

			IF EXISTS (SELECT * FROM [dbo].[Resource] WHERE TenantId = @guidTenantId AND [Key] = @strKey AND [Language] = @strLanguage AND Id <> @intId)
			BEGIN
					SET @strMessage = 'Resource already exits'
			END
			ELSE
			BEGIN

					UPDATE 	[dbo].[Resource]
					SET [Key] = @strKey,
					[Value] = @strValue,
					[Language] = @strLanguage
					WHERE TenantId = @guidTenantId AND Id = @intId

			END
	


END  

GO
/****** Object:  StoredProcedure [dbo].[Role_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Role_Create]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Role_Create] AS' 
END
GO


ALTER PROCEDURE [dbo].[Role_Create]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidRoleId UNIQUEIDENTIFIER,
    @strName [dbo].[mediumText],
    @byteRoleType AS TINYINT,
	@guidModifiedBy as UNIQUEIDENTIFIER
)
AS


SET NOCOUNT ON
SET ARITHABORT ON
    
BEGIN TRANSACTION	
BEGIN

    IF EXISTS
    (
        SELECT *
        FROM [dbo].[Role]
        WHERE [TenantId] = @guidTenantId
              AND [Name] = @strName
    )
    BEGIN

        RETURN 2;
    END;

    INSERT INTO [dbo].[Role]
    (
        [TenantId],
        [Id],
        [Name],
        [Type]
    )
    VALUES
    (@guidTenantId, @guidRoleId, @strName, @byteRoleType);

INSERT INTO [dbo].[Item]([TenantId],[Id],[EntityCode],[EntitySubTypeCode],[Code],[Name],[Active],[UpdatedBy],[UpdatedOn])VALUES
(@guidTenantId,@guidRoleId,'RLT00001','EN10004-ST01',NULL,@strName,1,@guidModifiedBy,GETUTCDATE())

	IF @@ERROR <> 0
	BEGIN
		ROLLBACK TRANSACTION
		RETURN 1
	END	
		

END

COMMIT TRANSACTION

RETURN 0


GO
/****** Object:  StoredProcedure [dbo].[Role_Create_Xml]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Role_Create_Xml]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Role_Create_Xml] AS' 
END
GO

ALTER PROCEDURE [dbo].[Role_Create_Xml]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @xmlRoles XML
)
AS
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    DECLARE @DATA TABLE
    (
        idx INT IDENTITY(1, 1),
        RoleId1 UNIQUEIDENTIFIER NOT NULL,
        Name1 [dbo].[mediumText] NULL,
        RoleType1 TINYINT NOT NULL
    );

    INSERT INTO @DATA
    SELECT ref.value('./@RoleId', 'uniqueidentifier') AS RoleId,
           ref.value('./@Name', '[dbo].[mediumText]') AS Name,
           ref.value('./@RoleType', 'tinyint') AS RoleType
    FROM @xmlRoles.nodes('/Roles/Role') AS T(ref);

    INSERT INTO [dbo].[Role]
    (
        [TenantId],
        [Id],
        [Name],
        [Type]
    )
    SELECT @guidTenantId,
           RoleId1,
           Name1,
           RoleType1
    FROM @DATA;

INSERT INTO Item([TenantId],[Id],[EntityCode],[EntitySubTypeCode],[Code],[Name],[Active],[UpdatedBy],[UpdatedOn])
SELECT @guidTenantId,   RoleId1,'RLT00001','EN10004-ST01',NULL, Name1,1,'00000000-0000-0000-0000-000000000000',GETUTCDATE()

    FROM @DATA;



    IF @@ERROR <> 0
    BEGIN
        RETURN 1;
    END;
    RETURN 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[Role_Delete]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Role_Delete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Role_Delete] AS' 
END
GO

ALTER PROCEDURE [dbo].[Role_Delete]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidRoleId UNIQUEIDENTIFIER
)
AS
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    DELETE FROM [dbo].[Role]
    WHERE [TenantId] = @guidTenantId
          AND [Id] = @guidRoleId;


    RETURN 0;
END;

GO
/****** Object:  StoredProcedure [dbo].[Role_Get]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Role_Get]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Role_Get] AS' 
END
GO

ALTER PROCEDURE [dbo].[Role_Get]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidRoleId UNIQUEIDENTIFIER
)
AS
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;


    SELECT [Id],
           [Name],
           [Type]
    FROM [dbo].[Role]
    WHERE [TenantId] = @guidTenantId
          AND [Id] = @guidRoleId;


END;

GO
/****** Object:  StoredProcedure [dbo].[Role_GetAll]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Role_GetAll]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Role_GetAll] AS' 
END
GO

ALTER PROCEDURE [dbo].[Role_GetAll] 
(
	@guidTenantId UNIQUEIDENTIFIER
)
AS
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;


    SELECT Id,
           [Name],
           [Type]
    FROM [dbo].[Role]
    WHERE [TenantId] = @guidTenantId;

END;

GO
/****** Object:  StoredProcedure [dbo].[Role_GetBy_Id]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Role_GetBy_Id]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Role_GetBy_Id] AS' 
END
GO

ALTER PROCEDURE [dbo].[Role_GetBy_Id] 
(
	@strUserId UNIQUEIDENTIFIER
)
AS
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT RL.Id,
           RL.[Name],
           RL.[Type]
    FROM dbo.[Role] RL
        INNER JOIN dbo.[UserInRole] UR
            ON RL.Id = UR.RoleId
    WHERE UR.UserId = @strUserId;

END;


GO
/****** Object:  StoredProcedure [dbo].[Role_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Role_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Role_Update] AS' 
END
GO

ALTER PROCEDURE [dbo].[Role_Update]
    (
      @guidTenantId UNIQUEIDENTIFIER,
      @guidRoleId  UNIQUEIDENTIFIER,   
	  @strName  [dbo].[mediumText]
    )
AS 
    BEGIN
        SET NOCOUNT ON 
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

		
		IF EXISTS(SELECT * FROM [dbo].[Role] WHERE  [TenantId]= @guidTenantId AND [Name]=@strName AND [Id]<> @guidRoleId)
		BEGIN
			RETURN 1
		END	

		UPDATE [dbo].[Role] SET [Name]=@strName WHERE [TenantId]=@guidTenantId AND [Id]=@guidRoleId
        
			
		RETURN 0
    END

GO
/****** Object:  StoredProcedure [dbo].[Roles_GetAll_ById]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Roles_GetAll_ById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Roles_GetAll_ById] AS' 
END
GO

ALTER PROCEDURE [dbo].[Roles_GetAll_ById]
    (
      @guidTenantId UNIQUEIDENTIFIER,
	  @guidRoleId UNIQUEIDENTIFIER =null
    )
AS 
    BEGIN
        SET NOCOUNT ON 
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

		
		SELECT [Id],[Name],[Type] FROM  [dbo].[Role] WHERE  [TenantId]= @guidTenantId 
		 AND  (((@guidRoleId IS NOT NULL) AND (Id = @guidRoleId)) OR (@guidRoleId IS NULL))

			
    END

GO
/****** Object:  StoredProcedure [dbo].[Rule_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Rule_Create]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Rule_Create] AS' 
END
GO


ALTER PROCEDURE [dbo].[Rule_Create]
    (
      @guidTenantId UNIQUEIDENTIFIER,
      @guidId  UNIQUEIDENTIFIER,   
	  @strRuleName  [dbo].[mediumText],
      @intRuleType AS SMALLINT,
	  @strSource NVARCHAR(MAX),
	  @strTarget NVARCHAR(MAX),
	  @guidUpdatedBy UNIQUEIDENTIFIER
	  ,@strEntityId [dbo].[xSmallText] = null
	  ,@strMessage varchar(100) output
    )
AS 
    BEGIN
        SET NOCOUNT ON 
		---SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED	
		
		--IF EXISTS(SELECT * FROM [dbo].[Rule] WHERE  Id = @Id)
		--BEGIN
			
		--	RETURN 2
		--END	

		DECLARE @guidUserID UNIQUEIDENTIFIER;
		set @guidUserID = (SELECT TOP 1 Id FROM dbo.[User] WHERE TenantId = @guidTenantId)

		IF NOT EXISTS (SELECT * from [dbo].[Rule] where TRIM(LOWER(RuleName)) = TRIM(LOWER(@strRuleName)) AND TenantId = @guidTenantId AND EntityId = @strEntityId)
		BEGIN
				INSERT  INTO [dbo].[Rule]
                (
					TenantId,Id,RuleName,RuleType,[Source],[Target],UpdatedBy,EntityId
                )
				VALUES  
				(  
					@guidTenantId, @guidId,@strRuleName,@intRuleType,@strSource, @strTarget, @guidUserID, @strEntityId
                )
			
				RETURN 0
		END
		ELSE IF EXISTS (SELECT * from [dbo].[Rule] where TRIM(LOWER(RuleName)) = TRIM(LOWER(@strRuleName)) AND TenantId = @guidTenantId AND EntityId = @strEntityId)
		BEGIN	
				SET @strMessage = 'This rule name already exists'
				RETURN 0
		END

    END

GO
/****** Object:  StoredProcedure [dbo].[Rule_Delete]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Rule_Delete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Rule_Delete] AS' 
END
GO


ALTER PROCEDURE [dbo].[Rule_Delete]
    (
     @guidTenantId UNIQUEIDENTIFIER,
     @guidId  UNIQUEIDENTIFIER
    )
AS 
    BEGIN
        SET NOCOUNT ON 
		SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED		

		DELETE FROM [dbo].[Rule] WHERE [TenantId]=@guidTenantId AND [Id]=@guidId
        
			
		RETURN 0
    END

GO
/****** Object:  StoredProcedure [dbo].[Rule_GetAll]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Rule_GetAll]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Rule_GetAll] AS' 
END
GO
-- =============================================
-- Author:		Tanmoy Raha
-- Create date: 28.05.2019
-- Description:	Get all rules by TenantId
-- =============================================
ALTER PROCEDURE [dbo].[Rule_GetAll]
(
	@guidTenantId UNIQUEIDENTIFIER
	,@strEntityId [dbo].[xSmallText] = null
)
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
    
	IF @strEntityId IS NOT NULL
	BEGIN
			SELECT TenantId,Id,RuleName,RuleType,[Source],[Target],UpdatedOn,UpdatedBy,EntityId 
			FROM [dbo].[Rule]
			WHERE TenantId = @guidTenantId AND EntityId = @strEntityId
	END
	ELSE
	BEGIN
			SELECT TenantId,Id,RuleName,RuleType,[Source],[Target],UpdatedOn,UpdatedBy,EntityId 
			FROM [dbo].[Rule]
			WHERE TenantId = @guidTenantId
	END
END

GO
/****** Object:  StoredProcedure [dbo].[Rule_GetById]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Rule_GetById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Rule_GetById] AS' 
END
GO


ALTER PROCEDURE [dbo].[Rule_GetById]
    (
      @guidTenantId UNIQUEIDENTIFIER,
	  @guidId UNIQUEIDENTIFIER 
	  ,@strEntityId [dbo].[xSmallText] = null
    )
AS 
    BEGIN
        SET NOCOUNT ON 
		SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

		IF @strEntityId IS NOT NULL
		BEGIN
				SELECT TenantId,Id,RuleName,RuleType,[Source],[Target],UpdatedOn,UpdatedBy,EntityId  
				FROM  [dbo].[Rule]
				WHERE  [TenantId]= @guidTenantId AND  [Id]=@guidId AND EntityId = @strEntityId
		END
		ELSE 
		BEGIN
				SELECT TenantId,Id,RuleName,RuleType,[Source],[Target],UpdatedOn,UpdatedBy,EntityId  
				FROM  [dbo].[Rule]
				WHERE  [TenantId]= @guidTenantId AND  [Id]=@guidId
		END
			
    END

GO
/****** Object:  StoredProcedure [dbo].[Rule_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Rule_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Rule_Update] AS' 
END
GO


ALTER PROCEDURE [dbo].[Rule_Update]
    (
      @guidTenantId UNIQUEIDENTIFIER,
      @guidId  UNIQUEIDENTIFIER,   
	  @strRuleName  [dbo].[mediumText],
      @intRuleType AS SMALLINT,
	  @strSource NVARCHAR(MAX),
	  @strTarget NVARCHAR(MAX),
	  @guidUpdatedBy UNIQUEIDENTIFIER
	  ,@strEntityId [dbo].[xSmallText] = null
	  ,@strMessage varchar(100) output
    )
AS 
    BEGIN
        SET NOCOUNT ON 
		SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

		
		IF EXISTS(SELECT * FROM [dbo].[Rule] WHERE  [TenantId] <> @guidTenantId AND [Id]<> @guidId)
		BEGIN
			RETURN 1
		END	

		DECLARE @guidUserID UNIQUEIDENTIFIER;
		set @guidUserID = (SELECT TOP 1 Id FROM dbo.[User] WHERE TenantId = @guidTenantId)

		IF NOT EXISTS (SELECT * from [dbo].[Rule] where TRIM(LOWER(RuleName)) = TRIM(LOWER(@strRuleName)) AND TenantId = @guidTenantId AND EntityId = @strEntityId AND [Id] <> @guidId)
		BEGIN

				UPDATE [dbo].[Rule] SET RuleName=@strRuleName
				,[Source]=@strSource
				,[Target]=@strTarget		
				,UpdatedBy = @guidUserID
				,EntityId = @strEntityId
				 WHERE [TenantId]=@guidTenantId AND [Id]=@guidId       
			
				RETURN 0
		END
		ELSE IF EXISTS (SELECT * from [dbo].[Rule] where TRIM(LOWER(RuleName)) = TRIM(LOWER(@strRuleName)) AND TenantId = @guidTenantId AND EntityId = @strEntityId AND [Id] <> @guidId)
		BEGIN	
				SET @strMessage = 'This rule name already exists'
				RETURN 0
		END

    END

GO
/****** Object:  StoredProcedure [dbo].[Scheduler_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Scheduler_Create]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Scheduler_Create] AS' 
END
GO


ALTER PROCEDURE [dbo].[Scheduler_Create]
    (
      @guidTenantId UNIQUEIDENTIFIER,
      @guidSchedulerId  UNIQUEIDENTIFIER,   
	  @guidBatchTypeId  UNIQUEIDENTIFIER,
      @intSyncTime int,
	  @intRecurrenceType as tinyint
    )
AS 
    BEGIN
        SET NOCOUNT ON 
		SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
	
		
		

        INSERT  INTO [dbo].[Scheduler]
                (
					[TenantId],[Id],[BatchTypeId],[SyncTime],[RecurrencePattern]
                )
        VALUES  (  @guidTenantId,@guidSchedulerId,@guidBatchTypeId ,@intSyncTime,@intRecurrenceType
                )
			
		RETURN 0
    END

GO
/****** Object:  StoredProcedure [dbo].[Scheduler_GetBy_BatchId]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Scheduler_GetBy_BatchId]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Scheduler_GetBy_BatchId] AS' 
END
GO


ALTER PROCEDURE [dbo].[Scheduler_GetBy_BatchId]
    (
      @guidTenantId UNIQUEIDENTIFIER,
      @guidBatchTypeId UNIQUEIDENTIFIER
    )
AS 
    BEGIN
        SET NOCOUNT ON 
		SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

        SELECT [Id],[BatchTypeId], [SyncTime],[RecurrencePattern] FROM [dbo].[Scheduler]  WHERE [TenantId]=@guidTenantId AND [BatchTypeId]=@guidBatchTypeId        
			
		RETURN 0
    END

GO
/****** Object:  StoredProcedure [dbo].[Scheduler_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Scheduler_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Scheduler_Update] AS' 
END
GO


ALTER PROCEDURE [dbo].[Scheduler_Update]
    (
      @guidTenantId UNIQUEIDENTIFIER,
      @guidSchedulerId  UNIQUEIDENTIFIER,   
	  @guidBatchTypeId  UNIQUEIDENTIFIER,
      @intSyncTime int,
	  @intRecurrenceType as tinyint
    )
AS 
    BEGIN
        SET NOCOUNT ON 
		SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

        UPDATE  [dbo].[Scheduler] SET [SyncTime]=@intSyncTime,[RecurrencePattern]=@intRecurrenceType WHERE [TenantId]=@guidTenantId AND [Id]=@guidSchedulerId        
			
		RETURN 0
    END

GO
/****** Object:  StoredProcedure [dbo].[SchedulerDaily_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchedulerDaily_Create]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SchedulerDaily_Create] AS' 
END
GO


ALTER PROCEDURE [dbo].[SchedulerDaily_Create]
    (
      @guidTenantId UNIQUEIDENTIFIER,
      @guidSchedulerDailyId  UNIQUEIDENTIFIER,   
	  @guidSchedulerId  UNIQUEIDENTIFIER,
      @intUnit int,
	  @intValue int=null
    )
AS 
    BEGIN
        SET NOCOUNT ON 
		SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
	
		
		

        INSERT  INTO [dbo].[SchedulerDaily]
                (
					[TenantId],[Id],[SchedulerId],[Unit],[Value]
                )
        VALUES  (  @guidTenantId,@guidSchedulerDailyId,@guidSchedulerId ,@intUnit,@intValue
                )
			
		RETURN 0
    END

GO
/****** Object:  StoredProcedure [dbo].[SchedulerDaily_Delete]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchedulerDaily_Delete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SchedulerDaily_Delete] AS' 
END
GO


ALTER PROCEDURE [dbo].[SchedulerDaily_Delete]
    (
      @guidTenantId UNIQUEIDENTIFIER,        
	  @guidSchedulerId  UNIQUEIDENTIFIER
    )
AS 
    BEGIN
        SET NOCOUNT ON 
		SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
	

        DELETE FROM   [dbo].[SchedulerDaily] WHERE [TenantId]=@guidTenantId AND [SchedulerId]=@guidSchedulerId
     
			
		RETURN 0
    END

GO
/****** Object:  StoredProcedure [dbo].[SchedulerDaily_GetBySchedulerId]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchedulerDaily_GetBySchedulerId]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SchedulerDaily_GetBySchedulerId] AS' 
END
GO


ALTER PROCEDURE [dbo].[SchedulerDaily_GetBySchedulerId]
    (
      @guidTenantId UNIQUEIDENTIFIER,
      @guidSchedulerId  UNIQUEIDENTIFIER
    )
AS 
    BEGIN
        SET NOCOUNT ON 
		SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
	
		SELECT [Id],[SchedulerId],[Unit],[Value]  FROM  [dbo].[SchedulerDaily] 
		WHERE [TenantId]=@guidTenantId AND [SchedulerId]=@guidSchedulerId
			
		RETURN 0
    END

GO
/****** Object:  StoredProcedure [dbo].[SchedulerDaily_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchedulerDaily_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SchedulerDaily_Update] AS' 
END
GO


ALTER PROCEDURE [dbo].[SchedulerDaily_Update]
    (
      @guidTenantId UNIQUEIDENTIFIER,
      @guidSchedulerDailyId  UNIQUEIDENTIFIER,   
	  @guidSchedulerId  UNIQUEIDENTIFIER,
      @intUnit int,
	  @intValue int=null
    )
AS 
    BEGIN
        SET NOCOUNT ON 
		SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
	
		
		

        UPDATE  [dbo].[SchedulerDaily] set [Unit]=@intUnit,[Value]=@intValue 
		WHERE [TenantId]=@guidTenantId AND [SchedulerId]=@guidSchedulerId
     
			
		RETURN 0
    END

GO
/****** Object:  StoredProcedure [dbo].[SchedulerMonthly_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchedulerMonthly_Create]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SchedulerMonthly_Create] AS' 
END
GO


ALTER PROCEDURE [dbo].[SchedulerMonthly_Create]
    (
      @guidTenantId UNIQUEIDENTIFIER,
      @guidSchedulerMonthlyId  UNIQUEIDENTIFIER,   
	  @guidSchedulerId  UNIQUEIDENTIFIER,
      @intUnit int,
	  @intDayValue1 int=null,
	  @intDayValue2  int=null,
	  @intTheValue1 int=null,
	  @intTheValue2 int=null,
	  @intTheValue3 int=null
    )
AS 
    BEGIN
        SET NOCOUNT ON 
		SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

        INSERT  INTO [dbo].[SchedulerMonthly]
                (
					[TenantId],[Id],[SchedulerId],[Unit],[DayValue1],[DayValue2],[TheValue1],[TheValue2],[TheValue3]
                )
        VALUES  (  @guidTenantId,@guidSchedulerMonthlyId,@guidSchedulerId ,@intUnit,@intDayValue1, @intDayValue2, @intTheValue1, @intTheValue2, @intTheValue3
                )
			
		RETURN 0
    END

GO
/****** Object:  StoredProcedure [dbo].[SchedulerMonthly_Delete]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchedulerMonthly_Delete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SchedulerMonthly_Delete] AS' 
END
GO


ALTER PROCEDURE [dbo].[SchedulerMonthly_Delete]
    (
      @guidTenantId UNIQUEIDENTIFIER, 
	  @guidSchedulerId  UNIQUEIDENTIFIER
    )
AS 
    BEGIN
        SET NOCOUNT ON 
		SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

        DELETE FROM  [dbo].[SchedulerMonthly]          WHERE [TenantId]=@guidTenantId AND [SchedulerId]=@guidSchedulerId                
		RETURN 0
    END

GO
/****** Object:  StoredProcedure [dbo].[SchedulerMonthly_GetBy_SchedulerId]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchedulerMonthly_GetBy_SchedulerId]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SchedulerMonthly_GetBy_SchedulerId] AS' 
END
GO


ALTER PROCEDURE [dbo].[SchedulerMonthly_GetBy_SchedulerId]
    (
      @guidTenantId UNIQUEIDENTIFIER,     
	  @guidSchedulerId  UNIQUEIDENTIFIER
    )
AS 
    BEGIN
        SET NOCOUNT ON 
		SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

		SELECT [Id],[SchedulerId],[Unit],[DayValue1],[DayValue2],[TheValue1],[TheValue2],[TheValue3]
        FROM [dbo].[SchedulerMonthly] 
                WHERE [TenantId]=@guidTenantId AND  [SchedulerId]=@guidSchedulerId                
		RETURN 0
    END

GO
/****** Object:  StoredProcedure [dbo].[SchedulerMonthly_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchedulerMonthly_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SchedulerMonthly_Update] AS' 
END
GO


ALTER PROCEDURE [dbo].[SchedulerMonthly_Update]
    (
      @guidTenantId UNIQUEIDENTIFIER,
      @guidSchedulerMonthlyId  UNIQUEIDENTIFIER,   
	  @guidSchedulerId  UNIQUEIDENTIFIER,
      @intUnit int,
	  @intDayValue1 int=null,
	  @intDayValue2  int=null,
	  @intTheValue1 int=null,
	  @intTheValue2 int=null,
	  @intTheValue3 int=null
    )
AS 
    BEGIN
        SET NOCOUNT ON 
		SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

        UPDATE [dbo].[SchedulerMonthly] SET 
		[Unit]=@intUnit,[DayValue1]=@intDayValue1,[DayValue2]=@intDayValue2,[TheValue1]=@intTheValue1,[TheValue2]=@intTheValue2,[TheValue3]=@intTheValue3
                WHERE [TenantId]=@guidTenantId AND [Id]=@guidSchedulerMonthlyId AND [SchedulerId]=@guidSchedulerId                
		RETURN 0
    END

GO
/****** Object:  StoredProcedure [dbo].[SchedulerWeekly_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchedulerWeekly_Create]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SchedulerWeekly_Create] AS' 
END
GO


ALTER PROCEDURE [dbo].[SchedulerWeekly_Create]
    (
      @guidTenantId UNIQUEIDENTIFIER,
      @guidSchedulerMonthlyId  UNIQUEIDENTIFIER,   
	  @guidSchedulerId  UNIQUEIDENTIFIER,
      @intValue int=null,
	  @bMonday bit,	
	  @bTuesday bit,
	  @bWednesday bit,
	  @bThrusday bit,
	  @bFriday bit,
	  @bSaturday bit,
	  @bSunday bit
    )
AS 
    BEGIN
        SET NOCOUNT ON 
		SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
		

        INSERT  INTO [dbo].[SchedulerWeekly]
                (
					[TenantId],[Id],[SchedulerId],[Value],[Monday],[Tuesday],[Wednesday],[Thrusday],[Friday],[Saturday],[Sunday]
                )
        VALUES  (  @guidTenantId,@guidSchedulerMonthlyId,@guidSchedulerId ,@intValue,@bMonday,@bTuesday,@bWednesday,@bThrusday,@bFriday, @bSaturday, @bSunday
                )
			
		RETURN 0
    END

GO
/****** Object:  StoredProcedure [dbo].[SchedulerWeekly_Delete]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchedulerWeekly_Delete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SchedulerWeekly_Delete] AS' 
END
GO


ALTER PROCEDURE [dbo].[SchedulerWeekly_Delete]
    (
      @guidTenantId UNIQUEIDENTIFIER, 
	  @guidSchedulerId  UNIQUEIDENTIFIER
    )
AS 
    BEGIN
        SET NOCOUNT ON 
		SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
		

        DELETE FROM   [dbo].[SchedulerWeekly] 	WHERE [TenantId]=@guidTenantId AND [SchedulerId]=@guidSchedulerId     
			
		RETURN 0
    END

GO
/****** Object:  StoredProcedure [dbo].[SchedulerWeekly_GetBy_SchedulerId]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchedulerWeekly_GetBy_SchedulerId]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SchedulerWeekly_GetBy_SchedulerId] AS' 
END
GO


ALTER PROCEDURE [dbo].[SchedulerWeekly_GetBy_SchedulerId]
    (
      @guidTenantId UNIQUEIDENTIFIER,     
	  @guidSchedulerId  UNIQUEIDENTIFIER
    )
AS 
    BEGIN
        SET NOCOUNT ON 
		SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

		SELECT [Id],[SchedulerId],[Value],[Monday],[Tuesday],[Wednesday],[Thrusday],[Friday],[Saturday],[Sunday]
        FROM  [dbo].[SchedulerWeekly]
                WHERE [TenantId]=@guidTenantId AND  [SchedulerId]=@guidSchedulerId                
		RETURN 0
    END

GO
/****** Object:  StoredProcedure [dbo].[SchedulerWeekly_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchedulerWeekly_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SchedulerWeekly_Update] AS' 
END
GO


ALTER PROCEDURE [dbo].[SchedulerWeekly_Update]
    (
      @guidTenantId UNIQUEIDENTIFIER,
      @guidSchedulerMonthlyId  UNIQUEIDENTIFIER,   
	  @guidSchedulerId  UNIQUEIDENTIFIER,
      @intValue int=null,
	  @bMonday bit,	
	  @bTuesday bit,
	  @bWednesday bit,
	  @bThrusday bit,
	  @bFriday bit,
	  @bSaturday bit,
	  @bSunday bit
    )
AS 
    BEGIN
        SET NOCOUNT ON 
		SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
		

        UPDATE  [dbo].[SchedulerWeekly] SET 
		[Value]=@intValue,[Monday]=@bMonday,[Tuesday]=@bTuesday,[Wednesday]=@bWednesday,[Thrusday]=@bThrusday,[Friday]=@bFriday,[Saturday]=@bSaturday,[Sunday]=@bSunday
		WHERE [TenantId]=@guidTenantId AND [SchedulerId]=@guidSchedulerId     
			
		RETURN 0
    END

GO
/****** Object:  StoredProcedure [dbo].[SchedulerYearly_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchedulerYearly_Create]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SchedulerYearly_Create] AS' 
END
GO


ALTER PROCEDURE [dbo].[SchedulerYearly_Create]
    (
      @guidTenantId UNIQUEIDENTIFIER,
      @guidSchedulerYearlyId  UNIQUEIDENTIFIER,   
	  @guidSchedulerId  UNIQUEIDENTIFIER,
      @intRecurrenceValue INT,
	  @intUnit INT,	
	  @intOnValue1 INT=null,
	  @intOnValue2 INT=null,
	  @intTheValue1 INT=null,
	  @intTheValue2 INT=null,
	  @intTheValue3 INT=null
    )
AS 
    BEGIN
        SET NOCOUNT ON 
		SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
		

        INSERT  INTO [dbo].[SchedulerYearly]
                (
					[TenantId],[Id],[SchedulerId],[RecurrenceValue],[Unit],[OnValue1],[OnValue2],[TheValue1],[TheValue2],[TheValue3]
                )
        VALUES  (  @guidTenantId,@guidSchedulerYearlyId,@guidSchedulerId , @intRecurrenceValue, @intUnit,@intOnValue1,@intOnValue2, @intTheValue1,@intTheValue2,@intTheValue3)
			
		RETURN 0
    END

GO
/****** Object:  StoredProcedure [dbo].[SchedulerYearly_Delete]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchedulerYearly_Delete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SchedulerYearly_Delete] AS' 
END
GO


ALTER PROCEDURE [dbo].[SchedulerYearly_Delete]
    (
      @guidTenantId UNIQUEIDENTIFIER,  
	  @guidSchedulerId  UNIQUEIDENTIFIER
    )
AS 
    BEGIN
        SET NOCOUNT ON 
		SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
		

       DELETE FROM  [dbo].[SchedulerYearly] WHERE [TenantId]=@guidTenantId AND [SchedulerId]=@guidSchedulerId
               
		RETURN 0
    END

GO
/****** Object:  StoredProcedure [dbo].[SchedulerYearly_GetBy_SchedulerId]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchedulerYearly_GetBy_SchedulerId]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SchedulerYearly_GetBy_SchedulerId] AS' 
END
GO


ALTER PROCEDURE [dbo].[SchedulerYearly_GetBy_SchedulerId]
    (
      @guidTenantId UNIQUEIDENTIFIER,    
	  @guidSchedulerId  UNIQUEIDENTIFIER
    )
AS 
    BEGIN
        SET NOCOUNT ON 
		SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

		SELECT [Id],[SchedulerId],[RecurrenceValue],[Unit],[OnValue1],[OnValue2],[TheValue1],[TheValue2],[TheValue3]		
		FROM [dbo].[SchedulerYearly] WHERE [TenantId]=@guidTenantId AND [SchedulerId]=@guidSchedulerId               
			
		RETURN 0
    END

GO
/****** Object:  StoredProcedure [dbo].[SchedulerYearly_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchedulerYearly_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SchedulerYearly_Update] AS' 
END
GO


ALTER PROCEDURE [dbo].[SchedulerYearly_Update]
    (
      @guidTenantId UNIQUEIDENTIFIER,
      @guidSchedulerYearlyId  UNIQUEIDENTIFIER,   
	  @guidSchedulerId  UNIQUEIDENTIFIER,
      @intRecurrenceValue INT,
	  @intUnit INT,	
	  @intOnValue1 INT=null,
	  @intOnValue2 INT=null,
	  @intTheValue1 INT=null,
	  @intTheValue2 INT=null,
	  @intTheValue3 INT=null
    )
AS 
    BEGIN
        SET NOCOUNT ON 
		SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
		

        UPDATE  [dbo].[SchedulerYearly] SET 
		[RecurrenceValue]=@intRecurrenceValue,[Unit]=@intUnit,[OnValue1]=@intOnValue1,[OnValue2]=@intOnValue2,[TheValue1]=@intTheValue1,
		[TheValue2]=@intTheValue2,[TheValue3]=@intTheValue3 
         WHERE [TenantId]=@guidTenantId AND [SchedulerId]=@guidSchedulerId
		RETURN 0
    END

GO
/****** Object:  StoredProcedure [dbo].[Setting_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Setting_Create]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Setting_Create] AS' 
END
GO

ALTER PROCEDURE [dbo].[Setting_Create]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidId UNIQUEIDENTIFIER,
	@context int,
	@content nvarchar(max),
	@guidUpdatedBy UNIQUEIDENTIFIER
)
AS
BEGIN
    
			INSERT INTO [dbo].[Settings]
			   ([TenantId]
			   ,[Id]
			   ,[Context]
			   ,[Content]
			   ,[UpdatedOn]
			   ,[UpdatedBy])
			VALUES
			   (@guidTenantId
			   ,@guidId
			   ,@context
			   ,@content
			   ,GETUTCDATE()
			   ,@guidUpdatedBy)      
   
END;

GO
/****** Object:  StoredProcedure [dbo].[Setting_DeleteById]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Setting_DeleteById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Setting_DeleteById] AS' 
END
GO

ALTER PROCEDURE [dbo].[Setting_DeleteById]
(
    @guidTenantId UNIQUEIDENTIFIER,  
    @guidId UNIQUEIDENTIFIER 

)
AS
BEGIN
    
	delete from [dbo].[Settings] where [TenantId] = @guidTenantId AND [Id] = @guidId
   
END;

GO
/****** Object:  StoredProcedure [dbo].[Setting_GetByContextType]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Setting_GetByContextType]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Setting_GetByContextType] AS' 
END
GO

ALTER PROCEDURE [dbo].[Setting_GetByContextType]
(
    @guidTenantId UNIQUEIDENTIFIER,  
    @contexttype int null

)
AS
BEGIN
SET NOCOUNT ON 
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
   
	 Select Id,Context,Content,UpdatedOn,UpdatedBy from [dbo].[Settings] where [TenantId] = @guidTenantId and Context=@contexttype

END;


GO
/****** Object:  StoredProcedure [dbo].[Setting_GetById]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Setting_GetById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Setting_GetById] AS' 
END
GO

ALTER PROCEDURE [dbo].[Setting_GetById]
(
    @guidTenantId UNIQUEIDENTIFIER,  
    @guidId UNIQUEIDENTIFIER null

)
AS
BEGIN
SET NOCOUNT ON 
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

    IF  @guidId<>null
	BEGIN
	Select Id,Context,Content,UpdatedOn,UpdatedBy from [dbo].[Settings] where [TenantId] = @guidTenantId AND [Id] = @guidId 
    END
	ELSE
	 Select Id,Context,Content,UpdatedOn,UpdatedBy from [dbo].[Settings] where [TenantId] = @guidTenantId
END;


GO
/****** Object:  StoredProcedure [dbo].[Setting_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Setting_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Setting_Update] AS' 
END
GO

ALTER PROCEDURE [dbo].[Setting_Update]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidId UNIQUEIDENTIFIER,
	@context int,
	@content nvarchar(max),
	@guidUpdatedBy UNIQUEIDENTIFIER
)
AS
BEGIN
    
			update [dbo].[Settings]
			   Set [Context]=@context
			   ,[Content]=@content
			   ,[UpdatedOn]=GETUTCDATE()
			   ,[UpdatedBy]=@guidUpdatedBy
			where  [TenantId]=@guidTenantId and [Id]=@guidId
   
END;

GO
/****** Object:  StoredProcedure [dbo].[Tenant_GetDefaultLanguageDetails]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tenant_GetDefaultLanguageDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Tenant_GetDefaultLanguageDetails] AS' 
END
GO
-- ===================================================
-- Author:		Tanmoy Raha
-- Create date: 14.06.2019
-- Description:	Get tenant default language details
-- ==================================================
ALTER PROCEDURE [dbo].[Tenant_GetDefaultLanguageDetails]
	@guidTenantId UNIQUEIDENTIFIER
	
AS
BEGIN

	SET NOCOUNT ON;
				
	--SELECT   top 1  Tenant.TenantId AS Tenant_Code, Tenant.Id AS Tenent_Id, Tenant.OrgNo, PickListValue.Id AS PickListValue_Id, 
	--PickListValue.PickListId, PickListValue.[Key], PickListValue.[Text], Tenant.PreferredLanguageId
	--FROM   Tenant 
	--INNER JOIN  PickListValue ON Tenant.TenantId = PickListValue.TenantId AND Tenant.PreferredLanguageId = PickListValue.Id
	--WHERE Tenant.TenantId = @guidTenantId
		
	SELECT   Tenant.TenantId AS Tenant_Code, Tenant.Id AS Tenent_Id, Tenant.OrgNo, PickListValue.Id AS PickListValue_Id, 
	PickListValue.PickListId, PickListValue.[Key], PickListValue.[Text], Tenant.PreferredLanguageId
	FROM   Tenant 
	left JOIN  PickListValue ON Tenant.Id = PickListValue.TenantId AND Tenant.PreferredLanguageId = PickListValue.Id
	WHERE Tenant.Id = @guidTenantId

END

GO
/****** Object:  StoredProcedure [dbo].[Tenant_GetDefaultLanguageDetails_BCK_24062019]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tenant_GetDefaultLanguageDetails_BCK_24062019]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Tenant_GetDefaultLanguageDetails_BCK_24062019] AS' 
END
GO
-- ===================================================
-- Author:		Tanmoy Raha
-- Create date: 14.06.2019
-- Description:	Get tenant default language details
-- ==================================================
ALTER PROCEDURE [dbo].[Tenant_GetDefaultLanguageDetails_BCK_24062019]
	@guidTenantId UNIQUEIDENTIFIER
	,@strOrgNo [dbo].[xSmallText] = null
AS
BEGIN

	SET NOCOUNT ON;


			IF @strOrgNo IS NULL
			BEGIN
			
						SELECT   top 1  Tenant.TenantId AS Tenant_Code, Tenant.Id AS Tenent_Id, Tenant.OrgNo, PickListValue.Id AS PickListValue_Id, 
						PickListValue.PickListId, PickListValue.[Key], PickListValue.[Text], Tenant.PreferredLanguageId
						FROM   Tenant 
						INNER JOIN  PickListValue ON Tenant.TenantId = PickListValue.TenantId AND Tenant.PreferredLanguageId = PickListValue.Id
						WHERE Tenant.TenantId = @guidTenantId
			
			END
			ELSE 
			BEGIN

						SELECT    top 1    Tenant.TenantId AS Tenant_Code, Tenant.Id AS Tenent_Id, Tenant.OrgNo, PickListValue.Id AS PickListValue_Id, 
						PickListValue.PickListId, PickListValue.[Key], PickListValue.[Text], Tenant.PreferredLanguageId
						FROM            Tenant INNER JOIN
										PickListValue ON Tenant.TenantId = PickListValue.TenantId AND Tenant.PreferredLanguageId = PickListValue.Id
						WHERE			Tenant.TenantId = @guidTenantId AND  Tenant.OrgNo = @strOrgNo
			END


END

GO
/****** Object:  StoredProcedure [dbo].[Tenant_GetTenantId]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tenant_GetTenantId]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Tenant_GetTenantId] AS' 
END
GO

ALTER PROCEDURE [dbo].[Tenant_GetTenantId]  
(   
 @strEntityId [dbo].[xSmallText]  ,   
 @strCode [dbo].[mediumText] 
)  
AS   
BEGIN  
SET NOCOUNT ON   
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED  
SELECT it.Id   
FROM   
dbo.[Item] it  
INNER JOIN [dbo].[Tenant] te ON it.Id = te.Id  
WHERE [Code] = @strCode  
AND EntityCode = @strEntityId  
  
END  
  
GO
/****** Object:  StoredProcedure [dbo].[TenantSubscription_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TenantSubscription_Create]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[TenantSubscription_Create] AS' 
END
GO


ALTER PROCEDURE [dbo].[TenantSubscription_Create]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidTenantSubscriptionId UNIQUEIDENTIFIER,
    @strName [dbo].[mediumText],
    @guidGroupId UNIQUEIDENTIFIER
)
AS
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;



    IF EXISTS
    (
        SELECT *
        FROM [dbo].[TenantSubscription]
        WHERE [TenantId] = @guidTenantId
              AND [Name] = @strName
    )
    BEGIN
        RETURN 2;
    END;

    BEGIN TRY
        BEGIN TRAN;

		INSERT INTO dbo.Item
		(
		    TenantId,
		    Id,
		    EntityCode,
		    EntitySubTypeCode,
		    Code,
		    Name,
		    Active,
		    UpdatedBy,
		    UpdatedOn
		)
		VALUES
		(   @guidTenantId,     -- TenantId - uniqueidentifier
		    @guidTenantSubscriptionId,     -- Id - uniqueidentifier
		    'EN20059',     -- EntityCode - xSmallText
		    'EN10001-ST01',      -- EntitySubTypeCode - nvarchar(30)
		     @strName,     -- Code - mediumText
		    @strName,     -- Name - largeText
		    1,     -- Active - bit
		    NEWID(),     -- UpdatedBy - uniqueidentifier
		    GETDATE() -- UpdatedOn - datetime
		    )


        INSERT INTO [dbo].[TenantSubscription]
        (
            [TenantId],
            [Id],
            [Name],
            [Group],
            [Status]
        )
        VALUES
        (@guidTenantId, @guidTenantSubscriptionId, @strName, @guidGroupId, 1);


        COMMIT TRAN;
		RETURN 0;
    END TRY
    BEGIN CATCH
        
        ROLLBACK TRAN;
		RETURN 1;
    END CATCH;

END;

GO
/****** Object:  StoredProcedure [dbo].[TenantSubscription_Delete]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TenantSubscription_Delete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[TenantSubscription_Delete] AS' 
END
GO


ALTER PROCEDURE [dbo].[TenantSubscription_Delete]
    (
      @guidTenantId UNIQUEIDENTIFIER,
      @guidTenantSubscriptionId  UNIQUEIDENTIFIER
    )
AS 

BEGIN

        SET NOCOUNT ON;
        BEGIN TRANSACTION;

        BEGIN TRY

         
                    DELETE TSED FROM [dbo].[TenantSubscriptionEntityDetail] TSED INNER JOIN [dbo].[TenantSubscriptionEntity] TSE 
					ON TSED.[TenantSubscriptionEntityId]=TSE.Id
					INNER JOIN [dbo].[TenantSubscription] TS ON TS.Id=TSE.TenantSubscriptionId
					WHERE  TS.[TenantId]= @guidTenantId  AND TS.Id =@guidTenantSubscriptionId    

	                DELETE TSE FROM  [dbo].[TenantSubscriptionEntity] TSE 					
					INNER JOIN [dbo].[TenantSubscription] TS ON TS.Id=TSE.TenantSubscriptionId
					WHERE  TS.[TenantId]= @guidTenantId  AND TS.Id =@guidTenantSubscriptionId 

	                DELETE FROM [dbo].[TenantSubscription] WHERE  [TenantId]= @guidTenantId  AND Id =@guidTenantSubscriptionId  		

           COMMIT TRANSACTION;
        END TRY
        BEGIN CATCH

            ROLLBACK TRANSACTION;
        END CATCH;
        IF ( @@ERROR <> 0 )
        begin
            RETURN 1;
        ROLLBACK TRANSACTION;
        end

        RETURN 0;
    END;

GO
/****** Object:  StoredProcedure [dbo].[TenantSubscription_Get]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TenantSubscription_Get]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[TenantSubscription_Get] AS' 
END
GO


ALTER PROCEDURE [dbo].[TenantSubscription_Get]
    (
      @guidTenantId UNIQUEIDENTIFIER,
	  @guidTenantSubscriptionId UNIQUEIDENTIFIER
    )
AS 
    BEGIN
        SET NOCOUNT ON 
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

		
		SELECT TS.Id,TS.[Name],TS.[Group],TS.[RecurringPrice],TS.[Duration],TS.[SetUpPrice],TS.[Status] ,PLV.[Text]	
		 FROM [dbo].[TenantSubscription] TS INNER JOIN [dbo].[PickListValue] PLV ON PLV.Id=TS.[Group]
		  WHERE  TS.[TenantId]= @guidTenantId  ANd TS.Id=@guidTenantSubscriptionId
			
    END

GO
/****** Object:  StoredProcedure [dbo].[TenantSubscription_GetAll]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TenantSubscription_GetAll]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[TenantSubscription_GetAll] AS' 
END
GO


ALTER PROCEDURE [dbo].[TenantSubscription_GetAll]
    (
      @guidTenantId UNIQUEIDENTIFIER
    )
AS 
    BEGIN
        SET NOCOUNT ON 
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

		
		SELECT TS.Id,TS.[Name],TS.[Group],TS.[RecurringPrice],TS.[Duration],TS.[SetUpPrice],TS.[Status] ,PLV.[Text]	
		 FROM [dbo].[TenantSubscription] TS INNER JOIN [dbo].[PickListValue] PLV ON PLV.Id=TS.[Group]		 
		 WHERE  TS.[TenantId]= @guidTenantId 
			
    END

GO
/****** Object:  StoredProcedure [dbo].[TenantSubscription_Status]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TenantSubscription_Status]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[TenantSubscription_Status] AS' 
END
GO


ALTER PROCEDURE [dbo].[TenantSubscription_Status]
    (
      @guidTenantId UNIQUEIDENTIFIER,
      @guidTenantSubscriptionId  UNIQUEIDENTIFIER
    )
AS 
    BEGIN
        SET NOCOUNT ON 
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

		UPDATE [dbo].[TenantSubscription] SET [Status]= (CASE WHEN [Status]=1 THEN 0 ELSE 1 END) WHERE  [TenantId]= @guidTenantId  AND Id =@guidTenantSubscriptionId               
  
		RETURN 0
    END

GO
/****** Object:  StoredProcedure [dbo].[TenantSubscription_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TenantSubscription_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[TenantSubscription_Update] AS' 
END
GO


ALTER PROCEDURE [dbo].[TenantSubscription_Update]
    (
      @guidTenantId UNIQUEIDENTIFIER,
      @guidTenantSubscriptionId  UNIQUEIDENTIFIER,   
	  @strName  [dbo].[mediumText],
      @guidGroupId UNIQUEIDENTIFIER,
	  @dRecurringPrice [dbo].[amount]= null,
	  @tinyintDuration tinyint,
	  @dSetUpPrice  [dbo].[amount] =null
    )
AS 
    BEGIN
        SET NOCOUNT ON 
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

		
		
		IF EXISTS(SELECT * FROM [dbo].[TenantSubscription] WHERE  [TenantId]= @guidTenantId AND [Name]=@strName AND Id !=@guidTenantSubscriptionId )
		BEGIN
			
			RETURN 2
		END	

        UPDATE [dbo].[TenantSubscription] SET
                
					
					[Name]=@strName,[Group]=@guidGroupId,[RecurringPrice]=@dRecurringPrice,[Duration]=@tinyintDuration,[SetUpPrice]=@dSetUpPrice
					WHERE [TenantId]=@guidTenantId AND [Id]=@guidTenantSubscriptionId                
  
		RETURN 0
    END

GO
/****** Object:  StoredProcedure [dbo].[TenantSubscriptionEntity_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TenantSubscriptionEntity_Create]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[TenantSubscriptionEntity_Create] AS' 
END
GO


ALTER PROCEDURE [dbo].[TenantSubscriptionEntity_Create]
    (
      @guidTenantId UNIQUEIDENTIFIER,
	  @guidTenantSubscriptionEntityId UNIQUEIDENTIFIER,
      @guidTenantSubscriptionId  UNIQUEIDENTIFIER,   
	  @strEntityId  [dbo].[xSmallText]
    )
AS 
    BEGIN
        SET NOCOUNT ON 
		SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
		
		
        INSERT  INTO [dbo].[TenantSubscriptionEntity]([TenantId],[Id] ,[TenantSubscriptionId],[EntityId] )
            
        VALUES  (  @guidTenantId,@guidTenantSubscriptionEntityId, @guidTenantSubscriptionId,@strEntityId)
			
		RETURN 0
    END

GO
/****** Object:  StoredProcedure [dbo].[TenantSubscriptionEntity_Create_Xml]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TenantSubscriptionEntity_Create_Xml]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[TenantSubscriptionEntity_Create_Xml] AS' 
END
GO

ALTER PROCEDURE [dbo].[TenantSubscriptionEntity_Create_Xml]
(
	@guidTenantId uniqueidentifier,
	@xmlEntities as xml
)
AS

SET NOCOUNT ON  
BEGIN

WITH 
	ITEMS (TenantSubscriptionEntityId,TenantSubscriptionId,EntityId,LimtNumber,LimitType) 
	AS
	(
		SELECT ref.value( './@TenantSubscriptionEntityId', 'uniqueidentifier' ) as TenantSubscriptionEntityId1, 
		       ref.value( './@TenantSubscriptionId', 'uniqueidentifier' ) as TenantSubscriptionId1, 
		       ref.value( './@EntityId', '[dbo].[xSmallText]' ) as EntityId1, 
		       ref.value( './@LimtNumber', 'INT' ) as LimtNumber1, 
		       ref.value( './@LimitType', 'TINYINT' ) as LimitType1		       
		FROM @xmlEntities.nodes('/SubscriptionEntities/SubscriptionEntity') as T(ref)
	)
    
    INSERT INTO [dbo].[TenantSubscriptionEntity]([TenantId],[Id],TenantSubscriptionId,EntityId,[LimitNumber],LimitType)
	SELECT @guidTenantId,TenantSubscriptionEntityId,TenantSubscriptionId,EntityId,LimtNumber,LimitType FROM ITEMS
	 
END
GO
/****** Object:  StoredProcedure [dbo].[TenantSubscriptionEntity_Delete]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TenantSubscriptionEntity_Delete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[TenantSubscriptionEntity_Delete] AS' 
END
GO


ALTER PROCEDURE [dbo].[TenantSubscriptionEntity_Delete]
    (
      @guidTenantId UNIQUEIDENTIFIER,
	  @guidTenantSubscriptionEntityId UNIQUEIDENTIFIER
    )
AS 
    BEGIN
        SET NOCOUNT ON 
		SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

		 DELETE TSED FROM [dbo].[TenantSubscriptionEntityDetail] TSED INNER JOIN [dbo].[TenantSubscriptionEntity] TSE 
					ON TSED.[TenantSubscriptionEntityId]=TSE.Id				
					WHERE  TSE.[TenantId]= @guidTenantId  AND TSE.Id =@guidTenantSubscriptionEntityId 

		DELETE FROM  [dbo].[TenantSubscriptionEntity] 
		WHERE [TenantId]=@guidTenantId AND [Id] =@guidTenantSubscriptionEntityId		
			
		RETURN 0
    END

GO
/****** Object:  StoredProcedure [dbo].[TenantSubscriptionEntity_Get]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TenantSubscriptionEntity_Get]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[TenantSubscriptionEntity_Get] AS' 
END
GO


ALTER PROCEDURE [dbo].[TenantSubscriptionEntity_Get]
    (
      @guidTenantId UNIQUEIDENTIFIER,
	  @guidTenantSubscriptionEntityId UNIQUEIDENTIFIER
    )
AS 
    BEGIN
        SET NOCOUNT ON 
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

		
		SELECT Id,[TenantSubscriptionId],[EntityId],[LimitNumber],[LimitType]
		FROM  [dbo].[TenantSubscriptionEntity] WHERE  [TenantId]= @guidTenantId AND [Id]=@guidTenantSubscriptionEntityId
			
    END

GO
/****** Object:  StoredProcedure [dbo].[TenantSubscriptionEntity_GetAll]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TenantSubscriptionEntity_GetAll]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[TenantSubscriptionEntity_GetAll] AS' 
END
GO


ALTER PROCEDURE [dbo].[TenantSubscriptionEntity_GetAll]
(
    @guidTenantId UNIQUEIDENTIFIER
	,@guidTenantSubscriptionId UNIQUEIDENTIFIER
)
AS
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;


    SELECT ts.Id,
           ts.TenantSubscriptionId,
           ts.EntityId,
           ts.LimitNumber,
           ts.LimitType
    FROM [dbo].[TenantSubscriptionEntity] AS ts
	INNER JOIN dbo.Tenant AS tt ON tt.Id = ts.TenantId
    WHERE ts.TenantId = @guidTenantId
          AND ts.TenantSubscriptionId = @guidTenantSubscriptionId;

END;



GO
/****** Object:  StoredProcedure [dbo].[TenantSubscriptionEntity_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TenantSubscriptionEntity_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[TenantSubscriptionEntity_Update] AS' 
END
GO


ALTER PROCEDURE [dbo].[TenantSubscriptionEntity_Update]
    (
      @guidTenantId UNIQUEIDENTIFIER,
	  @guidTenantSubscriptionEntityId UNIQUEIDENTIFIER,
      @guidTenantSubscriptionId  UNIQUEIDENTIFIER,   
	  @strEntityId  [dbo].[xSmallText],
      @intLimitNumber int =null,
	  @tinyintLimitType tinyint=null
    )
AS 
    BEGIN
        SET NOCOUNT ON 
		SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

		UPDATE [dbo].[TenantSubscriptionEntity] SET [LimitNumber]=@intLimitNumber,[LimitType]=@tinyintLimitType
		WHERE [TenantId]=@guidTenantId AND [Id] =@guidTenantSubscriptionEntityId		
			
		RETURN 0
    END

GO
/****** Object:  StoredProcedure [dbo].[TenantSubscriptionEntityDetail_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TenantSubscriptionEntityDetail_Create]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[TenantSubscriptionEntityDetail_Create] AS' 
END
GO


ALTER PROCEDURE [dbo].[TenantSubscriptionEntityDetail_Create]
    (
      @guidTenantId UNIQUEIDENTIFIER,
	  @guidSubscriptionEntityDetailId UNIQUEIDENTIFIER,
      @guidSubscriptionEntityId  UNIQUEIDENTIFIER, 
	  @guidContext UNIQUEIDENTIFIER,
	  @dRecurringPrice [dbo].[amount]=NULL,
	  @tinyintRecurringDuration tinyint=null,
	  @dOneTimePrice [dbo].[amount]=NULL,
	  @tinyintOneTimeDuration tinyint=null
    )
AS 
    BEGIN
        SET NOCOUNT ON 
		SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
		
		
        INSERT  INTO [dbo].[TenantSubscriptionEntityDetail](
		[TenantId],[Id] ,[TenantSubscriptionEntityId],[Context],[RecurringPrice],[RecurringDuration],[OneTimePrice],[OneTimeDuration]
		 )
            
        VALUES  ( 
		 @guidTenantId,@guidSubscriptionEntityDetailId,@guidSubscriptionEntityId,@guidContext,@dRecurringPrice,@tinyintRecurringDuration,
		 @dOneTimePrice,@tinyintOneTimeDuration
		 )
			
		RETURN 0
    END

GO
/****** Object:  StoredProcedure [dbo].[TenantSubscriptionEntityDetail_Create_Xml]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TenantSubscriptionEntityDetail_Create_Xml]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[TenantSubscriptionEntityDetail_Create_Xml] AS' 
END
GO

ALTER PROCEDURE [dbo].[TenantSubscriptionEntityDetail_Create_Xml]
(
	@guidTenantId uniqueidentifier,
	@xmlSubscriptionEntityDetails as xml
)
AS

SET NOCOUNT ON  
BEGIN

WITH 
	ITEMS (SubscriptionEntityDetailId,SubscriptionEntityId,Context,RecurringPrice,RecurringDuration,OneTimePrice,OneTimeDuration) 
	AS
	(
		SELECT ref.value( './@SubscriptionEntityDetailId', 'uniqueidentifier' ) as SubscriptionEntityDetailId, 
		       ref.value( './@SubscriptionEntityId', 'uniqueidentifier' ) as SubscriptionEntityId, 
		       ref.value( './@Context', 'uniqueidentifier' ) as Context, 
		       ref.value( './@RecurringPrice', '[dbo].[amount]' ) as RecurringPrice, 
		       ref.value( './@RecurringDuration', 'TINYINT' ) as RecurringDuration,			   
			   ref.value( './@OneTimePrice', '[dbo].[amount]' ) as OneTimePrice	,
			   ref.value( './@OneTimeDuration', 'TINYINT' ) as OneTimeDuration	   
		FROM @xmlSubscriptionEntityDetails.nodes('/SubscriptionEntityDetails/SubscriptionEntityDetail') as T(ref)
	)
    
    INSERT INTO [dbo].[TenantSubscriptionEntityDetail]([TenantId],[Id] ,[TenantSubscriptionEntityId],Context,RecurringPrice,RecurringDuration,OneTimePrice,OneTimeDuration)
	SELECT @guidTenantId,SubscriptionEntityDetailId,SubscriptionEntityId,Context,RecurringPrice,RecurringDuration,OneTimePrice,OneTimeDuration FROM ITEMS
	 
END
GO
/****** Object:  StoredProcedure [dbo].[TenantSubscriptionEntityDetail_Delete]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TenantSubscriptionEntityDetail_Delete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[TenantSubscriptionEntityDetail_Delete] AS' 
END
GO


ALTER PROCEDURE [dbo].[TenantSubscriptionEntityDetail_Delete]
    (
      @guidTenantId UNIQUEIDENTIFIER,
	  @guidSubscriptionEntityDetailId UNIQUEIDENTIFIER
    )
AS 
    BEGIN
        SET NOCOUNT ON 
		SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

		DELETE FROM [dbo].[TenantSubscriptionEntityDetail] WHERE [TenantId]=@guidTenantId AND [Id]=@guidSubscriptionEntityDetailId		
		RETURN 0
    END

GO
/****** Object:  StoredProcedure [dbo].[TenantSubscriptionEntityDetail_DeleteBySubsEntityId]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TenantSubscriptionEntityDetail_DeleteBySubsEntityId]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[TenantSubscriptionEntityDetail_DeleteBySubsEntityId] AS' 
END
GO


ALTER PROCEDURE [dbo].[TenantSubscriptionEntityDetail_DeleteBySubsEntityId]
    (
      @guidTenantId UNIQUEIDENTIFIER,
	  @guidSubscriptionEntityId UNIQUEIDENTIFIER
    )
AS 
    BEGIN
        SET NOCOUNT ON 
		SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

		DELETE FROM [dbo].[TenantSubscriptionEntityDetail] WHERE [TenantId]=@guidTenantId AND [TenantSubscriptionEntityId]=@guidSubscriptionEntityId	
		RETURN 0
    END

GO
/****** Object:  StoredProcedure [dbo].[TenantSubscriptionEntityDetail_Get]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TenantSubscriptionEntityDetail_Get]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[TenantSubscriptionEntityDetail_Get] AS' 
END
GO


ALTER PROCEDURE [dbo].[TenantSubscriptionEntityDetail_Get]
    (
      @guidTenantId UNIQUEIDENTIFIER,
	  @guidTenantSubscriptionEntityDetailId UNIQUEIDENTIFIER
    )
AS 
    BEGIN
        SET NOCOUNT ON 
		SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

		SELECT  [Id],[TenantSubscriptionEntityId],[Context],[RecurringPrice],[RecurringDuration],[OneTimePrice],[OneTimeDuration] 
		FROM [dbo].[TenantSubscriptionEntityDetail] WHERE [TenantId]=@guidTenantId AND[Id] =@guidTenantSubscriptionEntityDetailId
		RETURN 0
    END

GO
/****** Object:  StoredProcedure [dbo].[TenantSubscriptionEntityDetail_GetAll]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TenantSubscriptionEntityDetail_GetAll]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[TenantSubscriptionEntityDetail_GetAll] AS' 
END
GO


ALTER PROCEDURE [dbo].[TenantSubscriptionEntityDetail_GetAll]
    (
      @guidTenantId UNIQUEIDENTIFIER,
	  @guidTenantSubscriptionEntityId UNIQUEIDENTIFIER
    )
AS 
    BEGIN
        SET NOCOUNT ON 
		SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

		SELECT  [Id],[TenantSubscriptionEntityId],[Context],[RecurringPrice],[RecurringDuration],[OneTimePrice],[OneTimeDuration] 
		FROM [dbo].[TenantSubscriptionEntityDetail] WHERE [TenantId]=@guidTenantId AND [TenantSubscriptionEntityId]=@guidTenantSubscriptionEntityId	
		RETURN 0
    END

GO
/****** Object:  StoredProcedure [dbo].[TenantSubscriptionEntityDetail_Update]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TenantSubscriptionEntityDetail_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[TenantSubscriptionEntityDetail_Update] AS' 
END
GO


ALTER PROCEDURE [dbo].[TenantSubscriptionEntityDetail_Update]
    (
      @guidTenantId UNIQUEIDENTIFIER,
	  @guidSubscriptionEntityDetailId UNIQUEIDENTIFIER,
	  @dRecurringPrice [dbo].[amount]=NULL,
	  @tinyintRecurringDuration tinyint=null,
	  @dOneTimePrice [dbo].[amount]=NULL,
	  @tinyintOneTimeDuration tinyint=null
    )
AS 
    BEGIN
        SET NOCOUNT ON 
		SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
		
		
        UPDATE  [dbo].[TenantSubscriptionEntityDetail] SET 
		[RecurringPrice]=@dRecurringPrice,[RecurringDuration]=@tinyintRecurringDuration,[OneTimePrice]=@dOneTimePrice,[OneTimeDuration]=@tinyintOneTimeDuration
		WHERE [TenantId]=@guidTenantId AND [Id]=@guidSubscriptionEntityDetailId
		
			
		RETURN 0
    END

GO
/****** Object:  StoredProcedure [dbo].[User_GetBy_Id]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User_GetBy_Id]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[User_GetBy_Id] AS' 
END
GO
ALTER PROCEDURE [dbo].[User_GetBy_Id]
(
    @guidTenantId UNIQUEIDENTIFIER,
	@userId UNIQUEIDENTIFIER
)
AS 
BEGIN
	SET NOCOUNT ON 
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	SELECT UR.Id, UR.FirstName + ' ' + UR.LastName AS Username ,
	(CASE WHEN (@userId=TT.[SuperAdminId]) THEN CAST(1 AS bit)ELSE CAST(0 AS bit)END) AS SUPERADMIN	,TT.IsSystemRoot
	FROM [dbo].[User] UR
   INNER JOIN [dbo].[Item] IT ON UR.Id = IT.Id
   INNER JOIN Tenant TT ON TT.Id=UR.TenantId
   WHERE UR.Id = @userId and UR.TenantId=@guidTenantId

END

GO
/****** Object:  StoredProcedure [dbo].[UserCredential_Get]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserCredential_Get]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UserCredential_Get] AS' 
END
GO

ALTER PROCEDURE [dbo].[UserCredential_Get]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidRefId UNIQUEIDENTIFIER
)
AS
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    DECLARE @UserCredentialId UNIQUEIDENTIFIER;
    SET @UserCredentialId =
    (
        SELECT UserCredentialId
        FROM [User]
        WHERE Id = @guidRefId
              AND TenantId = @guidTenantId
    );
    SELECT InvalidAttemptCount,
           IsLocked,
           LockedOn
    FROM UserCredential
    WHERE id = @UserCredentialId;

--RETURN 1      
END;
GO
/****** Object:  StoredProcedure [dbo].[UserCredential_UpdateLockedStatus]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserCredential_UpdateLockedStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UserCredential_UpdateLockedStatus] AS' 
END
GO
ALTER PROCEDURE [dbo].[UserCredential_UpdateLockedStatus]
(
    @guidTenantId UNIQUEIDENTIFIER,
    @guidCredentialId UNIQUEIDENTIFIER,
    @intInvalidAttemptCount INT,
    @bitIsLocked BIT,
    @datetimeLockedOn DATETIME = NULL
)
AS
BEGIN
    SET NOCOUNT ON;    

    UPDATE [Credential]
    SET InvalidAttemptCount = @intInvalidAttemptCount,
        IsLocked = @bitIsLocked,
        LockedOn = @datetimeLockedOn
    WHERE Id = @guidCredentialId;


--RETURN 1          
END;
GO
/****** Object:  StoredProcedure [dbo].[WorkFlow_All_Get]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlow_All_Get]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[WorkFlow_All_Get] AS' 
END
GO


ALTER PROCEDURE [dbo].[WorkFlow_All_Get]
    (
      @guidTenantId UNIQUEIDENTIFIER,
	  @strEntityId [dbo].[xSmallText]
    )
AS 
    BEGIN
        SET NOCOUNT ON 
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

		
		SELECT [Id],[EntityId],[Status],[SubType] FROM [dbo].[WorkFlow] 
		WHERE  [TenantId]= @guidTenantId AND [EntityId]=@strEntityId

			
    END

GO
/****** Object:  StoredProcedure [dbo].[WorkFlow_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlow_Create]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[WorkFlow_Create] AS' 
END
GO


ALTER PROCEDURE [dbo].[WorkFlow_Create]
    (
     @guidTenantId UNIQUEIDENTIFIER,
      @guidWorkFlowId AS UNIQUEIDENTIFIER,   
	  @strEntityId  [dbo].[xSmallText],
      @bitStatus AS bit,	
      @strSubTypeCode [dbo].[smallText]
    )
AS 
    BEGIN
        SET NOCOUNT ON 
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

		
		IF EXISTS(SELECT * FROM [dbo].[WorkFlow] WHERE  [TenantId]= @guidTenantId AND [EntityId]=@strEntityId AND [SubType]=@strSubTypeCode)
		BEGIN
			RETURN 1
		END	

        INSERT  INTO [dbo].[WorkFlow]
                (
					[TenantId],[Id],[EntityId],[Status],[SubType]
                )
        VALUES  (  @guidTenantId, @guidWorkFlowId,@strEntityId,@bitStatus,@strSubTypeCode
                )
			
		RETURN 0
    END

GO
/****** Object:  StoredProcedure [dbo].[WorkFlow_Create_Xml]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlow_Create_Xml]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[WorkFlow_Create_Xml] AS' 
END
GO
ALTER PROCEDURE [dbo].[WorkFlow_Create_Xml]
(
	@guidTenantId UNIQUEIDENTIFIER,
	@xmlWorkFlowId AS XML
) AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @DATA TABLE
(
	idx INT IDENTITY(1,1),
	WorkFlowId1 uniqueidentifier NOT NULL,
	EntityId1 [dbo].[xSmallText] NOT NULL,
	Status1 BIT  NULL,
	SubTypeCode1  [dbo].[smallText]  NULL
)


INSERT INTO @DATA  SELECT 
ref.value( './@WorkFlowId', 'uniqueidentifier' ) as WorkFlowId,
ref.value( './@EntityId', '[dbo].[xSmallText]' ) as EntityId,
ref.value( './@Status', 'BIT' ) as Status2,
ref.value( './@SubTypeCode', '[dbo].[smallText]' ) as SubTypeCode
FROM @xmlWorkFlowId.nodes('/WorkFlows/WorkFlow') as T(ref)

INSERT INTO [dbo].[WorkFlow]
(
[TenantId],
[Id],
[EntityId],
[Status],
[SubType]
)
SELECT
@guidTenantId,
WorkFlowId1,
EntityId1,
Status1,
SubTypeCode1
FROM @DATA

IF @@ERROR <> 0
BEGIN
RETURN 1
END
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[WorkFlow_Get]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlow_Get]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[WorkFlow_Get] AS' 
END
GO


ALTER PROCEDURE [dbo].[WorkFlow_Get]
    (
      @guidTenantId UNIQUEIDENTIFIER,
	  @strEntityId [dbo].[xSmallText] ,     	
      @strSubTypeCode [dbo].[smallText]
    )
AS 
    BEGIN
        SET NOCOUNT ON 
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

		
		SELECT [Id],[EntityId],[Status],[SubType] FROM [dbo].[WorkFlow] 
		WHERE  [TenantId]= @guidTenantId AND [EntityId]=@strEntityId AND  [SubType] = @strSubTypeCode

			
    END

GO
/****** Object:  StoredProcedure [dbo].[WorkFlowInnerStep_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowInnerStep_Create]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[WorkFlowInnerStep_Create] AS' 
END
GO

ALTER PROCEDURE [dbo].[WorkFlowInnerStep_Create]
(
	@guidTenantId UNIQUEIDENTIFIER,
	@guidInnerStepId AS UNIQUEIDENTIFIER,
	@guidWorkFlowStepId AS UNIQUEIDENTIFIER,
	@guidWorkFlowId AS UNIQUEIDENTIFIER,
	@guidTransitionType AS UNIQUEIDENTIFIER
) AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @sequence int=(select ISNULL(MAX(SequenceNumber),0) FROM  [dbo].[WorkFlowInnerStep] WHERE [TenantId] = @guidTenantId AND [WorkFlowStepId]=@guidWorkFlowStepId)+1

INSERT INTO [dbo].[WorkFlowInnerStep]([TenantId],[Id],[WorkFlowStepId],[WorkFlowId],[TransitionType],[SequenceNumber] )
VALUES(@guidTenantId,@guidInnerStepId,@guidWorkFlowStepId,@guidWorkFlowId,@guidTransitionType,@sequence)

IF @@ERROR <> 0
BEGIN
RETURN 1
END
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[WorkFlowInnerStep_Create_Xml]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowInnerStep_Create_Xml]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[WorkFlowInnerStep_Create_Xml] AS' 
END
GO

ALTER PROCEDURE [dbo].[WorkFlowInnerStep_Create_Xml]
(
	@guidTenantId UNIQUEIDENTIFIER,
	@xmlWorkFlowInnerSteps as xml
) AS

	SET NOCOUNT ON
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	DECLARE @DATA TABLE
	(
		idx INT IDENTITY(1,1),
		InnerStepId1 uniqueidentifier NOT NULL,
		WorkFlowStepId1 uniqueidentifier NOT NULL,
		WorkFlowId1 uniqueidentifier NOT NULL,
		TransitionTypeId1 uniqueidentifier NOT NULL,
		SequenceNumber1 tinyint  NULL
	)


	INSERT INTO @DATA  SELECT 
	ref.value( './@InnerStepId', 'uniqueidentifier' ) as InnerStepId,
	ref.value( './@WorkFlowStepId', 'uniqueidentifier' ) as WorkFlowStepId,
	ref.value( './@WorkFlowId', 'uniqueidentifier' ) as WorkFlowId,
	ref.value( './@TransitionTypeId', 'uniqueidentifier' ) as TransitionTypeId,
	ref.value( './@SequenceNumber', 'tinyint' ) as SequenceNumber
	FROM @xmlWorkFlowInnerSteps.nodes('/WorkFlowInnerSteps/WorkFlowInnerStep') as T(ref)

 INSERT INTO [dbo].[WorkFlowInnerStep]([TenantId],[Id],[WorkFlowStepId],[WorkFlowId],[TransitionType],[SequenceNumber] )
							  SELECT   @guidTenantId,InnerStepId1,WorkFlowStepId1,WorkFlowId1,TransitionTypeId1,SequenceNumber1 FROM @DATA

IF @@ERROR <> 0
BEGIN
RETURN 1
END
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[WorkFlowInnerStep_Delete]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowInnerStep_Delete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[WorkFlowInnerStep_Delete] AS' 
END
GO
ALTER PROCEDURE [dbo].[WorkFlowInnerStep_Delete]
(
	@guidTenantId UNIQUEIDENTIFIER,
	@guidInnerStepId uniqueidentifier
) AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED



           DELETE WFPT FROM [dbo].[WorkFlowProcessTask] WFPT 
						INNER JOIN [dbo].[WorkFlowProcess] WFP ON WFP.Id=WFPT.WorkFlowProcessId
						INNER JOIN [dbo].[WorkFlowInnerStep] WFIS ON WFIS.WorkFlowStepId=WFP.OperationOrTransactionId 
			            WHERE WFPT.[TenantId] = @guidTenantId  AND  WFIS.Id= @guidInnerStepId


		    DELETE WFP FROM [dbo].[WorkFlowProcess] WFP 
						INNER JOIN [dbo].[WorkFlowInnerStep] WFIS ON WFIS.WorkFlowStepId=WFP.OperationOrTransactionId 
			            WHERE WFP.[TenantId] = @guidTenantId  AND  WFIS.Id= @guidInnerStepId

		   DELETE FROM [dbo].[WorkFlowInnerStep]  WHERE TenantId = @guidTenantId AND  Id= @guidInnerStepId

IF @@ERROR <> 0
BEGIN
RETURN 1
END
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[WorkFlowInnerStep_Get]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowInnerStep_Get]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[WorkFlowInnerStep_Get] AS' 
END
GO
ALTER PROCEDURE [dbo].[WorkFlowInnerStep_Get]
(
	@guidTenantId UNIQUEIDENTIFIER,
	@guidWorkFlowId uniqueidentifier
) AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT [Id],[WorkFlowStepId],[WorkFlowId],[TransitionType],[SequenceNumber]  FROM [dbo].[WorkFlowInnerStep]
				WHERE  TenantId= @guidTenantId AND WorkFlowId=@guidWorkFlowId


RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[WorkFlowInnerStep_GetByStepIds]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowInnerStep_GetByStepIds]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[WorkFlowInnerStep_GetByStepIds] AS' 
END
GO
ALTER PROCEDURE [dbo].[WorkFlowInnerStep_GetByStepIds]
(
	@guidTenantId UNIQUEIDENTIFIER,
	@xmlStepIds xml
) AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @DATA TABLE
(
idx INT IDENTITY(1,1),
Id1 uniqueidentifier NOT NULL
)
INSERT INTO @DATA  SELECT ref.value( './@value', 'uniqueidentifier' ) as Id FROM @xmlStepIds.nodes('/Items/Item') as T(ref)

SELECT [Id],[WorkFlowStepId],[WorkFlowId],[TransitionType],[SequenceNumber]  FROM [dbo].[WorkFlowInnerStep]
				WHERE  TenantId= @guidTenantId AND [WorkFlowStepId]  IN (SELECT Id1 FROM @DATA)


RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[WorkFlowInnerStep_GetByStepTransactionType]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowInnerStep_GetByStepTransactionType]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[WorkFlowInnerStep_GetByStepTransactionType] AS' 
END
GO
ALTER PROCEDURE [dbo].[WorkFlowInnerStep_GetByStepTransactionType]
(
	@guidTenantId UNIQUEIDENTIFIER,
	@guidStepTransactionType uniqueidentifier,
	@guidWorkFlowId uniqueidentifier
) AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT WFIS.[Id],WFIS.[WorkFlowStepId],WFIS.[WorkFlowId],WFIS.[TransitionType],WFIS.[SequenceNumber]  
FROM [dbo].[WorkFlowInnerStep] WFIS
INNER JOIN [dbo].[WorkFlowStep] WFS ON WFS.Id=WFIS.WorkFlowStepId
INNER JOIN [dbo].[WorkFlow] WF ON WF.Id=WFS.WorkFlowId
				WHERE  WFIS.TenantId= @guidTenantId AND WFS.TransitionType=@guidStepTransactionType AND WFS.WorkFlowId=@guidWorkFlowId


RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[WorkFlowInnerStep_GetByWorkFlowIds]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowInnerStep_GetByWorkFlowIds]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[WorkFlowInnerStep_GetByWorkFlowIds] AS' 
END
GO
ALTER PROCEDURE [dbo].[WorkFlowInnerStep_GetByWorkFlowIds]
(
	@guidTenantId UNIQUEIDENTIFIER,
	@xmlWorkFlowIds xml
) AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @DATA TABLE
			(
			   idx INT IDENTITY(1,1),
			   workFlowId1 UNIQUEIDENTIFIER NOT NULL
			)
			INSERT INTO @DATA  SELECT ref.value( './@value', 'UNIQUEIDENTIFIER' ) as Id FROM @xmlWorkFlowIds.nodes('/Items/Item') as T(ref)

SELECT [Id],[WorkFlowStepId],[WorkFlowId],[TransitionType],[SequenceNumber]  FROM [dbo].[WorkFlowInnerStep]
				WHERE  TenantId= @guidTenantId AND WorkFlowId IN (SELECT workFlowId1 FROM @DATA)

RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[WorkFlowInnerStep_MoveUpDown]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowInnerStep_MoveUpDown]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[WorkFlowInnerStep_MoveUpDown] AS' 
END
GO

ALTER PROCEDURE [dbo].[WorkFlowInnerStep_MoveUpDown]
(
	@guidTenantId UNIQUEIDENTIFIER,
	@xmlWorkFlowInnerSteps as xml
) AS

	SET NOCOUNT ON
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	DECLARE @DATA TABLE
	(
		idx INT IDENTITY(1,1),
		InnerStepId1 uniqueidentifier NOT NULL,
		SequenceNumber1 SMALLINT  NULL
	)


	INSERT INTO @DATA  SELECT 
	ref.value( './@InnerStepId', 'uniqueidentifier' ) as InnerStepId,
	ref.value( './@SequenceNumber', 'SMALLINT' ) as SequenceNumber
	FROM @xmlWorkFlowInnerSteps.nodes('/WorkFlowInnerSteps/WorkFlowInnerStep') as T(ref)

  UPDATE WFIS SET WFIS.SequenceNumber=WFIS1.SequenceNumber1 FROM  [dbo].[WorkFlowInnerStep] WFIS INNER JOIN @DATA WFIS1 ON WFIS.Id=WFIS1.InnerStepId1 
	     WHERE [TenantId]=@guidTenantId

IF @@ERROR <> 0
BEGIN
RETURN 1
END
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[WorkFlowOperation_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowOperation_Create]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[WorkFlowOperation_Create] AS' 
END
GO
ALTER PROCEDURE [dbo].[WorkFlowOperation_Create]
(
	@guidTenantId UNIQUEIDENTIFIER,
	@xmlWorkFlowOperations AS XML
) AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @DATA TABLE
(
	idx INT IDENTITY(1,1),
	WorkFlowOperationId1 uniqueidentifier NOT NULL,
	OperationType1 tinyint NOT NULL,
	WorkFlowId1 uniqueidentifier NOT NULL,
	IsSync1  bit NOT NULL
)


INSERT INTO @DATA  SELECT 
ref.value( './@WorkFlowOperationId', 'uniqueidentifier' ) as WorkFlowOperationId,
ref.value( './@OperationType', 'tinyint' ) as OperationType,
ref.value( './@WorkFlowId', 'uniqueidentifier' ) as WorkFlowId,
ref.value( './@IsSync', 'bit' ) as IsSync
FROM @xmlWorkFlowOperations.nodes('/WorkFlowOperations/WorkFlowOperation') as T(ref)

INSERT INTO [dbo].[WorkFlowOperation]
(
[TenantId],
[Id],
[OperationType],
[WorkFlowId],
[IsSync]
)
SELECT
@guidTenantId,
WorkFlowOperationId1,
OperationType1,
WorkFlowId1,
IsSync1
FROM @DATA

IF @@ERROR <> 0
BEGIN
RETURN 1
END
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[WorkFlowOperation_Get]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowOperation_Get]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[WorkFlowOperation_Get] AS' 
END
GO
ALTER PROCEDURE [dbo].[WorkFlowOperation_Get]
(
	@guidTenantId UNIQUEIDENTIFIER, 
	@guidWorkFlowId uniqueidentifier
) AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT [Id],[OperationType],[WorkFlowId] FROM  [dbo].[WorkFlowOperation] WHERE [TenantId]=@guidTenantId AND [WorkFlowId]=@guidWorkFlowId
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[WorkFlowOperation_GetBy_WorkFlowIds]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowOperation_GetBy_WorkFlowIds]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[WorkFlowOperation_GetBy_WorkFlowIds] AS' 
END
GO
ALTER PROCEDURE [dbo].[WorkFlowOperation_GetBy_WorkFlowIds]
(
	@guidTenantId UNIQUEIDENTIFIER,
	@xmlWorkFlowIds xml
) AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @DATA TABLE
(
idx INT IDENTITY(1,1),
Id1 uniqueidentifier NOT NULL
)
INSERT INTO @DATA  SELECT ref.value( './@value', 'uniqueidentifier' ) as Id FROM @xmlWorkFlowIds.nodes('/Items/Item') as T(ref)

SELECT [Id],[OperationType],[WorkFlowId] FROM  [dbo].[WorkFlowOperation] 
WHERE [TenantId]=@guidTenantId AND [WorkFlowId]  IN (SELECT Id1 FROM @DATA)
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[WorkFlowProcess_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowProcess_Create]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[WorkFlowProcess_Create] AS' 
END
GO
ALTER PROCEDURE [dbo].[WorkFlowProcess_Create]
(
	@guidTenantId UNIQUEIDENTIFIER,
	@xmlWorkFlowProcess as xml
) AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @DATA TABLE
(
	idx INT IDENTITY(1,1),
	WorkFlowProcessId1 uniqueidentifier NOT NULL,
	WorkFlowId1  uniqueidentifier NOT NULL,
	OperationOrTransactionId1 uniqueidentifier NOT NULL,
	OperationOrTransactionType1 tinyint NOT NULL,
	ProcessType1  tinyint NOT NULL
)


INSERT INTO @DATA  SELECT 
ref.value( './@WorkFlowProcessId', 'uniqueidentifier' ) as WorkFlowProcessId,
ref.value( './@WorkFlowId', 'uniqueidentifier' ) as WorkFlowId,
ref.value( './@OperationOrTransactionId', 'uniqueidentifier' ) as OperationOrTransactionId,
ref.value( './@OperationOrTransactionType', 'tinyint' ) as OperationOrTransactionType,
ref.value( './@ProcessType', 'tinyint' ) as ProcessType
FROM @xmlWorkFlowProcess.nodes('/WorkFlowProcess/WorkFlowProces') as T(ref)

INSERT INTO [dbo].[WorkFlowProcess]([TenantId],[Id],[WorkFlowId],[OperationOrTransactionId],[OperationOrTransactionType],  [ProcessType])
SELECT @guidTenantId,WorkFlowProcessId1,WorkFlowId1,OperationOrTransactionId1,OperationOrTransactionType1,ProcessType1
FROM @DATA

IF @@ERROR <> 0
BEGIN
RETURN 1
END
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[WorkFlowProcess_Get]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowProcess_Get]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[WorkFlowProcess_Get] AS' 
END
GO
ALTER PROCEDURE [dbo].[WorkFlowProcess_Get]
(
	@guidTenantId UNIQUEIDENTIFIER,
	@guidWorkFlowId as uniqueidentifier
) AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT [Id],[WorkFlowId],[OperationOrTransactionId],[OperationOrTransactionType],  [ProcessType] FROM [dbo].[WorkFlowProcess]
WHERE [TenantId]=@guidTenantId AND [WorkFlowId]=@guidWorkFlowId

RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[WorkFlowProcess_GetBy_OperationOrTransitionIds]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowProcess_GetBy_OperationOrTransitionIds]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[WorkFlowProcess_GetBy_OperationOrTransitionIds] AS' 
END
GO
ALTER PROCEDURE [dbo].[WorkFlowProcess_GetBy_OperationOrTransitionIds]
(
	@guidTenantId UNIQUEIDENTIFIER,
	@xmlOperationOrTransitionIds as xml
) AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @DATA TABLE
(
idx INT IDENTITY(1,1),
Id1 uniqueidentifier NOT NULL
)
INSERT INTO @DATA  SELECT ref.value( './@value', 'uniqueidentifier' ) as Id FROM @xmlOperationOrTransitionIds.nodes('/Items/Item') as T(ref)

SELECT [Id],[WorkFlowId],[OperationOrTransactionId],[OperationOrTransactionType],  [ProcessType] FROM [dbo].[WorkFlowProcess]
WHERE [TenantId]=@guidTenantId AND [OperationOrTransactionId]  IN (SELECT Id1 FROM @DATA)

RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[WorkFlowProcess_GetByW_WorkFlowIds]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowProcess_GetByW_WorkFlowIds]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[WorkFlowProcess_GetByW_WorkFlowIds] AS' 
END
GO
ALTER PROCEDURE [dbo].[WorkFlowProcess_GetByW_WorkFlowIds]
(
	@guidTenantId UNIQUEIDENTIFIER,
	@xmlWorkFlowIds xml
) AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @DATA TABLE
(
idx INT IDENTITY(1,1),
Id1 uniqueidentifier NOT NULL
)
INSERT INTO @DATA  SELECT ref.value( './@value', 'uniqueidentifier' ) as Id FROM @xmlWorkFlowIds.nodes('/Items/Item') as T(ref)

SELECT [Id],[WorkFlowId],[OperationOrTransactionId],[OperationOrTransactionType],  [ProcessType] FROM [dbo].[WorkFlowProcess]
WHERE [TenantId]=@guidTenantId AND [WorkFlowId]  IN (SELECT Id1 FROM @DATA)

RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[WorkFlowProcessTask_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowProcessTask_Create]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[WorkFlowProcessTask_Create] AS' 
END
GO
ALTER PROCEDURE [dbo].[WorkFlowProcessTask_Create]
(	
	@guidTenantId UNIQUEIDENTIFIER,
	@guidWorkFlowProcessTaskId AS UNIQUEIDENTIFIER,
	@guidWorkFlowId AS UNIQUEIDENTIFIER,
	@guidWorkFlowProcessId AS UNIQUEIDENTIFIER,
	@guidProcessCode AS UNIQUEIDENTIFIER
) AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @sequence int=(select ISNULL(MAX(SequenceNumber),0) FROM [dbo].[WorkFlowProcessTask] WHERE [TenantId] = @guidTenantId AND [WorkFlowProcessId]=@guidWorkFlowProcessId)+1


INSERT INTO [dbo].[WorkFlowProcessTask]
(
[TenantId],
[Id],
[WorkFlowId],
[WorkFlowProcessId],
[ProcessCode],
[SequenceNumber]
)
values(
@guidTenantId,
@guidWorkFlowProcessTaskId,
@guidWorkFlowId,
@guidWorkFlowProcessId,
@guidProcessCode,
@sequence
)

IF @@ERROR <> 0
BEGIN
RETURN 1
END
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[WorkFlowProcessTask_Create_Xml]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowProcessTask_Create_Xml]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[WorkFlowProcessTask_Create_Xml] AS' 
END
GO
ALTER PROCEDURE [dbo].[WorkFlowProcessTask_Create_Xml]
(
	@guidTenantId UNIQUEIDENTIFIER,
	@xmlWorkFlowProcessTasks AS XML
) AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @DATA TABLE
(
	idx INT IDENTITY(1,1),
	WorkFlowProcessTaskId1 uniqueidentifier NOT NULL,
	WorkFlowId1 uniqueidentifier NULL,
	WorkFlowProcessId1 uniqueidentifier NULL,
	ProcessCode1	 uniqueidentifier NULL,
	SequenceCode1  tinyint NOT NULL
)

INSERT INTO @DATA  SELECT 
ref.value( './@WorkFlowProcessTaskId', 'uniqueidentifier' ) as WorkFlowProcessTaskId,
ref.value( './@WorkFlowId', 'uniqueidentifier' ) as WorkFlowId,
ref.value( './@WorkFlowProcessId', 'uniqueidentifier' ) as WorkFlowProcessId,
ref.value( './@ProcessCode', 'uniqueidentifier' ) as ProcessCode,
ref.value( './@SequenceCode', 'tinyint' ) as SequenceCode
FROM @xmlWorkFlowProcessTasks.nodes('/WorkFlowProcessTasks/WorkFlowProcessTask') as T(ref)

INSERT INTO [dbo].[WorkFlowProcessTask]
(
[TenantId],
[Id],
[WorkFlowId],
[WorkFlowProcessId],
[ProcessCode],
[SequenceNumber]
)
SELECT @guidTenantId,WorkFlowProcessTaskId1,WorkFlowId1,WorkFlowProcessId1,ProcessCode1,SequenceCode1 FROM @DATA


IF @@ERROR <> 0
BEGIN
RETURN 1
END
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[WorkFlowProcessTask_Delete]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowProcessTask_Delete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[WorkFlowProcessTask_Delete] AS' 
END
GO
ALTER PROCEDURE [dbo].[WorkFlowProcessTask_Delete]
(
	@guidTenantId UNIQUEIDENTIFIER,
	@guidWorkFlowProcessTaskId as uniqueidentifier
) AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
DELETE  FROM [dbo].[WorkFlowProcessTask] WHERE [TenantId] = @guidTenantId  AND  Id= @guidWorkFlowProcessTaskId

IF @@ERROR <> 0
BEGIN
RETURN 1
END
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[WorkFlowProcessTask_Get]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowProcessTask_Get]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[WorkFlowProcessTask_Get] AS' 
END
GO
ALTER PROCEDURE [dbo].[WorkFlowProcessTask_Get]
(
	@guidTenantId UNIQUEIDENTIFIER,
	@guidWorkFlowId uniqueidentifier
) AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED


SELECT [Id],[WorkFlowId],[WorkFlowProcessId],[ProcessCode],[SequenceNumber],null FROM [dbo].[WorkFlowProcessTask] WHERE [TenantId]=@guidTenantId AND [WorkFlowId]=@guidWorkFlowId

RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[WorkFlowProcessTask_Get_ByInnerStepId]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowProcessTask_Get_ByInnerStepId]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[WorkFlowProcessTask_Get_ByInnerStepId] AS' 
END
GO
ALTER PROCEDURE [dbo].[WorkFlowProcessTask_Get_ByInnerStepId]
(
	@guidTenantId UNIQUEIDENTIFIER,
	@guidInnerStepId uniqueidentifier
) AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED


SELECT WFPT.[Id],WFPT.[WorkFlowId],WFPT.[WorkFlowProcessId],WFPT.[ProcessCode],WFPT.[SequenceNumber] ,WFP.ProcessType
FROM [dbo].[WorkFlowProcessTask] WFPT INNER JOIN [dbo].[WorkFlowProcess] WFP ON WFP.Id=WFPT.WorkFlowProcessId
WHERE WFPT.[TenantId]=@guidTenantId AND WFP.OperationOrTransactionId =@guidInnerStepId

RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[WorkFlowProcessTask_Get_ByProcessIds]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowProcessTask_Get_ByProcessIds]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[WorkFlowProcessTask_Get_ByProcessIds] AS' 
END
GO
ALTER PROCEDURE [dbo].[WorkFlowProcessTask_Get_ByProcessIds]
(
	@guidTenantId UNIQUEIDENTIFIER,
	@xmlProcessIds  xml
) AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @DATA TABLE
(
idx INT IDENTITY(1,1),
Id1 uniqueidentifier NOT NULL
)
INSERT INTO @DATA  SELECT ref.value( './@value', 'uniqueidentifier' ) as Id FROM @xmlProcessIds.nodes('/Items/Item') as T(ref)

SELECT WFPT.[Id],WFPT.[WorkFlowId],WFPT.[WorkFlowProcessId],WFPT.[ProcessCode],WFPT.[SequenceNumber] ,NULL AS ProcessType FROM [dbo].[WorkFlowProcessTask] WFPT 
WHERE WFPT.[TenantId]=@guidTenantId AND WFPT.[WorkFlowProcessId] IN (SELECT Id1 FROM @DATA)

RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[WorkFlowProcessTask_GetBy_WorkFlowIds]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowProcessTask_GetBy_WorkFlowIds]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[WorkFlowProcessTask_GetBy_WorkFlowIds] AS' 
END
GO
ALTER PROCEDURE [dbo].[WorkFlowProcessTask_GetBy_WorkFlowIds]
(
	@guidTenantId UNIQUEIDENTIFIER,
	@xmlWorkFlowIds xml
) AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @DATA TABLE
(
idx INT IDENTITY(1,1),
Id1 uniqueidentifier NOT NULL
)
INSERT INTO @DATA  SELECT ref.value( './@value', 'uniqueidentifier' ) as Id FROM @xmlWorkFlowIds.nodes('/Items/Item') as T(ref)


		SELECT [Id],[WorkFlowId],[WorkFlowProcessId],[ProcessCode],[SequenceNumber],null FROM [dbo].[WorkFlowProcessTask] 
		WHERE [TenantId]=@guidTenantId AND [WorkFlowId]  IN (SELECT Id1 FROM @DATA)

RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[WorkFlowProcessTask_MoveUpDown]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowProcessTask_MoveUpDown]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[WorkFlowProcessTask_MoveUpDown] AS' 
END
GO
ALTER PROCEDURE [dbo].[WorkFlowProcessTask_MoveUpDown]
(
	@guidTenantId UNIQUEIDENTIFIER,
	@xmlWorkFlowProcessTask AS XML
) AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @DATA TABLE
(
	idx INT IDENTITY(1,1),
	WorkFlowProcessTaskId1 uniqueidentifier NOT NULL,	
	SequenceCode1  tinyint NOT NULL
)

INSERT INTO @DATA  SELECT 
ref.value( './@WorkFlowProcessTaskId', 'uniqueidentifier' ) as WorkFlowProcessTaskId,
ref.value( './@SequenceCode', 'tinyint' ) as SequenceCode
FROM @xmlWorkFlowProcessTask.nodes('/WorkFlowProcessTasks/WorkFlowProcessTask') as T(ref)

UPDATE WFPT SET WFPT.SequenceNumber=WFPT1.SequenceCode1 FROM  [dbo].[WorkFlowProcessTask] WFPT INNER JOIN @DATA WFPT1 
								ON WFPT.Id=WFPT1.WorkFlowProcessTaskId1 
  WHERE [TenantId]=@guidTenantId

IF @@ERROR <> 0
BEGIN
RETURN 1
END
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[WorkFlowRole_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowRole_Create]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[WorkFlowRole_Create] AS' 
END
GO
ALTER PROCEDURE [dbo].[WorkFlowRole_Create]
(
	@guidTenantId UNIQUEIDENTIFIER,
	@guidRoleAssignmetId uniqueidentifier,
	@guidWorkFlowStepId  uniqueidentifier,
	@guidRoleId UNIQUEIDENTIFIER ,
	@guidWorkFlowId uniqueidentifier,
	@intAssignmentOperationType tinyint
) AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

INSERT INTO [dbo].[WorkFlowRole]
(
	[TenantId],
	[Id],
	[WorkFlowStepId],
	[RoleId],
	[WorkFlowId],
	[AssignmentOperatorType]
)
VALUES(
	@guidTenantId,
	@guidRoleAssignmetId,
	@guidWorkFlowStepId,
	@guidRoleId,
	@guidWorkFlowId,
	@intAssignmentOperationType
)


IF @@ERROR <> 0
BEGIN
RETURN 1
END
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[WorkFlowRole_Create_Xml]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowRole_Create_Xml]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[WorkFlowRole_Create_Xml] AS' 
END
GO
ALTER PROCEDURE [dbo].[WorkFlowRole_Create_Xml]
(
	@guidTenantId UNIQUEIDENTIFIER,
	@xmlRoleAssignmets xml
) AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @DATA TABLE
(
	idx INT IDENTITY(1,1),
	RoleAssignmetId1 uniqueidentifier NOT NULL,
	WorkFlowStepId1 uniqueidentifier NULL,
	RoleId1 uniqueidentifier NULL,
	WorkFlowId1	 uniqueidentifier NULL,
	AssignmentOperationType1  tinyint NOT NULL
)

INSERT INTO @DATA  SELECT 
ref.value( './@RoleAssignmetId', 'uniqueidentifier' ) as RoleAssignmetId,
ref.value( './@WorkFlowStepId', 'uniqueidentifier' ) as WorkFlowStepId,
ref.value( './@RoleId', 'uniqueidentifier' ) as RoleId,
ref.value( './@WorkFlowId', 'uniqueidentifier' ) as WorkFlowId,
ref.value( './@AssignmentOperationType', 'tinyint' ) as AssignmentOperationType
FROM @xmlRoleAssignmets.nodes('/WorkFlowRoles/WorkFlowRole') as T(ref)

INSERT INTO [dbo].[WorkFlowRole]
(
	[TenantId],
	[Id],
	[WorkFlowStepId],
	[RoleId],
	[WorkFlowId],
	[AssignmentOperatorType]
)
SELECT 
	@guidTenantId,RoleAssignmetId1,WorkFlowStepId1,RoleId1,WorkFlowId1,AssignmentOperationType1 FROM @DATA

IF @@ERROR <> 0
BEGIN
RETURN 1
END
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[WorkFlowRole_Delete]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowRole_Delete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[WorkFlowRole_Delete] AS' 
END
GO
ALTER PROCEDURE [dbo].[WorkFlowRole_Delete]
(
	@guidTenantId UNIQUEIDENTIFIER,
	@guidWorkFlowStepId uniqueidentifier,
	@guidRoleId UNIQUEIDENTIFIER ,
	@guidWorkFlowId uniqueidentifier,
	@intAssignmentOperationType tinyint
) AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DELETE FROM  [dbo].[WorkFlowRole] WHERE [TenantId]=@guidTenantId AND [WorkFlowStepId]=@guidWorkFlowStepId AND [RoleId]=@guidRoleId 
AND [WorkFlowId]=@guidWorkFlowId AND [AssignmentOperatorType]=@intAssignmentOperationType

IF @@ERROR <> 0
BEGIN
RETURN 1
END
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[WorkFlowRole_Get]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowRole_Get]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[WorkFlowRole_Get] AS' 
END
GO
ALTER PROCEDURE [dbo].[WorkFlowRole_Get]
(
	@guidTenantId UNIQUEIDENTIFIER,
	@guidWorkFlowId uniqueidentifier
) AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT  [Id],[WorkFlowStepId],[RoleId],[WorkFlowId],[AssignmentOperatorType] FROM  [dbo].[WorkFlowRole] 
WHERE WorkFlowId=@guidWorkFlowId AND TenantId=@guidTenantId

IF @@ERROR <> 0
BEGIN
RETURN 1
END
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[WorkFlowRole_Get_StepIds]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowRole_Get_StepIds]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[WorkFlowRole_Get_StepIds] AS' 
END
GO

ALTER PROCEDURE [dbo].[WorkFlowRole_Get_StepIds]
(
	@guidTenantId UNIQUEIDENTIFIER,
	@xmlStepIds xml
) AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @DATA TABLE
(
idx INT IDENTITY(1,1),
Id1 uniqueidentifier NOT NULL
)
INSERT INTO @DATA  SELECT ref.value( './@value', 'uniqueidentifier' ) as Id FROM @xmlStepIds.nodes('/Items/Item') as T(ref)

SELECT  WFR.[Id],WFR.[WorkFlowStepId],WFR.[RoleId],WFR.[WorkFlowId],WFR.[AssignmentOperatorType],null as RoleName FROM  [dbo].[WorkFlowRole]  WFR
WHERE  WFR.TenantId=@guidTenantId AND WFR.[WorkFlowStepId] IN (SELECT Id1 FROM @DATA)

IF @@ERROR <> 0
BEGIN
RETURN 1
END
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[WorkFlowRole_Get_WorkFlowIds]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowRole_Get_WorkFlowIds]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[WorkFlowRole_Get_WorkFlowIds] AS' 
END
GO

ALTER PROCEDURE [dbo].[WorkFlowRole_Get_WorkFlowIds]
(
	@guidTenantId UNIQUEIDENTIFIER,
	@xmlWorkFlowIds xml
) AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @DATA TABLE
(
idx INT IDENTITY(1,1),
Id1 uniqueidentifier NOT NULL
)
INSERT INTO @DATA  SELECT ref.value( './@value', 'uniqueidentifier' ) as Id FROM @xmlWorkFlowIds.nodes('/Items/Item') as T(ref)

SELECT  WFR.[Id],WFR.[WorkFlowStepId],WFR.[RoleId],WFR.[WorkFlowId],WFR.[AssignmentOperatorType],RL.Name FROM  [dbo].[WorkFlowRole]  WFR
INNER JOIN [dbo].[Role] RL ON RL.Id=WFR.RoleId
WHERE  WFR.TenantId=@guidTenantId AND WFR.WorkFlowId IN (SELECT Id1 FROM @DATA)

IF @@ERROR <> 0
BEGIN
RETURN 1
END
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[WorkFlows_GetBy_EntityIds]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlows_GetBy_EntityIds]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[WorkFlows_GetBy_EntityIds] AS' 
END
GO


ALTER PROCEDURE [dbo].[WorkFlows_GetBy_EntityIds]
    (
      @guidTenantId UNIQUEIDENTIFIER,
	  @xmlEntityIds xml
    )
AS 
    BEGIN
        SET NOCOUNT ON 
        SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

		  DECLARE @DATA TABLE
			(
			   idx INT IDENTITY(1,1),
			   entityId1 [dbo].[xSmallText] NOT NULL
			)
			INSERT INTO @DATA  SELECT ref.value( './@value', '[dbo].[xSmallText]' ) as Id FROM @xmlEntityIds.nodes('/Items/Item') as T(ref)

		
		SELECT [Id],[EntityId],[Status],[SubType] FROM [dbo].[WorkFlow] 
				WHERE  [TenantId]= @guidTenantId AND [EntityId] IN (SELECT entityId1 FROM @DATA)

			
    END

GO
/****** Object:  StoredProcedure [dbo].[WorkFlows_GetBy_Ids]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlows_GetBy_Ids]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[WorkFlows_GetBy_Ids] AS' 
END
GO


ALTER PROCEDURE [dbo].[WorkFlows_GetBy_Ids]
    (
      @guidTenantId UNIQUEIDENTIFIER,
	  @xmlWorkFlowIds xml
    )
AS 
    BEGIN
        SET NOCOUNT ON 
        SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

		  DECLARE @DATA TABLE
			(
			   idx INT IDENTITY(1,1),
			   Id1 uniqueidentifier NOT NULL
			)
			INSERT INTO @DATA  SELECT ref.value( './@value', 'uniqueidentifier' ) as Id FROM @xmlWorkFlowIds.nodes('/Items/Item') as T(ref)

		
		SELECT [Id],[EntityId],[Status],[SubType] FROM [dbo].[WorkFlow] 
				WHERE  [TenantId]= @guidTenantId AND [Id] IN (SELECT Id1 FROM @DATA)

			
    END

GO
/****** Object:  StoredProcedure [dbo].[WorkFlowStep_Create]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowStep_Create]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[WorkFlowStep_Create] AS' 
END
GO
ALTER PROCEDURE [dbo].[WorkFlowStep_Create]
(
	@guidTenantId UNIQUEIDENTIFIER,
	@guidWorkFlowStepId AS UNIQUEIDENTIFIER,
	@guidWorkFlowId AS UNIQUEIDENTIFIER,
	@guidTransitionType AS UNIQUEIDENTIFIER
) AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @sequence int=(select ISNULL(MAX(SequenceNumber),0) FROM [dbo].[WorkFlowStep]  WHERE [TenantId] = @guidTenantId AND [WorkFlowId]=@guidWorkFlowId)+1

INSERT INTO [dbo].[WorkFlowStep]([TenantId],[Id],[WorkFlowId],[TransitionType],[SequenceNumber] )
VALUES(@guidTenantId,@guidWorkFlowStepId,@guidWorkFlowId,@guidTransitionType,@sequence)

IF @@ERROR <> 0
BEGIN
RETURN 1
END
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[WorkFlowStep_Create_Xml]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowStep_Create_Xml]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[WorkFlowStep_Create_Xml] AS' 
END
GO
ALTER PROCEDURE [dbo].[WorkFlowStep_Create_Xml]
(
	@guidTenantId UNIQUEIDENTIFIER,
	@xmlWorkFlowSteps as xml
) AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @DATA TABLE
(
	idx INT IDENTITY(1,1),
	WorkFlowStepId1 uniqueidentifier NOT NULL,
	WorkFlowId1 uniqueidentifier NOT NULL,
	TransitionTypeId1 uniqueidentifier NOT NULL,
	SequenceNumber1 tinyint  NULL,
	IsAssigmentMandatory1 bit null ,
	AllotedTime int null,
	CriticalTime int null

)


INSERT INTO @DATA  SELECT 
ref.value( './@WorkFlowStepId', 'uniqueidentifier' ) as WorkFlowStepId,
ref.value( './@WorkFlowId', 'uniqueidentifier' ) as WorkFlowId,
ref.value( './@TransitionTypeId', 'uniqueidentifier' ) as TransitionTypeId,
ref.value( './@SequenceNumber', 'TINYINT' ) as SequenceNumber,
ref.value( './@IsAssigmentMandatory', 'BIT' ) as IsAssigmentMandatory,
ref.value( './@AllotedTime', 'INT' ) as AllotedTime,
ref.value( './@CriticalTime', 'INT' ) as CriticalTime
FROM @xmlWorkFlowSteps.nodes('/WorkFlowSteps/WorkFlowStep') as T(ref)

INSERT INTO [dbo].[WorkFlowStep]([TenantId],[Id],[WorkFlowId],[TransitionType],[SequenceNumber],[IsAssignmentMandatory],[AllotedTime],[CriticalTime] )
SELECT @guidTenantId,WorkFlowStepId1,WorkFlowId1,TransitionTypeId1,SequenceNumber1,IsAssigmentMandatory1,AllotedTime,CriticalTime  FROM @DATA



IF @@ERROR <> 0
BEGIN
RETURN 1
END
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[WorkFlowStep_Delete]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowStep_Delete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[WorkFlowStep_Delete] AS' 
END
GO
ALTER PROCEDURE [dbo].[WorkFlowStep_Delete]
(
	@guidTenantId UNIQUEIDENTIFIER, 
	@guidWorkFlowStepId AS UNIQUEIDENTIFIER,
	@guidWorkFlowId uniqueidentifier
) AS
BEGIN

        SET NOCOUNT ON;
        BEGIN TRANSACTION;

        BEGIN TRY
         --   DELETE  FROM [dbo].[WorkFlowPerformanceCheck] WHERE TenantId = @guidTenantId AND  WorkFlowId= @guidWorkFlowId AND [WorkFlowStepId]=@guidWorkFlowStepId

			DELETE FROM [dbo].[WorkFlowRole]  WHERE TenantId = @guidTenantId AND  WorkFlowId= @guidWorkFlowId  AND [WorkFlowStepId]=@guidWorkFlowStepId


		    DELETE WFPT FROM [dbo].[WorkFlowProcessTask] WFPT 
						INNER JOIN [dbo].[WorkFlowProcess] WFP ON WFP.Id=WFPT.WorkFlowProcessId
						INNER JOIN [dbo].[WorkFlowInnerStep] WFIS ON WFIS.WorkFlowStepId=WFP.OperationOrTransactionId 
			            WHERE WFPT.[TenantId] = @guidTenantId  AND  WFIS.WorkFlowStepId= @guidWorkFlowStepId


		    DELETE WFP FROM [dbo].[WorkFlowProcess] WFP 
						INNER JOIN [dbo].[WorkFlowInnerStep] WFIS ON WFIS.WorkFlowStepId=WFP.OperationOrTransactionId 
			            WHERE WFP.[TenantId] = @guidTenantId  AND  WFIS.WorkFlowStepId= @guidWorkFlowStepId

		   DELETE FROM [dbo].[WorkFlowInnerStep]  WHERE TenantId = @guidTenantId AND  [WorkFlowStepId]= @guidWorkFlowStepId
		   DELETE FROM [dbo].[WorkFlowStep]  WHERE TenantId = @guidTenantId AND  Id= @guidWorkFlowStepId		

           COMMIT TRANSACTION;
        END TRY
        BEGIN CATCH

            ROLLBACK TRANSACTION;
        END CATCH;
        IF ( @@ERROR <> 0 )
        begin
            RETURN 1;
        ROLLBACK TRANSACTION;
        end

        RETURN 0;
    END;

GO
/****** Object:  StoredProcedure [dbo].[WorkFlowStep_Get]    Script Date: 15-Jul-19 15:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowStep_Get]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[WorkFlowStep_Get] AS' 
END
GO

ALTER PROCEDURE [dbo].[WorkFlowStep_Get]
(
	@guidTenantId UNIQUEIDENTIFIER,
	@guidWorkFlowId uniqueidentifier
) AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT [Id],[WorkFlowId],[TransitionType],[SequenceNumber],[IsAssignmentMandatory],[AllotedTime],[CriticalTime] FROM 
[dbo].[WorkFlowStep] WHERE [WorkFlowId]=@guidWorkFlowId AND [TenantId]=@guidTenantId order by [SequenceNumber]


RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[WorkFlowStep_MoveUpDown]    Script Date: 15-Jul-19 15:39:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowStep_MoveUpDown]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[WorkFlowStep_MoveUpDown] AS' 
END
GO
ALTER PROCEDURE [dbo].[WorkFlowStep_MoveUpDown]
(
	@guidTenantId UNIQUEIDENTIFIER,
	@xmlWorkFlowSteps as xml
) AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @DATA TABLE
(
	idx INT IDENTITY(1,1),
	WorkFlowStepId1 uniqueidentifier NOT NULL,
	WorkFlowId1 uniqueidentifier NOT NULL,
	SequenceNumber1 SMALLINT  NULL
)


INSERT INTO @DATA  SELECT 
ref.value( './@WorkFlowStepId', 'uniqueidentifier' ) as WorkFlowStepId,
ref.value( './@WorkFlowId', 'uniqueidentifier' ) as WorkFlowId,
ref.value( './@SequenceNumber', 'SMALLINT' ) as SequenceNumber
FROM @xmlWorkFlowSteps.nodes('/WorkFlowSteps/WorkFlowStep') as T(ref)

UPDATE WFS SET WFS.SequenceNumber=WFS1.SequenceNumber1 FROM [dbo].[WorkFlowStep]  WFS INNER JOIN @DATA WFS1 ON WFS.Id=WFS1.WorkFlowStepId1 AND WFS.WorkFlowId=WFS1.WorkFlowId1
  WHERE [TenantId]=@guidTenantId

IF @@ERROR <> 0
BEGIN
RETURN 1
END
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[WorkFlowStep_Update]    Script Date: 15-Jul-19 15:39:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowStep_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[WorkFlowStep_Update] AS' 
END
GO
ALTER PROCEDURE [dbo].[WorkFlowStep_Update]
(
	@guidTenantId UNIQUEIDENTIFIER,
	@guidWorkFlowStepId AS UNIQUEIDENTIFIER,
	@guidWorkFlowId AS UNIQUEIDENTIFIER,	
	@bIsAssigmentMandatory AS BIT,
	@intAllotedTime as int=null,
	@intCriticalTime as int=null
) AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

UPDATE [dbo].[WorkFlowStep] SET [IsAssignmentMandatory]=@bIsAssigmentMandatory   ,[AllotedTime]=@intAllotedTime,[CriticalTime]=@intCriticalTime
				WHERE [TenantId]=@guidTenantId AND [Id]=@guidWorkFlowStepId AND [WorkFlowId]=[WorkFlowId]

IF @@ERROR <> 0
BEGIN
RETURN 1
END
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[WorkFlowSteps_Get_WorkFlowIds]    Script Date: 15-Jul-19 15:39:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowSteps_Get_WorkFlowIds]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[WorkFlowSteps_Get_WorkFlowIds] AS' 
END
GO

ALTER PROCEDURE [dbo].[WorkFlowSteps_Get_WorkFlowIds]
(
	@guidTenantId UNIQUEIDENTIFIER,
	@xmlWorkFlowIds xml
) AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @DATA TABLE
(
idx INT IDENTITY(1,1),
Id1 uniqueidentifier NOT NULL
)
INSERT INTO @DATA  SELECT ref.value( './@value', 'uniqueidentifier' ) as Id FROM @xmlWorkFlowIds.nodes('/Items/Item') as T(ref)

SELECT [Id],[WorkFlowId],[TransitionType],[SequenceNumber],[IsAssignmentMandatory],[AllotedTime],[CriticalTime] FROM 
[dbo].[WorkFlowStep] WHERE   [TenantId]=@guidTenantId  AND [WorkFlowId] IN (SELECT Id1 FROM @DATA)


RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[WorkFlowSteps_GetBy_UserCode]    Script Date: 15-Jul-19 15:39:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowSteps_GetBy_UserCode]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[WorkFlowSteps_GetBy_UserCode] AS' 
END
GO

ALTER PROCEDURE [dbo].[WorkFlowSteps_GetBy_UserCode]
(
	@guidTenantId UNIQUEIDENTIFIER,
	@guidUserId UNIQUEIDENTIFIER,
	@bitIsSuperAdmin BIT
) AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT WFS.[Id],WFS.[WorkFlowId],WFS.[TransitionType],WFS.[SequenceNumber],WFS.[IsAssignmentMandatory],WFS.[AllotedTime],WFS.[CriticalTime] 
FROM [dbo].[WorkFlowStep] WFS WHERE   WFS.[TenantId]=@guidTenantId

AND (((@bitIsSuperAdmin=0) AND (EXISTS(
SELECT WFR.RoleId FROM [dbo].[WorkFlowRole] WFR INNER JOIN UserInRole UIR ON UIR.RoleId=WFR.RoleId
WHERE  WFR.TenantId=@guidTenantId AND  UIR.UserId=@guidUserId AND  WFR.WorkFlowStepId=WFS.Id 
))) OR (@bitIsSuperAdmin=1)) 


RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[WorkFlowSteps_UserCanAssign]    Script Date: 15-Jul-19 15:39:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowSteps_UserCanAssign]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[WorkFlowSteps_UserCanAssign] AS' 
END
GO

ALTER PROCEDURE [dbo].[WorkFlowSteps_UserCanAssign]
(
	@guidTenantId UNIQUEIDENTIFIER,
	@guidUserId UNIQUEIDENTIFIER,
    @strEntityId [dbo].[xSmallText] ,     	
    @strSubTypeCode [dbo].[smallText]
) AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT DISTINCT WFS.[TransitionType] FROM [WorkFlowRole] WFR 
INNER JOIN [dbo].[WorkFlowStep] WFS ON WFS.Id=WFR.WorkFlowStepId
INNER JOIN [dbo].[WorkFlow] WF ON WF.Id=WFS.WorkFlowId
INNER JOIN [dbo].[UserInRole] UIR ON UIR.RoleId=WFR.RoleId
WHERE  UIR.UserId=@guidUserId AND WF.EntityId= @strEntityId AND WF.SubType=@strSubTypeCode


RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[WorkFlowTransition_Create]    Script Date: 15-Jul-19 15:39:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowTransition_Create]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[WorkFlowTransition_Create] AS' 
END
GO
ALTER PROCEDURE [dbo].[WorkFlowTransition_Create]
    (
      @guidTenantId UNIQUEIDENTIFIER,
      @guidTransitionHistoryId AS UNIQUEIDENTIFIER,
	  @guidRefId  AS UNIQUEIDENTIFIER,
	  @strEntityCode  [dbo].[xSmallText] ,
	  @guidWorkFlowStepId AS UNIQUEIDENTIFIER,
	  @dtStartTime AS DATETIME,	
	  @guidAssignedUserId AS UNIQUEIDENTIFIER=null,	 
	  @guidCreatedBy  AS UNIQUEIDENTIFIER=null
      	
	)
AS
    BEGIN												
        SET NOCOUNT ON;
        SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

      INSERT INTO [dbo].[WorkFlowTransitionHistory](TenantId,Id,RefId,EntityId ,WorkFlowStepId,StartTime ,AssignedUserId,CreatedBy)
      VALUES    (@guidTenantId,@guidTransitionHistoryId,@guidRefId, @strEntityCode , @guidWorkFlowStepId, @dtStartTime,  @guidAssignedUserId,@guidCreatedBy)
         
    END;

GO
/****** Object:  StoredProcedure [dbo].[WorkFlowTransition_GetByRefId]    Script Date: 15-Jul-19 15:39:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowTransition_GetByRefId]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[WorkFlowTransition_GetByRefId] AS' 
END
GO
ALTER PROCEDURE [dbo].[WorkFlowTransition_GetByRefId]
    (
      @guidTenantId UNIQUEIDENTIFIER,
      @guidRefId AS UNIQUEIDENTIFIER
      	
	)
AS
    BEGIN												
        SET NOCOUNT ON;
        SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
        SELECT  
                WTF.Id ,
                WTF.RefId ,
                WTF.EntityId ,
                WTF.WorkFlowStepId,              
                WTF.StartTime ,
                WTF.EndTime ,
                WTF.AssignedUserId,          
				WTF.CreatedBy,
				WFS.TransitionType
        FROM   [dbo].[WorkFlowTransitionHistory] WTF
INNER JOIN WorkFlowStep WFS ON WFS.Id=WTF.WorkFlowStepId
        WHERE   WTF.[TenantId] = @guidTenantId  AND WTF.RefId = @guidRefId 
         
    END;

GO
/****** Object:  StoredProcedure [dbo].[WorkFlowTransition_Update]    Script Date: 15-Jul-19 15:39:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkFlowTransition_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[WorkFlowTransition_Update] AS' 
END
GO
ALTER PROCEDURE [dbo].[WorkFlowTransition_Update]
    (
      @guidTenantId UNIQUEIDENTIFIER,
      @guidTransitionHistoryId AS UNIQUEIDENTIFIER,	
	  @dtEndTime AS DATETIME	 
      	
	)
AS
    BEGIN												
        SET NOCOUNT ON;
        SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

		UPDATE [dbo].[WorkFlowTransitionHistory] SET EndTime=@dtEndTime WHERE TenantId=@guidTenantId AND Id=@guidTransitionHistoryId
		RETURN 0

         
    END;

GO
