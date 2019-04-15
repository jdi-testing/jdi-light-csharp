using System.Collections.Generic;
using System.Linq;
using JDI.Light.Elements.Base;
using JDI.Light.Elements.Common;
using JDI.Light.Factories;
using JDI.Light.Interfaces.Complex;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Complex
{
    public class RadioButtons : Selector, IRadioButtons
    {
        public By RadiButtonLocator => By.CssSelector("[type='radio']");

        public By LabelLocator => By.CssSelector(".html-left > label");

        private List<UIElement> Labels => FindElements(LabelLocator)
            .Select(e => UIElementFactory.CreateInstance<UIElement>(LabelLocator, this, e)).ToList();

        private List<UIElement> Radios => FindElements(RadiButtonLocator)
            .Select(e => UIElementFactory.CreateInstance<UIElement>(RadiButtonLocator, this, e)).ToList();

        protected RadioButtons(By byLocator) : base(byLocator)
        {
        }
        
        public void Select(string value)
        {
            Labels.First(label => label.Text.Trim() == value).Click();
        }

        public void Select(int index)
        {
            Labels[index-1].Click();
        }

        public string Selected()
        {
            var checkedId = Radios.First(IsChecked).GetAttribute("id");

            return Labels.First(label => checkedId.Contains(label.GetAttribute("for")))?.Text;
        }

        public List<string> Values()
        {
            return Labels.Select(label => label.Text.Trim()).ToList();
        }

        private bool IsChecked(UIElement radioButton) => radioButton.GetAttribute("checked") != null;
    }
}