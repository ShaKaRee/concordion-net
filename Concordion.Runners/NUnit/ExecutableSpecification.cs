using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Concordion.Internal;
using NUnit.Framework;

namespace Concordion.Runners.NUnit
{
    [TestFixture]
    public class ExecutableSpecification
    {
    
        [Test]
        public void ConcordionTest()
        {
            var concordionResult = new FixtureRunner().Run(this);
            if (concordionResult.hasExceptions())
            {
                throw new Exception("Exception in Concordion test: please see Concordion test reports");
            }
            else if (concordionResult.getFailureCount() > 0)
            {
                Assert.Fail("Concordion Test Failures: " + concordionResult.getFailureCount(),
                                   "for stack trace, please see Concordion test reports");
            }
            else
            {
                Assert.Pass();
            }
        }
    }
}
