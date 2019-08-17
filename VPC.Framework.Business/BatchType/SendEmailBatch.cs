using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Mail;
using VPC.Entities.BatchType;
using VPC.Entities.Common;
using VPC.Entities.Email;
using VPC.Entities.EntityCore.Model.Query;
using VPC.Entities.EntityCore.Model.Setting;
using VPC.Entities.WorkFlow.Engine.Email;
using VPC.Framework.Business.BatchItems.APIs;
using VPC.Framework.Business.DynamicQueryManager.Contracts;
using VPC.Framework.Business.EntityResourceManager.Contracts;
using VPC.Framework.Business.SettingsManager.Contracts;
using System.Net.Mime;

namespace VPC.Framework.Business.BatchType
{
    [BatchType((int)BatchTypeContextEnum.Email)]
    public partial class SendEmailBatch : IBatchTypes
    {
        private readonly Logger _log = LogManager.GetCurrentClassLogger();
        private readonly ISettingManager _iSettingManager = new SettingManager();
        IEntityResourceManager _iEntityResourceManager = new VPC.Framework.Business.EntityResourceManager.Contracts.EntityResourceManager ();
       // IEntityResourceManager _iEntityResourceManager = new VPC.Framework.Business.EntityResourceManager.Contracts.EntityResourceManager();
       // IManagerWorkFlow _workFlow = new ManagerWorkFlow();
        IEntityQueryManager _queryManager = new EntityQueryManager();
        //IOperationFlowEngine operationEngine = new OperationFlowEngine();
        //IMetadataManager _iMetadataManager = new VPC.Framework.Business.MetadataManager.Contracts.MetadataManager();
       // ITransitionFlowEngine transitionEngine = new TransitionFlowEngine();
       IManagerBatchItem _managerBatchItem=new ManagerBatchItem();

        void IBatchTypes.OnExecute(dynamic obj)
        {                
            VPC.Entities.BatchType.BatchType batchType = (VPC.Entities.BatchType.BatchType)obj[0];
            var tenantId=Guid.Parse(batchType.TenantId.Value);
            
            try
                {

                ISettingManager _iSettingManager = new SettingManager();
                IEntityResourceManager _iEntityResourceManager = new VPC.Framework.Business.EntityResourceManager.Contracts.EntityResourceManager ();
                IManagerBatchItem _managerBatchItem=new ManagerBatchItem();             
            
                var result = _iSettingManager.GetSettingsByContext(tenantId, SettingContextTypeEnum.EMAIL);
                if (result != null)
                {
                    EmailSenderOptions options = Newtonsoft.Json.JsonConvert.DeserializeObject<EmailSenderOptions>(result.Content);
                    //Get Batch Item
                    var allBatchItems=_managerBatchItem.GetBatchItems(tenantId,new Guid(batchType.InternalId.Value),(batchType.ItemRetryCount.Value.Length>0 ? (int?)Int32.Parse(batchType.ItemRetryCount.Value) : (int?)null ));
                    foreach(var allBatchItem in allBatchItems)
                    {                    
                            try
                            {                             
                            
                                var queryContext = new QueryContext { Fields = "Body,Sender,Recipient,Subject,Id" };
                                DataTable templatedt = _queryManager.GetResultById (tenantId, "email",allBatchItem.ReferenceId, queryContext);

                                _managerBatchItem.BatchItemUpdateStartTime(tenantId,allBatchItem.BatchItemId);
                                var email= EntityMapper<Email>.Mapper(templatedt);                           
                                SendEmail(tenantId,allBatchItem,email.Recipient.Value, email.Subject.Value, email.Body.Value, options);
                                
                                //Update batch History
                                _managerBatchItem.BatchHistoryCreate(tenantId,new BatchItemHistory{
                                    BatchHistoryId=Guid.NewGuid(),
                                    BatchItemId=allBatchItem.BatchItemId,
                                    EntityId=allBatchItem.EntityId,
                                    ReferenceId=allBatchItem.ReferenceId,
                                    Status=EmailEnum.Send,
                                    RunTime=allBatchItem.NextRunTime
                                });
                                //Update Email Status                                
                                _iEntityResourceManager.UpdateSpecificField(tenantId, "email", allBatchItem.ReferenceId, "Status", ((int)EmailEnum.Send).ToString());

                            }
                            catch (System.Exception ex)
                            {
                                _log.Error("An error has occurred while sending email", ex.Message);
                              
                                //Update batch History
                                 _managerBatchItem.BatchHistoryCreate(tenantId,new BatchItemHistory{
                                    BatchHistoryId=Guid.NewGuid(),
                                      BatchItemId=allBatchItem.BatchItemId,
                                    EntityId=allBatchItem.EntityId,
                                    ReferenceId=allBatchItem.ReferenceId,
                                    Status=EmailEnum.Fail,
                                    RunTime=allBatchItem.NextRunTime,
                                    FailedReason=ex.Message
                                });
                                //Update Batch item status
                                _managerBatchItem.BatchItemUpdate(tenantId,(batchType.ItemRetryCount.Value.Length>0 ? (int?)Int32.Parse(batchType.ItemRetryCount.Value) : (int?)null ),
                                new BatchItem{
                                    BatchItemId=allBatchItem.BatchItemId,
                                    Status=EmailEnum.Fail,                           
                                    FailedReason=ex.Message                                
                                    });
                                //Update Email Status                                
                                _iEntityResourceManager.UpdateSpecificField(tenantId, "email", allBatchItem.ReferenceId, "Status", ((int)EmailEnum.Fail).ToString());
                                throw ex;
                            }
                         
                      //Update Batch item status
                      _managerBatchItem.BatchItemUpdateStatus(tenantId,new BatchItem{Status=EmailEnum.Send,BatchItemId=allBatchItem.BatchItemId});
                    }
                }
                else
                {
                    _log.Error("Email gateway not configured");
                }
            }
            catch (System.Exception ex)
            {
                _log.Error("Email send failed", ex.Message);
            }
        }

        private void SendEmail(Guid tenantId,BatchItem batchItem,string toEmail, string subject, string message, EmailSenderOptions Options)
        {
            MailMessage mail = GetMailMessage(tenantId,batchItem,toEmail, subject, message, Options.emailSender, Options.emailUserName, Options.useHtml);
            SmtpClient client = GetSmtpClient(Options.emailServer, Options.emailPort, Options.requiresAuthentication,
                Options.emailEmail, Options.emailPassword, Options.useSsl);

            client.Send(mail);
        }

        private  MailMessage GetMailMessage(Guid tenantId,BatchItem batchItem,string toEmail, string subject, string message,string defaultSenderEmail, string defaultSenderDisplayName = null, bool useHtml = true)
        {
            MailAddress sender;

            if (string.IsNullOrEmpty(defaultSenderEmail))
            {
                throw new ArgumentException("No sender mail address was provided");
            }
            else
            {
                sender = !string.IsNullOrEmpty(defaultSenderDisplayName) ?
                    new MailAddress(defaultSenderEmail, defaultSenderDisplayName) : new MailAddress(defaultSenderEmail);
            }

            MailMessage mail = new MailMessage()
            {
                From = sender,
                Subject = subject,
                Body = message,
                IsBodyHtml = useHtml
            };

            //get batch content
            var batchContent=_managerBatchItem.GetBatchContents(tenantId,batchItem.BatchItemId);
            if(batchContent.Count>0)
            {
                byte[] attachment= Convert.FromBase64String(batchContent[0].Content);
                MemoryStream memoryStream = new MemoryStream(attachment);
                mail.Attachments.Add( new Attachment( memoryStream, "UserExport.xlsx" , MediaTypeNames.Application.Pdf ));
            }

             
     

            mail.To.Add(toEmail);
            return mail;
        }

        private static SmtpClient GetSmtpClient(string host, int port, bool requiresAuthentication = true,
            string userName = null, string userKey = null, bool useSsl = false)
        {
            SmtpClient client = new SmtpClient();

            if (string.IsNullOrEmpty(host))
            {
                throw new ArgumentException("No domain was provided");
            }

            client.Host = host;

            if (port > -1)
            {
                client.Port = port;
            }

            client.UseDefaultCredentials = !requiresAuthentication;

            if (requiresAuthentication)
            {
                if (string.IsNullOrEmpty(userName))
                {
                    throw new ArgumentException("No user name was provided");
                }

                client.Credentials = new NetworkCredential(userName, userKey);
            }

            client.EnableSsl = useSsl;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            return client;
        }
   
   
    }

    public class EmailSenderOptions
    {
        public string emailServer { get; set; }
        public int emailPort { get; set; }
        public string emailEmail { get; set; }
        public string emailPassword { get; set; }
        public bool useSsl { get; set; } = true;
        public bool requiresAuthentication { get; set; } = true;
        public string emailSender { get; set; }
        public string emailUserName { get; set; }
        public bool useHtml { get; set; } = true;
    }
}
