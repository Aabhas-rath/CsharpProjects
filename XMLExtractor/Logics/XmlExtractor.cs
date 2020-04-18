using Logics.Configuration;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Logics
{
    public class XmlExtractor : IExtractor, IDisposable
    {
        private FileInfo xmlfile;
        private Configurations config;

        public XmlExtractor(FileInfo xmlfile, Configurations config)
        {
            this.xmlfile = xmlfile;
            this.config = config;
        }

        private List<string> LoadIdentifiers(XmlDocument document, string xPath, bool loadFromAttribute = false, string attributeName = "")
        {
            List<string> returnList = new List<string>();
            if (loadFromAttribute)
                foreach (XmlNode node in document.SelectNodes(xPath))
                {
                    returnList.Add(node.Attributes[attributeName].Value);
                }
            else
                foreach (XmlNode node in document.SelectNodes(xPath))
                {
                    returnList.Add(node.InnerText);
                }
            return returnList;
        }

        private Dictionary<string, List<string>> ExtractIdentifers(FileInfo xmlfile, Configurations config)
        {
            Dictionary<string, List<string>> returnList = new Dictionary<string, List<string>>();
            using (XmlTextReader reader = new XmlTextReader(xmlfile.FullName))
            {
                XmlDocument document = new XmlDocument();
                document.Load(reader);
                foreach (IdentifierSetting setting in config.IdentifierSettings)
                {
                    returnList.Add(setting.Name, LoadIdentifiers(document, setting.XPath, setting.IdentifierInAttribute, setting.AttributeName));
                }
            }
            return returnList;
        }

        public Dictionary<string, List<string>> Extract()
        {
            return ExtractIdentifers(xmlfile, config);
        }

        public void Dispose()
        {
            if (config != null) 
                config = null;
            if (xmlfile != null)
                xmlfile = null;
        }
    }
}
