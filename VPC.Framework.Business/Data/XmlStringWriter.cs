using System.IO;
using System.Xml;

namespace VPC.Framework.Business.Data
{
    internal sealed class XmlStringWriter
    {
        private readonly StringWriter _stringWriter;
        private readonly WriterMode _writerMode;
        private readonly XmlWriter _xmlWriter;
        private string _xmlString;

        public XmlStringWriter(WriterMode writerMode)
            : this(new StringWriter(), writerMode)
        {
        }

        public XmlStringWriter(StringWriter stringWriter, WriterMode writerMode)
        {
            _stringWriter = stringWriter;
            _xmlWriter = new XmlTextWriter(_stringWriter);
            _writerMode = writerMode;

            if (_writerMode == WriterMode.Managed)
                _xmlWriter.WriteStartDocument();
        }

        public XmlWriter XmlWriter
        {
            get { return _xmlWriter; }
        }


        public string XmlString
        {
            get
            {
                if (_xmlString == null)
                {
                    Close();
                    _xmlString = _stringWriter.ToString();
                }
                return _xmlString;
            }
        }

        public WriterMode Mode
        {
            get { return _writerMode; }
        }

        public static XmlStringWriter Create(WriterMode writerMode)
        {
            return new XmlStringWriter(writerMode);
        }

        public void Close()
        {
            if (_xmlWriter.WriteState != WriteState.Closed)
            {
                if (_writerMode == WriterMode.Managed)
                    _xmlWriter.WriteEndDocument();

                _xmlWriter.Close();
            }
            _stringWriter.Close();
        }
    }

    public enum WriterMode
    {
        Basic = 1,
        Managed = 2
    }
}