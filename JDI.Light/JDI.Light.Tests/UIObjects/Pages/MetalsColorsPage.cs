using JDI.Light.Attributes;
using JDI.Light.Elements.Base;
using JDI.Light.Elements.Common;
using JDI.Light.Elements.Composite;
using JDI.Light.Interfaces.Common;
using JDI.Light.Tests.UIObjects.Sections;
using OpenQA.Selenium;

namespace JDI.Light.Tests.UIObjects.Pages
{
    public class MetalsColorsPage : WebPage
    {
        public class CustomCheck
        {
            public static bool CheckFunc(UIElement e)
            {
                var a = e.Get<UIElement>(By.XPath("//*[@id='elements-checklist']//*[*[text()='Water']]/input"));
                return a.FindImmediately(() => a.GetAttribute("checked") != null, false);
            }
        }

        [FindBy(Id = "calculate-button")]
        public Label Calculate;
        
        [Css("#calculate-button")]
        public IWebElement CalculateButton;

        [FindBy(Id = "calculate-button")]
        public ILabel CalculateLabel;

        [FindBy(Css = ".summ-res")]
        public ITextElement CalculateText;

        [FindBy(XPath = "//*[@id='elements-checklist']//*[text()='Water']")]
        [IsChecked(typeof(CustomCheck), nameof(CustomCheck.CheckFunc))]
        public CheckBox CbWater;

        [FindBy(Id = "summary-block")]
        public Summary SummaryBlock;

        [FindBy(Css = "#colors")]
        public DropDown ColorsDropDown { get; set; }

        [FindBy(Css = "#colors .filter-option")]
        public DropDown ColorsDropDownText { get; set; }

        [FindBy(Css = "#metals")]
        public DataList MetalsDataList { get; set; }

        [FindBy(Css = "#metals span.caret")]
        public DataList MetalsDataListCaret { get; set; }

        [FindBy(Css = "#metals input")]
        public DataList MetalsInput { get; set; }

        [FindBy(Css = "#submit-button")]
        public Button SubmitButton { get; set; }

        [FindBy(Css = "#metals")]
        public ComboBox MetalsComboBox { get; set; }

        [FindBy(Css = "#metals span.caret")]
        public ComboBox MetalsComboBoxCaret { get; set; }

        [FindBy(Css = "#metals input")]
        public ComboBox MetalsInputComboBox { get; set; }
    }
}