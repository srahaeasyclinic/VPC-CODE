using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Permissions;
using System.Xml;
using VPC.Framework.Business.Data.SqlClient;
using VPC.Framework.Business.Data.Validator;
using VPC.Framework.Business.Exception;


namespace VPC.Framework.Business.Data
{
    public delegate void ProcessDataRecordAction(IDataRecord reader);

    public delegate void ProcessDataRecordAction<A>(A element, IDataRecord reader);

    public delegate A ProcessDataRecordFunction<A>(IDataRecord reader);

    public delegate B ProcessDataRecordFunction<A, B>(A element, IDataRecord reader);

   
    internal abstract class DataStoreAccessor
    {
        private readonly string _connectionString;
        protected DataStoreAccessor()
        { 
            _connectionString = Configuration.ConnectionString.GetConnectionString();
        }

        

        protected string ConnectionString
        {
            get { return _connectionString; }
        }

        protected SqlProcedureCommand CreateProcedureCommand(string commandText)
        {
            return new SqlProcedureCommand(commandText, ConnectionString);
        }

        protected static SqlProcedureCommand CreateProcedureCommand(SqlConnection connection, string commandText)
        {
            return new SqlProcedureCommand(commandText, connection);
        }

        protected SqlConnection CreateConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        protected string ExecuteCommandAndReturnXmlString(SqlProcedureCommand cmd, string rootLocalName)
        {
            try
            {
                return cmd.ExecuteXmlString(rootLocalName);
            }
            catch (SqlException e)
            {
                string message;
                try
                {
                    message = DataDiagnostics.FormatCommandExceptionMessage(cmd.Command, 0);
                }
                catch (System.Exception ex)
                {
                    message = ex.Message;
                }
                throw ReportAndTranslateException(e, message);
            }
        }

        protected SqlDataReader ExecuteCommandAndReturnReader(SqlProcedureCommand cmd)
        {
            try
            {
                return cmd.ExecuteReader();
            }
            catch (SqlException e)
            {
                string message;
                try
                {
                    message = DataDiagnostics.FormatCommandExceptionMessage(cmd.Command, 0);
                }
                catch (System.Exception ex)
                {
                    message = ex.Message;
                }
                throw ReportAndTranslateException(e, message);
            }
        }

        protected byte[] ExecuteCommandAndReturnBinary(SqlProcedureCommand cmd)
        {
            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SequentialAccess))
                {
                    if (reader.Read())
                    {
                        var binary = (byte[]) reader[0];

                        if (reader.Read())
                            throw new StorageException("Multiple binary rows return. Expected one.");

                        return binary;
                    }

                    return null;
                }
            }
            catch (SqlException e)
            {
                string message;
                try
                {
                    message = DataDiagnostics.FormatCommandExceptionMessage(cmd.Command, 0);
                }
                catch (System.Exception ex)
                {
                    message = ex.Message;
                }

                throw ReportAndTranslateException(e, message);
            }
        }

        protected List<Guid> ExecuteCommandAndReturnGuidArray(SqlProcedureCommand cmd)
        {
            var list = new List<Guid>();
            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(reader.GetGuid(0));
                    }
                }
            }
            catch (SqlException e)
            {
                string message;
                try
                {
                    message = DataDiagnostics.FormatCommandExceptionMessage(cmd.Command, 0);
                }
                catch (System.Exception ex)
                {
                    message = ex.Message;
                }
                throw ReportAndTranslateException(e, message);
            }

            return list;
        }
         
        protected List<T> ExecuteListCommand<T>(string storedProcedureName,
                                                ProcessDataRecordFunction<T> processRecordFunction)
        {
            SqlProcedureCommand cmd = CreateProcedureCommand(storedProcedureName);
            var list = new List<T>();
            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(processRecordFunction(reader));
                    }
                }
            }
            catch (SqlException e)
            {
                string message;
                try
                {
                    message = DataDiagnostics.FormatCommandExceptionMessage(cmd.Command, 0);
                }
                catch (System.Exception ex)
                {
                    message = ex.Message;
                }
                throw ReportAndTranslateException(e, message);
            }

            return list;
        }

        protected void ExecuteReaderCommand(SqlProcedureCommand cmd, ProcessDataRecordAction processRecord)
        {
            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        processRecord(reader);
                    }
                }
            }
            catch (SqlException e)
            {
                string message;
                try
                {
                    message = DataDiagnostics.FormatCommandExceptionMessage(cmd.Command, 0);
                }
                catch (System.Exception ex)
                {
                    message = ex.Message;
                }
                throw ReportAndTranslateException(e, message);
            }
        }

        protected List<T> ExecuteListCommand<T>(SqlProcedureCommand cmd,ProcessDataRecordFunction<T> processRecordFunction)
        {
            var list = new List<T>();
            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(processRecordFunction(reader));
                    }
                }
            }
            catch (SqlException e)
            {
                string message;
                try
                {
                    message = DataDiagnostics.FormatCommandExceptionMessage(cmd.Command, 0);
                }
                catch (System.Exception ex)
                {
                    message = ex.Message;
                }
                throw ReportAndTranslateException(e, message);
            }

            return list;
        }

        protected T[] ExecuteArrayCommand<T>(string storedProcedureName,ProcessDataRecordFunction<T> processRecordFunction)
        {
            return ExecuteListCommand(storedProcedureName, processRecordFunction).ToArray();
        }

        protected T[] ExecuteArrayCommand<T>(SqlProcedureCommand cmd, ProcessDataRecordFunction<T> processRecordFunction)
        {
            return ExecuteListCommand(cmd, processRecordFunction).ToArray();
        }
        
        protected int ExecuteCommand(SqlProcedureCommand cmd, ICommandResultValidator validator)
        {
            try
            {
                int value = cmd.Execute();
                if (validator.IsValid(value))
                    return value;

                string message = DataDiagnostics.FormatCommandExceptionMessage(cmd.Command, value);
                if (value == 2)
                    throw new ArgumentException(message);

                throw new System.Exception(message);
            }
            catch (SqlException e)
            {
                string message;
                try
                {
                    message = DataDiagnostics.FormatCommandExceptionMessage(cmd.Command, 0);
                }
                catch (System.Exception ex)
                {
                    message = ex.Message;
                }
                throw ReportAndTranslateException(e, message);
            }
        }

        protected int ExecuteCommand(SqlProcedureCommand cmd)
        {
            return ExecuteCommand(cmd, new DefaultCommandResultValidator());
        }

        protected int ExecuteCommandWithValidResults(SqlProcedureCommand cmd, params int[] validResults)
        {
            return ExecuteCommand(cmd, new CommandResultRangeValidator(validResults));
        }
        
        protected static string ArrayToXmlString(Guid[] array)
        {
            var xmlWriter = new XmlStringWriter(WriterMode.Managed);
            XmlWriter writer = xmlWriter.XmlWriter;
            writer.WriteStartElement("a");

            foreach (Guid item in array)
            {
                writer.WriteStartElement("i");
                writer.WriteAttributeString("v", XmlConvert.ToString(item));
                writer.WriteEndElement();
            }
            writer.WriteEndElement(); // array
            return xmlWriter.XmlString;
        }
         
        protected System.Exception ReportAndTranslateException(SqlException e, string commentFormat, params object[] arg)
        {
            return ReportAndTranslateException(e, string.Format(commentFormat, arg));
        }

        protected System.Exception ReportAndTranslateException(SqlException e, string commentFormat, object arg0)
        {
            return ReportAndTranslateException(e, string.Format(commentFormat, arg0));
        }

        protected System.Exception ReportAndTranslateException(SqlException e, string comment)
        {
            System.Exception newException; 

            // Translation
            if (e.Number == 547)
            {
                newException = new DataRelationException("Invalid data relation", e);
            }
            else if (e.Number == 7603 || e.Number == 7619)
            {
                newException = new InvalidFilterException("Invalid filter", e);
            }
            else if (e.Number >= 7600 && e.Number < 7700)
            {
                newException = new FilterException("Error in filter", e);
            }
            else if (e.Number >= 8100 && e.Number < 8200)
            {
                newException = new StorageException("Input value is exceeding the range", e);
            }
            else
            {
                newException = new StorageException("Storage error", e);
            }  
            return newException;
        }
    }
}