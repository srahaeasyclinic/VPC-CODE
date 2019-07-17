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
    public interface IReviewCredential
    {
        Guid GetUserName(Guid tenantId,string userName );

        CredentialInfo GetPassword(Guid tenantId,string userName );

        CredentialInfo GetCredential(Guid tenantId,Guid refId ); 
        List<CredentialHistory> GetCredentialHistory(Guid tenantId,Guid refId ,int count); 
        bool UpdateLockedStatus (Guid tenantid,Guid  credentialId,bool islocked,int? attemptcount,DateTime? lockeddowndate);
      
        
    }
    internal class ReviewCredential : IReviewCredential
    {
        DataCredential data=new DataCredential();
        DataCredentialHistory datacredentialhistory=new DataCredentialHistory();
      
        CredentialInfo IReviewCredential.GetCredential(Guid tenantId, Guid refId)
        {
           return data.GetCredential(tenantId,refId);
        }

        CredentialInfo IReviewCredential.GetPassword(Guid tenantId, string userName)
        {
            return data.GetPassword(tenantId,userName);
        }

        Guid IReviewCredential.GetUserName(Guid tenantId, string userName)
        {
           return data.GetUserName(tenantId,userName);
        }
        List<CredentialHistory> IReviewCredential.GetCredentialHistory(Guid tenantId,Guid refId ,int count)
        {
             return datacredentialhistory.GetCredentialHistory(tenantId,refId, count);
        }

        bool IReviewCredential.UpdateLockedStatus (Guid tenantid,Guid  credentialId,bool islocked,int? attemptcount,DateTime? lockeddowndate)
        {
            return data.UpdateLockedStatus ( tenantid,  credentialId, islocked, attemptcount,lockeddowndate);
        }

        

     
    }
}
