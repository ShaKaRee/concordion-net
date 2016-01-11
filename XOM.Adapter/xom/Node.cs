using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace nu.xom
{
    public abstract class Node : java.lang.Object
    {
        protected XElement m_XElement;

        public void detach()
        {
            this.m_XElement.Remove();
        }

        public Document getDocument()
        {
            return new Document(this.m_XElement.Document);
        }

        public Nodes query(string xPath)
        {
            throw new NotSupportedException("not supported on xom adapter");
        }

        public Nodes query(string xPath, XPathContext namespaces)
        {
            IList<XElement> descendantElements = new List<XElement>();
            var name = GetName(xPath);
            foreach (XElement element in this.m_XElement.Descendants())
            {
                if (element.Name.LocalName.Equals(name))
                {
                    descendantElements.Add(element);
                }
            }
            return new Nodes(descendantElements);

            //ToDo: remove obsolete code
            //var namespaceManager = new XmlNamespaceManager(new NameTable());
            //namespaceManager.AddNamespace(namespaces.Prefix, namespaces.Uri);
            //var xPathSelectElements = this.m_XElement.XPathSelectElements(xPath, namespaceManager);
            //return new Nodes(xPathSelectElements);
        }

        private static string GetName(string xPath)
        {
            return xPath.Substring(xPath.LastIndexOf(":") + 1);
        }

        public string toXML()
        {
            return this.m_XElement.ToString(SaveOptions.None);
        }
    }
}
