using System;
using System.IO;
using System.Text;
using System.Xml;

namespace VPC.Framework.Business.Configuration
{
    public sealed class SystemConfiguration
    {
        private const string CompanyName = "Crux";
        private const string ProductName = "Crux";
        private ApplicationEnvironment _appEnvironment;
        private string _configurationPath;

        public SystemConfiguration()
        {
            _appEnvironment = ApplicationEnvironment.Production;
        }

        private string ConfigurationPath
        {
            get
            {
                if (_configurationPath == null)
                {
                    string applicationFolder =Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonApplicationData),CompanyName);
                    _configurationPath = Path.Combine(applicationFolder, ProductName);
                }
                return _configurationPath;
            }
        }

        private string ConfigurationFilePath
        {
            get { return Path.Combine(ConfigurationPath, "SystemConfig.xml"); }
        }

        private static string NamespaceUri
        {
            get { return "http://crux/systemconfiguration"; }
        }

        public string DataStoreFolder { get; set; }

        public string SmtpServer { get; set; }

        public string ReportingServer { get; set; }

        public ApplicationEnvironment ApplicationEnvironment
        {
            get { return _appEnvironment; }
            set { _appEnvironment = value; }
        }

        public bool Load()
        {
            if (!Directory.Exists(ConfigurationPath))
                return false;

            if (!File.Exists(ConfigurationFilePath))
                return false;

            using (XmlReader reader = XmlReader.Create(ConfigurationFilePath))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.NamespaceURI == NamespaceUri)
                    {
                        switch (reader.LocalName)
                        {
                            case "DataStore":
                                DataStoreFolder = reader.ReadString();
                                break;
                            case "SmtpServer":
                                SmtpServer = reader.ReadString();
                                break;
                            case "ReportingServer":
                                ReportingServer = reader.ReadString();
                                break;
                            case "ApplicationEnvironment":
                                _appEnvironment =
                                    (ApplicationEnvironment)
                                    Enum.Parse(typeof (ApplicationEnvironment), reader.ReadString(), true);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            return true;
        }

        public void Save()
        {
            string applicationFolder = ConfigurationPath;
            if (!Directory.Exists(applicationFolder))
                Directory.CreateDirectory(applicationFolder);

            var writer = new XmlTextWriter(ConfigurationFilePath, Encoding.Unicode) {Formatting = Formatting.Indented};
            writer.WriteStartDocument();
            writer.WriteStartElement("Configuration", NamespaceUri);
            writer.WriteElementString("DataStore", NamespaceUri, DataStoreFolder);
            writer.WriteElementString("SmtpServer", NamespaceUri, SmtpServer);
            writer.WriteElementString("ReportingServer", NamespaceUri, ReportingServer);
            writer.WriteElementString("ApplicationEnvironment", NamespaceUri, ApplicationEnvironment.ToString());

            writer.WriteEndElement(); // Configuration
            writer.WriteEndDocument();
            writer.Close();
        }
    }
}