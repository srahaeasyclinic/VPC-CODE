
using System;
using System.ComponentModel;

namespace VPC.Entities.Common.Functions
{

     [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public class EntityGroup: Attribute
    {
        private readonly string _groupId;
        private readonly string _groupName;   
        public EntityGroup(string id, string name="")
        {
            _groupId = id;
            _groupName = name;     
        }       
        public string GetGroupId()
        {
            return _groupId;
        }
        public string GetGroupName()
        {
            return _groupName;
        }
    }

}
