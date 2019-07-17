using System.Data.SqlClient;
using System.Globalization;
using System.IO;

namespace VPC.Framework.Business.Data
{
    internal static class DataDiagnostics
    {
        internal static string FormatCommandExceptionMessage(SqlCommand command, int code)
        {
            var writer = new StringWriter();
            writer.WriteLine("Command '{0}' failed with code={1}", command.CommandText, code);
            foreach (SqlParameter param in command.Parameters)
            {
                if (param.Value != null)
                {
                    string value = param.Value.ToString();
                    if (value.Length > 100)
                        value = value.Substring(0, 100);

                    writer.WriteLine("Param {0}: Value {1}.", param.ParameterName, value);
                }
                else
                {
                    writer.WriteLine("Param {0}.", param.ParameterName);
                }
            }

            return writer.ToString();
        }

        internal static string FormatSqlException(SqlException e)
        {
            var writer = new StringWriter(CultureInfo.InvariantCulture);

            writer.WriteLine("Source: {0}", e.Source);
            writer.WriteLine("Number: {0}", e.Number);
            writer.WriteLine("Class: {0}", e.Class);
            writer.WriteLine("State: {0}", e.State);
            writer.WriteLine("Server: {0}", e.Server);
            writer.WriteLine("Message: {0}", e.Message);
            writer.WriteLine("Procedure: {0}", e.Procedure);
            writer.WriteLine("LineNumber: {0}", e.LineNumber);

            return writer.ToString();
        }
    }
}