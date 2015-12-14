using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Concordion.Internal.Util;
using System.IO;
using java.io;
using org.concordion.api;

namespace Concordion.Spec.Support
{
    class StubSource : Source
    {
        private Dictionary<Resource, string> resources = new Dictionary<Resource, string>();

        public void AddResource(string resourceName, string content) 
        {
            this.AddResource(new Resource(resourceName), content);
        }

        public void AddResource(Resource resource, string content) 
        {
            if (!this.resources.ContainsKey(resource))
            {
                this.resources.Add(resource, content);
            }
            else
            {
                this.resources.Remove(resource);
                this.resources.Add(resource, content);
            }
        }

        #region ISource Members

        public InputStream createInputStream(Resource resource)
        {
            Check.IsTrue(canFind(resource), "No such resource exists in simulator: " + resource.getPath());
            return new ByteArrayInputStream(Encoding.UTF8.GetBytes(this.resources[resource]));
        }

        public bool canFind(Resource resource)
        {
            return this.resources.ContainsKey(resource);
        }

        #endregion
    }
}
