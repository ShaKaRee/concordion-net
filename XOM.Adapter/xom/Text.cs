using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace nu.xom
{
    public class Text : Node
    {
        public XText XText { private set; get; }

        public Text(string data)
        {
            this.XText = new XText(data);
        }

        public Text(XText xText)
        {
            this.XText = xText;
        }
    }
}
