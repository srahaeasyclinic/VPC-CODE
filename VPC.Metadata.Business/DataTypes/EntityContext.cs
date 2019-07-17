using System;

namespace VPC.Metadata.Business.DataTypes
{
    public class EntityContext 
    {

        private string _id;
        public EntityContext(string context)
        {
            _id = context;
        }
        public EntityContext()
        {
        }
        public string GetContext()
        {
            return _id;
        }
    }
}
