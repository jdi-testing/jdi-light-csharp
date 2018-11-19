using System.Collections.Generic;
using System.Linq;
using JDI.Core.Extensions;
using JDI.Core.Interfaces.Complex;
using JDI.Core.Selenium.Base;
using JDI.Core.Selenium.Elements.Common;
using JDI.Core.Settings;
using OpenQA.Selenium;

namespace JDI.Core.Selenium.Elements.Complex
{
    public class TextList : UIElement, ITextList
    {
        private readonly WebElements<Label> _texts;

        public TextList(By locator, List<IWebElement> webElements = null) :
            base(locator, webElements: webElements)
        {
            _texts = new WebElements<Label>(Locator);
        }

        public new List<IWebElement> WebElements => base.WebElements;
        public IList<Label> TextElements => _texts;

        public int Count()
        {
            return _texts.Count;
        }

        public IList<string> WaitText(string expected)
        {
            if (Timer.Wait(() => Texts.Contains(expected)))
                return Texts;
            throw JDISettings.Exception($"Wait Text '{expected}' Failed ({ToString()}");
        }

        public IList<string> Texts => _texts.Select(el => el.GetText).ToList();

        public string this[int index]
        {
            get
            {
                var texts = Texts;
                return index >= 0
                    ? texts[index]
                    : texts[texts.Count - index];
            }
            set
            {
                /* Not applicable */
            }
        }

        public string Value => Texts.Print();

        public string GetValue()
        {
            return Value;
        }

        public new bool Displayed
        {
            get
            {
                var elements = WebElements;
                return elements != null && elements.Any(el => el.Displayed);
            }
        }

        public new bool Hidden
        {
            get
            {
                var elements = WebElements;
                return elements == null || !elements.Any() || elements.All(el => !el.Displayed);
            }
        }

        public new void WaitDisplayed()
        {
            if (!Timer.Wait(() =>
            {
                var elements = WebElements;
                return elements != null && elements.Any() && elements.All(el => el.Displayed);
            }))
                throw JDISettings.Exception($"Wait displayed failed ({ToString()})");
        }

        public new void WaitVanished()
        {
            if (!Timer.Wait(() =>
            {
                var elements = WebElements;
                return elements == null || !elements.Any() && elements.All(el => !el.Displayed);
            }))
                throw JDISettings.Exception($"Wait vanished failed ({ToString()})");
        }
    }
}