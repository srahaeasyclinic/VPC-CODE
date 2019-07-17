using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VPC.WebApi.Model
{
    public class FieldModel
    {
        public string Name { get; set; }
        public string DataType { get; set; }
        public string ControlType { get; set; }
        public string PickListId { get; set; }
        public bool ReadOnly { get; set; }
        public List<Validator> Validators { get; set; }

        //only for address
        public List<FieldModel> Fields { get; set; }
        public int DecimalPrecision { get; set; }
        public bool IsApplicableForFilter { get; internal set; }
        public string RefId { get; set; }
        public string defaultValue { get; set; }
        public string properties { get; set; }
    }
}
