namespace VPC.Entities.EntityCore
{
    public class InfoType
    {
        #region Infrastructure entity

        public const string Tenant = "EN10001";
        public const string Company = "EN10002";
        public const string User = "EN10003";
        public const string Role = "EN10004";
        public const string Customer = "EN10005";
        public const string Location = "EN10006";
        public const string Room = "EN10007";
        public const string TenantIpRange = "EN10008";
      //  public const string Subscription = "EN10009";
        //public const string TenantServiceStatus = "EN10010";
        public const string Menu = "EN10011";
        //EN10012
        //public const string Department = "EN10013";
        public const string Email = "EN10014";
        public const string SMS = "EN10015";
        public const string Emailtemplate = "EN10016";
        public const string SMStemplate = "EN10017";
        public const string UserSecurityRole = "EN10018";
        public const string UserCompany = "EN10019";
        public const string UserDepartment = "EN10020";
        public const string UserActivity = "EN10021";
        public const string UserInRole = "EN10022";
        public const string UserLocation = "EN10023";
        public const string UserLeave = "EN10024";
        //public const string UserInRole = "EN10025";
        public const string Contact = "EN10026";

        #endregion

        #region Production entity 

        public const string ProductionOrderOperationConfiguration = "EN10024";

        public const string Attribute = "EN20001";

        public const string AttributeValue = "EN20002";

        public const string Tool = "EN20003";

        public const string Equipment = "EN20004";

        public const string EquipmentIOTDevice = "EN20005";

        public const string PlanningGroup = "EN20006";

        public const string Manufacturer = "EN20007";

        public const string Vendor = "EN20008";

        public const string Warehouse = "EN20009";

        public const string WarehouseLocation = "EN20010";

        public const string UOMConversion = "EN20011";

        public const string Workcenter = "EN20012";

        public const string WorkcenterCompetence = "EN20013";

        public const string WorkcenterTool = "EN20014";

        public const string WorkcenterEquipment = "EN20015";

        public const string WorkcenterUser = "EN20016";

        public const string RouteTemplate = "EN20017";

        public const string RouteTemplateOperation = "EN20018";

        public const string RouteTemplateOperationWorkcenter = "EN20019";

        public const string RouteTemplateOperationWorkcenterCompetence = "EN20020";

        public const string Product = "EN20021";

        public const string BOM = "EN20022";

        public const string BOMComponent = "EN20023";

        public const string BOMComponentAttribute = "EN20024";

        public const string BOMComponentAttributeValue = "EN20025";

        public const string BOMAlternativeComponent = "EN20026";

        public const string BOMProduced = "EN20027";

        public const string ProductRoute = "EN20028";

        public const string ProductOperation = "EN20029";

        public const string ProductOperationWorkcenter = "EN20030";

        public const string ProductOperationWorkcenterCompetence = "EN20031";

        public const string ProductOperationWorkcenterTool = "EN20032";

        public const string ProductOperationWorkcenterEquipment = "EN20033";

        public const string ProductVariant = "EN20034";

        public const string ProductVariantAttribute = "EN20035";

        public const string ProductAttribute = "EN20036";

        public const string ProductAttributeValue = "EN20037";

        public const string ProductVariantRule = "EN20038";

        public const string ProductVariantRuleAttribute = "EN20039";

        public const string ProductVariantRuleAttributeValue = "EN20040";

        public const string ProductTool = "EN20041";

        public const string ProductRouting = "EN20042";

        public const string ProductVendor = "EN20043";

        public const string ProductionOrder = "EN20044";

        public const string ProductionOrderComponent = "EN20045";

        public const string ProductionOrderAlternativeComponent = "EN20046";

        public const string ProductionOrderOperation = "EN20047";

        public const string ProductionOrderOperationCompetence = "EN20048";

        public const string ProductionOrderOperationTool = "EN20049";

        public const string ProductionOrderOperationEquipment = "EN20050";

        public const string ProductionOrderProduced = "EN20051";

        public const string Collection = "EN20052";

        public const string CollectionTask = "EN20053";

        public const string CollectionTaskDetail = "EN20054";

        public const string LockRegister = "EN20055";

        public const string LockRegisterDetail = "EN20056";

        public const string ProcurementRule = "EN20057";

        public const string ProductVersion = "EN20058";        

        //test purpose (added by @tad)
        public const string RoleTest = "RLT00001";
        public const string TenantSubscriptions = "EN20059";
        public const string BatchType = "EN20060";

        public const string PriceList = "EN20061";
 public const string PriceListValue = "EN20062";

        #endregion


        public const string T_Product = "EN2002100";
        public const string T_ProductVersion = "EN2002101";
    }
}