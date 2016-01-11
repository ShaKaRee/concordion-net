using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nu.xom
{
    public class XPathContext
    {
        public string Prefix { get; set; }

        public string Uri { get; set; }

        public XPathContext(String prefix, String uri)
        {
            this.Prefix = prefix;
            this.Uri = uri;
        }
    }
}
