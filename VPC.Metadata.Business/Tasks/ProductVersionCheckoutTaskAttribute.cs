using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPC.Metadata.Business.Tasks
{
    public class ProductVersionCheckoutTaskAttribute : TaskAttribute
    {
        
        public ProductVersionCheckoutTaskAttribute(string name,string task,string verb="" ,string taskDisplay="Product version") : base(name,task,verb,taskDisplay)
        {
        }
    }   
}
