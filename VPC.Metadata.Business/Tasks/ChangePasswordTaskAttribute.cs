using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPC.Metadata.Business.Tasks
{
    public class ChangePasswordTaskAttribute : TaskAttribute
    {
        
        public ChangePasswordTaskAttribute(string name,string task,string verb="" ,string taskDisplay="Change password") : base(name,task,verb,taskDisplay)
        {
        }
    }   
}
