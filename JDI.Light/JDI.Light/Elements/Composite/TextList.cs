using System.Collections.Generic;
using System.Linq;
using JDI.Light.Elements.Base;
using JDI.Light.Elements.Common;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Composite
{
    public class TextList : UIElement
    {
        public TextList(By locator) : base(locator)
        {
        }

        public IList<Label> TextElements
        {
            get
            {
                var res = WebElements.Select(e =>
                {
                    var l = new Label(Locator) { WebElement = e, Parent = Parent};
                    return l;
                }).ToList();
                return res;
            }
        }

        public int Count()
        {
            return TextElements.Count;
        }

        public IList<string> WaitText(string expected)
        {
            if (Invoker.Wait(() => Texts.Contains(expected)))
                return Texts;
            throw Jdi.Assert.Exception($"Wait Text '{expected}' Failed ({ToString()}");
        }

        public IList<string> Texts => TextElements.Select(el => el.GetText()).ToList();

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
            if (!Invoker.Wait(() =>
            {
                var elements = WebElements;
                return elements != null && elements.Any() && elements.All(el => el.Displayed);
            }))
                throw Jdi.Assert.Exception($"Wait displayed failed ({ToString()})");
        }

        public new void WaitVanished()
        {
            if (!Invoker.Wait(() =>
            {
                var elements = WebElements;
                var res = elements == null;
                if (!res)
                {
                    return !elements.Any() && elements.All(el => !el.Displayed);
                }
                return res;
            }))
                throw Jdi.Assert.Exception($"Wait vanished failed ({ToString()})");
        }
    }
}