using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPC.Metadata.Business.Tasks
{  
    public class TaskType
    {
        public const string FrontTask = "FrontTask";
        public const string BackTask = "BackTask";
        
    }   

    public class TaskVerb{
        public const string Post="Post";
        public const string Put="Put";
        public const string Delete="Delete";
        public const string Patch="Patch";
    }

    public class TaskDisplay
    {
        public const string PopUp="PopUp";
        public const string Full="Full";
    }
}
