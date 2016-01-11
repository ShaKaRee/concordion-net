using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

namespace nu.xom
{
    public class Element : ParentNode
    {
        public Element(string name) : base(new XElement(name)) { }

        public Element(XElement xElement) : base(xElement) { }

        public string getValue()
        {
            return this.m_XElement.Value;
        }

        public void appendChild(string text)
        {
            this.m_XElement.Add(text);
        }

        public void appendChild(Text text)
        {
            var xText = text.XText;
            this.m_XElement.Add(xText);
        }

        public void removeChild(Node child)
        {
            throw new NotSupportedException("not supported on xom adapter");
        }

        public void addAttribute(Attribute attribute)
        {
            this.m_XElement.SetAttributeValue(attribute.XAttribute.Name, attribute.XAttribute.Value);
        }

        public string getAttributeValue(string name)
        {
            XAttribute attribute = this.m_XElement.Attribute(XName.Get(name));
            return (attribute != null) ? attribute.Value : null;
        }

        public string getAttributeValue(string localName, string namespaceURI)
        {
            XAttribute attribute = this.m_XElement.Attribute(XName.Get(localName, namespaceURI));
            return (attribute != null) ? attribute.Value : null;
        }

        public Attribute getAttribute(int index)
        {
            var xAttribute = this.m_XElement.Attributes().ElementAt(index);
            return new Attribute(xAttribute);
        }

        public Attribute getAttribute(string name)
        {
            return new Attribute(this.m_XElement.Attributes(XName.Get(name)).ElementAt(0));
        }

        public int getAttributeCount()
        {
            return this.m_XElement.Attributes().Count();
        }

        public Attribute removeAttribute(Attribute attribute)
        {
            throw new NotSupportedException("not supported on xom adapter");
        }

        public Document getDocument()
        {
            return new Document(this.m_XElement.Document);
        }

        public Element getFirstChildElement(string name)
        {
            foreach (XElement descendant in this.m_XElement.Descendants())
            {
                if (descendant.Name.LocalName == name)
                {
                    return new Element(descendant);
                }
            }
            return null;
        }

        public string getLocalName()
        {
            return this.m_XElement.Name.LocalName;
        }

        public ParentNode getParentNode()
        {
            return new Element(this.m_XElement.Parent);
        }

        public Elements getChildElements()
        {
            return new Elements(this.m_XElement.Elements());
        }

        public Elements getChildElements(string name)
        {
            List<XElement> result = new List<XElement>();
            var allChildElements = this.m_XElement.Elements();
            foreach (var childElement in allChildElements)
            {
                if (childElement.Name.LocalName.Equals(name) || 
                    childElement.Name.LocalName.Length == 0)
                {
                    result.Add(childElement);
                }
            }
            return new Elements(result);
        }
    }
}
