using System;
using VPC.Entities.EntityCore;


namespace VPC.Entities.WorkFlow.Engine
{
    public static partial class WorkFlowEngine
    {
        public const string _registrationProductOrder = "D817115A-15A1-47D4-9465-3C52753E8DA9";
        public const string _planning = "DD8EC323-4AB6-45A7-BA8C-A52C68810CF1";
        public const string _approval = "8DF9EC48-9A64-49C2-A33D-5EAB1620E5F5";

        public const string _release = "ADBCEC9E-E076-4C14-9567-D6BC13650EDE";
        public const string _reporting = "5C3513E6-D452-4302-AA11-C1860C81BD30";
        public const string _closeProductOrder  = "DDB7AAEA-4D6A-42F5-BB7F-D9610182B207";

        public const string _cancelProductOrder  = "47599490-19FA-41FC-A252-9FC843283D6F";
        public const string _pause = "2880F1A8-AA61-4BB5-A2AA-359151743E4F";
        public const string _productionOrder = InfoType.ProductionOrder;
        public const string _productionOrderName = "Key_ProductionOrder";


        [WorkFlowModel(Name = _productionOrderName, Context = _productionOrder, Key = "Registration", Status = "Registered", Description="PO_Register_Desc")]
        public static Guid RegistrationProductOrder 
        {
            get
            {
                return new Guid(_registrationProductOrder );
            }
        }
        
        [WorkFlowModel(Name = _productionOrderName, Context = _productionOrder, Key = "Planning", Status = "ToPlanning", Description="PO_ToPlanning_Desc")]
        public static Guid Planning
        {
            get
            {
                return new Guid(_planning);
            }
        }

        [WorkFlowModel(Name = _productionOrderName, Context = _productionOrder, Key = "Approval", Status = "Planned", Description="PO_Approval_Desc")]
        public static Guid Approval
        {
            get
            {
                return new Guid(_approval);
            }
        }

        [WorkFlowModel(Name = _productionOrderName, Context = _productionOrder, Key = "Release", Status = "Released", Description="PO_Release_Desc")]
        public static Guid Release
        {
            get
            {
                return new Guid(_release);
            }
        }

        [WorkFlowModel(Name = _productionOrderName, Context = _productionOrder, Key = "Reporting", Status = "Started", Description="PO_Started_Desc")]
        public static Guid Reporting
        {
            get
            {
                return new Guid(_reporting);
            }
        }

        [WorkFlowModel(Name = _productionOrderName, Context = _productionOrder, Key = "Close", Status = "Finished",Description="PO_Finish_Desc")]
        public static Guid CloseProductOrder 
        {
            get
            {
                return new Guid(_closeProductOrder );
            }
        }

        [WorkFlowModel(Name = _productionOrderName, Context = _productionOrder, Key = "Cancel", Status = "Cancelled",Description="PO_Cancelled_Desc")]
        public static Guid CancelProductOrder 
        {
            get
            {
                return new Guid(_cancelProductOrder );
            }
        }

        [WorkFlowModel(Name = _productionOrderName, Context = _productionOrder, Key = "Pause", Status = "OnHold", Description="PO_OnHold_Desc")]
        public static Guid Pause
        {
            get
            {
                return new Guid(_pause);
            }
        }


    }

}
