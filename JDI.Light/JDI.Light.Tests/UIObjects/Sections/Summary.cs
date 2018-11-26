using JDI.Light.Attributes;
using JDI.Light.Interfaces.Common;
using JDI.Light.Selenium.Elements.Complex;
using JDI.Light.Selenium.Elements.Composite;
using JDI.Light.Tests.Enums;

namespace JDI.Light.Tests.UIObjects.Sections
{
    public class Summary : Section
    {
        // Exception in CascadeInit
        //[FindBy(Css = "#odds-selector p")]
        //public Selector<Odds> OddNumbersSelector;

        [FindBy(Id = "calculate-button")] public IButton Calculate;

        [FindBy(Css = "#odds-selector p")] public RadioButtons<Odds> OddNumbers;
    }
}