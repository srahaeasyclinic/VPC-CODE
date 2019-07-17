using System.Data;
using System.Data.SqlClient;

namespace VPC.Framework.Business.Data.SqlClient
{
    internal sealed class SqlTextCommand : SqlParameterCommand
    {
        public SqlTextCommand(string commandText, string connectionString)
            : base(commandText, connectionString, CommandType.Text)
        {
        }

        public SqlTextCommand(string commandText, SqlConnection connection)
            : base(commandText, connection, CommandType.Text)
        {
        }

        public int Execute()
        {
            if (InternalConnection) 
            {
                using (Connection)
                {
                    Connection.Open();
                    return Command.ExecuteNonQuery();
                }
            }

            return Command.ExecuteNonQuery();
        }
    }
}