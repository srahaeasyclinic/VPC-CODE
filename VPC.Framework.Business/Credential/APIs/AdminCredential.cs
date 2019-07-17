using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using VPC.Entities.Credential;
using VPC.Entities.Role;
using VPC.Framework.Business.Credential.Data;
using VPC.Framework.Business.Data;
using VPC.Framework.Business.Data.SqlClient;

namespace VPC.Framework.Business.Credential.APIs
{
    public interface IAdminCredential
    {
       bool Create(Guid tenantId, CredentialInfo info);

        bool Update(Guid tenantId, CredentialInfo info);

        bool Delete(Guid tenantId,CredentialInfo info);  

        
         bool SetIsNew(Guid tenantId,CredentialInfo info);  
    }
    internal class AdminCredential : IAdminCredential
    {
        DataCredential data=new DataCredential();
        bool IAdminCredential.Create(Guid tenantId, CredentialInfo info)
        {
            return data.Create(tenantId,info);
        }

        bool IAdminCredential.Delete(Guid tenantId, CredentialInfo info)
        {
             return data.Delete(tenantId,info);
        }

        bool IAdminCredential.Update(Guid tenantId, CredentialInfo info)
        {
            return data.Update(tenantId,info);
        }
          bool IAdminCredential.SetIsNew(Guid tenantId, CredentialInfo info)
        {
            return data.SetIsNew(tenantId,info);
        }
    }
}
