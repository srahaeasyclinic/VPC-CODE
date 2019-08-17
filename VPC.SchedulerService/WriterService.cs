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

   
List<BatchType> allTasks=new List<BatchType>();

    public void BatchInfinityProcess()
    {
        var  allBatches=_managerbatchType.GetEnabledBatchType();  
        foreach (var batch in allBatches)
        {          
           //Check already added  
           var checkAddedd=(from allTask in allTasks where  CompareObject.AreObjectsEqual(allTask, batch) select allTask).ToList();
            if(checkAddedd.Count==0)
                {                   
                    var task = new Task(() => AssignTaskForProcessing(batch), TaskCreationOptions.LongRunning);
                    task.Start();  
                     allTasks.Add(batch);
                }
        }

        //Check to delete
        foreach(var task in allTasks)
        {
            var checkExistance=(from batche in allBatches where  CompareObject.AreObjectsEqual(task, batche) select batche).ToList();
            if(checkExistance.Count==0)
            {
              allTasks.Remove(task); 
            }
            
        }
    }

     void AssignTaskForProcessing(BatchType type)
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
                        var allBatchTypes=_managerbatchType.GetEnabledBatchType();                       
                        if(allBatchTypes.Count>0)
                        {
                           var itsBatchType=(from batchType in allBatchTypes where Guid.Parse(batchType.TenantId.Value)==Guid.Parse(type.TenantId.Value) 
                                                &&  Guid.Parse(batchType.InternalId.Value)==Guid.Parse(type.InternalId.Value) select batchType).FirstOrDefault();
                            if(itsBatchType==null)
                                {                            
                                    break;                   
                                }

                            var isExists= CompareObject.AreObjectsEqual(type, itsBatchType);
                            if(!isExists)
                            {
                                break;                     
                            }

                        }                        

                        //check scheduler
                        var batchtype = typeof(IBatchTypes);
                        var myType = DataUtility.GetBatchTypeByContext((BatchTypeContextEnum)(Convert.ToInt16(type.Context.Value)));
                        if (myType !=null)
                        {                            
                                var myObject = Activator.CreateInstance(myType);
                                // Retrieve the method you are looking for
                                MethodInfo preMethodInfo = batchtype.GetMethod("OnExecute");
                                //PostProcess
                                // Invoke the method on the instance we created above
                                var arrayList = new ArrayList
                                {                               
                                    type
                                };
                                var result = (BatchTypeReturnMessage)preMethodInfo.Invoke(myObject,  new object[] { arrayList });

                            shouldRun = true;
                             Thread.Sleep(!string.IsNullOrEmpty(type.IdleTime.Value) ? Convert.ToInt32(type.IdleTime.Value) :  10000);
                        }
                        else
                        {
                            shouldRun = true;
                            Thread.Sleep(!string.IsNullOrEmpty(type.IdleTime.Value) ? Convert.ToInt32(type.IdleTime.Value) :  10000);
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
