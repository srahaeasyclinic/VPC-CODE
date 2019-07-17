using System;
using System.IO;

namespace VPC.WebApi.Utility
{
    public static class ExceptionFormatter
    {
        private static void SerializeInnerException(Exception e, TextWriter writer)
        {
            if (e != null)
            {
                writer.WriteLine("Inner Exception: {0}", e.Message);
                writer.WriteLine("Inner Exception: {0}", e.GetType());
                SerializeInnerException(e.InnerException, writer);
            }
        }

        public static string SerializeToString(Exception exception)
        {
            var sourceMethod = string.Empty;
            var sourceType = string.Empty;
            var methodBase = exception.TargetSite;
            if (methodBase != null)
            {
                sourceMethod = methodBase.ToString();
                var declaringType = methodBase.DeclaringType;
                sourceType = (declaringType == null) ? string.Empty : declaringType.AssemblyQualifiedName;
            }
            var writer = new StringWriter();

            writer.WriteLine("User: Anonymous");
            writer.WriteLine("Message: {0}", exception.Message);
            writer.WriteLine("ExceptionType: {0}", exception.GetType());
            writer.WriteLine("SourceType: {0}", sourceType);
            writer.WriteLine("SourceMethod: {0}", sourceMethod);
            writer.WriteLine("StackTrace:");
            writer.WriteLine(exception.StackTrace);
            SerializeInnerException(exception.InnerException, writer);
            return writer.ToString();
        }
    }
}