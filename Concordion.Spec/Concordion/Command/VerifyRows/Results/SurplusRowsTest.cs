﻿using Concordion.Integration;

namespace Concordion.Spec.Concordion.Command.VerifyRows.Results
{
    [ConcordionTest]
    public class SurplusRowsTest : MissingRowsTest
    {
        public void addPerson(string firstName, string lastName)
        {
            base.addPerson(firstName, lastName, 1973);
        }
    }
}
