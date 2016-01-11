using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace nu.xom
{
    public class Attribute : Node
    {
        public XAttribute XAttribute { private set; get; }

        public Attribute(string localName, string value)
        {
            this.XAttribute = new XAttribute(localName, value);
        }

        public Attribute(XAttribute xAttribute)
        {
            this.XAttribute = xAttribute;
        }

        public string getNamespaceURI()
        {
            return this.XAttribute.Name.NamespaceName;
        }

        public string getLocalName()
        {
            return this.XAttribute.Name.LocalName;
        }

        public string getValue()
        {
            return this.XAttribute.Value;
        }
    }
}
