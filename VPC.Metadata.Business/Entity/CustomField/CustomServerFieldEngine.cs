using System;
using System.Linq;
using System.Reflection;
using VPC.Metadata.Business.Entity.CustomField.Execution;


namespace VPC.Metadata.Business.Entity.CustomField {
    public class CustomServerFieldEngine {




        public dynamic GetValue(string className, CustomFieldExecutionPayload payload)
        {
            var type = GetType (className);
            if (type == null) return string.Empty;
            var myObject = Activator.CreateInstance (type);
            MethodInfo executeMethod = type.GetMethod ("Execute");
            if (executeMethod == null) return string.Empty;
            var result = (CustomFieldExecutionMessage) executeMethod.Invoke (myObject, new object[] { payload });
            return result.Message;
        }

        private static Type GetType (string triggerName) {

            var classes = Assembly
                .GetEntryAssembly () ?
                .GetReferencedAssemblies ()
                .Select (Assembly.Load)
                .SelectMany (x => x.DefinedTypes)
                .Where (type =>
                    typeof (ICustomServerFieldExecution).GetTypeInfo ().IsAssignableFrom (type.AsType ()) &&
                    !type.IsInterface

                ).ToList ();

            Type t = null;
            if (classes == null) return t;
            return classes.FirstOrDefault(item => item.Name.ToLower().Equals(triggerName.ToLower()));
        }
    }
}