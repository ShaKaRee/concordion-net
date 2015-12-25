using System;
using System.Collections.Generic;
using System.Linq;

namespace Concordion.NET.Api.Extension
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ExtensionsAttribute : Attribute
    {
        public Type[] ExtensionTypes { get; set; }

        public ExtensionsAttribute(params Type[] extensionTypes)
        {
            this.ExtensionTypes = extensionTypes;
        }
    }
}
