using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Concordion.Api.Listener;
using org.concordion.api;

namespace Concordion.Spec.Support
{
    public class ProcessingResult
    {
        private readonly ResultSummary resultSummary;
        private readonly EventRecorder eventRecorder;
        private readonly string documentXML;

        public long SuccessCount
        {
            get
            {
                return this.resultSummary.getSuccessCount();
            }
        }

        public long FailureCount
        {
            get
            {
                return this.resultSummary.getFailureCount();
            }
        }

        public long ExceptionCount
        {
            get
            {
                return this.resultSummary.getExceptionCount();
            }
        }

        public bool HasFailures
        {
            get
            {
                return this.FailureCount + this.ExceptionCount != 0;
            }
        }

        public bool IsSuccess
        {
            get
            {
                return !this.HasFailures;
            }
        }

        public ProcessingResult(ResultSummary resultSummary, EventRecorder eventRecorder, string documentXML) 
        {
            this.resultSummary = resultSummary;
            this.eventRecorder = eventRecorder;
            this.documentXML = documentXML;
        }

        public string SuccessOrFailureInWords()
        {
            return this.HasFailures ? "FAILURE" : "SUCCESS";
        }

        public XElement GetOutputFragment()
        {
            foreach (var descendant in this.GetXDocument().Root.Descendants("fragment"))
            {
                return descendant;
            }
            return null;
        }

        public string GetOutputFragmentXML()
        {
            var fragment = this.GetOutputFragment();
            var xmlFragmentBuilder = new StringBuilder();
            foreach (var child in fragment.Elements())
            {
                //xmlFragmentBuilder.Append(child.ToString(SaveOptions.DisableFormatting).Replace(" xmlns:concordion=\"http://www.concordion.org/2007/concordion\"", String.Empty));
                xmlFragmentBuilder.Append(child.ToString().Replace(" xmlns:concordion=\"http://www.concordion.org/2007/concordion\"", String.Empty));
            }

            return xmlFragmentBuilder.ToString();
        }

        public XDocument GetXDocument()
        {
            return XDocument.Parse(this.documentXML);
        }

        public AssertFailureEvent GetLastAssertEqualsFailureEvent()
        {
            return this.eventRecorder.GetLast(typeof(AssertFailureEvent)) as AssertFailureEvent;
        }

        public Element GetRootElement()
        {
            //ToDo
            //return new Element(this.GetXDocument().Root);
            return null;
        }

        public bool HasCssDeclaration(string cssFilename)
        {
            //ToDo
            //var head = this.GetRootElement().GetFirstChildElement("head");
            //return head.GetChildElements("link").Any(
            //    link =>
            //        string.Equals("text/css", link.GetAttributeValue("type")) &&
            //        string.Equals("stylesheet", link.GetAttributeValue("rel")) &&
            //        string.Equals(cssFilename, link.GetAttributeValue("href")));
            return false;
        }

        public bool HasEmbeddedCss(string css)
        {
            //ToDo
            //var head = this.GetRootElement().GetFirstChildElement("head");
            //return head.GetChildElements("style").Any(style => style.Text.Contains(css));
            return false;
        }

        public bool HasJavaScriptDeclaration(string cssFilename) {
            //ToDo
            //var head = this.GetRootElement().GetFirstChildElement("head");
            //return head.GetChildElements("script").Any(
            //    script => 
            //        string.Equals("text/javascript", script.GetAttributeValue("type")) && 
            //        string.Equals(cssFilename, script.GetAttributeValue("src")));
            return false;
        }

        public bool HasEmbeddedJavaScript(string javaScript) {
            //ToDo
            //var head = this.GetRootElement().GetFirstChildElement("head");
            //return head.GetChildElements("script").Any(
            //    script => 
            //        string.Equals("text/javascript", (string) script.GetAttributeValue("type")) && 
            //        script.Text.Contains(javaScript));
            return false;
        }
    }
}
