using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPC.Metadata.Business.Tasks
{
    public class SendSMSTaskAttribute : TaskAttribute
    {
        public SendSMSTaskAttribute(string name,string task,string verb="" ,string taskDisplay="Send sms") : base(name,task,verb,taskDisplay)
        {
        }
    }
}
