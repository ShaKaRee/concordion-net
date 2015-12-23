using System;
using System.Collections.Generic;
using System.Linq;
using Concordion.Spec.Support;
using Concordion.Integration;
using org.concordion.api;
using org.concordion.api.listener;
using org.concordion.@internal.listener;

namespace Concordion.Spec.Concordion.Results.Exception
{
    [ConcordionTest]
    public class ExceptionTest
    {
        private List<string> stackTraceElements = new List<string>();

        public void addStackTraceElement(string declaringClassName, string methodName, string filename, int lineNumber)
        {
            stackTraceElements.Add(String.Format("at {0}.{1} in {2}:line {3}", declaringClassName, methodName, filename, lineNumber));
        }

        public string markAsException(string fragment, string expression, string errorMessage)
        {
            var exception = new StackTraceSettingException(errorMessage);
            exception.StackTraceElements.AddRange(stackTraceElements);

            var document = new TestRig()
                                .ProcessFragment(fragment)
                                .GetDocument();
            var xmlDocument = document.toXML();

            var element = new Element((nu.xom.Element) document.query("//p").get(0));
            var xmlSTringBeforeException = element.toXML();

            new ThrowableRenderer().throwableCaught(new ThrowableCaughtEvent(exception, element, expression));

            var xmlString = element.toXML();
            return xmlString;
        }
    }
}
