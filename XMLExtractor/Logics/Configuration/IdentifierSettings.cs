using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logics.Configuration
{
    [ConfigurationCollection(typeof(IdentifierSetting))]
    public class IdentifierSettings : ConfigurationElementCollection
    {
        public IdentifierSetting this[int index]
        {
            get { return (IdentifierSetting)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                    BaseRemoveAt(index);
                BaseAdd(index, value);
            }
        }
        new public IdentifierSetting this[string Key]
        {
            get { return BaseGet(Key) as IdentifierSetting; }
            set
            {
                if (BaseGet(Key) != null)
                    BaseRemove(Key);
                BaseAdd(value);
            }
        }
        public override ConfigurationElementCollectionType CollectionType => ConfigurationElementCollectionType.BasicMap;
        protected override string ElementName => "IdentifierSetting";
        protected override ConfigurationElement CreateNewElement() => new IdentifierSetting();
        protected override object GetElementKey(ConfigurationElement element) => (element as IdentifierSetting).Name;
    }
}
