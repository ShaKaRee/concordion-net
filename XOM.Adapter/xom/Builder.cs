using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Concordion.NET.IO;
using ikvm.extensions;
using java.io;
using java.lang;
using StringBuilder = System.Text.StringBuilder;
using StringReader = System.IO.StringReader;

namespace nu.xom
{
    public class Builder
    {
        public Document build(InputStream inputStream)
        {
            try
            {
                TextReader textReader;
                if (inputStream is StreamWrapper)
                {
                    //ToDo: simplify - use always nu.xom.uti.io.StreamWrapper
                    //var streamWrapper = inputStream as StreamWrapper;
                    //textReader = new StreamReader(streamWrapper.Stream);
                    var streamWrapper = new util.io.StreamWrapper(inputStream);
                    textReader = new StreamReader(streamWrapper);
                }
                else
                {
                    var streamWrapper = new util.io.StreamWrapper(inputStream);
                    textReader = new StreamReader(streamWrapper);
                }

                var xDocument = XDocument.Load(textReader);
                inputStream.close();
                return new Document(xDocument);
            }
            catch (System.Exception exception)
            {
                throw new ParsingException(exception);
            }
        }
    }
}
