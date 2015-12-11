﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Concordion.Api;
using Concordion.NET.Internal;
using org.concordion.api;
using org.concordion.@internal;
using ClassNameBasedSpecificationLocator = Concordion.NET.Internal.ClassNameBasedSpecificationLocator;

namespace Concordion.Internal
{
    public class FixtureRunner
    {
        private object m_Fixture;

        private Source m_Source;

        private FileTarget m_Target;

        private SpecificationConfig m_SpecificationConfig;

        public string ResultPath { get; private set; }

        public FixtureRunner() { }

        public FixtureRunner(SpecificationConfig specificationConfig)
            : this()
        {
            m_SpecificationConfig = specificationConfig;
        }

        public ResultSummary Run(object fixture)
        {
            Console.WriteLine("starting test: " + fixture);

            try
            {
                this.m_Fixture = fixture;
                if (m_SpecificationConfig == null)
                {
                    this.m_SpecificationConfig = new SpecificationConfig().Load(fixture.GetType());
                }
                if (!string.IsNullOrEmpty(m_SpecificationConfig.BaseInputDirectory))
                {
                    this.m_Source = new FileSource(fixture.GetType().Assembly, m_SpecificationConfig.BaseInputDirectory);
                }
                else
                {
                    this.m_Source = new EmbeddedResourceSource(fixture.GetType().Assembly);
                }
                this.m_Target = new FileTarget(this.m_SpecificationConfig.BaseOutputDirectory);

                var fileExtensions = this.m_SpecificationConfig.SpecificationFileExtensions;
                if (fileExtensions.Count > 1)
                {
                    return RunAllSpecifications(fileExtensions);
                }
                else if (fileExtensions.Count == 1)
                {
                    return RunSingleSpecification(fileExtensions.First());
                }
                else
                {
                    throw new InvalidOperationException(string.Format("no specification extensions defined for: {0}", this.m_SpecificationConfig));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                var exceptionResult = new ExtendedSummarizingResultRecorder();
                exceptionResult.Error(e);
                return exceptionResult;
            }
        }

        private ResultSummary RunAllSpecifications(IEnumerable<string> fileExtensions)
        {
            var testSummary = new ExtendedSummarizingResultRecorder();
            var anySpecExecuted = false;
            foreach (var fileExtension in fileExtensions)
            {
                var specLocator = new ClassNameBasedSpecificationLocator(fileExtension);
                var specResource = specLocator.locateSpecification(m_Fixture);
                if (m_Source.canFind(specResource))
                {
                    var fixtureResult = RunSingleSpecification(fileExtension);
                    AddToTestResults(fixtureResult, testSummary);
                    anySpecExecuted = true;
                }
            }
            if (!anySpecExecuted)
            {
                string specPath;
                if (!string.IsNullOrEmpty(m_SpecificationConfig.BaseInputDirectory))
                {
                    specPath = string.Format("directory {0}",
                        Path.GetFullPath(m_SpecificationConfig.BaseInputDirectory));
                }
                else
                {
                    specPath = string.Format("assembly {0}",
                        m_Fixture.GetType().Assembly.GetName().Name);
                }
                testSummary.Error(new AssertionErrorException(string.Format(
                    "no active specification found for {0} in {1}",
                    this.m_Fixture.GetType().Name,
                    specPath)));
            }
            return testSummary;
        }

        private ResultSummary RunSingleSpecification(string fileExtension)
        {
            var specificationLocator = new ClassNameBasedSpecificationLocator(fileExtension);
            //ToDo? ResultPath = m_Target.ResolvedPathFor(specificationLocator.locateSpecification(m_Fixture));
            var embeddedStylesheetResource = HtmlFramework.EMBEDDED_STYLESHEET_RESOURCE;
            var concordionExtender = new ConcordionBuilder(embeddedStylesheetResource.Replace("\r", ""));
            //var concordionExtender = new ConcordionBuilder();
            concordionExtender
                .withSource(m_Source)
                //ToDo? .withTarget(m_Target)
                .withSpecificationLocator(specificationLocator)
                .withFixture(m_Fixture)
                .withEvaluatorFactory(new SimpleEvaluatorFactory());
            //ToDo: var extensionLoader = new ExtensionLoader(m_SpecificationConfig);
            //extensionLoader.AddExtensions(m_Fixture, concordionExtender);

            var concordion = concordionExtender.build();
            return concordion.process(specificationLocator.locateSpecification(m_Fixture), m_Fixture);
        }

        private void AddToTestResults(ResultSummary singleResult, ResultRecorder resultSummary)
        {
            if (resultSummary == null) return;

            if (singleResult.hasExceptions())
            {
                //resultSummary.AddResultDetails(singleResult.ErrorDetails);
            }
            else if (singleResult.getFailureCount() > 0)
            {
                //resultSummary.AddResultDetails(singleResult.FailureDetails);
            }
            else
            {
                //ToDo: resultSummary..Success();
            }
        }
    }
}
