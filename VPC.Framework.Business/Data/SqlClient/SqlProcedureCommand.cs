using System.Data;
using System.Data.SqlClient;

namespace VPC.Framework.Business.Data.SqlClient
{
    internal sealed class SqlProcedureCommand : SqlParameterCommand
    {
        private SqlParameter _returnValue;

        public SqlProcedureCommand(string commandText, string connectionString)
            : base(commandText, connectionString, CommandType.StoredProcedure)
        {
            AppendRetVal();
        }

        public SqlProcedureCommand(string commandText, SqlConnection connection)
            : base(commandText, connection, CommandType.StoredProcedure)
        {
            AppendRetVal();
        }

        public int ReturnValue
        {
            get { return (int) _returnValue.Value; }
        }


        private void AppendRetVal()
        {
            _returnValue = AppendReturnValue();
        }

        public int Execute()
        {
            int affectedRecords;
            return Execute(out affectedRecords);
        }

        public int Execute(out int affectedRecords)
        {
            if (InternalConnection) // Managed Connection
            {
                using (Connection)
                {
                    Connection.Open();
                    affectedRecords = Command.ExecuteNonQuery();
                }
            }
            else
            {
                affectedRecords = Command.ExecuteNonQuery();
            }
            return (int) _returnValue.Value;
        }
    }
}