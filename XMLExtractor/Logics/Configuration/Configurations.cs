using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Logics.Configuration
{
    public class Configurations
    {
        private Dictionary<string, string> _appsettings = null;
        private readonly IdentifierSettings _identifierSettings;
        public Configurations()
        {
           _identifierSettings = ((IdentifiersSection)ConfigurationManager.GetSection("Identifiers")).Settings;
            _appsettings = ConfigurationManager.AppSettings.AllKeys.ToDictionary(k => k, k => ConfigurationManager.AppSettings[k]);
        }

        Dictionary<string,string> Appsettings 
        {
            get
            {
                if (_appsettings == null)
                {
                    _appsettings = ConfigurationManager.AppSettings.AllKeys.ToDictionary(k => k, k => ConfigurationManager.AppSettings[k]);
                }
                return _appsettings;
            }
        }

        public int NumberOfIdentifiers 
        { 
            get 
            {
                return _identifierSettings.Count;
            }
        }

        public List<string> IdentifiersConfigured 
        {
            get
            {
                var list = new List<string>();
                foreach (IdentifierSetting item in _identifierSettings)
                {
                    list.Add(item.Name);
                }
                return list;
            }
        }
        public IdentifierSettings IdentifierSettings { get => _identifierSettings; }

    }
}
