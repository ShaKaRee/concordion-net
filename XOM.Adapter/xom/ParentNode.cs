using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using sun.nio.ch;

namespace nu.xom
{
    public class ParentNode : Node
    {
        public ParentNode(XElement xElement)
        {
            this.m_XElement = xElement;
        }

        public void insertChild(Node child, int position)
        {
            if (position != 0) throw new NotSupportedException("insert only supported on position 0");
            var childAsParentNode = child as ParentNode;
            if (childAsParentNode != null)
            {
                this.m_XElement.AddFirst(childAsParentNode.m_XElement);
            }
        }

        public void appendChild(Node child)
        {
            var childAsParentNode = child as ParentNode;
            if (childAsParentNode != null)
            {
                this.m_XElement.Add(childAsParentNode.m_XElement);
            }
            var childAsText = child as Text;
            if (childAsText != null)
            {
                this.m_XElement.Add(childAsText.XText);
            }
        }

        public int getChildCount()
        {
            return this.m_XElement.Nodes().Count();
        }

        public Node getChild(int position)
        {
            var result = this.m_XElement.Nodes().ElementAt(position);
            if (result is XElement)
            {
                return new Element(result as XElement);
            }
            if (result is XText)
            {
                return new Text(result as XText);
            }
            return null;
        }
    }
}
