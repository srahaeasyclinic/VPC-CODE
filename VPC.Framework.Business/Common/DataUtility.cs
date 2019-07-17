
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json.Linq;
using VPC.Entities.BatchType;
using VPC.Entities.Common;
using VPC.Entities.Common.Dashlets;
using VPC.Entities.Common.Features;
using VPC.Entities.Common.Functions;
using VPC.Entities.Common.Reports;
using VPC.Entities.Email;
using VPC.Entities.EntityCore.Metadata;
using VPC.Entities.EntityCore.Model.Setting;
using VPC.Entities.EntitySecurity;
using VPC.Entities.Role;
using VPC.Entities.TenantSubscription;
using VPC.Entities.WorkFlow;
using VPC.Framework.Business.BatchType;
using VPC.Framework.Business.EntityResourceManager.Contracts;
using VPC.Framework.Business.MetadataManager.Contracts;
using VPC.Framework.Business.SettingsManager.Contracts;

namespace VPC.Framework.Business.Common
{
    public  static class DataUtility
    {
        
        public static string GetXmlForIds(List<Guid> ids)
        {
            var sw = new StringWriter();
            using (XmlWriter writer = new XmlTextWriter(sw))
            {
                writer.WriteStartElement("Items");
                foreach (var id in ids)
                {
                    writer.WriteStartElement("Item");
                    writer.WriteAttributeString("value", id.ToString());
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }

            return sw.ToString();
        }
        public static string GetXmlForIds(List<int?> ids)
        {
            var sw = new StringWriter();
            using (XmlWriter writer = new XmlTextWriter(sw))
            {
                writer.WriteStartElement("Items");
                foreach (var i in ids)
                {
                    if (i != null)
                    {
                        var id = (int)i;
                        writer.WriteStartElement("Item");
                        writer.WriteAttributeString("value", id.ToString());
                    }
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }

            return sw.ToString();
        }

        public static string GetXmlForIds(List<string> ids)
        {
            var sw = new StringWriter();
            using (XmlWriter writer = new XmlTextWriter(sw))
            {
                writer.WriteStartElement("Items");
                foreach (var id in ids)
                {
                    writer.WriteStartElement("Item");
                    writer.WriteAttributeString("value", id);
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }

            return sw.ToString();
        }
         
        // public static string GetXmlForConfigurationRoleAssignment(List<ConfigurationRoleAssignment> roleAssigns)
        // {
        //     var sw = new StringWriter();
        //     using (XmlWriter writer = new XmlTextWriter(sw))
        //     {
        //         writer.WriteStartElement("RoleAssigns");

        //         foreach (var roleAssign in roleAssigns)
        //         {
        //             foreach (var role in roleAssign.RoleId)
        //             {
        //                 writer.WriteStartElement("RoleAssign");
        //                 writer.WriteAttributeString("RoleAssignmetId", Guid.NewGuid().ToString());
        //                 writer.WriteAttributeString("StepId", roleAssign.StepId.ToString());
        //                 writer.WriteAttributeString("RoleId", role.ToString());
        //                 writer.WriteAttributeString("WorkFlowId", roleAssign.WorkFlowId.ToString());
        //                 writer.WriteAttributeString("AssignmentOperation", roleAssign.AssignmentOperation.ToString());
        //                 writer.WriteAttributeString("OperatorType", roleAssign.OperatorType.ToString());
        //                 writer.WriteEndElement();
        //             }
        //         }

        //         writer.WriteEndElement();
        //     }
        //     return sw.ToString();
        // }

        // public static String GetXmlForWorkFlowPerformanceCheck(List<WorkFlowPerformanceCheckInfo> infos)
        // {
        //     var sw = new StringWriter();
        //     using (XmlWriter writer = new XmlTextWriter(sw))
        //     {
        //         writer.WriteStartElement("WorkFlowPerformanceChecks");
        //         foreach (WorkFlowPerformanceCheckInfo info in infos)
        //         {
        //             writer.WriteStartElement("WorkFlowPerformanceCheck");
        //             writer.WriteAttributeString("WorkFlowPerformanceCheckId", info.PerformanceCheckId.ToString());
        //             writer.WriteAttributeString("WorkFlowId", info.WorkFlowId.ToString());
        //             writer.WriteAttributeString("WorkFlowStepId", info.WorkFlowStepId.ToString());
        //             writer.WriteAttributeString("Context", info.OperatorType.ToString());
        //             if (info.AllotedTime.HasValue && info.AllotedTime > 0)
        //                 writer.WriteAttributeString("AllotedTime", info.AllotedTime.Value.ToString());
        //             if (info.CriticalTime.HasValue && info.CriticalTime > 0)
        //                 writer.WriteAttributeString("CriticalTime", info.CriticalTime.Value.ToString());
        //             writer.WriteEndElement();
        //         }
        //         writer.WriteEndElement();
        //     }
        //     return sw.ToString();
        // }

        // public static String GetXmlForWorkFlowSteps(List<WorkFlowSteps> infos)
        // {
        //     var sw = new StringWriter();
        //     using (XmlWriter writer = new XmlTextWriter(sw))
        //     {
        //         writer.WriteStartElement("WorkFlowSteps");
        //         foreach (WorkFlowSteps info in infos)
        //         {
        //             writer.WriteStartElement("WorkFlowStep");
        //             writer.WriteAttributeString("WorkFlowStepId", info.WorkFlowStepId.ToString());
        //             writer.WriteAttributeString("WorkFlowId", info.WorkFlowId.ToString());
        //             writer.WriteAttributeString("TransitionType", info.TransitionType.ToString());
        //             writer.WriteAttributeString("SequenceNumber", info.SequenceNumber.ToString());                   
        //             writer.WriteAttributeString("IsRoleAssigned", info.IsRoleAssigned.ToString());                   
        //             writer.WriteAttributeString("IsAssigmentMandatory", info.IsAssigmentMandatory.ToString());
        //             writer.WriteAttributeString("IsActivate", info.IsActivate.ToString());
        //             writer.WriteAttributeString("IsAccess", info.IsAccess.ToString());
        //             writer.WriteEndElement();
        //         }
        //         writer.WriteEndElement();
        //     }
        //     return sw.ToString();
        // }

      public static String GetXmlForRoles(List<RoleInfo> infos)
        {
            var sw = new StringWriter();
            using (XmlWriter writer = new XmlTextWriter(sw))
            {
                writer.WriteStartElement("Roles");
                foreach (RoleInfo info in infos)
                {
                    writer.WriteStartElement("Role");                    
                    writer.WriteAttributeString("RoleId", info.RoleId.ToString());
                    writer.WriteAttributeString("Name", info.Name.ToString());
                    writer.WriteAttributeString("RoleType", ((int)info.RoleType).ToString());  
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
            return sw.ToString();
        }

        public static String GetXmlForBatchTypes(List<BatchTypeInfo> infos)
        {
            var sw = new StringWriter();
            using (XmlWriter writer = new XmlTextWriter(sw))
            {
                writer.WriteStartElement("BatchTypes");
                foreach (BatchTypeInfo info in infos)
                {
                    writer.WriteStartElement("BatchType");                    
                    writer.WriteAttributeString("BatchTypeId", info.BatchTypeId.ToString());
                    writer.WriteAttributeString("Context", info.Context.ToString());
                    writer.WriteAttributeString("Priority", info.Priority.ToString());
                    writer.WriteAttributeString("IdleTime", info.IdleTime.ToString());  
                    writer.WriteAttributeString("bStatus", info.Status.ToString());               
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
            return sw.ToString();
        }

        public static String GetXmlForWorkFlows(List<WorkFlowInfo> infos)
        {
            var sw = new StringWriter();
            using (XmlWriter writer = new XmlTextWriter(sw))
            {
                writer.WriteStartElement("WorkFlows");
                foreach (WorkFlowInfo info in infos)
                {
                    writer.WriteStartElement("WorkFlow");                    
                    writer.WriteAttributeString("WorkFlowId", info.WorkFlowId.ToString());
                    writer.WriteAttributeString("EntityId", info.EntityId.ToString());
                    writer.WriteAttributeString("Status", info.Status.ToString());                   
                    writer.WriteAttributeString("SubTypeCode", info.SubTypeCode.ToString()); 
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
            return sw.ToString();
        }

        public static String GetXmlSubscriptionEntities(List<TenantSubscriptionEntityInfo> infos)
        {            
            var sw = new StringWriter();
            using (XmlWriter writer = new XmlTextWriter(sw))
            {
                writer.WriteStartElement("SubscriptionEntities");
                foreach (TenantSubscriptionEntityInfo info in infos)
                {
                    writer.WriteStartElement("SubscriptionEntity");
                    writer.WriteAttributeString("TenantSubscriptionEntityId", info.TenantSubscriptionEntityId.ToString());
                    writer.WriteAttributeString("TenantSubscriptionId", info.TenantSubscriptionId.ToString());  

                    writer.WriteAttributeString("EntityId", info.EntityId.ToString());
                    if(info.LimtNumber.HasValue)
                    {
                        writer.WriteAttributeString("LimtNumber", info.LimtNumber.Value.ToString());
                        writer.WriteAttributeString("LimitType", ((int)info.LimitType).ToString());
                    } 
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
            return sw.ToString();
        }

        public static String GetXmlSubscriptionEntityDetails(List<TenantSubscriptionEntityDetailInfo> infos)
        {            
            var sw = new StringWriter();
            using (XmlWriter writer = new XmlTextWriter(sw))
            {
                writer.WriteStartElement("SubscriptionEntityDetails");
                foreach (TenantSubscriptionEntityDetailInfo info in infos)
                {
                    writer.WriteStartElement("SubscriptionEntityDetail");
                    writer.WriteAttributeString("SubscriptionEntityDetailId", info.SubscriptionEntityDetailId.ToString());
                    writer.WriteAttributeString("SubscriptionEntityId", info.SubscriptionEntityId.ToString());  
                    writer.WriteAttributeString("Context", info.Context.ToString());
                    if(info.RecurringPrice.HasValue)
                    {
                        writer.WriteAttributeString("RecurringPrice", info.RecurringPrice.Value.ToString());
                        writer.WriteAttributeString("RecurringDuration", ((int)info.RecurringDuration).ToString());
                    } 
                    if(info.OneTimePrice.HasValue)
                    {
                        writer.WriteAttributeString("OneTimePrice", info.OneTimePrice.Value.ToString());
                        writer.WriteAttributeString("OneTimeDuration", ((int)info.OneTimeDuration).ToString());
                    }
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
            return sw.ToString();
        }

        public static String GetXmlForWorkFlowStepsSequence(List<WorkFlowStepInfo> infos)
        {
            var count=1;
            var sw = new StringWriter();
            using (XmlWriter writer = new XmlTextWriter(sw))
            {
                writer.WriteStartElement("WorkFlowSteps");
                foreach (WorkFlowStepInfo info in infos)
                {
                    writer.WriteStartElement("WorkFlowStep");
                    writer.WriteAttributeString("WorkFlowStepId", info.WorkFlowStepId.ToString());
                    writer.WriteAttributeString("WorkFlowId", info.WorkFlowId.ToString());                   
                    writer.WriteAttributeString("SequenceNumber", (count++).ToString()); 
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
            return sw.ToString();
        }

        public static String GetXmlForWorkFlowStepsCreate(List<WorkFlowStepInfo> infos)
        {        

            var sw = new StringWriter();
            using (XmlWriter writer = new XmlTextWriter(sw))
            {
                writer.WriteStartElement("WorkFlowSteps");
                foreach (WorkFlowStepInfo info in infos)
                {
                    writer.WriteStartElement("WorkFlowStep");
                    writer.WriteAttributeString("WorkFlowStepId", info.WorkFlowStepId.ToString());
                    writer.WriteAttributeString("WorkFlowId", info.WorkFlowId.ToString());                   
                    writer.WriteAttributeString("TransitionTypeId", info.TransitionType.Id.ToString()); 
                    writer.WriteAttributeString("SequenceNumber", info.SequenceNumber.ToString()); 
                    writer.WriteAttributeString("IsAssigmentMandatory", info.IsAssigmentMandatory.ToString()); 
                    if(info.AllotedTime.HasValue)
                    writer.WriteAttributeString("AllotedTime", info.AllotedTime.ToString()); 
                    if(info.CriticalTime.HasValue)
                     writer.WriteAttributeString("CriticalTime", info.CriticalTime.ToString()); 
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
            return sw.ToString();
        }

        public static String GetXmlForWorkFlowInnerStepsCreate(List<WorkFlowInnerStepInfo> infos)
        {       
             var sw = new StringWriter();
            using (XmlWriter writer = new XmlTextWriter(sw))
            {
                writer.WriteStartElement("WorkFlowInnerSteps");
                foreach (WorkFlowInnerStepInfo info in infos)
                {
                    writer.WriteStartElement("WorkFlowInnerStep");
                    writer.WriteAttributeString("InnerStepId", info.InnerStepId.ToString()); 
                    writer.WriteAttributeString("WorkFlowStepId", info.WorkFlowStepId.ToString());
                    writer.WriteAttributeString("WorkFlowId", info.WorkFlowId.ToString()); 
                    writer.WriteAttributeString("TransitionTypeId", info.TransitionType.Id.ToString()); 
                    writer.WriteAttributeString("SequenceNumber", info.SequenceNumber.ToString());                   
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
            return sw.ToString();
        }
        public static String GetXmlForWorkFlowInnerStepsSequence(List<WorkFlowInnerStepInfo> infos)
        {
        var count=1;
            var sw = new StringWriter();
            using (XmlWriter writer = new XmlTextWriter(sw))
            {
                writer.WriteStartElement("WorkFlowInnerSteps");
                foreach (WorkFlowInnerStepInfo info in infos)
                {
                    writer.WriteStartElement("WorkFlowInnerStep");
                    writer.WriteAttributeString("InnerStepId", info.InnerStepId.ToString()); 
                    writer.WriteAttributeString("SequenceNumber", (count++).ToString());                   
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
            return sw.ToString();
        }

        public static String GetXmlForWorkFlowRoles(List<WorkFlowRoleInfo> infos)
        {        
            var sw = new StringWriter();
            using (XmlWriter writer = new XmlTextWriter(sw))
            {
                writer.WriteStartElement("WorkFlowRoles");
                foreach (WorkFlowRoleInfo info in infos)
                {
                    writer.WriteStartElement("WorkFlowRole");
                    writer.WriteAttributeString("RoleAssignmetId", info.RoleAssignmetId.ToString()); 
                    writer.WriteAttributeString("WorkFlowStepId", info.WorkFlowStepId.ToString()); 
                    writer.WriteAttributeString("RoleId", info.RoleId.ToString());  
                    writer.WriteAttributeString("WorkFlowId", info.WorkFlowId.ToString()); 
                    writer.WriteAttributeString("AssignmentOperationType", info.AssignmentOperationType.ToString());                  
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
            return sw.ToString();
        }

      public static String GetXmlForWorkFlowProcessTask(List<WorkFlowProcessTaskInfo> infos)
        {
            var sw = new StringWriter();
            using (XmlWriter writer = new XmlTextWriter(sw))
            {
                writer.WriteStartElement("WorkFlowProcessTasks");
                foreach (WorkFlowProcessTaskInfo info in infos)
                {
                    writer.WriteStartElement("WorkFlowProcessTask");
                    writer.WriteAttributeString("WorkFlowProcessTaskId", info.WorkFlowProcessTaskId.ToString()); 
                    writer.WriteAttributeString("WorkFlowId", info.WorkFlowId.ToString()); 
                    writer.WriteAttributeString("WorkFlowProcessId", info.WorkFlowProcessId.ToString());  
                    writer.WriteAttributeString("ProcessCode", info.ProcessCode.ToString()); 
                    writer.WriteAttributeString("SequenceCode", info.SequenceCode.ToString());                  
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
            return sw.ToString();
        }

       public static String GetXmlForWorkFlowOperations(List<WorkFlowOperationInfo> infos)
        {
            var sw = new StringWriter();
            using (XmlWriter writer = new XmlTextWriter(sw))
            {
                writer.WriteStartElement("WorkFlowOperations");
                foreach (WorkFlowOperationInfo info in infos)
                {
                    writer.WriteStartElement("WorkFlowOperation");
                    writer.WriteAttributeString("WorkFlowOperationId", info.WorkFlowOperationId.ToString()); 
                    writer.WriteAttributeString("OperationType",((int)info.OperationType).ToString());  
                     writer.WriteAttributeString("WorkFlowId", info.WorkFlowId.ToString()); 
                      writer.WriteAttributeString("IsSync", info.IsSync.ToString());                  
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
            return sw.ToString();
        }

      

        public static String GetXmlForWorkFlowProcess(List<WorkFlowProcessInfo> infos)
        {
            var sw = new StringWriter();
            using (XmlWriter writer = new XmlTextWriter(sw))
            {
                writer.WriteStartElement("WorkFlowProcess");
                foreach (WorkFlowProcessInfo info in infos)
                {
                    writer.WriteStartElement("WorkFlowProces");
                    writer.WriteAttributeString("WorkFlowProcessId", info.WorkFlowProcessId.ToString());
                    writer.WriteAttributeString("WorkFlowId", info.WorkFlowId.ToString());
                    writer.WriteAttributeString("OperationOrTransactionId", info.OperationOrTransactionId.ToString());  
                    writer.WriteAttributeString("OperationOrTransactionType", info.OperationOrTransactionType.ToString());
                    writer.WriteAttributeString("ProcessType", info.ProcessType.ToString());                                    
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
            return sw.ToString();
        }

        public static DateTime GetUnixDateTime()
        {
            var unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            // Unix timestamp is seconds past epoch
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimestamp);
            return dtDateTime;
        }

        public static DateTime GetUnixDateTime(DateTime datetime)
        {
            var date = new DateTime(1970, 1, 1, 0, 0, 0, datetime.Kind);
            var unixTimestamp = Convert.ToInt64((datetime - date).TotalSeconds);
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimestamp).ToLocalTime();
            return dtDateTime;
        }

        // public List<ItemName> GetFunctions(Type functionItem)
        // {
        //     var functions = new List<SubscriptionFunction>();
        //     var dataFunctionInRole = new DataFunctionsInRole();
        //     List<FunctionsInRole> roleFunctions = dataFunctionInRole.GetFunctionsByRole(tenantId, roleId);
        //     List<Guid> relatefunctions =
        //         (from roleFunction in roleFunctions select roleFunction.FunctionId).ToList();
        //     functions.AddRange(General.Utility.GetStaticGuidPropertiesFromTypeDescription(functionItem).Select(module => relatefunctions.Contains(module.Value) ? new SubscriptionFunction(module.Value, module.Key) {IsRelate = true} : new SubscriptionFunction(module.Value, module.Key) {IsRelate = false}));
        //     return functions;
        // }

        public static List<EntitySecurityInfo> GetFunctionEntityWise(string entityId)
        {
            var dic = new List<EntitySecurityInfo>();           
            var fields = new FunctionContext().GetType().GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (FieldInfo fi in fields)
            {
                var descriptionObj = fi.GetCustomAttributes(false).OfType<EntityGroup>().SingleOrDefault();
                if (descriptionObj!=null && !string.IsNullOrEmpty(descriptionObj.GetGroupId() )  && (descriptionObj.GetGroupId()==entityId) )
                {                    
                    dic.Add(new EntitySecurityInfo{Name=descriptionObj != null ? descriptionObj.GetGroupName() : fi.Name,
                                                   FunctionContext=Guid.Parse(fi.GetValue(new Guid()).ToString()) });
                }
               
            }
            return dic;
        }


        public static List<TenantSubscriptionEntityDetailInfo> GetFeatureEntityWise(string entityId)
        {
            var dic = new List<TenantSubscriptionEntityDetailInfo>();           
            var fields = new FeatureContext().GetType().GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (FieldInfo fi in fields)
            {
                var descriptionObj = fi.GetCustomAttributes(false).OfType<EntityGroup>().SingleOrDefault();
                if (descriptionObj!=null && !string.IsNullOrEmpty(descriptionObj.GetGroupId() )  && (descriptionObj.GetGroupId()==entityId) )
                {                    
                    dic.Add(new TenantSubscriptionEntityDetailInfo{Name=descriptionObj != null ? descriptionObj.GetGroupName() : fi.Name,
                                                   Context=Guid.Parse(fi.GetValue(new Guid()).ToString()) });
                }
               
            }
            return dic;
        }

        public static List<TenantSubscriptionEntityDetailInfo> GetReportEntityWise(string entityId)
        {
            var dic = new List<TenantSubscriptionEntityDetailInfo>();           
            var fields = new ReportContext().GetType().GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (FieldInfo fi in fields)
            {
                var descriptionObj = fi.GetCustomAttributes(false).OfType<EntityGroup>().SingleOrDefault();
                if (descriptionObj!=null && !string.IsNullOrEmpty(descriptionObj.GetGroupId() )  && (descriptionObj.GetGroupId()==entityId) )
                {                    
                    dic.Add(new TenantSubscriptionEntityDetailInfo{Name=descriptionObj != null ? descriptionObj.GetGroupName() : fi.Name,
                                                   Context=Guid.Parse(fi.GetValue(new Guid()).ToString()) });
                }
               
            }
            return dic;
        }

        public static List<TenantSubscriptionEntityDetailInfo> GetDashletEntityWise(string entityId)
        {
            var dic = new List<TenantSubscriptionEntityDetailInfo>();           
            var fields = new DashletContext().GetType().GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (FieldInfo fi in fields)
            {
                var descriptionObj = fi.GetCustomAttributes(false).OfType<EntityGroup>().SingleOrDefault();
                if (descriptionObj!=null && !string.IsNullOrEmpty(descriptionObj.GetGroupId() )  && (descriptionObj.GetGroupId()==entityId) )
                {                    
                    dic.Add(new TenantSubscriptionEntityDetailInfo{Name=descriptionObj != null ? descriptionObj.GetGroupName() : fi.Name,
                                                   Context=Guid.Parse(fi.GetValue(new Guid()).ToString()) });
                }
               
            }
            return dic;
        }

        public static  void CopyPropertiesTo(this object fromObject, object toObject)
            {
                PropertyInfo[] toObjectProperties = toObject.GetType().GetProperties();
                foreach (PropertyInfo propTo in toObjectProperties)
                {
                    PropertyInfo propFrom = fromObject.GetType().GetProperty(propTo.Name);
                    if (propFrom!=null && propFrom.CanWrite)
                        propTo.SetValue(toObject, propFrom.GetValue(fromObject, null), null);
                }
            }      
        
        public static string GetEnumDescription(Enum value)
        {
            var type = value.GetType();
            var fieldIInfo = type.GetField(Enum.GetName(type, value));
            if (fieldIInfo != null)
            {
                var attribute =
                    (System.ComponentModel.DescriptionAttribute)
                        Attribute.GetCustomAttribute(fieldIInfo, typeof(System.ComponentModel.DescriptionAttribute));
                return attribute != null ? attribute.Description : value.ToString();
            }
            return string.Empty;
        }

        public static Dictionary<int, string> GetIntFieldNameFromEnum(Type type)
        {
            var names = Enum.GetNames(type);
            var i = 0;
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            foreach (object o in Enum.GetValues(type))
                dictionary.Add((int)o, names[i++]);
            return dictionary;
        }
   
          public static List<BatchTypeInfo> GetAllBatchTypes()
        {
            var batchTypes=new List<BatchTypeInfo>();
            IEnumerable<Type> types = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IBatchTypes)) && t.GetConstructor(Type.EmptyTypes) != null);
            foreach (Type tType in types)
            {
                object[] ss = tType.GetCustomAttributes(true);
                batchTypes.AddRange(from BatchTypeAttribute o in ss select 
                new BatchTypeInfo { Name = o.BatchName,Context=o.Context,RunningType=new ItemNameInt{Id= o.BatchType , Name=GetEnumDescription((BatchTypes)o.BatchType) }});
            }
            return batchTypes;
        }

         public static Type GetBatchTypeByContext(string typeContext)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IBatchTypes)) && t.GetConstructor(Type.EmptyTypes) != null);
            Type myType = null;

            foreach (var t in from t in types let attr = (BatchTypeAttribute)t.GetCustomAttributes(true).FirstOrDefault() where attr != null where attr.Context == typeContext select t)
            {
                myType = t;
            }
            return myType;
        }

         public static List<JObject> ConvertToJObjectList(DataTable dataTable)
            {
            var list = new List<JObject>();

                foreach (DataRow row in dataTable.Rows)
                {
                    var item = new JObject();

                    foreach (DataColumn column in dataTable.Columns)
                    {
                        item.Add(column.ColumnName, JToken.FromObject(row[column.ColumnName]));
                    }

                    list.Add(item);
                }

            return list;
            }

        public static Guid SaveEmail(Guid tenantId,Guid userId,EmailTemplate template,string emailId)
        {
            if ( template.Body !=null &&  !string.IsNullOrEmpty(template.Body.Value))
            {    ISettingManager _iSettingManager = new SettingManager();
                 IEntityResourceManager _iEntityResourceManager = new VPC.Framework.Business.EntityResourceManager.Contracts.EntityResourceManager ();
                 IMetadataManager _iMetadataManager = new VPC.Framework.Business.MetadataManager.Contracts.MetadataManager ();
                 var emailSubType = _iMetadataManager.GetSubTypes ("email");
                 var sendername = _iSettingManager.GetSenderNameByContext(tenantId, SettingContextTypeEnum.EMAIL);
                            dynamic jsonObject = new JObject ();
                            jsonObject.Body = template.Body.Value.Replace("'", "''");            
                            jsonObject.Sender = sendername;
                            jsonObject.Recipient = emailId;                            
                            jsonObject.Date = HelperUtility.GetCurrentUTCDate();
                            jsonObject.Subject = template.Title.Value;
                            var returnId = _iEntityResourceManager.SaveResult (tenantId,userId, "email", jsonObject, emailSubType[0].Name.ToString ());
            return returnId;
            }

            return Guid.Empty;
           
        }

         public static string GenerateName(int len)
        { 
            Random r = new Random();
            string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
            string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
            string Name = "";
            Name += consonants[r.Next(consonants.Length)].ToUpper();
            Name += vowels[r.Next(vowels.Length)];
            int b = 2; //b tells how many times a new letter has been added. It's 2 right now because the first two letters are already in the name.
            while (b < len)
            {
                Name += consonants[r.Next(consonants.Length)];
                b++;
                Name += vowels[r.Next(vowels.Length)];
                b++;
            }

            return Name;


        }

     public static bool Compare<T>(T Object1, T object2)
     {
          //Get the type of the object
          Type type = typeof(T);

          //return false if any of the object is false
          if (Object1 == null || object2 == null)
             return false;

         //Loop through each properties inside class and get values for the property from both the objects and compare
         foreach (System.Reflection.PropertyInfo property in type.GetProperties())
         {
              if (property.Name != "ExtensionData")
              {
                  string Object1Value = string.Empty;
                  string Object2Value = string.Empty;
                  if (type.GetProperty(property.Name).GetValue(Object1, null) != null)
                        Object1Value = type.GetProperty(property.Name).GetValue(Object1, null).ToString();
                  if (type.GetProperty(property.Name).GetValue(object2, null) != null)
                        Object2Value = type.GetProperty(property.Name).GetValue(object2, null).ToString();
                  if (Object1Value.Trim() != Object2Value.Trim())
                  {
                      return false;
                  }
              }
         }
         return true;
     }
        public static String GetXmlForResourceCreate(Guid tenantId, List<VPC.Entities.EntityCore.Model.Resource.Resource> resources)
        {
            var sw = new StringWriter();
            using (XmlWriter writer = new XmlTextWriter(sw))
            {
                writer.WriteStartElement("Resources");
                foreach (VPC.Entities.EntityCore.Model.Resource.Resource resource in resources)
                {
                    writer.WriteStartElement("Resource");
                    writer.WriteAttributeString("TenantId", tenantId.ToString());
                    writer.WriteAttributeString("Key", resource.Key);
                    writer.WriteAttributeString("Value", resource.Value);
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
            return sw.ToString();
        }

        public static List<string> GetStaticClassValue(Type type)
        {
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static);

            var values = new List<string>();
            foreach (var field in fields)
                values.Add(field.Name);
            return values;
        }

        public static string GetEnumName(Enum value)
        {
            var type = value.GetType();
            var fieldIInfo = type.GetField(Enum.GetName(type, value));
            if (fieldIInfo != null)
            {
              return fieldIInfo.Name;                
            }
            return string.Empty ;
        }

    }
}