using System;
using System.Collections.Generic;

namespace VPC.Entities.EntityCore.Model.Storage
{
    public class FieldModel
    {
        public dynamic Value { get; set; }
        public string Name { get; set; }
        public string TypeOf { get; set; }
        public string DataType { get; set; }
        public string ControlType { get; set; }
        public string PickListId { get; set; }
        public bool ReadOnly { get; set; }
        public List<Validator> Validators { get; set; }
        public dynamic SelectedView { get; set; }

        //only for address
        public List<FieldModel> Fields { get; set; }
        public List<FieldModel> Tabs { get; set; }
        public int? DecimalPrecision { get; set; }

        //   public bool? IsApplicableForFilter { get; set; }
        //public string RefId { get; set; }
        public string DefaultValue { get; set; }
        public string Properties { get; set; }

        public bool? ApplicableForSimpleSearch { get; set; }
        public bool? ApplicableForAdvanceSearch { get; set; }
        public bool? ApplicableForFreeTextSearch { get; set; }

        public Setting Setting { get; set; }
        public bool IsQueryable { get; set; }
        public bool Required { get; set; }
        public List<int> AccessibleLayoutTypes { get; set; }
        public List<string> ReceiverDataTypes { get; set; }
        public List<string> ReceivingTypes { get; set; }
        public List<string> BroadcastingTypes { get; set; }
        public bool IsTagable { get; set; }
        public string RefId { get; set; }
        public List<Operation> Toolbar { get; set; } 
         public string EntityName {get;set;}
    }
}