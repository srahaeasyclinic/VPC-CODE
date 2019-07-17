using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPC.Metadata.Business.Tasks
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class TaskAttribute : Attribute
    {
        private readonly string _name;
        private readonly string _taskType;
        private readonly string _taskVerb;
        private readonly string _taskDisplay;
        public TaskAttribute(string name,string taskType,string taskVerb,string taskDisplay )
        {
            _name = name;
            _taskType=taskType;
            _taskVerb=taskVerb;
            _taskDisplay=taskDisplay;
        }

        public string Name { get { return _name; } }
        public string TaskType{get{return _taskType;}}
        public string TaskVerb{get{return _taskVerb;}}
        public string TaskDisplay {get{return _taskDisplay;}}

        
    }   
}
