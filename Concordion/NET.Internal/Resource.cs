using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Concordion.Internal;

namespace Concordion.NET.Internal
{
    public class Resource
    {
        #region Fields

        private static readonly char PATH_SEPARATOR = '\\';
        private static readonly string RELATIVE_PATH_INDICATOR = ".." + PATH_SEPARATOR;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>The path.</value>
        public virtual string Path
        {
            get;
            set;
        }

        public string FixtureAssemblyName
        {
            get;
            private set;
        }

        public string ReducedPath
        {
            get
            {
                return Path.RemoveFirst(FixtureAssemblyName.Replace('.', PATH_SEPARATOR) + PATH_SEPARATOR);
            }
        }

        /// <summary>
        /// Gets or sets the parts of the Path
        /// </summary>
        /// <value>The parts.</value>
        private string[] Parts
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the parent directory of this resource
        /// </summary>
        /// <value>The parent.</value>
        public Resource Parent
        {
            get
            {
                if (Path.Equals(System.IO.Path.GetPathRoot(Path)))
                {
                    return null;
                }

                StringBuilder parentPath = new StringBuilder("\\");
                for (int i = 0; i < Parts.Length - 1; i++)
                {
                    parentPath.Append(Parts[i] + PATH_SEPARATOR);
                }
                return new Resource(parentPath.ToString(), FixtureAssemblyName);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is a directory
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is package; otherwise, <c>false</c>.
        /// </value>
        public bool IsPackage
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the directory this resource resides in.
        /// </summary>
        /// <value>The package.</value>
        public Resource Package
        {
            get
            {
                if (IsPackage)
                {
                    return this;
                }
                return Parent;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Resource"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public Resource(string path)
        {
            Path = path.Replace('/', PATH_SEPARATOR);

            if (System.IO.Path.IsPathRooted(path))
            {
                Path = Path.Replace(System.IO.Path.GetPathRoot(path), @"\");
            }

            if (Path.EndsWith(PATH_SEPARATOR.ToString(), StringComparison.InvariantCulture))
            {
                IsPackage = true;
            }

            Parts = Path.Split(new string[] { PATH_SEPARATOR.ToString() }, StringSplitOptions.RemoveEmptyEntries);
            if (Parts.Length == 0)
            {
                Name = "";
            }
            else
            {
                Name = Parts[Parts.Length - 1];
            }
        }

        public Resource(string path, string fixtureAssemblyName)
            : this(path)
        {
            this.FixtureAssemblyName = fixtureAssemblyName;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a resource relative to this one based on the path
        /// </summary>
        /// <param name="relativePath">The relative path.</param>
        /// <returns></returns>
        public Resource GetRelativeResource(string relativePath)
        {
            //Check.IsFalse(relativePath.StartsWith(PATH_SEPARATOR.ToString()), "Relative path should not start with a slash");

            string subPath = relativePath;

            Resource p = Package;

            while (subPath.StartsWith(RELATIVE_PATH_INDICATOR))
            {
                p = p.Parent;
                if (p == null)
                {
                    throw new Exception("Path '" + relativePath + "' relative to '" + Path + "' " +
                    "evaluates above the root package.");
                }
                subPath = subPath.RemoveFirst(RELATIVE_PATH_INDICATOR);
            }

            //ToDo: Check.IsFalse(subPath.Contains(RELATIVE_PATH_INDICATOR), String.Format("The {0} operator is currently only supported at the start of expressions", RELATIVE_PATH_INDICATOR));

            return new Resource(p.Path + subPath, FixtureAssemblyName);
        }

        /// <summary>
        /// Gets the relative path.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <returns></returns>
        public string GetRelativePath(Resource resource)
        {
            if (resource.Path == Path)
            {
                return Name;
            }

            // Find common stem and ignore it
            // Use ../ to move up the path from here to common stem
            // Append the rest of the path from resource

            string[] therePieces = resource.Package.Path.Split(new string[] { PATH_SEPARATOR.ToString() }, StringSplitOptions.RemoveEmptyEntries);
            string[] herePieces = Package.Path.Split(new string[] { PATH_SEPARATOR.ToString() }, StringSplitOptions.RemoveEmptyEntries);

            int sharedPiecesCount = 0;
            for (int i = 0; i < herePieces.Length; i++)
            {
                if (therePieces.Length <= i)
                {
                    break;
                }
                if (therePieces[i].Equals(herePieces[i]))
                {
                    sharedPiecesCount++;
                }
                else
                {
                    break;
                }
            }

            StringBuilder r = new StringBuilder();

            for (int i = sharedPiecesCount; i < herePieces.Length; i++)
            {
                r.Append(RELATIVE_PATH_INDICATOR);
            }

            for (int i = sharedPiecesCount; i < therePieces.Length; i++)
            {
                r.Append(therePieces[i]);
                r.Append(PATH_SEPARATOR);
            }

            if (resource.IsPackage)
            {
                return r.ToString();
            }
            return r.ToString() + resource.Name;
        }

        #endregion

        #region Override Methods

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>.</param>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.NullReferenceException">
        /// The <paramref name="obj"/> parameter is null.
        /// </exception>
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is Resource)) return false;

            Resource other = obj as Resource;
            if (other.Path != this.Path)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        public override int GetHashCode()
        {
            return Path.GetHashCode();
        }

        #endregion
    }
}
