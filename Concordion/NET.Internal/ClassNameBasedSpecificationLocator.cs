﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using org.concordion.api;

namespace Concordion.NET.Internal
{
    public class ClassNameBasedSpecificationLocator : SpecificationLocator
    {
        #region ISpecificationLocator Members

        private string m_SpecificationSuffix;

        public ClassNameBasedSpecificationLocator() : this("html") { }

        public ClassNameBasedSpecificationLocator(string mSpecificationSuffix)
        {
            this.m_SpecificationSuffix = mSpecificationSuffix;
        }

        public org.concordion.api.Resource locateSpecification(object fixture)
        {
            var fixtureName = fixture.GetType().ToString();
            fixtureName = fixtureName.Replace(".", "\\");

            //Add Test und Fixture -> Case Sensitive 
            fixtureName = Regex.Replace(fixtureName, "(Fixture|Test)$", "");
            //Suffix from Concordion.Specification.config
            var path = "\\" + fixtureName + "." + m_SpecificationSuffix;
            return new org.concordion.api.Resource(path);
        }

        #endregion
    }
}