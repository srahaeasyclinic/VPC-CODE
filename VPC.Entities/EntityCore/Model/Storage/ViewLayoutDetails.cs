using System.Collections.Generic;


namespace VPC.Entities.EntityCore.Model.Storage
{
    public class ViewLayoutDetails:ListViewBasicDetails
    {
      public List<RowLevelOperations> Actions { get; set; }
    }
}

