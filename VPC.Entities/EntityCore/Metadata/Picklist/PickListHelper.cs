using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using VPC.Entities.WorkFlow;
using VPC.Entities.WorkFlow.Engine;

namespace VPC.Entities.EntityCore.Metadata.Picklist
{  
    internal static class PickListHelper
    {
        internal static Dictionary<string, dynamic> GetPickListData(Type t)
        {
            var statusEnum = new Dictionary<string, dynamic>();
            Array enumValueArray = Enum.GetValues(t);
            foreach (int enumValue in enumValueArray)
            {
                var Name = Enum.GetName(t, enumValue);
                statusEnum.Add(Name, enumValue);
            }
            return statusEnum;
        }

        internal static Dictionary<string, dynamic> GetPickListWorkFlowData(string module)
        {
            var statusEnum = new Dictionary<string, dynamic>();
            
            Type itemType = typeof(WorkFlowEngine);
            PropertyInfo[] properties = itemType.GetProperties();
            List<PropertyInfo> lst = (from prop in properties
                                      let attrs = prop.GetCustomAttributes(true)
                                      from attr in attrs
                                      let authAttr = attr as WorkFlowModelAttribute
                                      where authAttr != null && authAttr.Context == module
                                      select prop).ToList();
            var steps= (from propertyInfo in lst
                    let firstOrDefault = (from desattr in propertyInfo.GetCustomAttributes(true)
                                          let dattr = desattr as WorkFlowModelAttribute
                                          select dattr).ToList().FirstOrDefault()

                    where firstOrDefault != null
                    select new WorkFlowResource
                               {
                                   Id = (Guid)propertyInfo.GetValue(propertyInfo, null),
                                   Description = firstOrDefault.Description,
                                   Status=firstOrDefault.Status,
                                   Key = firstOrDefault.Key,
                                   TransitionLabelKey = firstOrDefault.TransitionLabelKey
                               }).ToList();

            
            foreach (var step in steps)
            {                
                statusEnum.Add(step.Key,step.Id.ToString());
            }
            return statusEnum;
        }


        internal static DataTable GetValues(Dictionary<string, dynamic> lists)
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("InternalId", typeof(string));
            dt.Columns.Add("Text", typeof(string));
            dt.Columns.Add("Active", typeof(bool));
            dt.Columns.Add("IsDeletetd", typeof(bool));
            dt.Columns.Add("Flagged", typeof(bool));
            dt.Columns.Add("totalRow", typeof(int));
            dt.Columns.Add("rowNumber", typeof(int));

            var i = 1;

            foreach (var list in lists)
            {
                DataRow dr = dt.NewRow();
                dr["InternalId"] = list.Value;
                dr["Text"] = list.Key;
                dr["Active"] = true;
                dr["IsDeletetd"] = false;
                dr["Flagged"] = false;
                dr["totalRow"] = lists.Count;
                dr["rowNumber"] = i;
                dt.Rows.Add(dr);
                i++;
            }

            return dt;
        }
        

        internal static Dictionary<string,dynamic> GetHours()
        {
            var list = new Dictionary<string, dynamic>();          
            for (int i=0;i<=23; i++)
            {               
                list.Add(i.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'), i.ToString());
            }
            return list;

        }
        internal static Dictionary<string,dynamic> GetMinute()
        {
            var list = new Dictionary<string, dynamic>();          
            for (int i=1;i<=60; i++)
            {               
                list.Add(i.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'), i.ToString());
            }
            return list;

        }
    
    }
}