using System.Collections.Generic;
using System.Linq;
using JDI.Light.Elements.Base;
using JDI.Light.Exceptions;
using JDI.Light.Factories;
using JDI.Light.Interfaces.Complex;
using OpenQA.Selenium;
using Label = JDI.Light.Elements.Common.Label;

namespace JDI.Light.Elements.Complex
{
    public class RadioButtons : UIElement, IRadioButtons
    {
        public By RadioButtonLocator { get; set; }

        public By LabelLocator { get; set; }

        private List<Label> Labels => FindElements(LabelLocator)
            .Select(element => UIElementFactory.CreateInstance<Label>(LabelLocator, this, element)).ToList();

        private List<UIElement> Radios => FindElements(RadioButtonLocator)
            .Select(element => UIElementFactory.CreateInstance<UIElement>(RadioButtonLocator, this, element)).ToList();

        protected RadioButtons(By byLocator) : base(byLocator)
        {
            RadioButtonLocator = By.CssSelector("[type='radio']");
            LabelLocator = By.CssSelector(".html-left > label");
        }

        public By RadioLocator { get; set; }

        public void Select(string name, bool checkEnabled = true)
        {
            var element = Labels.FirstOrDefault(label => label.Text.Trim() == name);
            if (element == null)
            {
                throw new ElementNotFoundException($"label: {name} not found");
            }
            CheckEnabled(name, checkEnabled);
            element.Click();
        }

        public void Select(int index, bool checkEnabled = true)
        {
            CheckEnabled(checkEnabled);
            Labels[index - 1].Click();
        }

        public new string Selected()
        {
            var checkedId = Radios.First(radio => radio.GetAttribute("checked") != null).GetAttribute("id");

            return Labels.First(label => checkedId.Contains(label.GetAttribute("for")))?.Text;
        }

        public List<string> Values()
        {
            return Labels.Select(label => label.Text.Trim()).ToList();
        }

        public string Value => Selected();

        private void CheckEnabled(string name, bool checkEnabled = true)
        {
            if (checkEnabled)
            {
                var element = Radios.First(radio => radio.GetAttribute("id") == name.ToLower());
                if (!element.Enabled)
                {
                    throw new ElementDisabledException(this);
                }
            }
        }
    }
}