using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPC.Metadata.Business.Tasks
{
    public class MergeTaskAttribute : TaskAttribute
    {
        public Guid MergeTo { get; set; }
        public Guid MergeFrom { get; set; }
        public MergeTaskAttribute(string name,string task,string verb="" ,string taskDisplay="Merge") : base(name,task,verb,taskDisplay)
        {
        }
    }   
}
