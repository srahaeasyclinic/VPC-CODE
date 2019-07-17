using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using VPC.Entities.Credential;
using VPC.Entities.Role;
using VPC.Framework.Business.Credential.APIs;
using VPC.Framework.Business.Credential.Data;
using VPC.Framework.Business.Data;
using VPC.Framework.Business.Data.SqlClient;

namespace VPC.Framework.Business.Credential.Contracts
{
    public interface IManagerCredential
    {
        bool Create(Guid tenantId, CredentialInfo info);
        bool Update(Guid tenantId, CredentialInfo info);
        bool Delete(Guid tenantId,CredentialInfo info); 

        Guid GetUserName(Guid tenantId,string userName );
        CredentialInfo GetPassword(Guid tenantId,string userName );
        CredentialInfo GetCredential(Guid tenantId,Guid refId );

        bool  SetIsNew (Guid tenantId,CredentialInfo info);
        bool UpdateLockedStatus (Guid tenantid,Guid  credentialId,bool islocked,int? attemptcount,DateTime? lockeddowndate);
     
    }
    internal class ManagerCredential : IManagerCredential
    {
        private readonly IAdminCredential _adminCredential = new AdminCredential();
        private readonly IReviewCredential _reviewCredential = new ReviewCredential();
        bool IManagerCredential.Create(Guid tenantId, CredentialInfo info)
        {
            return _adminCredential.Create(tenantId,info);
        }

        bool IManagerCredential.Delete(Guid tenantId, CredentialInfo info)
        {
             return _adminCredential.Delete(tenantId,info);
        }

        bool IManagerCredential.Update(Guid tenantId, CredentialInfo info)
        {
            return _adminCredential.Update(tenantId,info);
        }

        CredentialInfo IManagerCredential.GetCredential(Guid tenantId, Guid refId)
        {
           return _reviewCredential.GetCredential(tenantId,refId);
        }

        CredentialInfo IManagerCredential.GetPassword(Guid tenantId, string userName)
        {
            return _reviewCredential.GetPassword(tenantId,userName);
        }

        Guid IManagerCredential.GetUserName(Guid tenantId, string userName)
        {
           return _reviewCredential.GetUserName(tenantId,userName);
        }
        bool IManagerCredential.SetIsNew(Guid tenantId, CredentialInfo info)
        {
            return _adminCredential.SetIsNew(tenantId,info);
        }
        bool IManagerCredential.UpdateLockedStatus(Guid tenantid,Guid  credentialId,bool islocked,int? attemptcount,DateTime? lockeddowndate)
        {
            return _reviewCredential.UpdateLockedStatus( tenantid,  credentialId, islocked, attemptcount,lockeddowndate);
        }
        
    }
}
