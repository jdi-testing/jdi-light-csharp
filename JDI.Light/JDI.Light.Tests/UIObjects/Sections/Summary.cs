using JDI.Light.Attributes;
using JDI.Light.Elements.Common;
using JDI.Light.Elements.Composite;

namespace JDI.Light.Tests.UIObjects.Sections
{
    public class Summary : Section
    {
        [FindBy(Id = "calculate-button")]
        public Button Calculate { get; set; }
    }
}