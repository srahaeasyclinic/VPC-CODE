using System.Collections.Generic;


namespace VPC.Entities.EntityCore.Model.Storage
{
    public class ListLayoutDetails:ListViewBasicDetails
    {
      
        public int MaxResult { get; set; }
        public List<SearchProperties> SearchProperties { get; set; }
        public List<RowLevelOperations> Actions { get; set; }
        public List<Operation> Toolbar { get; set; }        
        public string DefaultGroupBy { get; set; }
    }
}

