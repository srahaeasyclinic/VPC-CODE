using System;
using System.Data;

namespace VPC.Framework.Business.Data
{
    /// <summary>
    /// <see cref="IDataReader"/> wrapper class that helps manage null fields.
    /// </summary>
    internal sealed class NullDataReader
    {
        private readonly IDataReader _reader;

        public NullDataReader(IDataReader reader)
        {
            _reader = reader;
        }

        public IDataReader Reader
        {
            get { return _reader; }
        }

        public bool GetBoolean(int ordinal, bool defaultValue)
        {
            return _reader.IsDBNull(ordinal) ? defaultValue : _reader.GetBoolean(ordinal);
        }

        public DateTime GetDateTime(int ordinal, DateTime defaultValue, bool isUtc)
        {
            return _reader.IsDBNull(ordinal)
                       ? defaultValue
                       : DateTime.SpecifyKind(_reader.GetDateTime(ordinal),
                                              (isUtc ? DateTimeKind.Utc : DateTimeKind.Unspecified));
        }

        public decimal GetDecimal(int ordinal, decimal defaultValue)
        {
            return _reader.IsDBNull(ordinal) ? defaultValue : _reader.GetDecimal(ordinal);
        }

        public decimal GetDecimal(int ordinal)
        {
            return GetDecimal(ordinal, 0);
        }

        public double GetDouble(int ordinal, double defaultValue)
        {
            return _reader.IsDBNull(ordinal) ? defaultValue : _reader.GetDouble(ordinal);
        }

        public double GetDouble(int ordinal)
        {
            return GetDouble(ordinal, 0);
        }

        public float GetFloat(int ordinal, float defaultValue)
        {
            return _reader.IsDBNull(ordinal) ? defaultValue : _reader.GetFloat(ordinal);
        }

        public float GetFloat(int ordinal)
        {
            return GetFloat(ordinal, 0);
        }

        public Guid GetGuid(int ordinal, Guid defaultValue)
        {
            return _reader.IsDBNull(ordinal) ? defaultValue : _reader.GetGuid(ordinal);
        }

        public Guid? GetGuidNullable(int ordinal, Guid? defaultValue)
        {
            return _reader.IsDBNull(ordinal) ? defaultValue : _reader.GetGuid(ordinal);
        }

        public Guid GetGuid(int ordinal)
        {
            return GetGuid(ordinal, Guid.Empty);
        }

        public byte GetByte(int ordinal, byte defaultValue)
        {
            return _reader.IsDBNull(ordinal) ? defaultValue : _reader.GetByte(ordinal);
        }

        public byte GetByte(int ordinal)
        {
            return GetByte(ordinal, 0);
        }

        public Int16 GetInt16(int ordinal, Int16 defaultValue)
        {
            return _reader.IsDBNull(ordinal) ? defaultValue : _reader.GetInt16(ordinal);
        }

        public Int16 GetInt16(int ordinal)
        {
            return GetInt16(ordinal, 0);
        }

        public int GetInt32(int ordinal, int defaultValue)
        {
            return _reader.IsDBNull(ordinal) ? defaultValue : _reader.GetInt32(ordinal);
        }

        public int GetInt32(int ordinal)
        {
            return GetInt32(ordinal, 0);
        }

        public Int64 GetInt64(int ordinal, Int64 defaultValue)
        {
            return _reader.IsDBNull(ordinal) ? defaultValue : _reader.GetInt64(ordinal);
        }

        public Int64 GetInt64(int ordinal)
        {
            return GetInt64(ordinal, 0);
        }

        public string GetString(int ordinal, string defaultValue)
        {
            return _reader.IsDBNull(ordinal) ? defaultValue : _reader.GetString(ordinal);
        }

        public string GetString(int ordinal)
        {
            return GetString(ordinal, String.Empty);
        }
    }
}