using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using VPC.Metadata.Business.Entity.Configuration;

namespace VPC.Entities.Common
{
    public class Utility
    {
        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }

        public static string GetPluralName(Type type)
        {
            var operations = (PluralNameAttribute[])type.GetCustomAttributes(typeof(PluralNameAttribute), false);
            return (operations.Any()) ? operations[0].Name : string.Empty;
        }

        public static string GetDisplayName(Type type)
        {
            var operations = (DisplayNameAttribute[])type.GetCustomAttributes(typeof(DisplayNameAttribute), false);
            return (operations.Any()) ? operations[0].Name : string.Empty;
        }

        public static bool GetWorflowConfiguration(Type type)
        {
            var operations = (SupportWorkflowAttribute[])type.GetCustomAttributes(typeof(SupportWorkflowAttribute), false);
            return operations.Any() && operations[0].Value;
        }

        public static bool CustomizeValue(Type type)
        {
            var operations = (CustomizeValueAttribute[])type.GetCustomAttributes(typeof(CustomizeValueAttribute), false);
            return operations.Any();
        }

        public static List<string> GetTagables(Type t)
            {
                List<string> _list = new List<string>();

                PropertyInfo[] props = t.GetProperties();
                foreach (PropertyInfo prop in props)
                {
                    object[] attrs = prop.GetCustomAttributes(true);
                    foreach (object attr in attrs)
                    {
                        TagableAttribute authAttr = attr as TagableAttribute;
                        if (authAttr != null)
                        {
                            _list.Add( prop.Name);
                        }
                    }
                }

                return _list;
            }

            
    }
}