using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

namespace nu.xom
{
    public class Document : Node
    {
        private XDocument m_Document;

        public Document(XDocument document)
        {
            this.m_Document = document;
        }

        public Element getRootElement()
        {
            var rootElement = this.m_Document.Root;
            return new Element(rootElement);
        }

        public Nodes query(string xpath)
        {
            return new Nodes(this.m_Document.XPathSelectElements(xpath));
        }
    }
}
