using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace VPC.Entities.Role
{
    public enum RoleTypeEnum
    {
        [Description("IT Specialist")]
        ITSpecialist = 1,   

        [Description("HR Coordinator")]
        HRCoordinator = 2, 

        [Description("Service Technician")]
        ServiceTechnician = 3, 

        [Description("Detailer and Car Washe")]
        DetailerAndCarWashe = 4, 

        [Description("Smog Technician")]
        SmogTechnician = 5,
    }
}
