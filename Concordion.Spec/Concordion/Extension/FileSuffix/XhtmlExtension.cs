using Concordion.Api.Extension;
using Concordion.Internal;
using Concordion.NET.Internal;

namespace Concordion.Spec.Concordion.Extension.FileSuffix
{
    public class XhtmlExtension : IConcordionExtension
    {
        public void AddTo(IConcordionExtender concordionExtender)
        {
            //ToDo concordionExtender
            //    .WithSpecificationLocator(new ClassNameBasedSpecificationLocator("xhtml"));
        }
    }
}
