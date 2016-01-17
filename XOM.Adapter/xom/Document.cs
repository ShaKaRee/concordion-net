﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

namespace nu.xom
{
    public class Document : Node
    {
        private readonly XDocument m_XDocument;

        public Document(XDocument xDocument) : base(xDocument)
        {
            this.m_XDocument = xDocument;
        }

        public Document(Element element) : this(new XDocument(element.XElement))
        { }

        public Element getRootElement()
        {
            var rootElement = this.m_XDocument.Root;
            return new Element(rootElement);
        }
    }
}
