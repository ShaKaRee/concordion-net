using Concordion.NET.Internal;
using org.concordion.api;
using File = java.io.File;
using FileTarget = org.concordion.@internal.FileTarget;
using DefaultConcordionRunner = Concordion.NET.Internal.Runner.DefaultConcordionRunner;

namespace Concordion.Internal
{
    public class FixtureRunner
    {
        private SpecificationConfig m_SpecificationConfig;

        public string ResultPath;

        public FixtureRunner() { }

        public FixtureRunner(SpecificationConfig specificationConfig)
            : this()
        {
            m_SpecificationConfig = specificationConfig;
        }

        public ResultSummary Run(object fixture)
        {
            var concordionRunner = new DefaultConcordionRunner();
            return concordionRunner.Run(fixture);
        }
    }
}
