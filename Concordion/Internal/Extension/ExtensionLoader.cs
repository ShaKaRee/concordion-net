using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Concordion.Internal.Util;
using Concordion.NET.Internal;
using org.concordion.api.extension;
using ExtensionAttribute = Concordion.NET.Api.Extension.ExtensionAttribute;
using ExtensionsAttribute = Concordion.NET.Api.Extension.ExtensionsAttribute;

namespace Concordion.Internal.Extension
{
    public class ExtensionLoader
    {
        private SpecificationConfig Configuration { get; set; }

        public ExtensionLoader(SpecificationConfig configuration)
        {
            Configuration = configuration;
        }

        public void AddExtensions(object fixture, ConcordionExtender concordionExtender)
        {
            foreach (var concordionExtension in GetExtensionsFromConfiguration())
            {
                concordionExtension.addTo(concordionExtender);
            }

            foreach (var concordionExtension in GetExtensionsForFixture(fixture))
            {
                concordionExtension.addTo(concordionExtender);
            }
        }

        private IEnumerable<ConcordionExtension> GetExtensionsFromConfiguration()
        {
            if (Configuration == null) return Enumerable.Empty<ConcordionExtension>();

            var extensions = new List<ConcordionExtension>();
            foreach (var extension in Configuration.ConcordionExtensions)
            {
                var extensionTypeName = extension.Key;
                var extensionAsseblyName = extension.Value;
                extensions.Add(CreateConcordionExtension(extensionTypeName, extensionAsseblyName));
            }
            return extensions;
        }

        private IEnumerable<ConcordionExtension> GetExtensionsForFixture(object fixture)
        {
            var extensions = new List<ConcordionExtension>();
            foreach (var fixtureType in GetClassHierarchyParentFirst(fixture.GetType()))
            {
                extensions.AddRange(GetExtensionsFromFieldAttributes(fixture, fixtureType));
                extensions.AddRange(GetExtensionsFromClassAttributes(fixtureType));
            }
            return extensions;
        }

        private IEnumerable<Type> GetClassHierarchyParentFirst(Type fixtureType)
        {
            var fixtureTypes = new List<Type>();

            var current = fixtureType;
            while (current != null && current != typeof(object))
            {
                fixtureTypes.Add(current);
                current = current.BaseType;
            }
            fixtureTypes.Reverse();

            return fixtureTypes;
        }

        private IEnumerable<ConcordionExtension> GetExtensionsFromFieldAttributes(object fixture, Type fixtureType)
        {
            var extensions = new List<ConcordionExtension>();
            FieldInfo[] fieldInfos = fixtureType.GetFields(BindingFlags.Public | BindingFlags.DeclaredOnly |
                                                            BindingFlags.Instance | BindingFlags.Static);
            foreach (var fieldInfo in fieldInfos)
            {
                if (HasAttribute(fieldInfo, typeof(ExtensionAttribute), false))
                {
                    var extension = fieldInfo.GetValue(fixture) as ConcordionExtension;
                    Check.NotNull(extension, string.Format("Extension field '{0}' must be non-null", fieldInfo.Name));
                    extensions.Add(extension);
                }
            }
            return extensions;
        }

        private IEnumerable<ConcordionExtension> GetExtensionsFromClassAttributes(Type fixtureType)
        {
            if (!HasAttribute(fixtureType, typeof(ExtensionsAttribute), false)) return Enumerable.Empty<ConcordionExtension>();

            var extensions = new List<ConcordionExtension>();
            foreach (var attribute in fixtureType.GetCustomAttributes(typeof(ExtensionsAttribute), false))
            {
                var extensionsAttribute = attribute as ExtensionsAttribute;
                foreach (var extensionType in extensionsAttribute.ExtensionTypes)
                {
                    var extensionTypeName = extensionType.FullName;
                    var extensionAssemblyName = extensionType.Assembly.GetName().Name;
                    extensions.Add(CreateConcordionExtension(extensionTypeName, extensionAssemblyName));
                }
            }
            return extensions;
        }

        private static ConcordionExtension CreateConcordionExtension(string typeName, string assemblyName)
        {
            ConcordionExtension extension;
            var instance = Activator.CreateInstance(assemblyName, typeName).Unwrap();
            if (instance is ConcordionExtension)
            {
                extension = instance as ConcordionExtension;
            }
            else if(instance is ConcordionExtensionFactory)
            {
                var extensionFactory = instance as ConcordionExtensionFactory;
                extension = extensionFactory.createExtension();
            }
            else
            {
                throw new InvalidCastException(
                    string.Format("Extension {0} must implement {1} or {2}",
                                  typeName, typeof(ConcordionExtension), typeof(ConcordionExtensionFactory)));
            }
            return extension;
        }

        private bool HasAttribute(MemberInfo memberInfo, Type attributeType, bool inherit)
        {
            return memberInfo.GetCustomAttributes(attributeType, inherit).Any(attribute => attribute.GetType() == attributeType);
        }
    }
}
