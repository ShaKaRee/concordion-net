// Copyright 2009 Jeffrey Cameron
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Linq;
using ognl;
using Concordion.Internal.Util;
using System.Data;
using org.concordion.api;

namespace Concordion.Internal
{
    public class OgnlEvaluator : Evaluator
    {
        #region Properties

        public object Fixture
        {
            get;
            private set;
        }

        private OgnlContext OgnlContext
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        public OgnlEvaluator(object fixture)
        {
            this.Fixture = fixture;
            this.OgnlContext = new OgnlContext();
        }

        #endregion

        #region Methods

        private void AssertStartsWithHash(string expression)
        {
            if (!expression.StartsWith("#"))
            {
                throw new InvalidExpressionException("Variable for concordion:set must start"
                        + " with '#'\n (i.e. change concordion:set=\"" + expression + "\" to concordion:set=\"#" + expression + "\".");
            }
        }

        private void PutVariable(string rawVariableName, object value)
        {
            Check.IsFalse(rawVariableName.StartsWith("#"), "Variable name passed to evaluator should not start with #");
            Check.IsTrue(!rawVariableName.Equals("in"), "'%s' is a reserved word and cannot be used for variables names", rawVariableName);
            OgnlContext[rawVariableName] = value;
        }

        #endregion

        #region IEvaluator Members

        public virtual object getVariable(string expression)
        {
            AssertStartsWithHash(expression);
            string rawVariableName = expression.Substring(1);
            return OgnlContext[rawVariableName];
        }

        public virtual void setVariable(string expression, object value)
        {
            AssertStartsWithHash(expression);
            if (expression.Contains("="))
            {
                evaluate(expression);
            }
            else
            {
                String rawVariable = expression.Substring(1);
                PutVariable(rawVariable, value);
            }
        }

        public virtual object evaluate(string expression)
        {
            Check.NotNull(Fixture, "Root object is null");
            Check.NotNull(expression, "Expression to evaluate cannot be null");
            var result = Ognl.getValue(expression, OgnlContext, Fixture);
            if (result is Boolean)
            {
                result = new java.lang.Boolean(result.ToString());
            }
            return result;
        }

        #endregion
    }
}
