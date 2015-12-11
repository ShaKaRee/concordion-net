using System;
using System.Collections.Generic;
using System.Linq;
using java.io;
using NUnit.Framework;
using Moq;
using org.concordion.api;
using org.concordion.@internal;

namespace Concordion.Test.Integration
{
    [TestFixture]
    public class FileTarget_Fixture
    {
        [Test]
        public void Test_Can_Get_File_Path_Successfully()
        {
            var resource = new Mock<Resource>("blah\\blah.txt");
            resource.Expect(x => x.getPath()).Returns("blah\\blah.txt");

            var target = new FileTarget(new File(@"c:\temp"));

            Assert.AreEqual(@"c:\temp\blah\blah.txt", target.resolvedPathFor(resource.Object));
        }
    }
}
