﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Concordion.Api.Listener
{
    public interface ISpecificationProcessingListener
    {
        void BeforeProcessingSpecification(SpecificationProcessingEvent processingEvent);

        void AfterProcessingSpecification(SpecificationProcessingEvent processingEvent);
    }
}
