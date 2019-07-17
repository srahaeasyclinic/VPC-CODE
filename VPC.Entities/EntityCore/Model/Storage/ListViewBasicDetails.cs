using System.Collections.Generic;


namespace VPC.Entities.EntityCore.Model.Storage
{
    public class ListViewBasicDetails
    {
        public List<SelectedItem> Fields { get; set; }
        public OrderDetails DefaultSortOrder { get; set; }
    }
}

