using System;
using System.Collections.Generic;
using System.Linq;
using Concordion.Integration;
using Concordion.Spec.Support;
using org.concordion.api;

namespace Concordion.Spec.Concordion.Command.Run
{
    [ConcordionTest]
    public class RunTest
    {
    	public String successOrFailure(String fragment, String hardCodedTestResult, String evaluationResult)
    	{
            java.lang.System.setProperty("concordion.runner.runtestrunner", 
                "Concordion.Spec.Concordion.Command.Run.RunTestRunner, Concordion.Spec, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");

    	    if ("SUCCESS".Equals(hardCodedTestResult))
    	    {
    	        RunTestRunner.Result = Result.SUCCESS;
    	    }
    	    if ("FAILURE".Equals(hardCodedTestResult))
    	    {
    	        RunTestRunner.Result = Result.FAILURE;
    	    }

            return new TestRig()
                .WithStubbedEvaluationResult(evaluationResult)
                .ProcessFragment(fragment)
                .SuccessOrFailureInWords();
        }
    }
}
