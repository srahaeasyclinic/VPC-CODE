
using System;
using System.Collections.Generic;
using VPC.Entities.EntityCore.Model.Storage;

namespace VPC.Entities.EntityCore
{
    public class InitilizeResponseMessage
    {
        public List<Informatiom> Info { get; set; }
    }

    public class Informatiom
    {
        public string Message { get; set; }
        public int ErrorLevel { get; set; }
    }

    public class EntityMessageInfo
    {
        public string EntityContext { get; set; }
        public LayoutFor LayoutFor { get; set; }
        public bool IsSupportWorkflow { get; set; }
        public string EntityName { get; set; }
        public List<string> Subtypes { get; set; }
        public List<Operation> Operations { get; set; }
    }

    class ProductComparer : IEqualityComparer<EntityMessageInfo>
    {

        public bool Equals(EntityMessageInfo x, EntityMessageInfo y)
        {
            if (Object.ReferenceEquals(x, y)) return true;
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            return x.EntityContext == y.EntityContext && x.LayoutFor == y.LayoutFor;
        }



        public int GetHashCode(EntityMessageInfo obj)
        {
            if (Object.ReferenceEquals(obj, null)) return 0;
            int hashEntityContext = obj.EntityContext == null ? 0 : obj.EntityContext.GetHashCode();
            int hashEntityLayoutFor = obj.LayoutFor.GetHashCode();

            //Calculate the hash code for the product.
            return hashEntityContext ^ hashEntityLayoutFor;
        }
    }
}
