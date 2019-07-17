namespace VPC.Entities.EntityCore.Metadata.Picklist
{
    public static class PicklistType
    {
        public static short Title = short.Parse(PicklistTypeStr.Title);
        public static short BankAccountType = short.Parse(PicklistTypeStr.BankAccountType);
        public static short Gender = short.Parse(PicklistTypeStr.Gender);
        public static short Currency = short.Parse(PicklistTypeStr.Currency);
        public static short Country = short.Parse(PicklistTypeStr.Country);
        public static short Language = short.Parse(PicklistTypeStr.Language);
        public static short Timezone = short.Parse(PicklistTypeStr.Timezone);
        public static short State = short.Parse(PicklistTypeStr.State);
        public static short City = short.Parse(PicklistTypeStr.City);
        public static short Municipality = short.Parse(PicklistTypeStr.Municipality);
        public static short SecurityFunction = short.Parse(PicklistTypeStr.SecurityFunction);
        public static short Uom = short.Parse(PicklistTypeStr.Uom);
        public static short UserCategory = short.Parse(PicklistTypeStr.UserCategory);
        public static short UserGroup = short.Parse(PicklistTypeStr.UserGroup);
        public static short TenantType = short.Parse(PicklistTypeStr.TenantType);
        public static short OrganizationType = short.Parse(PicklistTypeStr.OrganizationType);
        public static short Qualification = short.Parse(PicklistTypeStr.Qualification);
        public static short Profession = short.Parse(PicklistTypeStr.Profession);
        public static short UnitType = short.Parse(PicklistTypeStr.UnitType);
        public static short TimeCalculationType = short.Parse(PicklistTypeStr.TimeCalculationType);
        public static short Active = short.Parse(PicklistTypeStr.Active);
        public static short SubscriptionCategory = short.Parse(PicklistTypeStr.SubscriptionCategory);
        public static short RecurringUnit = short.Parse(PicklistTypeStr.RecurringUnit);
        public static short IOTDevice = short.Parse(PicklistTypeStr.IOTDevice);
        public static short WorkcenterType = short.Parse(PicklistTypeStr.WorkcenterType);
        public static short WorkcenterSubType = short.Parse(PicklistTypeStr.WorkcenterSubType);
        public static short OperationType = short.Parse(PicklistTypeStr.OperationType);
        public static short Department = short.Parse(PicklistTypeStr.Department);
        public static short ProductionLine = short.Parse(PicklistTypeStr.ProductionLine);
        public static short Competence = short.Parse(PicklistTypeStr.Competence);
        public static short RouteTemplateType = short.Parse(PicklistTypeStr.RouteTemplateType);
        public static short ProductCategory = short.Parse(PicklistTypeStr.ProductCategory);
        public static short ProductGroup = short.Parse(PicklistTypeStr.ProductGroup);
        public static short ProductType = short.Parse(PicklistTypeStr.ProductType);
        public static short ProcessingType = short.Parse(PicklistTypeStr.ProcessingType);
        public static short TaxCodeCategory = short.Parse(PicklistTypeStr.TaxCodeCategory);
        public static short StockGroupCategory = short.Parse(PicklistTypeStr.StockGroupCategory);
        public static short BomType = short.Parse(PicklistTypeStr.BomType);
        public static short BomCategory = short.Parse(PicklistTypeStr.BomCategory);
        public static short ProducedType = short.Parse(PicklistTypeStr.ProducedType);
        public static short RuleType = short.Parse(PicklistTypeStr.RuleType);
        public static short ProcurementGroup = short.Parse(PicklistTypeStr.ProcurementGroup);
        public static short MenuType = short.Parse(PicklistTypeStr.MenuType);
        public static short MenuGroup = short.Parse(PicklistTypeStr.MenuGroup);
        public static short ActionType = short.Parse(PicklistTypeStr.ActionType);
        public static short SubscriptionGroup = short.Parse(PicklistTypeStr.SubscriptionGroup);
        public static short CommunicationContextType = short.Parse(PicklistTypeStr.CommunicationContextType);
        public static short RoomType = short.Parse(PicklistTypeStr.RoomType);




        public static short CommunicationDirection = short.Parse(PicklistTypeStr.CommunicationDirection);
        public static short CounterSchedule = short.Parse(PicklistTypeStr.CounterSchedule);

        // extendedPicklistsValue
        public static short EmploymentStatus = short.Parse(PicklistTypeStr.EmploymentStatus);
        public static short ReasonForLeaving = short.Parse(PicklistTypeStr.ReasonForLeaving);
        public static short Designation = short.Parse(PicklistTypeStr.Designation);
        public static short LeaveCategory = short.Parse(PicklistTypeStr.LeaveCategory);
        public static short AutoReleaseMode = short.Parse(PicklistTypeStr.AutoReleaseMode);

        public static short CommunicationMetadata = short.Parse(PicklistTypeStr.MetaDataPikclist);
        //public static short CommunicationContextType = short.Parse(PicklistTypeStr.CommType);
        public static short HPicklist = short.Parse(PicklistTypeStr.HiertestPikclist);
    }

    public static class PicklistTypeStr
    {

        public static string Title = "10001";
        public static string OrganizationType = "10002";
        public static string BankAccountType = "10003";
        public static string Gender = "10004";
        public static string Qualification = "10005";
        public static string Profession = "10006";
        public static string UnitType = "10007";
        public static string TimeCalculationType = "10008";
        public static string Active = "10009";
        public static string SubscriptionCategory = "10010";
        public static string RecurringUnit = "10011";
        public static string CommunicationDirection = "10012";
        public static string UserCategory = "10013";
        public static string UserGroup = "10014";
        public static string MenuGroup = "10015";
        public static string SubscriptionGroup = "10016";
        public static string Uom = "10017";
        public static string ActionType = "10018";
        public static string MenuType = "10019";
        public static string TenantType = "10020";
        public static string IOTDevice = "10021";
        public static string WorkcenterType = "10022";
        public static string WorkcenterSubType = "10023";
        public static string OperationType = "10024";
        public static string Department = "10025";
        public static string ProductionLine = "10026";
        public static string Competence = "10027";
        public static string RouteTemplateType = "10028";
        public static string ProductCategory = "10029";
        public static string ProductGroup = "10030";
        public static string ProductType = "10031";
        public static string ProcessingType = "10032";
        public static string TaxCodeCategory = "10033";
        public static string StockGroupCategory = "10034";
        public static string BomType = "10035";
        public static string BomCategory = "10036";
        public static string ProducedType = "10037";
        public static string RuleType = "10038";
        public static string ProcurementGroup = "10039";
        public static string CommunicationContextType = "10040";
        public static string AutoReleaseMode = "10041";
        public static string RoomType = "10042";
        public static string CounterSchedule = "10043";
        public static string EmploymentStatus = "10044";
        public static string ReasonForLeaving = "10045";
        public static string Designation = "10046";


        //Standard complex pick-list        
        public static string Currency = "20001";
        public static string Country = "20002";
        public static string Language = "20003";
        public static string Timezone = "20004";
        public static string State = "20005";
        public static string City = "20006";
        public static string Municipality = "20007";
        public static string SecurityFunction = "20008";

        //extendedpicklists    need to be change these value
        public static string LeaveCategory = "30006";
        //public static string CommType = "30007";
        public static string MetaDataPikclist = "30008";
        public static string HiertestPikclist = "30009";
    }
}