using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ikvm.io;
using java.io;

namespace Concordion.NET.IO
{
    public class StreamWrapper : InputStream
    {
        public Stream Stream { private set; get; }

        private InputStreamWrapper m_InputStreamWrapper;

        public StreamWrapper(Stream stream)
        {
            this.Stream = stream;
            this.m_InputStreamWrapper = new InputStreamWrapper(stream);
        }

        public override int available()
        {
            return this.m_InputStreamWrapper.available();
        }

        public override void close()
        {
            this.m_InputStreamWrapper.close();
        }

        public override void mark(int readlimit)
        {
            this.m_InputStreamWrapper.mark(readlimit);
        }

        public override bool markSupported()
        {
            return this.m_InputStreamWrapper.markSupported();
        }

        public override int read()
        {
            return this.m_InputStreamWrapper.read();
        }

        public override int read(byte[] b)
        {
            return this.m_InputStreamWrapper.read(b);
        }

        public override void reset()
        {
            this.m_InputStreamWrapper.reset();
        }

        public override long skip(long n)
        {
            return this.m_InputStreamWrapper.skip(n);
        }
    }
}
