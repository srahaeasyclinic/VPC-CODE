using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using NLog;
using VPC.Entities.BatchType;
using VPC.Entities.Role;
using VPC.Framework.Business.BatchType.APIs;
using VPC.Framework.Business.BatchType.Data;
using VPC.Framework.Business.Common;
using VPC.Framework.Business.EntitySecurity.APIs;
using VPC.Framework.Business.Role.Data;

namespace VPC.Framework.Business.BatchType.Contracts
{
 public interface IManagerBatchType
    {    
        bool Create(Guid tenantId, BatchTypeInfo info);
        bool Update(Guid tenantId, BatchTypeInfo info);
        bool CreateBatchTypes(Guid tenantId, List<BatchTypeInfo> infos);
        bool Delete(Guid tenantId, Guid batchTypeId);
        bool UpdateStatus(Guid tenantId, Guid batchTypeId);  
        List<BatchTypeInfo> GetBatchTypes(Guid tenantId);
        BatchTypeInfo GetBatchType(Guid tenantId,Guid batchTypeId);

        List<KeyValuePair<Guid,BatchTypeInfo>>  GetEnabledBatchTypes();
       // void AssignTaskForProcessing(Guid tenantId,BatchTypeInfo type);
    }
    
    public  class ManagerBatchType : IManagerBatchType
    {
        private readonly Logger _log = LogManager.GetCurrentClassLogger();
        private readonly IAdminBatchType _adminBatchType= new AdminBatchType();
         private readonly IReviewBatchType _reviewBatchType= new ReviewBatchType();        

            public ManagerBatchType()
            {
            }

            bool IManagerBatchType.Create(Guid tenantId, BatchTypeInfo info)
        {
            info.BatchTypeId=Guid.NewGuid();
            return  _adminBatchType.Create(tenantId,info);
        }
        
        bool IManagerBatchType.Update(Guid tenantId, BatchTypeInfo info)
        {
            return  _adminBatchType.Update(tenantId,info);
        }

        bool IManagerBatchType.CreateBatchTypes(Guid tenantId, List<BatchTypeInfo> infos)
        {
            return _adminBatchType.CreateBatchTypes(tenantId,infos);
        }

        bool IManagerBatchType.Delete(Guid tenantId, Guid batchTypeId)
        {
            return  _adminBatchType.Delete(tenantId,batchTypeId);
        }
        bool IManagerBatchType.UpdateStatus(Guid tenantId, Guid batchTypeId)
        {
            return  _adminBatchType.UpdateStatus(tenantId,batchTypeId);
        }

        List<BatchTypeInfo> IManagerBatchType.GetBatchTypes(Guid tenantId)
        {
           return  _reviewBatchType.GetBatchTypes(tenantId);
        }


        BatchTypeInfo IManagerBatchType.GetBatchType(Guid tenantId, Guid batchTypeId)
        {
           return  _reviewBatchType.GetBatchType(tenantId,batchTypeId);
        }

        List<KeyValuePair<Guid,BatchTypeInfo>>  IManagerBatchType.GetEnabledBatchTypes()
        {
             return  _reviewBatchType.GetEnabledBatchTypes();
        }
        // void IManagerBatchType.AssignTaskForProcessing(Guid tenantId,BatchTypeInfo type)
        // {
        //     var shouldRun = true;
        //     while (true)
        //     {
        //         if (shouldRun)
        //         {
        //             try
        //             {
        //                 shouldRun = false;
        //                 //Check Is Batch settinmgs Changes                    
        //                 var allBatchTypes=_securityCacheManager.BatchTypesCache();                       
        //                 if(allBatchTypes.Count>0)
        //                 {
        //                    var itsBatchType=(from allBatchType in allBatchTypes where allBatchType.Key==tenantId && allBatchType.Value.BatchTypeId==type.BatchTypeId select allBatchType.Value ).FirstOrDefault();
        //                     if(itsBatchType==null)
        //                         {                            
        //                             break;                   
        //                         }

        //                     var isExists= DataUtility.Compare<BatchTypeInfo>(type, itsBatchType);
        //                     if(!isExists)
        //                     {
        //                         break;                     
        //                     }

        //                 }
                        

        //                 //check scheduler
        //                 var batchtype = typeof(IBatchTypes);
        //                 var myType = DataUtility.GetBatchTypeByContext(type.Context);
        //                 if (myType !=null)
        //                 {                            
        //                         var myObject = Activator.CreateInstance(myType);
        //                         // Retrieve the method you are looking for
        //                         MethodInfo preMethodInfo = batchtype.GetMethod("OnExecute");
        //                         //PostProcess
        //                         // Invoke the method on the instance we created above
        //                         var arrayList = new ArrayList
        //                         {
        //                             tenantId,
        //                             type
        //                         };
        //                         var result = (BatchTypeReturnMessage)preMethodInfo.Invoke(myObject,  new object[] { arrayList });

        //                     shouldRun = true;
        //                     Thread.Sleep(type.IdleTime.HasValue ? type.IdleTime.Value : 10000);
        //                 }
        //                 else
        //                 {
        //                     shouldRun = true;
        //                     Thread.Sleep(type.IdleTime.HasValue ? type.IdleTime.Value :  10000);
        //                 } 
        //             }
        //             catch (ThreadAbortException)
        //             {
        //                 shouldRun = true;
        //                 Thread.ResetAbort();
        //             }
        //             catch(System.Exception)
        //             {
        //                 shouldRun = true;
        //             }                    
        //         }
        //     }
        // }       
   
   
    }
}