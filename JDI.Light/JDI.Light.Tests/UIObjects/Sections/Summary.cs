using JDI.Light.Attributes;
using JDI.Light.Elements.Composite;
using JDI.Light.Interfaces.Common;

namespace JDI.Light.Tests.UIObjects.Sections
{
    public class Summary : Section
    {
        [FindBy(Id = "calculate-button")] public IButton Calculate;
    }
}