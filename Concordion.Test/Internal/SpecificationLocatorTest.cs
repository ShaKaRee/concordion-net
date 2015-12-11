using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Concordion.Api;
using Concordion.Internal;
using Concordion.NET.Internal;
using NUnit.Framework;

namespace Concordion.Test.Internal
{
    [TestFixture]
    public class SpecificationLocatorTest
    {
        [Test]
        public void ShouldRemoveTestSuffixes()
        {
            var specificationLocator = new ClassNameBasedSpecificationLocator();
            var resource = specificationLocator.locateSpecification(this);
            var path = resource.getPath().Replace(Path.DirectorySeparatorChar, '/');
            Assert.AreEqual("Concordion/Test/Internal/SpecificationLocator.html", path, "path from SpecificationLocator contains 'Test'");
        }

        [Test]
        public void ShouldNotRemoreWordTestInBetween()
        {
            var specificationLocator = new ClassNameBasedSpecificationLocator();
            var resource = specificationLocator.locateSpecification(new DummyContainingTestInNameTest());
            var path = resource.getPath().Replace(Path.DirectorySeparatorChar, '/');
            Assert.AreEqual("Concordion/Test/Internal/DummyContainingTestInName.html", path, "path from SpecificiationLocator removed 'Test' in the middle");
        }
    }
}
