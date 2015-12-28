﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Concordion.Test.Support;
using NUnit.Framework;
using org.concordion.api;
using org.concordion.@internal.listener;

namespace Concordion.Test.Listener
{
    [TestFixture]
    public class JavaScriptLinkerTest
    {
        private static readonly Resource NOT_NEEDED_PARAMETER = null;

        [Test]
        public void XmlOutputContainsAnExplicitEndTagForScriptElement()
        {
            var javaScriptLinker = new JavaScriptLinker(NOT_NEEDED_PARAMETER);

            var html = new XElement("html");
            var head = new XElement("head");
            html.Add(head);

            javaScriptLinker.beforeParsing(new XDocument(html));

            var expected = "<head><script type=\"text/javascript\"></script></head>";
            var actual = new HtmlUtil().RemoveWhitespaceBetweenTags(head.ToString());
            Assert.AreEqual(expected, actual);
        }
    }
}
