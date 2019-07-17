using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace VPC.Entities.Common
{    
    public class EntityMapper<T> where T : class
    { 

        public static T MapperJObject(JObject jObject)
        {            
              var instance = Activator.CreateInstance<T>(); 
              List<KeyValuePair<string,string>> data=new  List<KeyValuePair<string,string>>();
              foreach (var x in jObject)
                {
                    data.Add(new KeyValuePair<string,string>(x.Key,x.Value.ToString()) );              
                }
                MapperNode(data,instance);
            return instance;
        }

        public static IList<T> Mappers(DataTable dataTable)
        {            
            List<T> instances = new List<T>();
            List<List<KeyValuePair<string,string>>> mainObj=new  List< List<KeyValuePair<string,string>>>();              
            var columnNames = dataTable.Columns.Cast<DataColumn>().Select(column => column.ColumnName).ToList();
              for (int i = 0; i < dataTable.Rows.Count; i++)  
                {  
                    List<KeyValuePair<string,string>> childObj=new  List<KeyValuePair<string,string>>();
                    foreach(var columnName in columnNames)
                    {
                        childObj.Add(new KeyValuePair<string,string>(columnName,dataTable.Rows[i][columnName].ToString()) );  
                    } 

                    mainObj.Add(childObj); 
                }

                foreach(var main in mainObj)
                {
                    var instance = Activator.CreateInstance<T>(); 
                    MapperNode(main,instance);
                    instances.Add(instance);
                }
            return instances;
        }

        public static T Mapper(DataTable dataTable)
        { 
            var instance = Activator.CreateInstance<T>();            
             List<KeyValuePair<string,string>> childObj=new  List<KeyValuePair<string,string>>();             
            var columnNames = dataTable.Columns.Cast<DataColumn>().Select(column => column.ColumnName).ToList();
              for (int i = 0; i < dataTable.Rows.Count; i++)  
                { 
                   foreach(var columnName in columnNames)
                    {
                        childObj.Add(new KeyValuePair<string,string>(columnName,dataTable.Rows[i][columnName].ToString()) );  
                    } 
                }
            MapperNode(childObj,instance);
            return instance;
        }


       private static object GetValObjDy( object obj, string propertyName)
        {     
            var aaa=obj.GetType().GetProperty(propertyName); 
            if(aaa!=null)      
            {
                return aaa.GetValue(obj, null);
            }
            return null;
        }

        private static bool HasProperty(object objectToCheck, string propertyName)
            {
                var type = objectToCheck.GetType();
                return type.GetProperty(propertyName) != null;
            }

        private static object MapperNode(List<KeyValuePair<string,string>> dataTable,object instance )
        {
                       
            foreach(var columnName in dataTable)
                    {   
                        string[] nameParts = columnName.Key.Split('.');
                        if(columnName.Key.Contains("_"))
                        {
                         var replacedColumn= columnName.Key.Replace("_", ".");
                          nameParts = replacedColumn.Split('.');
                        }
                         var totalCount=(nameParts.Length-1);
                         var innerInstance = new object();  
                         for(int i=0; i<nameParts.Length;i++)  
                         {
                            
                             var isLast= i==totalCount;
                             if(i==0)
                             {
                                 //Check object instancsiated
                                 var oldInstance=GetValObjDy(instance,nameParts[i]);
                                 if(oldInstance!=null)
                                 {
                                     innerInstance=oldInstance;
                                     continue;
                                 }

                                 var isPropertyExists=HasProperty(instance,nameParts[i]);
                                 if(!isPropertyExists)
                                 {
                                     continue;
                                 }
                                 innerInstance= RecursiveMapperParent(instance,nameParts[i],columnName.Value,isLast);
                             }                                
                              else
                              {
                                var oldInstance=GetValObjDy(innerInstance,nameParts[i]);
                                 if(oldInstance!=null)
                                 {
                                     innerInstance=oldInstance;
                                     continue;
                                 }

                                 var isPropertyExists=HasProperty(innerInstance,nameParts[i]);
                                 if(!isPropertyExists)
                                 {
                                     continue;
                                 }

                                innerInstance=  RecursiveMapperParent(innerInstance,nameParts[i],columnName.Value,isLast);
                              }
                          


                        }
                        
                    }  
        
            return instance;
        }
        private static object RecursiveMapperParent(object instance,string key,string value,bool isLast)
        {
            foreach (var property in instance.GetType().GetProperties())
                        {              
                            if(property.Name==key)
                                {                   
                                if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
                                       {
                                           var innerInstance = Activator.CreateInstance(property.PropertyType);
                                            if(isLast)
                                            {
                                                innerInstance= RecursiveMapperChild(innerInstance,value);                                            
                                            }  
                                              property.SetValue(instance, innerInstance, null);      
                                             return innerInstance; 
                                               
                                        }
                                    }                
                                 }

                                 return instance;
        }

        private static object RecursiveMapperChild(object instance,object dataTableValue)
        {          
            foreach (var property in instance.GetType().GetProperties())
                {              
                    if( property.Name=="Value")
                            {  
                                          
                                if (property.Name=="Value")
                                {                                    
                                       if(dataTableValue!=null && dataTableValue != DBNull.Value && dataTableValue != "" )
                                       {
                                           var valueCast=Convert.ChangeType(dataTableValue, property.PropertyType);
                                            property.SetValue(instance, valueCast, null);   
                                       }
                                                                       
                                 
                                }                               
                             }                
                }
            
         return instance;            
        }



        // // public static T LoadRecord(List<KeyValuePair<string,string>> dataDic)
        // // {
        // //     List<T> instances = new List<T>();
            
        // //     var instance = Activator.CreateInstance<T>();
        // //     foreach(var dataDi in dataDic)
        // //     {
        // //         if(dataDi.Key=="ContactInformation.PersonalEmail1")
        // //         {
        // //             //RecursiveLoad(instance, dataDi);
        // //             instances.Add(instance);  
        // //         }                                
        // //     }              

        // //     return instance;
        // // }

        // // private static void Parant(object instance,KeyValuePair<string,string> dataDic)
        // // {
        // //     string[] nameParts = dataDic.Key.Split('.');  
        // //     foreach(var namePart in nameParts)
        // //     {

        // //     }

        // // }
        // // private static void RecursiveLoad(object instance,string key, string value)
        // // {
        // //     foreach (var property in instance.GetType().GetProperties())
        // //     {              
        // //         if (property.PropertyType == typeof(string) && property.Name=="Value")
        // //         {
        // //             property.SetValue(instance, value, null);
        // //             continue;
        // //         }

        // //         if (property.PropertyType.IsClass && property.PropertyType != typeof(string) && property.Name!="OperatorsAavailable")
        // //         {
        // //             var innerInstance = Activator.CreateInstance(property.PropertyType);

        // //             RecursiveLoad(innerInstance, dataDic);

        // //             property.SetValue(instance, innerInstance, null);
        // //         }
        // //     }
        // // }

        // //  public static Object GetPropValue(Object obj, String propName)
        // //         {
        // //             string[] nameParts = propName.Split('.');
        // //             if (nameParts.Length == 1)
        // //             {
        // //                 return obj.GetType().GetProperty(propName).GetValue(obj, null);
        // //             }

        // //             foreach (String part in nameParts)
        // //             {
        // //                 if (obj == null) { return null; }

        // //                 Type type = obj.GetType();
        // //                 PropertyInfo info = type.GetProperty(part);
        // //                 if (info == null) { return null; }

        // //                 obj = info.GetValue(obj, null);
        // //             }
        // //             return obj;
        // //         }

       
        // public static T Mapper1(DataTable dataTable)
        // {
        //     var instance = Activator.CreateInstance<T>();
        //      var columnNames = dataTable.Columns.Cast<DataColumn>().Select(column => column.ColumnName).ToList();
        //      for (int i = 0; i < dataTable.Rows.Count; i++)  
        //         {  
        //             foreach(var columnName in columnNames)
        //             {
        //                 var dataTableValue=dataTable.Rows[i][columnName];
        //                 foreach (var property in instance.GetType().GetProperties())
        //                     {              
        //                         if(property.Name==columnName)
        //                             {                   
        //                                if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
        //                                 {
        //                                     var innerInstance = Activator.CreateInstance(property.PropertyType);

        //                                     innerInstance= RecursiveLoadChild(innerInstance, dataTableValue);
                                        
        //                                     property.SetValue(instance, innerInstance, null);
        //                                 }
        //                             }                
        //                     }
        //             }  
        //         }  
        //     return instance;
        // }

        // public static IList<T> Mappers(DataTable dataTable)
        // {
            
        //   List<T> instances = new List<T>();
        //   var columnNames = dataTable.Columns.Cast<DataColumn>().Select(column => column.ColumnName).ToList();
           
        //    for (int i = 0; i < dataTable.Rows.Count; i++)  
        //         {  
        //             var instance = Activator.CreateInstance<T>();
        //             foreach(var columnName in columnNames)
        //             {
        //                 var dataTableValue=dataTable.Rows[i][columnName];
        //                 foreach (var property in instance.GetType().GetProperties())
        //                     {              
        //                         if(property.Name==columnName)
        //                             {                   
        //                                if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
        //                                 {
        //                                     var innerInstance = Activator.CreateInstance(property.PropertyType);

        //                                     innerInstance= RecursiveLoadChild(innerInstance, dataTableValue);
                                        
        //                                     property.SetValue(instance, innerInstance, null);
        //                                 }
        //                             }                
        //                     }

        //             }
        //             instances.Add(instance);  
        //         }  
        //     return instances;
        // }
        // private static object RecursiveLoadChild(object instance,object dataTableValue)
        // {           

        //     foreach (var property in instance.GetType().GetProperties())
        //             {               

        //                 if( property.Name=="Value")
        //                     {
            
        //                         if (property.PropertyType == typeof(string) && property.Name=="Value")
        //                         {                                    
        //                                if(dataTableValue!=null && dataTableValue != DBNull.Value)
        //                                   property.SetValue(instance, dataTableValue, null);                                
                                 
        //                         }
                               
        //                      }                
        //             }
            
        //  return instance;            
        // }
   
    }

}