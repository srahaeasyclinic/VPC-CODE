using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using VPC.Entities.WorkFlow;
using VPC.Entities.WorkFlow.Engine;

namespace VPC.Framework.Business.WorkFlow.Attribute
{
  
   public class WorkFlowHelper 
    {
        public static List<WorkFlowResource> GetAllSteps(string module)
        {
            Type itemType = typeof(WorkFlowEngine);
            PropertyInfo[] properties = itemType.GetProperties();
            List<PropertyInfo> lst = (from prop in properties
                                      let attrs = prop.GetCustomAttributes(true)
                                      from attr in attrs
                                      let authAttr = attr as WorkFlowModelAttribute
                                      where authAttr != null && authAttr.Context == module
                                      select prop).ToList();
            return (from propertyInfo in lst
                    let firstOrDefault = (from desattr in propertyInfo.GetCustomAttributes(true)
                                          let dattr = desattr as WorkFlowModelAttribute
                                          select dattr).ToList().FirstOrDefault()

                    where firstOrDefault != null
                    select new WorkFlowResource
                               {
                                   Id = (Guid)propertyInfo.GetValue(propertyInfo, null),
                                   Value = firstOrDefault.Key,
                                   Key = firstOrDefault.Key,
                                   TransitionLabelKey = firstOrDefault.TransitionLabelKey
                               }).ToList();
        }

        public static List<WorkFlowResource> GetAllOperations()
        {
            Type itemType = typeof(WorkFlowEngineOperation);
            PropertyInfo[] properties = itemType.GetProperties();
            return (from propertyInfo in properties
                    let attr = propertyInfo.GetCustomAttributes(true).FirstOrDefault() as DescriptionAttribute
                    where attr != null
                    select
                        new WorkFlowResource
                            {
                                Id = (Guid)propertyInfo.GetValue(propertyInfo, null),
                                Value = propertyInfo.Name,
                                Key = attr.Description
                            }).ToList();
        }

        public static List<WorkFlowResource> GetProcessorTitleByTransition(Guid from, Guid to, string module)
        {
            IEnumerable<Type> types =
               Assembly.GetExecutingAssembly().GetTypes().Where(t => t.GetInterfaces().Contains(typeof(ITransition))
                                                                     && t.GetConstructor(Type.EmptyTypes) != null);

            var processTitles = new List<WorkFlowResource>();

            foreach (Type tType in types)
            {
                object[] ss = tType.GetCustomAttributes(true);
                processTitles.AddRange(from TransitionAttribute o in ss
                                       where
                                           new Guid(o.From) == @from && new Guid(o.To) == to && o.Context == module
                                       select new WorkFlowResource { Id = new Guid(o.Id), Key = o.Key,ProcessType=o.ProcessType });
            }

            return processTitles;
        }

        public static List<WorkFlowResource> GetProcessorTitleByOperation(string operation, string module)
        {
            IEnumerable<Type> types =
               Assembly.GetExecutingAssembly().GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IOperation))
                                                                     && t.GetConstructor(Type.EmptyTypes) != null);

            var processTitles = new List<WorkFlowResource>();

            foreach (Type tType in types)
            {
                object[] ss = tType.GetCustomAttributes(true);
                processTitles.AddRange(from OperationAttribute o in ss
                                       where  o.OperationName == operation && o.Context == module
                                       select new WorkFlowResource { Id = new Guid(o.Id), Key = o.Key,ProcessType=o.ProcessType });
            }

            return processTitles;
        }

        public static List<WorkFlowResource> GetProcessorTitleByTransitionModule(string module)
        {
            IEnumerable<Type> types =
               Assembly.GetExecutingAssembly().GetTypes().Where(t => t.GetInterfaces().Contains(typeof(ITransition))
                                                                     && t.GetConstructor(Type.EmptyTypes) != null);

            var processTitles = new List<WorkFlowResource>();

            foreach (Type tType in types)
            {
                object[] ss = tType.GetCustomAttributes(true);
                processTitles.AddRange(from TransitionAttribute o in ss
                                       where o.Context == module
                                       select new WorkFlowResource { Id = new Guid(o.Id), Key = o.Key,ProcessType=o.ProcessType });
            }

            return processTitles;
        }

        public static List<WorkFlowResource> GetProcessorTitleByOperationModule(string module)
        {
            IEnumerable<Type> types =
               Assembly.GetExecutingAssembly().GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IOperation))
                                                                     && t.GetConstructor(Type.EmptyTypes) != null);

            var processTitles = new List<WorkFlowResource>();

            foreach (Type tType in types)
            {
                object[] ss = tType.GetCustomAttributes(true);
                processTitles.AddRange(from OperationAttribute o in ss
                                       where o.Context == module
                                       select new WorkFlowResource { Id = new Guid(o.Id), Key = o.Key,ProcessType=o.ProcessType });
            }

            return processTitles;
        }

        public static List<WorkFlowModelAttribute> GetAllWorkflowModules()
        {
            Type itemType = typeof(WorkFlowEngine);
            PropertyInfo[] properties = itemType.GetProperties();
            List<PropertyInfo> propLst = (from prop in properties
                                          let attrs = prop.GetCustomAttributes(true)
                                          from attr in attrs
                                          let authAttr = attr as WorkFlowModelAttribute
                                          select prop).ToList();

            return propLst.SelectMany(
                propertyInfo => propertyInfo.GetCustomAttributes(true).Cast<WorkFlowModelAttribute>()
                ).GroupBy(x => x.Context).Select(g => g.First()).ToList();
        }

        public static string GetWorkFlowModuleNameByContext(Guid context)
        {
            WorkFlowModelAttribute firstOrDefault = GetAllWorkflowModules().FirstOrDefault(x => new Guid(x.Context) == context);
            return firstOrDefault != null ? firstOrDefault.Name : "";
        }


        public static List<WorkFlowResource> GetAllOperationProcess()
        {
            IEnumerable<Type> types =
                Assembly.GetExecutingAssembly().GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IOperation))
                                                                      && t.GetConstructor(Type.EmptyTypes) != null);

            return (from type in types
                    select (WorkFlowModelAttribute)type.GetCustomAttributes(true).FirstOrDefault()
                        into dattr
                        where dattr != null
                        select new WorkFlowResource { Id = new Guid(dattr.Context), Key = dattr.Key }).ToList();
        }

        public static Type GetTransitionType(Guid? typeContext)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.GetInterfaces().Contains(typeof(ITransition))
                                                                     && t.GetConstructor(Type.EmptyTypes) != null);
            Type myType = null;

            foreach (var t in from t in types let attr = (TransitionAttribute)t.GetCustomAttributes(true).FirstOrDefault() where attr != null where new Guid(attr.Id) == typeContext select t)
            {
                myType = t;
            }
            return myType;
        }

        public static Type GetOperationType(Guid? typeContext)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IOperation))
                                                                     && t.GetConstructor(Type.EmptyTypes) != null);
            Type myType = null;

            foreach (var t in from t in types let attr = (OperationAttribute)t.GetCustomAttributes(true).FirstOrDefault() where attr != null where new Guid(attr.Id) == typeContext select t)
            {
                myType = t;
            }
            return myType;
        }

        public static string GetCultureInfo(System.Net.Http.Headers.HttpRequestHeaders headers)
        {
            if (!headers.Contains("Culture-Info")) return null;
            return headers.GetValues("Culture-Info").FirstOrDefault();
        }

        public static string GetKeyByContext(Guid guid, string module)
        {
            WorkFlowResource resource = GetAllSteps(module).FirstOrDefault(x => x.Id == guid);
            return resource != null ? resource.Value : "";
        }

         public static string GetKeyByContext(string entityId, Guid transitionType)
        {
            var workFlowResource = GetAllSteps(entityId).FirstOrDefault(x => x.Id == transitionType);
            return workFlowResource != null ? workFlowResource.Key : null;
        }

        public static string GetValueByContext(string entityId, Guid transitionType)
        {
            var workFlowResource = GetAllSteps(entityId).FirstOrDefault(x => x.Id == transitionType);
            return workFlowResource != null ? workFlowResource.Value : null;
        }
    }

}
