using System;
using JDI.Light.Attributes;
using JDI.Light.Elements.Base;
using JDI.Light.Elements.Common;
using JDI.Light.Elements.Complex;
using JDI.Light.Elements.Composite;
using JDI.Light.Interfaces.Common;
using JDI.Light.Tests.Enums;
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
                var a = new UIElement(By.XPath("//*[@id='elements-checklist']//*[*[text()='Water']]/input"));
                return a.FindImmediately(() => a.WebElement.Selected
                                               || a.WebElement.GetAttribute("checked") != null, false);
            }
        }

        [FindBy(Id = "calculate-button")]
        public Label Calculate;

        [FindBy(Id = "calculate-button")]
        public Button CalculateButton;

        [FindBy(Id = "calculate-button")]
        public ILabel CalculateLabel;

        [FindBy(Css = ".summ-res")]
        public IText CalculateText;

        [FindBy(XPath = "//*[@id='elements-checklist']//*[text()='Water']")]
        [Checked(typeof(CustomCheck), nameof(CustomCheck.CheckFunc))]
        public CheckBox CbWater;

        public Dropdown<Colors> Colors =
            new Dropdown<Colors>(By.CssSelector(".colors .filter-option"), By.CssSelector(".colors li span"));

        public ComboBox<Metals> ComboBox =
            new ComboBox<Metals>(By.CssSelector(".metals .caret"), By.CssSelector(".metals li span"),
                By.CssSelector(".metals input"))
            {
                GetTextAction = c => new Text(By.CssSelector(".metals .filter-option")).GetText
            };

        [FindBy(Css = "#elements-checklist label")]
        public CheckList<CheckboxElements> Elements;

        [FindBy(Id = "summary-block")]
        public Summary SummaryBlock;
    }
}