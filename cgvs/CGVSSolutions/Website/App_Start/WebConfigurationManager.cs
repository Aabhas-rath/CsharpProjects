using System.Configuration;

namespace Website
{
    public class WebConfigurationManager
    {
        public string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString; }
            set
            {
                if (string.IsNullOrEmpty(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString))
                {
                    ConfigurationManager.ConnectionStrings.Add(new ConnectionStringSettings("DBCS", value));
                }
                else
                    ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString = value;
            }
        }
    }
}