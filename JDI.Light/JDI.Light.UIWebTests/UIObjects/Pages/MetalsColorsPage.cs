using JDI.Core.Attributes;
using JDI.Core.Interfaces.Common;
using JDI.Core.Interfaces.Complex;
using JDI.Core.Selenium.Base;
using JDI.Core.Selenium.Elements.Common;
using JDI.Core.Selenium.Elements.Complex;
using JDI.Core.Selenium.Elements.Composite;
using JDI.UIWebTests.Enums;
using JDI.UIWebTests.UIObjects.Sections;
using OpenQA.Selenium;

namespace JDI.UIWebTests.UIObjects.Pages
{
    public class MetalsColorsPage : WebPage
    {
        [FindBy(Id = "calculate-button")]
        public Label Calculate;

        [FindBy(Id = "calculate-button")]
        public Button CalculateButton;

        [FindBy(Id = "calculate-button")]
        public ILabel CalculateLabel;

        [FindBy(Css = ".summ-res")]
        public IText CalculateText;

        [FindBy(XPath = "//*[@id='elements-checklist']//*[text()='Water']")]
        public CheckBox CbWater = new CheckBox
        {
            IsCheckedAction = el => new WebBaseElement(By.XPath("//*[@id='elements-checklist']//*[*[text()='Water']]/input")).WebElement.Selected
        };

        public IDropDown<Colors> Colors =
            new Dropdown<Colors>(By.CssSelector(".colors .filter-option"), By.CssSelector(".colors li span"));

        public ComboBox<Metals> ComboBox =
            new ComboBox<Metals>(By.CssSelector(".metals .caret"), By.CssSelector(".metals li span"),
                By.CssSelector(".metals input"))
            {
                GetTextAction = c => new Text(By.CssSelector(".metals .filter-option")).GetText
            };

        [FindBy(Css = "#elements-checklist label")]
        public CheckList<Elements> Elements;

        [FindBy(Id = "summary-block")]
        public Summary SummaryBlock;
    }
}