﻿using System;
using System.Collections.Generic;
using System.Linq;
using Concordion.NET.Api.Extension;

namespace Concordion.Spec.Concordion.Extension.Configuration
{
    [Extensions(typeof(FakeExtension1))]
    public class ExampleDerivedFixtureWithClassAttributes : ExampleFixtureBaseWithClassAttribute
    {
    }
}
