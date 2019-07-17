using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;

namespace VPC.Framework.Business.Data.SqlClient
{
    internal abstract class SqlParameterCommand
    {
        private SqlCommand _command;
        private SqlConnection _connection;
        private bool _internalConnection;

        protected SqlParameterCommand(string commandText, string connectionString, CommandType commandType)
        {
            Create(commandText, new SqlConnection(connectionString), commandType, true);
        }

        protected SqlParameterCommand(string commandText, SqlConnection connection, CommandType commandType)
        {
            Create(commandText, connection, commandType, false);
        }

        /// <summary>
        /// Gets a value indicating whether the connection is internal (managed).
        /// </summary>
        protected bool InternalConnection
        {
            get { return _internalConnection; }
        }

        public SqlCommand Command
        {
            get { return _command; }
        }

        public SqlConnection Connection
        {
            get { return _connection; }
        }

        private void Create(string commandText, SqlConnection connection, CommandType commandType,
                            bool internalConnection)
        {
            _connection = connection;
            _command = new SqlCommand(commandText, connection) {CommandType = commandType};
            _internalConnection = internalConnection;
        }

        public void Cancel()
        {
            _command.Cancel();
        }

        //////////////////////////
        // Object functions
        //////////////////////////

        //////////////////////////
        // Exact numerics
        //////////////////////////

        //////////////////////////
        // Approximate numerics
        //////////////////////////

        //////////////////////////
        // Character Strings
        //////////////////////////

        //////////////////////////
        // Unicode Character Strings
        //////////////////////////

        //////////////////////////
        // Binary Strings
        //////////////////////////

        //////////////////////////
        // Other Data Types
        //////////////////////////

        //////////////////////////
        // XML
        //////////////////////////

        //////////////////////////
        //Implementaiton Details
        //////////////////////////

        /// <summary>
        /// Appends a parameter of type SQL Server return value.
        /// </summary>
        /// <returns></returns>
        public SqlParameter AppendReturnValue()
        {
            return AppendInt(@"RETURN_VALUE", 0, ParameterDirection.ReturnValue);
        }

        #region Command Execution

        /// <summary>
        /// Executes the given xml command and returns a reference to a XmlReader.
        /// </summary>
        /// <returns>A reference to the new <see cref="XmlReader"/> object.</returns>
        /// <remarks>This method does not support a managed connection.</remarks>
        public XmlReader ExecuteXmlReader()
        {
            if (InternalConnection) // Managed Connection
                throw new InvalidOperationException("Managed connections don't work with this command.");

            return Command.ExecuteXmlReader();
        }

        public string ExecuteXmlString(string rootLocalName)
        {
            return ExecuteXmlString(null, rootLocalName, null);
        }

        public string ExecuteXmlString(string rootLocalName, string rootNs)
        {
            return ExecuteXmlString(null, rootLocalName, rootNs);
        }

        /// <summary>
        /// Executes the given xml command and returns the result as an xml string.
        /// </summary>
        /// <param name="rootPrefix">The namespace prefix of the root element.</param>
        /// <param name="rootLocalName">The local name of the root element.</param>
        /// <param name="rootNs">The namespace URI to associate with the root element.</param>
        /// <returns>The resulting xml.</returns>
        public string ExecuteXmlString(string rootPrefix, string rootLocalName, string rootNs)
        {
            var xmlWriter = new XmlStringWriter(WriterMode.Managed);
            xmlWriter.XmlWriter.WriteStartElement(rootPrefix, rootLocalName, rootNs);

            ExecuteXmlWriter(xmlWriter.XmlWriter);

            xmlWriter.XmlWriter.WriteEndElement();
            return xmlWriter.XmlString;
        }

        public string ExecuteXmlString()
        {
            var xmlWriter = new XmlStringWriter(WriterMode.Managed);
            ExecuteXmlWriter(xmlWriter.XmlWriter);
            return xmlWriter.XmlString;
        }

        public void ExecuteXmlWriter(XmlWriter writer)
        {
            if (InternalConnection) // Managed Connection
            {
                using (Connection)
                {
                    Connection.Open();
                    InternalXmlExecute(writer);
                }
            }
            else
            {
                InternalXmlExecute(writer);
            }
        }

        private void InternalXmlExecute(XmlWriter xmlWriter)
        {
            XmlReader xmlReader = null;
            try
            {
                xmlReader = Command.ExecuteXmlReader();
                xmlWriter.WriteNode(xmlReader, false);
            }
            finally
            {
                if (xmlReader != null)
                    xmlReader.Close();
            }
        }

        public object ExecuteScalar()
        {
            if (InternalConnection) // Managed Connection
            {
                using (Connection)
                {
                    Connection.Open();
                    return Command.ExecuteScalar();
                }
            }
            else
            {
                return Command.ExecuteScalar();
            }
        }

        public SqlDataReader ExecuteReader(CommandBehavior commandBehavior)
        {
            if (InternalConnection) // Managed Connection
            {
                Connection.Open();
                return Command.ExecuteReader(commandBehavior | CommandBehavior.CloseConnection);
            }
            else
            {
                return Command.ExecuteReader(commandBehavior);
            }
        }

        public SqlDataReader ExecuteReader()
        {
            return ExecuteReader(CommandBehavior.Default);
        }

        #endregion

        #region AppendObject

        /// <summary>
        /// Appends a parameter of type <see cref="object"/>.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="paramValue">The value of the parameter.</param>
        /// <param name="precision">The maximum number of digits used to represent the parameter.</param>
        /// <param name="scale">The number of decimal places to which the parameter is resolved.</param>
        /// <param name="size">The width of the parameter.</param>
        /// <param name="offset">The offset to the parameter.</param>
        /// <param name="nullable">The nullability of the parameter.</param>
        /// <param name="direction">One of the <see cref="ParameterDirection"/> values.</param>
        /// <param name="dbType">One of the <see cref="SqlDbType"/> values.</param>
        /// <returns>A reference to the new <see cref="SqlParameter"/> object.</returns>
        public SqlParameter AppendObject(string name, object paramValue, byte precision, byte scale, int size,
                                         int offset, bool nullable, ParameterDirection direction, SqlDbType dbType)
        {
            SqlParameter param = _command.Parameters.Add(name, dbType, size);
            param.Precision = precision;
            param.Scale = scale;
            param.Offset = offset;
            param.IsNullable = nullable;
            param.Direction = direction;
            param.Value = paramValue;
            return param;
        }

        public SqlParameter AppendObject(string name, object paramValue, byte precision, byte scale, int size,
                                         ParameterDirection direction, SqlDbType dbType)
        {
            SqlParameter param = _command.Parameters.Add(name, dbType, size);
            param.Value = paramValue;
            param.Precision = precision;
            param.Scale = scale;
            param.Direction = direction;
            return param;
        }

        public SqlParameter AppendObject(string name, object paramValue, byte precision, byte scale,
                                         ParameterDirection direction, SqlDbType dbType)
        {
            SqlParameter param = _command.Parameters.Add(name, dbType);
            param.Value = paramValue;
            param.Precision = precision;
            param.Scale = scale;
            param.Direction = direction;
            return param;
        }

        public SqlParameter AppendObject(string name, object paramValue, int size, ParameterDirection direction,
                                         SqlDbType dbType)
        {
            SqlParameter param = _command.Parameters.Add(name, dbType, size);
            param.Value = paramValue;
            param.Direction = direction;
            return param;
        }

        public SqlParameter AppendObject(string name, object paramValue, ParameterDirection direction, SqlDbType dbType)
        {
            SqlParameter param = _command.Parameters.Add(name, dbType);
            param.Value = paramValue;
            param.Direction = direction;
            return param;
        }

        public SqlParameter AppendObject(string name, byte precision, byte scale, int size, SqlDbType dbType)
        {
            SqlParameter param = _command.Parameters.Add(name, dbType);
            param.Precision = precision;
            param.Scale = scale;
            param.Size = size;
            return param;
        }

        #endregion // AppendObject

        #region AppendBigInt

        public SqlParameter AppendBigInt(string name, Int64 paramValue, ParameterDirection direction)
        {
            return AppendObject(name, paramValue, 19, 0, 8, direction, SqlDbType.BigInt);
        }

        public SqlParameter AppendBigInt(string name)
        {
            return AppendObject(name, 19, 0, 8, SqlDbType.BigInt);
        }

        public void AppendBigInt(string name, Int64 paramValue)
        {
            AppendBigInt(name, paramValue, ParameterDirection.Input);
        }

        #endregion

        #region AppendInt

        public SqlParameter AppendInt(string name, int paramValue, ParameterDirection direction)
        {
            return AppendObject(name, paramValue, 10, 0, 4, direction, SqlDbType.Int);
        }

        public SqlParameter AppendInt(string name)
        {
            return AppendObject(name, null, 10, 0, 4, ParameterDirection.Output, SqlDbType.Int);
        }

        public void AppendInt(string name, int paramValue)
        {
            AppendInt(name, paramValue, ParameterDirection.Input);
        }

        #endregion

        #region AppendSmallInt

        private SqlParameter AppendSmallInt(string name, short paramValue, ParameterDirection direction)
        {
            return AppendObject(name, paramValue, 5, 0, 2, direction, SqlDbType.SmallInt);
        }

        public SqlParameter AppendSmallInt(string name)
        {
            return AppendObject(name, null, 5, 0, 2, ParameterDirection.Output, SqlDbType.SmallInt);
        }

        public void AppendSmallInt(string name, short paramValue)
        {
            AppendSmallInt(name, paramValue, ParameterDirection.Input);
        }

        #endregion //AppendSmallInt

        #region AppendTinyInt

        public SqlParameter AppendTinyInt(string name, byte paramValue, ParameterDirection direction)
        {
            return AppendObject(name, paramValue, 3, 0, 1, direction, SqlDbType.TinyInt);
        }

        public SqlParameter AppendTinyInt(string name)
        {
            return AppendObject(name, null, 3, 0, 1, ParameterDirection.Output, SqlDbType.TinyInt);
        }

        public void AppendTinyInt(string name, byte paramValue)
        {
            AppendTinyInt(name, paramValue, ParameterDirection.Input);
        }

        #endregion // AppendTinyInt

        #region AppendBit

        public SqlParameter AppendBit(string name, bool paramValue, ParameterDirection direction)
        {
            return AppendObject(name, paramValue, 1, 0, 1, direction, SqlDbType.Bit);
        }

        public SqlParameter AppendBit(string name)
        {
            return AppendObject(name, null, 1, 0, 1, ParameterDirection.Output, SqlDbType.Bit);
        }

        public void AppendBit(string name, bool paramValue)
        {
            AppendBit(name, paramValue, ParameterDirection.Input);
        }

        #endregion

        #region AppendDecimal

        public SqlParameter AppendDecimal(string name, decimal paramValue, ParameterDirection direction)
        {
            SqlParameter param = _command.Parameters.Add(name, SqlDbType.Decimal);
            param.Value = paramValue;
            param.Direction = direction;
            return param;
        }

        public SqlParameter AppendDecimal(string name, decimal paramValue, byte precision, byte scale,
                                          ParameterDirection direction)
        {
            return AppendObject(name, paramValue, precision, scale, direction, SqlDbType.Decimal);
        }

        public SqlParameter AppendDecimal(string name)
        {
            return AppendDecimal(name, Decimal.Zero, ParameterDirection.Output);
        }

        public void AppendDecimal(string name, decimal paramValue)
        {
            AppendDecimal(name, paramValue, ParameterDirection.Input);
        }

        #endregion

        #region AppendMoney

        public SqlParameter AppendMoney(string name, decimal paramValue, ParameterDirection direction)
        {
            return AppendObject(name, paramValue, 19, 0, 8, direction, SqlDbType.Money);
        }

        #endregion AppendMoney

        #region AppendSmallMoney

        public SqlParameter AppendSmallMoney(string name, decimal paramValue, ParameterDirection direction)
        {
            return AppendObject(name, paramValue, 10, 0, 4, direction, SqlDbType.SmallMoney);
        }

        #endregion AppendSmallMoney

        #region AppendFloat

        public SqlParameter AppendFloat(string name, double paramValue, byte precision, ParameterDirection direction)
        {
            return AppendObject(name, paramValue, precision, 0, 8, direction, SqlDbType.Float);
        }

        public void AppendFloat(string name, double paramValue)
        {
            AppendObject(name, paramValue, 15, 0, 8, ParameterDirection.Input, SqlDbType.Float);
        }

        public SqlParameter AppendFloat(string name)
        {
            return AppendObject(name, null, 15, 0, 8, ParameterDirection.Output, SqlDbType.Float);
        }

        public SqlParameter AppendFloat(string name, double paramValue, ParameterDirection direction)
        {
            return AppendFloat(name, paramValue, 15, direction);
        }

        #endregion

        #region AppendReal

        public SqlParameter AppendReal(string name, float paramValue, byte precision, ParameterDirection direction)
        {
            return AppendObject(name, paramValue, precision, 0, 4, direction, SqlDbType.Real);
        }

        public SqlParameter AppendReal(string name, float paramValue, ParameterDirection direction)
        {
            return AppendObject(name, paramValue, 7, 0, 4, direction, SqlDbType.Real);
        }

        #endregion

        #region AppendDateTime

        public SqlParameter AppendDateTime(string name, DateTime paramValue, ParameterDirection direction)
        {
            return AppendObject(name, paramValue, 23, 0, 8, direction, SqlDbType.DateTime);
        }

        public void AppendDateTime(string name, DateTime paramValue)
        {
            AppendDateTime(name, paramValue, ParameterDirection.Input);
        }

        public SqlParameter AppendDateTime(string name)
        {
            return AppendObject(name, null, 23, 0, 8, ParameterDirection.Output, SqlDbType.DateTime);
        }

        #endregion // AppendDateTime

        #region AppendSmallDateTime

        public SqlParameter AppendSmallDateTime(string name, DateTime paramValue, ParameterDirection direction)
        {
            return AppendObject(name, paramValue, 16, 0, 4, direction, SqlDbType.SmallDateTime);
        }

        #endregion AppendSmallDateTime

        #region AppendChar

        public SqlParameter AppendChar(string name, string paramValue, int length, ParameterDirection direction)
        {
            return AppendObject(name, paramValue, length, direction, SqlDbType.Char);
        }

        public SqlParameter AppendChar(string name, int length)
        {
            return AppendChar(name, null, length, ParameterDirection.Output);
        }

        public void AppendChar(string name, string paramValue)
        {
            AppendChar(name, paramValue, (paramValue == null) ? 0 : paramValue.Length, ParameterDirection.Input);
        }

        #endregion

        #region AppendVarChar

        public SqlParameter AppendVarChar(string name, string paramValue, int length, ParameterDirection direction)
        {
            return AppendObject(name, paramValue, length, direction, SqlDbType.VarChar);
        }

        public SqlParameter AppendVarChar(string name, int length)
        {
            return AppendVarChar(name, null, length, ParameterDirection.Output);
        }

        public void AppendVarChar(string name, string paramValue)
        {
            AppendVarChar(name, paramValue, (paramValue == null) ? 0 : paramValue.Length, ParameterDirection.Input);
        }

        #endregion

        #region AppendText

        public SqlParameter AppendText(string name, string paramValue, int length, ParameterDirection direction)
        {
            return AppendObject(name, paramValue, length, direction, SqlDbType.Text);
        }

        public SqlParameter AppendText(string name, int length)
        {
            return AppendText(name, null, length, ParameterDirection.Output);
        }

        public void AppendText(string name, string paramValue)
        {
            AppendText(name, paramValue, (paramValue == null) ? 0 : paramValue.Length, ParameterDirection.Input);
        }

        #endregion

        #region AppendNChar

        public SqlParameter AppendNChar(string name, string paramValue, int length, ParameterDirection direction)
        {
            return AppendObject(name, paramValue, length, direction, SqlDbType.NChar);
        }

        public SqlParameter AppendNChar(string name, int length)
        {
            return AppendNChar(name, null, length, ParameterDirection.Output);
        }

        public void AppendNChar(string name, string paramValue)
        {
            AppendNChar(name, paramValue, (paramValue == null) ? 0 : paramValue.Length, ParameterDirection.Input);
        }

        #endregion

        #region AppendNVarChar

        public SqlParameter AppendSmallText(string name, string paramValue)
        {
            return AppendNVarChar(name, paramValue, 25, ParameterDirection.Input);
        }

        public SqlParameter AppendMediumText(string name, string paramValue)
        {
            return AppendNVarChar(name, paramValue, 255, ParameterDirection.Input);
        }

        public SqlParameter AppendLargeText(string name, string paramValue)
        {
            return AppendNVarChar(name, paramValue, 1000, ParameterDirection.Input);
        }

        public SqlParameter AppendXSmallText(string name, string paramValue)
        {
            return AppendNVarChar(name, paramValue, 10, ParameterDirection.Input);
        }

        public SqlParameter AppendXLargeText(string name, string paramValue)
        {
            return AppendNVarChar(name, paramValue, paramValue?.Length ?? 0, ParameterDirection.Input);
        }

        private SqlParameter AppendNVarChar(string name, string paramValue, int length, ParameterDirection direction)
        {
            return AppendObject(name, paramValue, length, direction, SqlDbType.NVarChar);
        }

        private SqlParameter AppendNVarChar(string name, int length)
        {
            return AppendNVarChar(name, null, length, ParameterDirection.Output);
        }

        private void AppendNVarChar(string name, string paramValue)
        {
            AppendNVarChar(name, paramValue, paramValue == null ? 0 : paramValue.Length, ParameterDirection.Input);
        }

        #endregion

        #region AppendNText

        public SqlParameter AppendNText(string name, string paramValue, int length, ParameterDirection direction)
        {
            return AppendObject(name, paramValue, length, direction, SqlDbType.NText);
        }

        public SqlParameter AppendNText(string name, int length)
        {
            return AppendNText(name, null, length, ParameterDirection.Output);
        }

        public void AppendNText(string name, string paramValue)
        {
            AppendNText(name, paramValue, (paramValue == null) ? 0 : paramValue.Length, ParameterDirection.Input);
        }

        #endregion

        #region AppendBinary

        public SqlParameter AppendBinary(string name, byte[] image, int size, ParameterDirection direction)
        {
            return AppendObject(name, image, size, direction, SqlDbType.Binary);
        }

        public SqlParameter AppendBinary(string name, byte[] image)
        {
            return AppendObject(name, image, image.Length, ParameterDirection.Input, SqlDbType.Binary);
        }

        #endregion // AppendBinary

        #region AppendVarBinary

        public SqlParameter AppendVarBinary(string name, byte[] image, int size, ParameterDirection direction)
        {
            return AppendObject(name, image, size, direction, SqlDbType.VarBinary);
        }

        public void AppendVarBinary(string name, byte[] image)
        {
            AppendVarBinary(name, image, image.Length, ParameterDirection.Input);
        }

        public SqlParameter AppendVarBinary(string name)
        {
            return AppendVarBinary(name, null, -1, ParameterDirection.Output);
        }

        #endregion // AppendVarBinary

        #region AppendImage

        public SqlParameter AppendImage(string name, byte[] image, ParameterDirection direction)
        {
            return AppendObject(name, image, image.Length, direction, SqlDbType.Image);
        }

        public void AppendImage(string name, byte[] image)
        {
            AppendImage(name, image, ParameterDirection.Input);
        }

        public SqlParameter AppendImage(string name)
        {
            return AppendObject(name, null, ParameterDirection.Output, SqlDbType.Image);
        }

        #endregion

        #region AppendGuid

        public SqlParameter AppendGuid(string name, Guid paramValue, ParameterDirection direction)
        {
            return AppendObject(name, paramValue, direction, SqlDbType.UniqueIdentifier);
        }

        public SqlParameter AppendGuid(string name, string paramValue, ParameterDirection direction)
        {
            return AppendObject(name, new Guid(paramValue), direction, SqlDbType.UniqueIdentifier);
        }

        public void AppendGuid(string name, Guid paramValue)
        {
            AppendGuid(name, paramValue, ParameterDirection.Input);
        }

        public SqlParameter AppendGuid(string name)
        {
            return AppendGuid(name, Guid.Empty, ParameterDirection.Output);
        }

        #endregion

        #region AppendXml

        public SqlParameter AppendXml(string name, XmlReader paramValue)
        {
            return AppendObject(name, new SqlXml(paramValue), ParameterDirection.Input, SqlDbType.Xml);
        }

        public SqlParameter AppendXml(string name, string xmlString)
        {
            using (XmlReader reader = XmlReader.Create(new StringReader(xmlString)))
            {
                return AppendObject(name, new SqlXml(reader), ParameterDirection.Input, SqlDbType.Xml);
            }
        }

        public SqlParameter AppendXml(string name, SqlXml paramValue, ParameterDirection direction)
        {
            return AppendObject(name, paramValue, direction, SqlDbType.Xml);
        }

        public SqlParameter AppendXml(string name)
        {
            return AppendObject(name, null, ParameterDirection.Output, SqlDbType.Xml);
        }

        #endregion // AppendXml

        public string OutputParameterValue(string parameterName)
        {
           return _command.Parameters[parameterName].Value.ToString();
        }
    }
}