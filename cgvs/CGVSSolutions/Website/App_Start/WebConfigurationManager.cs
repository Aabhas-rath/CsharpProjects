using System;
using System.Configuration;

namespace Website
{
    public sealed class WebConfigurationManager
    {
        private string constr = string.Empty;
        public string ConnectionString
        {
            get 
            {
                if (string.IsNullOrEmpty(constr))
                {
                    constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                }
                return constr;
            }
            set
            {
                    constr = value;
                    if (string.IsNullOrEmpty(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString))
                    {
                        ConfigurationManager.ConnectionStrings.Add(new ConnectionStringSettings("DBCS", value));
                    }
                    else
                        ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString = value;
            }
        }
        private static readonly Lazy<WebConfigurationManager> lazy = new Lazy<WebConfigurationManager>(() => new WebConfigurationManager());

        public static WebConfigurationManager Instance { get { return lazy.Value; } }

        private WebConfigurationManager()
        {
        }
    }
}