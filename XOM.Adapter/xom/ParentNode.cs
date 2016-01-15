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
        private XContainer m_XContainer;

        public ParentNode(XContainer xNode)
            : base(xNode)
        {
            this.m_XContainer = xNode;
        }

        public void insertChild(Node child, int position)
        {
            if (position != 0) throw new NotSupportedException("insert only supported on position 0");
            var childAsParentNode = child as ParentNode;
            if (childAsParentNode != null)
            {
                this.m_XContainer.AddFirst(childAsParentNode.m_XContainer);
            }
        }

        public void appendChild(Node child)
        {
            var childAsParentNode = child as ParentNode;
            if (childAsParentNode != null)
            {
                this.m_XContainer.Add(childAsParentNode.m_XContainer);
            }
            var childAsText = child as Text;
            if (childAsText != null)
            {
                this.m_XContainer.Add(childAsText.XText);
            }
        }

        public int getChildCount()
        {
            return this.m_XContainer.Nodes().Count();
        }

        public Node getChild(int position)
        {
            var result = this.m_XContainer.Nodes().ElementAt(position);
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
