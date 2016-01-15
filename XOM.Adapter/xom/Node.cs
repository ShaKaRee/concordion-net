using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace nu.xom
{
    public abstract class Node
    {
        private readonly XNode m_XNode;

        protected Node(XNode xNode)
        {
            this.m_XNode = xNode;
        }

        public void detach()
        {
            this.m_XNode.Remove();
        }

        public Document getDocument()
        {
            return new Document(this.m_XNode.Document);
        }

        public Nodes query(string xPath)
        {
            return query(xPath, null);
        }

        public Nodes query(string xPath, XPathContext namespaces)
        {
            IList<XElement> descendantElements = new List<XElement>();
            if (!(this.m_XNode is XContainer)) return new Nodes(descendantElements);

            var xContainer = this.m_XNode as XContainer;
            var name = GetName(xPath);
            foreach (var element in xContainer.Descendants())
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
            if (xPath.LastIndexOf(":") != -1)
            {
                return xPath.Substring(xPath.LastIndexOf(":") + 1);
            }
            else
            {
                return xPath.Substring(xPath.LastIndexOf("/") + 1);
            }
        }

        public string toXML()
        {
            return this.m_XNode.ToString(SaveOptions.None);
        }

        public bool @equals(object obj)
        {
            if (obj == null) return false;
            if (this == obj) return true;

            var node = obj as Node;
            if (node == null) return false;

            return this.m_XNode.Equals(node.m_XNode);
        }

        public int hashCode()
        {
            return this.m_XNode.GetHashCode();
        }
    }
}
