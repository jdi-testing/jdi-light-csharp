using JDI.Core.Interfaces.Common;
using JDI.UIWebTests.Enums;
using JDI.Web.Attributes;
using JDI.Web.Selenium.Elements.Complex;
using JDI.Web.Selenium.Elements.Composite;

namespace JDI.UIWebTests.UIObjects.Sections
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