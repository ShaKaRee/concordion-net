using Concordion.Integration;
using Concordion.NET.Api.Extension;

namespace Concordion.Spec.Concordion.Extension.FileSuffix
{
    [ConcordionTest]
    [Extensions(typeof(XhtmlExtension))]
    public class FileSuffixExtensionsTest
    {
        public bool hasBeenProcessed()
        {
            return true;
        }
    }
}
