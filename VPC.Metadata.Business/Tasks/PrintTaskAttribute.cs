using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPC.Metadata.Business.Tasks
{
   
    public class PrintTaskAttribute : TaskAttribute
    {
        public Guid PrintDocument { get; set; }
        public Guid PrintTemplate { get; set; }
        public PrintTaskAttribute(string name,string task,string verb="" ,string taskDisplay="Print" ) : base(name,task,verb,taskDisplay)
        {
        }
    }   
}
