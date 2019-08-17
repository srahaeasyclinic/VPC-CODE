using System;
using System.Linq;
using System.Reflection;
using VPC.Metadata.Business.Entity.Task.Execution;

namespace VPC.Metadata.Business.Entity.Task {
    public class TaskEngine {

        public TaskExecutionMessage GetValue (string className, TaskExecutionPayload payload) {
            var type = GetType (className);
            if (type == null) throw new FieldAccessException (className + "not found");
            var myObject = Activator.CreateInstance (type);
            MethodInfo executeMethod = type.GetMethod ("Execute");
            if (executeMethod == null) throw new FieldAccessException (className + "not found");
            var result = (TaskExecutionMessage) executeMethod.Invoke (myObject, new object[] { payload });
            return result;
        }

        private static Type GetType (string triggerName) {

            var classes = Assembly
                .GetEntryAssembly () ?
                .GetReferencedAssemblies ()
                .Select (Assembly.Load)
                .SelectMany (x => x.DefinedTypes)
                .Where (type =>
                    typeof (ITaskExecution).GetTypeInfo ().IsAssignableFrom (type.AsType ()) &&
                    !type.IsInterface

                ).ToList ();

            Type t = null;
            if (classes == null) return t;
            return classes.FirstOrDefault (item => item.Name.ToLower ().Equals (triggerName.ToLower ()));
        }
    }
}