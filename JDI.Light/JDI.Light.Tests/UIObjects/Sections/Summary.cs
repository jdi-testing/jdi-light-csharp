using JDI.Light.Attributes;
using JDI.Light.Elements.Composite;
using JDI.Light.Interfaces.Common;
using JDI.Light.Tests.Enums;

namespace JDI.Light.Tests.UIObjects.Sections
{
    public class Summary : Section
    {
        [FindBy(Id = "calculate-button")] public IButton Calculate;
    }
}