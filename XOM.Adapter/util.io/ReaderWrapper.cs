using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using java.io;

namespace nu.util.io
{
    public class ReaderWrapper : TextReader
    {
        public InputStream InputStream { get; private set; }
        
        public ReaderWrapper(InputStream inputStream)
        {
            this.InputStream = inputStream;
        }

        public override int Read()
        {
            return InputStream.read();
        }

        public override int Read(char[] buffer, int index, int count)
        {
            var content = new byte[buffer.Count()];
            var result = InputStream.read(content, index, count);
            for (int i = 0; i < content.Count(); i++)
            {
                //buffer[i] = content[i];
            }
            return result;
        }

        public override void Close()
        {
            InputStream.close();
        }
    }
}
