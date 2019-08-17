using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using VPC.Metadata.Business.Entity.Trigger.Execution;

namespace VPC.Metadata.Business.Entity.Trigger {
    public class TriggerEngine {

        /// <summary>
        /// single handled process mechanism
        /// </summary>
        public void Process () {
            throw new NotImplementedException ();
        }

        public string GetQuery (TriggerAttribute[] triggers, TriggerExecutionPayload payload) {
            if (!triggers.Any ()) return string.Empty;
            var singletonTrigger = triggers[0];
            var type = GetTriggerType (singletonTrigger.GetExecutionClassName ());
            if (type == null) return string.Empty;
            var myObject = Activator.CreateInstance (type);
            MethodInfo executeMethod = type.GetMethod ("ExecuteTrigger");
            if (executeMethod == null) return string.Empty;
            var result = (TriggerExecutionMessage) executeMethod.Invoke (myObject, new object[] { payload });
            return result.Message;
        }

        public static Type GetTriggerType (string triggerName) {

            var classes = Assembly
                .GetEntryAssembly () ?
                .GetReferencedAssemblies ()
                .Select (Assembly.Load)
                .SelectMany (x => x.DefinedTypes)
                .Where (type =>
                    typeof (ITriggerExecution).GetTypeInfo ().IsAssignableFrom (type.AsType ()) &&
                    !type.IsInterface

                ).ToList ();

            Type t = null;
            if (classes == null) return t;
            return classes.FirstOrDefault(item => item.Name.ToLower().Equals(triggerName.ToLower()));
        }
    }
}