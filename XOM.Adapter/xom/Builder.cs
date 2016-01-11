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
            TextReader textReader;
            if (inputStream is StreamWrapper)
            {
                var streamWrapper = inputStream as StreamWrapper;
                textReader = new StreamReader(streamWrapper.Stream);
            }
            else
            {
                var stringBuilder = new StringBuilder();
                var availableBytes = 0;
                do
                {
                    availableBytes = inputStream.available();
                    var bytes = new byte[availableBytes];
                    inputStream.read(bytes, 0, availableBytes);
                    stringBuilder.Append(bytes);
                } while (availableBytes > 0);
                var xmlString = stringBuilder.ToString();
                textReader = new StringReader(xmlString);
            }

            var xDocument = XDocument.Load(textReader);
            inputStream.close();
            return new Document(xDocument);
        }
    }
}
