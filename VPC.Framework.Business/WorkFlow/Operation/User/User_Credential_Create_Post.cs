

using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using NLog;
using VPC.Entities.Credential;
using VPC.Entities.EntityCore.Model.Query;
using VPC.Entities.WorkFlow;
using VPC.Entities.WorkFlow.Engine;
using VPC.Framework.Business.Credential;
using VPC.Framework.Business.EntityResourceManager.Contracts;
using VPC.Framework.Business.MetadataManager.Contracts;
using VPC.Framework.Business.WorkFlow.Attribute;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Framework.Business.Common;
using VPC.Entities.EntityCore;
using static VPC.Entities.EntityCore.Metadata.Picklist.CommunicationContextType;
using VPC.Entities.Common;
using VPC.Framework.Business.DynamicQueryManager.Contracts;
using VPC.Entities.EntityCore.Metadata;
using VPC.Framework.Business.Credential.Contracts;

namespace VPC.Framework.Business.WorkFlow.Operation.User
{
    //put Guid from Work flow engine and context for work flow type
    [Operation(OperationName = Operations.Create, Context = WorkFlowEngine._user,
     Key = "User_Credential_Create_Post", Id = "69F02214-24ED-4CBB-BCE3-9B8AD21F5928", ProcessType = WorkFlowProcessType.PostProcess)]
    public class User_Credential_Create_Post : IOperation
    {
        private readonly Logger _log = LogManager.GetCurrentClassLogger();
        IEntityResourceManager _iEntityResourceManager = new VPC.Framework.Business.EntityResourceManager.Contracts.EntityResourceManager();
        WorkFlowProcessMessage IOperation.Execute(dynamic obj)
        {
            var objWorkFlowProcessMessage = new WorkFlowProcessMessage { Success = true };
            var tenantId = (Guid)obj[2];
            Guid userId =Guid.Parse(obj[0].resultId.ToString());
            //throw new CustomWorkflowException<WorkFlowMessage>(WorkFlowMessage.NoOperation);
            if (userId != Guid.Empty)
            {
                InitCredential(tenantId, userId);
            }
            return objWorkFlowProcessMessage;

        }
        private void InitCredential(Guid newTenantId, Guid userId)
        {
            var queryFilter = new List<QueryFilter>();           
            SqlMembershipProvider sqlMembership = new SqlMembershipProvider();
            PasswordPolicy passwordpolicy = sqlMembership.getPasswordPolicy(newTenantId, true);
            IManagerCredential crd = new ManagerCredential();
            CredentialInfo credentialData = crd.GetCredential(newTenantId,userId);
            var isnew = false;
            if (passwordpolicy != null)
            {
                isnew = passwordpolicy.ResetOnFirstLogin.Value;
            }
            crd.SetIsNew(newTenantId, new CredentialInfo
            {
                CredentialId = credentialData.CredentialId,
                ParentId = credentialData.ParentId,
                IsNew = isnew
            });

        }

    }

}
