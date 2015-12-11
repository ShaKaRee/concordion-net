using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Concordion.Internal;
using ikvm.extensions;
using org.concordion.api;
using InputStream = java.io.InputStream;
using FileInputStream = java.io.FileInputStream;
using File = java.io.File;

namespace Concordion.NET.Internal
{
    public class FileSource : Source
    {
        #region Constants

        private const char PathSeparator = '\\';

        #endregion

        #region Properties

        private string BaseDirectory
        {
            get;
            set;
        }

        public Assembly FixtureAssembly
        {
            get;
            private set;
        }

        #endregion

        #region Constructors

        public FileSource(Assembly fixtureAssembly, string baseDirectory)
        {
            this.FixtureAssembly = fixtureAssembly;
            this.BaseDirectory = Path.GetFullPath(baseDirectory);
        }

        #endregion

        #region Source Members

        public TextReader CreateReader(org.concordion.api.Resource resource)
        {
            return new StreamReader(new FileStream(ExistingFilePath(resource), FileMode.Open));
        }

        public InputStream createInputStream(org.concordion.api.Resource resource)
        {
            return new FileInputStream(new java.io.File(ExistingFilePath(resource)));
        }

        public bool canFind(org.concordion.api.Resource resource)
        {
            return ExistingFilePath(resource) != null;
        }

        #endregion

        #region private methods

        private string ExistingFilePath(org.concordion.api.Resource resource)
        {
            var resourcePath = resource.getPath().Replace("/", "\\");
            if (resourcePath.StartsWith("\\"))
            {
                resourcePath = resourcePath.substring(1);
            }
            var filePath = Path.Combine(BaseDirectory, resourcePath);
            if (System.IO.File.Exists(filePath))
            {
                return filePath;
            }
            filePath = Path.Combine(BaseDirectory, resourcePath.RemoveFirst(FixtureAssembly.GetName().Name.Replace('.', PathSeparator) + PathSeparator));
            if (System.IO.File.Exists(filePath))
            {
                return filePath;
            }
            return null;
        }

        #endregion
    }
}
