using System;
using System.Collections.Generic;

namespace VPC.Entities.EntityCore.Model.Storage
{
    public class LayoutModel : LayoutBasicModel
    {
        public string Layout { get; set; }
        public ListLayoutDetails ListLayoutDetails { get; set; }
        public FormLayoutDetails FormLayoutDetails { get; set; }

        public ViewLayoutDetails ViewLayoutDetails { get; set; }

        // public List<Operation> Toolbar { get; set; }
        // public List<Tasks> Tasks { get; set; }
    }
}

