using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPC.Metadata.Business.Tasks
{
    public class SendEmailTaskAttribute : TaskAttribute
    {
        public Guid EmailTo { get; set; }
        public Guid EmailTemplate { get; set; }
        public SendEmailTaskAttribute(string name,string task,string verb="" ,string taskDisplay="" ) : base(name,task,verb,taskDisplay)
        {
        }
    }   
}
