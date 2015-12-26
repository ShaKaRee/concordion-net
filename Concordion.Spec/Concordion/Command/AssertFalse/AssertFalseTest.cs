using System;
using Concordion.Integration;
using Concordion.Spec.Support;

namespace Concordion.Spec.Concordion.Command.AssertFalse
{
    [ConcordionTest]
    public class AssertFalseTest
    {
        public string successOrFailure(string fragment, string evaluationResult)
        {
            return new TestRig()
                .WithStubbedEvaluationResult(Boolean.Parse(evaluationResult))
                .ProcessFragment(fragment)
                .SuccessOrFailureInWords();
        }
    }
}
