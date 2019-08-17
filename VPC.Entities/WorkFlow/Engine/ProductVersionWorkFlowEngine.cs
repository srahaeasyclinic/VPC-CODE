using System;
using System.Collections.Generic;
using System.Text;
using VPC.Entities.EntityCore;

namespace VPC.Entities.WorkFlow.Engine
{
    public static partial class WorkFlowEngine
    {
         public const string _checkInProductVersion = "A0341F38-8C6F-4577-997D-1C194EC4D0CB";
         public const string _toApproveProductVersion = "4A253266-1F72-4B2E-A971-1F7224D1A189";
         public const string _approvedProductVersion = "09E9FDC0-61B0-4336-8300-317F48E5D8F7";
         
         public const string _productVersion = InfoType.ProductVersion;
         public const string _productVersionName = "ProductVersion";

        [WorkFlowModel(Name = _productVersionName, Context = _productVersion, Key = "CheckInProductVersion", Status = "CheckedInProductVersion", Description = "CheckInProductVersionDesc",TransitionResourceValue="Checkedin",StatusResourceValue ="Checkin")]
        public static Guid NewProductVersion
        {
            get
            {
                return new Guid(_checkInProductVersion);
            }
        }

        [WorkFlowModel(Name = _productVersionName, Context = _productVersion, Key = "ToApprovedProductVersion", Status = "ToApprovalProductVersion", Description = "ToApprovalProductVersionDesc",TransitionResourceValue="Approved",StatusResourceValue ="To approval")]
        public static Guid ToApprovalProductVersion
        {
            get
            {
                return new Guid(_toApproveProductVersion);
            }
        }

        [WorkFlowModel(Name = _productVersionName, Context = _productVersion, Key = "ApproveProductVersion", Status = "ApprovedProductVersion", Description = "ApprovedProductVersionDesc",TransitionResourceValue="Approved",StatusResourceValue ="Approve")]
        public static Guid ApproveProductVersion
        {
            get
            {
                return new Guid(_approvedProductVersion);
            }
        }
    }
}
