using System.ComponentModel;

namespace VPC.Entities.WorkFlow
{
    public enum WorkFlowOperationType
    {
        //public const string VisitRegistration = "20DED854-C9E6-4B8D-A8F5-D8620CB612AA";  

        [Description("Create")]
        Create = 1,   

        [Description("Update")]
        Update = 2, 

        [Description("Delete")]
        Delete = 3, 

        [Description("Update status")]
        UpdateStatus = 4, 
    }
}
