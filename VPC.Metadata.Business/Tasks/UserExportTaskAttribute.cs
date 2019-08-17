using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPC.Metadata.Business.Tasks
{
    public class UserExportTaskAttribute : TaskAttribute
    {
        
        public UserExportTaskAttribute(string name,string task,string verb="" ,string taskDisplay="User export") : base(name,task,verb,taskDisplay)
        {
        }
    }   
}
