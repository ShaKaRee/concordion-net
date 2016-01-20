using System.Xml.Linq;

namespace nu.xom
{
    public class Text : Node
    {
        public XText XText { private set; get; }

        public Text(string data) 
            : this(new XText(data))
        { }

        public Text(XText xText) 
            : base(xText)
        {
            this.XText = xText;
        }
    }
}
