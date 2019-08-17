using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using NLog;
using VPC.Entities.BatchType;
using VPC.Entities.Common;
using VPC.Entities.Credential;
using VPC.Entities.EntityCore;
using VPC.Entities.EntityCore.Metadata;
using VPC.Entities.EntityCore.Model.Query;
using VPC.Entities.WorkFlow.Engine.Email;
using VPC.Framework.Business.BatchItems.APIs;
using VPC.Framework.Business.BatchType.Contracts;
using VPC.Framework.Business.Common;
using VPC.Framework.Business.Credential;
using VPC.Framework.Business.Credential.Contracts;
using VPC.Framework.Business.EntityResourceManager.Contracts;
using VPC.Framework.Business.SchedulerConfiguration.Scheduler.Contracts;
using static VPC.Entities.EntityCore.Metadata.Picklist.CommunicationContextType;


namespace VPC.Framework.Business.Task.UserTask
{
    public interface IUserTaskManager
    {
        bool ResetPassword(Guid tenantId, Guid userId);
        bool ImportUser(Guid tenantId, Guid userId);
    }
    public class UserTaskManager : IUserTaskManager
    {
        IManagerCredential crd = new ManagerCredential(); 
        IEntityResourceManager _iEntityResourceManager = new VPC.Framework.Business.EntityResourceManager.Contracts.EntityResourceManager(); 
        SqlMembershipProvider _sqlMembership=new SqlMembershipProvider();     
        IManagerBatchItem _managerBatchItem=new ManagerBatchItem();
        IManagerBatchType _managerBatchType=new ManagerBatchType();
        IManagerScheduler _schedulerManager = new ManagerScheduler();
        private readonly Logger _log = LogManager.GetCurrentClassLogger();
        
        
        bool IUserTaskManager.ResetPassword(Guid tenantId, Guid userId)
        {            
            var queryFilter = new List<QueryFilter>();
            queryFilter.Add(new QueryFilter { FieldName = "TenantId", Operator = "Equal", Value = tenantId.ToString() });
            queryFilter.Add(new QueryFilter { FieldName = "InternalId", Operator = "Equal", Value = userId.ToString() });
            var queryContext = new QueryContext { Fields = "FirstName,LastName,MiddleName,UserCredential.Username", Filters = queryFilter, PageSize = 100, PageIndex = 1, MaxResult = 1 };

            byte[] passwordHash, passwordSalt;
            Random random = new Random();
            int pass = random.Next(1000000);
            // pass=1234;
            SqlMembershipProvider.CreatePasswordHash(pass.ToString(), out passwordHash, out passwordSalt);            
            DataTable dataTableUser = _iEntityResourceManager.GetResultById(tenantId, "user", userId, queryContext);
            User userEntity = EntityMapper<User>.Mapper(dataTableUser);

            if (Guid.Parse(userEntity.InternalId.Value) == Guid.Empty)
                return false;
            CredentialInfo credentialData = crd.GetCredential(tenantId, Guid.Parse(userEntity.InternalId.Value));
            var jObject = DataUtility.ConvertToJObjectList(dataTableUser);
            jObject[0].Add(new JProperty("UserCredential.Username", credentialData.UserName.ToString()));
            jObject[0].Add(new JProperty("UserCredential.Password", pass.ToString()));            
            var emailTemplate = _iEntityResourceManager.GetWellKnownTemplate(tenantId, "emailtemplate", "user", (int)ContextTypeEnum.Forgotpassword, jObject[0]);
            if (emailTemplate != null && emailTemplate.Body != null)
            {
                CredentialInfo usercredentialinfo = crd.GetCredential(tenantId, userId);
                bool isnew = _sqlMembership.CheckResetOnFirstLogin(tenantId);
                crd.Update(tenantId, new CredentialInfo
                {
                    CredentialId = credentialData.CredentialId,
                    ParentId = Guid.Parse(userEntity.InternalId.Value),
                    PasswordHash = Convert.ToBase64String(passwordHash),
                    PasswordSalt = Convert.ToBase64String(passwordSalt),
                    IsNew = isnew
                });
                var returnVal = DataUtility.SaveEmail(tenantId, Guid.Parse(userEntity.InternalId.Value), emailTemplate, credentialData.UserName.ToString(),"ResetPassword",InfoType.User);
            }
            else
            {
                return false;
            }

            return true;
        }      

        bool IUserTaskManager.ImportUser(Guid tenantId, Guid userId)
        {
            try{

                  var batchType=_managerBatchType.GetBatchTypeByContext(tenantId,BatchTypeContextEnum.ExportUser);
                  if(batchType==null)
                  {
                      return false;
                  }

                var batchItemId=Guid.NewGuid();              
                _managerBatchItem.BatchItemCreate(tenantId,(!string.IsNullOrEmpty(batchType.ItemTimeout.Value) ? Convert.ToInt32(batchType.ItemTimeout.Value) : (int?)null)
                ,new BatchItem{
                    BatchItemId=batchItemId,
                    BatchTypeId=Guid.Parse(batchType.InternalId.Value),
                    Name=string.Format("{0}-{1}{2}{3}", "ExportUser",DateTime.UtcNow.Year,DateTime.UtcNow.Month, DateTime.UtcNow.Day),
                    Priority=(!string.IsNullOrEmpty(batchType.Priority.Value) ? Convert.ToInt32(batchType.Priority.Value) : (int?)null) ,
                    RetryCount=0 ,                     
                    EntityId=InfoType.User,
                    ReferenceId=Guid.Empty,
                    Status=EmailEnum.ReadyToSend,
                    NextRunTime= Convert.ToInt32(batchType.Type.Value) == (int)BatchTypeEnum.Scheduled ? _schedulerManager.GetNextRunDateTime (tenantId,new Guid(batchType.Scheduler.InternalId.Value)) : DateTime.MinValue,                   
                    AuditDetails=new AuditDetail{
                    CreatedBy=userId
                    } 
                });
                return true;

            }catch(System.Exception ex)
            {
                _log.Error("UserTaskManager ImportUser having exception message" + ex.Message); 
            }
            return false;
          

            // var queryFilter = new List<QueryFilter>();
            // queryFilter.Add(new QueryFilter { FieldName = "TenantId", Operator = "Equal", Value = tenantId.ToString() });
            // queryFilter.Add(new QueryFilter { FieldName = "InternalId", Operator = "Equal", Value = userId.ToString() });
            // var queryContext = new QueryContext { Fields = "FirstName,LastName,MiddleName,PersonalEmail1,PersonalEmail1", Filters = queryFilter, PageSize = 100, PageIndex = 1, MaxResult = 1 };         
            // DataTable dataTableUser = _iEntityResourceManager.GetResultById(tenantId, "user", userId, queryContext);
            // User userEntity = EntityMapper<User>.Mapper(dataTableUser);

            // if (Guid.Parse(userEntity.InternalId.Value) == Guid.Empty)
            //     return false;
            // CredentialInfo credentialData = crd.GetCredential(tenantId, Guid.Parse(userEntity.InternalId.Value));
            // var jObject = DataUtility.ConvertToJObjectList(dataTableUser);                       
            // var emailTemplate = _iEntityResourceManager.GetWellKnownTemplate(tenantId, "emailtemplate", "user", (int)ContextTypeEnum.ExportUser, jObject[0]);
            // if (emailTemplate != null && emailTemplate.Body != null)
            // {                
            //     var returnVal = DataUtility.SaveEmail(tenantId, Guid.Parse(userEntity.InternalId.Value), emailTemplate, credentialData.UserName.ToString(),"UserExport",InfoType.User);
            //     //Save data as Excel
            //     CreateExcelFile(tenantId,returnVal,userEntity.FirstName.Value,userEntity.LastName.Value);

            // }
            // else
            // {
            //     return false;
            // }

            // return true;
        }

        // private void CreateExcelFile(Guid tenantId,Guid batchItemId,string firstName,string lastName)
        // {
        //     using (ExcelEngine excelEngine = new ExcelEngine())
        //         {
        //             //Initialize Application.
        //             IApplication application = excelEngine.Excel;
                
        //             //Set default version for application.
        //             application.DefaultVersion = ExcelVersion.Excel2013;
                
        //             //Create a new workbook.
        //             IWorkbook workbook = application.Workbooks.Create(1);
                
        //             //Accessing first worksheet in the workbook.
        //             IWorksheet worksheet = workbook.Worksheets[0];
                
        //             //Adding text to a cell
        //             worksheet.Range["A1"].Text = firstName;
        //             worksheet.Range["A2"].Text = lastName;
                
        //             //Saving the Excel to the MemoryStream 
        //             MemoryStream stream = new MemoryStream();
                
        //             workbook.SaveAs(stream);
                
        //             //Set the position as '0'.
        //             stream.Position = 0;
                
        //             //Download the Excel file in the browser
        //             FileStreamResult fileStreamResult = new FileStreamResult(stream, "application/excel");
                
        //             fileStreamResult.FileDownloadName = "Output.xlsx";

        //             string Path = @"d:\Output.xlsx";

        //             if (!File.Exists(Path))
        //                 {
        //                     using (var sw = File.CreateText(Path))
        //                     {
        //                         sw.WriteLine("Ajay chouhan");
        //                     }
        //                 }

        //             //  _managerBatchItem.BatchContentCreate(tenantId,new Entities.BatchType.BatchItemContent{
        //             //      BatchItemContentId=Guid.NewGuid(),
        //             //      BatchItemId=batchItemId,
        //             //      Content=fileStreamResult.ToString(),
        //             //      Status=EmailEnum.ReadyToSend,
        //             //      FailedReason=string.Empty,
                     
        //             //  });
                
        //            // return fileStreamResult;
        //         }
        // }

    }
}
