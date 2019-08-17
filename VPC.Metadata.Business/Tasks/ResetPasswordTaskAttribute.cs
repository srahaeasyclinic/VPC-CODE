using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPC.Metadata.Business.Tasks
{
    public class ResetPasswordTaskAttribute : TaskAttribute
    {
        
        public ResetPasswordTaskAttribute(string name,string task,string verb="" ,string taskDisplay="Reset password") : base(name,task,verb,taskDisplay)
        {
        }
    }   
}
