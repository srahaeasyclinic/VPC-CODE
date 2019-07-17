using System.Collections.Generic;

namespace VPC.Entities.EntityCore.Model.Storage
{
    public class Properties
    {
        public string Name { get; set; }
        public string ControlType { get; set; }
        public string DataType { get; set; }
        public string RefId { get; set; }
        //public List<ControlTypesList> ControlTypes { get; set; }
        public string DefaultValue { get; set; }
        public int Sequence { get; set; }
        public List<KeyValue> Values { get; set; }
        public string typeOf { get; set; }
    }

//public class KeyValue{
//  public dynamic Id{get;set;}
//  public string Value{get;set;}
//}

}

