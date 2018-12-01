using System.Collections.Generic;
using System.Linq;
using JDI.Light.Elements.Common;
using JDI.Light.Elements.Composite;
using JDI.Light.Extensions;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Complex
{
    public class TextList : CompositeUIElement
    {
        private List<Label> _texts;

        public TextList(By locator) : base(locator)
        {
        }

        public IList<Label> TextElements
        {
            get
            {
                if (_texts != null)
                {
                    return _texts;
                }
                return _texts = WebElements.Select(e =>
                {
                    var l = new Label(Locator) {WebElement = e};
                    return l;
                }).ToList();
            }
        }

        public int Count()
        {
            return _texts.Count;
        }

        public IList<string> WaitText(string expected)
        {
            if (Timer.Wait(() => Texts.Contains(expected)))
                return Texts;
            throw JDI.Assert.Exception($"Wait Text '{expected}' Failed ({ToString()}");
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

        public string Value => Texts.FormattedJoin();

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
                throw JDI.Assert.Exception($"Wait displayed failed ({ToString()})");
        }

        public new void WaitVanished()
        {
            if (!Timer.Wait(() =>
            {
                var elements = WebElements;
                return elements == null || !elements.Any() && elements.All(el => !el.Displayed);
            }))
                throw JDI.Assert.Exception($"Wait vanished failed ({ToString()})");
        }
    }
}