using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;
using java.io;
using org.concordion.api;

namespace Concordion.NET.Internal
{
    public class EmbeddedResourceSource : Source
    {
        #region Properties

        public Assembly FixtureAssembly
        {
            get;
            private set;
        }

        #endregion

        #region Constructors

        public EmbeddedResourceSource(Assembly fixtureAssembly)
        {
            this.FixtureAssembly = fixtureAssembly;
        }

        #endregion

        #region Methods

        private string ConvertPathToNamespace(string path)
        {
            var dottedPath = path.Replace('\\', '.');
            if (dottedPath[0] == '.')
            {
                dottedPath = dottedPath.Remove(0, 1);
            }
            return dottedPath;
        }

        #endregion

        #region ISource Members

        //public TextReader CreateReader(Resource resource)
        //{
        //    var fullyQualifiedTypeName = ConvertPathToNamespace(resource.Path);

        //    if (canFind(resource))
        //    {
        //        return new StreamReader(FixtureAssembly.GetManifestResourceStream(fullyQualifiedTypeName));
        //    }

        //    throw new InvalidOperationException(String.Format("Cannot open the resource {0}", fullyQualifiedTypeName));
        //}

        public bool canFind(org.concordion.api.Resource resource)
        {
            var fullyQualifiedTypeName = ConvertPathToNamespace(resource.getPath());
            return FixtureAssembly.GetManifestResourceInfo(fullyQualifiedTypeName) != null;
        }

        #endregion

        public InputStream createInputStream(org.concordion.api.Resource resource)
        {
            throw new NotImplementedException();
        }
    }
}
