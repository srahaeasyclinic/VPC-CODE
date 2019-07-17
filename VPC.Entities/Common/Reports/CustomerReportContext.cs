
using System;
using System.ComponentModel;
using VPC.Entities.Common.Functions;
using VPC.Entities.EntityCore;

namespace VPC.Entities.Common.Reports
{
  public partial class ReportContext
    {        
        [EntityGroup(InfoType.Customer, "Customer Report1")]    
        public static Guid CustomerReport1 = new Guid(CustomerReportContext.CustomerReport1);

        [EntityGroup(InfoType.Customer, "Customer Report2")]    
        public static Guid CustomerReport2 = new Guid(CustomerReportContext.CustomerReport2);

        [EntityGroup(InfoType.Customer, "Customer Report3")]    
        public static Guid CustomerReport3 = new Guid(CustomerReportContext.CustomerReport3);
    }

    internal static class CustomerReportContext
    {
         internal const string CustomerReport1 = "47E85619-533A-4D6C-9003-4C6CF8FA9349";
         internal const string CustomerReport2 = "C517D06C-BA51-49E6-BDD2-49161EF7149D";
         internal const string CustomerReport3 = "B7CCF2FE-9FF2-4AC6-83C6-E4AE12E33886";


    }
}
