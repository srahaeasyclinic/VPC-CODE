using System.Collections.Generic;

namespace VPC.Entities.EntityCore.Model.Storage
{
    public class SelectedItem
    {
        public string Name { get; set; }
        public int Sequence { get; set; }
        public bool Hidden { get; set; }
        public string DataType { get; set; }
        public string RefId { get; set; }
        public string DefaultValue { get; set; }
        public string Properties { get; set; }
        //public List<string> Selected { get; set; }
        public List<ActiveValue> Values { get; set; }
        public bool Clickable { get; set; }   
        public string DefaultView { get; set; }     
        public string typeOf { get; set; }
    }
}

