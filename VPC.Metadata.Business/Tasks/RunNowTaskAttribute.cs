using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPC.Metadata.Business.Tasks
{
    public class RunNowTaskAttribute : TaskAttribute
    {        
        public RunNowTaskAttribute(string name,string task,string verb="" ,string taskDisplay="Run now") : base(name,task,verb,taskDisplay)
        {
        }
    }   
}
