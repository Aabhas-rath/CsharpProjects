using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logics.Configuration
{
    public class IdentifierSetting : ConfigurationElement
    {
        [ConfigurationProperty("Name",DefaultValue = "",IsKey =true,IsRequired =true)]
        public string Name 
        {
            get
            {
                return (string)this["Name"];
            }
            set
            {
                this["Name"] = value;
            }
        }
        [ConfigurationProperty("XPath",IsRequired =true,DefaultValue = "")]
        public string XPath
        {
            get
            {
                return (string)this["XPath"];
            }
            set
            {
                this["XPath"] = value;
            }
        }
        [ConfigurationProperty("IdentifierInAttribute", DefaultValue = false)]
        public bool IdentifierInAttribute
        {
            get
            {
                return (bool)this["IdentifierInAttribute"];
            }
            set
            {
                this["IdentifierInAttribute"] = value;
            }
        }
        [ConfigurationProperty("AttributeName", DefaultValue = "")]
        public string AttributeName
        {
            get
            {
                return (string)this["AttributeName"];
            }
            set
            {
                this["AttributeName"] = value;
            }
        }
    }
}
