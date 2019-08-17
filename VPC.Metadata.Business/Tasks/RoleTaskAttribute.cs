using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPC.Metadata.Business.Tasks
{
   
    public class RoleTaskAttribute : TaskAttribute
    {
        public Guid PrintDocument { get; set; }
        public Guid PrintTemplate { get; set; }
        public RoleTaskAttribute(string name,string task,string verb="" ,string taskDisplay="Role" ) : base(name,task,verb,taskDisplay)
        {
        }
    }   
}
