using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using VPC.Framework.Business.BatchType.Contracts;
using VPC.Entities.BatchType;
using VPC.Framework.Business.Common;
using VPC.Framework.Business.EntitySecurity.APIs;
using VPC.Framework.Business.BatchType;
using System.Reflection;
using System.Collections;

namespace IHostedServiceAsAService
{

    public class MyHostedService: IHostedService
    {

    IManagerBatchType _managerbatchType=new ManagerBatchType();
     ISecurityCacheManager _securityCacheManager=new SecurityCacheManager();

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Task.Run(() => InfinityProcess(cancellationToken));
        return Task.CompletedTask;
    }

    public void InfinityProcess(CancellationToken cancellationToken)
    {

        var task = new Task(() => BatchTypeProcess(), TaskCreationOptions.LongRunning);
          task.Start();        
    }

    protected void BatchTypeProcess()
    {
         var shouldRun = true;
            while (true)
            {
                if (shouldRun)
                {
                    try
                    {
                         shouldRun = false;
                         BatchInfinityProcess();
                         shouldRun = true;
                         Thread.Sleep(10000);
                    }
                    catch (ThreadAbortException)
                    {
                        shouldRun = true;
                        Thread.ResetAbort();
                    }
                    catch(System.Exception)
                    {
                        shouldRun = true;
                    }                    
                }
            }       
    }

   
List<BatchTypeInfo> allTasks=new List<BatchTypeInfo>();

    public void BatchInfinityProcess()
    {
        List<KeyValuePair<Guid,BatchTypeInfo>>  allBatches=_securityCacheManager.BatchTypesCache();  
        foreach (var allBatche in allBatches)
        {          
           //Check already added  
           var checkAddedd=(from allTask in allTasks where  DataUtility.Compare<BatchTypeInfo>(allTask, allBatche.Value) select allTask).ToList();
            if(checkAddedd.Count==0)
                {                   
                    var task = new Task(() => AssignTaskForProcessing(allBatche.Key,allBatche.Value), TaskCreationOptions.LongRunning);
                    task.Start();  
                     allTasks.Add(allBatche.Value);
                }
        }

        //Check to delete
        foreach(var allTask in allTasks)
        {
            var checkExistance=(from allBatche in allBatches where  DataUtility.Compare<BatchTypeInfo>(allTask, allBatche.Value) select allBatche).ToList();
            if(checkExistance.Count==0)
            {
              allTasks.Remove(allTask); 
            }
            
        }
    }

     void AssignTaskForProcessing(Guid tenantId,BatchTypeInfo type)
        {
            var shouldRun = true;
            while (true)
            {
                if (shouldRun)
                {
                    try
                    {
                        shouldRun = false;
                        //Check Is Batch settinmgs Changes                    
                        var allBatchTypes=_securityCacheManager.BatchTypesCache();                       
                        if(allBatchTypes.Count>0)
                        {
                           var itsBatchType=(from allBatchType in allBatchTypes where allBatchType.Key==tenantId && allBatchType.Value.BatchTypeId==type.BatchTypeId select allBatchType.Value ).FirstOrDefault();
                            if(itsBatchType==null)
                                {                            
                                    break;                   
                                }

                            var isExists= DataUtility.Compare<BatchTypeInfo>(type, itsBatchType);
                            if(!isExists)
                            {
                                break;                     
                            }

                        }
                        

                        //check scheduler
                        var batchtype = typeof(IBatchTypes);
                        var myType = DataUtility.GetBatchTypeByContext(type.Context);
                        if (myType !=null)
                        {                            
                                var myObject = Activator.CreateInstance(myType);
                                // Retrieve the method you are looking for
                                MethodInfo preMethodInfo = batchtype.GetMethod("OnExecute");
                                //PostProcess
                                // Invoke the method on the instance we created above
                                var arrayList = new ArrayList
                                {
                                    tenantId,
                                    type
                                };
                                var result = (BatchTypeReturnMessage)preMethodInfo.Invoke(myObject,  new object[] { arrayList });

                            shouldRun = true;
                            Thread.Sleep(type.IdleTime.HasValue ? type.IdleTime.Value : 10000);
                        }
                        else
                        {
                            shouldRun = true;
                            Thread.Sleep(type.IdleTime.HasValue ? type.IdleTime.Value :  10000);
                        } 
                    }
                    catch (ThreadAbortException)
                    {
                        shouldRun = true;
                        Thread.ResetAbort();
                    }
                    catch(System.Exception)
                    {
                        shouldRun = true;
                    }                    
                }
            }
        } 

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

}
