using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using VPC.Entities.BatchType;
using VPC.Entities.Common;
using VPC.Entities.Email;
using VPC.Entities.EntityCore.Model.Query;
using VPC.Entities.EntityCore.Model.Setting;
using VPC.Entities.WorkFlow;
using VPC.Entities.WorkFlow.Engine;
using VPC.Framework.Business.DynamicQueryManager.Contracts;
using VPC.Framework.Business.EntityResourceManager.Contracts;
using VPC.Framework.Business.MetadataManager.Contracts;
using VPC.Framework.Business.SettingsManager.Contracts;
using VPC.Framework.Business.WorkFlow.Attribute;
using VPC.Framework.Business.WorkFlow.Contracts;

namespace VPC.Framework.Business.BatchType
{
    [BatchType("Email", BatchTypeContext.Email, (int)BatchTypes.Recurrence)]
    public partial class EmailBatch : IBatchTypes
    {
        private readonly Logger _log = LogManager.GetCurrentClassLogger();
        private readonly ISettingManager _iSettingManager = new SettingManager();
        IEntityResourceManager _iEntityResourceManager = new VPC.Framework.Business.EntityResourceManager.Contracts.EntityResourceManager();
        IManagerWorkFlow _workFlow = new ManagerWorkFlow();
        IEntityQueryManager queryManager = new EntityQueryManager();
        IOperationFlowEngine operationEngine = new OperationFlowEngine();
        IMetadataManager _iMetadataManager = new VPC.Framework.Business.MetadataManager.Contracts.MetadataManager();
        ITransitionFlowEngine transitionEngine = new TransitionFlowEngine();

        BatchTypeReturnMessage IBatchTypes.OnExecute(dynamic obj)
        {
            try
            {
                var tenantId = (Guid)obj[0];
                BatchTypeInfo batchType = (BatchTypeInfo)obj[1];
                var result = _iSettingManager.GetSettingsByContext(tenantId, SettingContextTypeEnum.EMAIL);

                if (result != null)
                {
                    EmailSenderOptions options = Newtonsoft.Json.JsonConvert.DeserializeObject<EmailSenderOptions>(result.Content);

                    //get mail in draft mode
                    var queryFilter1 = new List<QueryFilter>();
                    queryFilter1.Add(new QueryFilter { FieldName = "CurrentWorkFlowStep", Operator = "Equal", Value = WorkFlowEngine.Draft.ToString() });
                    queryFilter1.Add(new QueryFilter { FieldName = "TenantId", Operator = "Equal", Value = tenantId.ToString() });
                    // queryFilter1.Add(new QueryFilter{FieldName="CurrentWorkFlowStep",Operator="Equal" ,Value= WorkFlowEngine.Fail.ToString()});
                    var queryContext1 = new QueryContext { Fields = "Date,Recipient,Sender,Subject,Body,CurrentWorkFlowStep", Filters = queryFilter1, PageSize = 100, PageIndex = 1 };
                    var emails = _iEntityResourceManager.GetResult(tenantId, "email", queryContext1);
                    var emailMapped = EntityMapper<Email>.Mappers(emails);

                    if (emailMapped.Count > 0)
                    {

                        foreach (var emailMap in emailMapped)
                        {
                            var currentWorkFlowId=Guid.Empty;
                            if(emailMap.CurrentWorkFlowStep.Value=="Draft")
                              currentWorkFlowId=WorkFlowEngine.Draft;
                            else if(emailMap.CurrentWorkFlowStep.Value=="Send")
                              currentWorkFlowId=WorkFlowEngine.Sent;
                            else if(emailMap.CurrentWorkFlowStep.Value=="Failure")
                              currentWorkFlowId=WorkFlowEngine.Fail;
                            else if(emailMap.CurrentWorkFlowStep.Value=="Cancel")
                              currentWorkFlowId=WorkFlowEngine.Cancel;

                            var nextSteps = _workFlow.GetNextPossibleSteps(tenantId, "email", "Standard", currentWorkFlowId);
                            var nextTransition = (from nextStep in nextSteps where nextStep.TransitionType.Id == WorkFlowEngine.Sent select nextStep).FirstOrDefault();

                            try
                            {
                                SendEmail(emailMap.Recipient.Value, emailMap.Subject.Value, emailMap.Body.Value, options);
                            }
                            catch (System.Exception ex)
                            {
                                _log.Error("An error has occurred while sending email", ex.Message);
                                nextTransition = (from nextStep in nextSteps where nextStep.TransitionType.Id == WorkFlowEngine.Fail select nextStep).FirstOrDefault();
                            }

                            var transitionWapper = new TransitionWapper
                            {
                                EntityName = "email",
                                SubTypeName = "Standard",
                                StepId = nextTransition.InnerStepId,
                                RefId = Guid.Parse(emailMap.InternalId.Value),
                                CurrentTransitionType = currentWorkFlowId,
                                NextTransitionType = nextTransition.TransitionType.Id
                            };

                            _workFlow.ManageTransitionFirstStep(tenantId, transitionWapper);
                        }
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

            return new BatchTypeReturnMessage { NoDataFound = true };
        }

        private void SendEmail(string toEmail, string subject, string message, EmailSenderOptions Options)
        {
            MailMessage mail = GetMailMessage(toEmail, subject, message,
                Options.emailSender, Options.emailUserName, Options.useHtml);
            SmtpClient client = GetSmtpClient(Options.emailServer, Options.emailPort, Options.requiresAuthentication,
                Options.emailEmail, Options.emailPassword, Options.useSsl);

            client.Send(mail);
        }

        private static MailMessage GetMailMessage(string toEmail, string subject, string message,
            string defaultSenderEmail, string defaultSenderDisplayName = null, bool useHtml = true)
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