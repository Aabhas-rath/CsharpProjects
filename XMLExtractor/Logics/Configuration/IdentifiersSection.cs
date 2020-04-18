using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logics.Configuration
{
    public class IdentifiersSection : ConfigurationSection
    {
        [ConfigurationProperty("Settings", IsDefaultCollection = true)]
        public IdentifierSettings Settings 
        { 
            get 
            {
                return (IdentifierSettings)base["Settings"];
            } 
        }
    }
}
