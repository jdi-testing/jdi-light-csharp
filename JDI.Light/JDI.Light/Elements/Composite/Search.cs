using System;
using System.Linq;
using JDI.Light.Elements.Base;
using JDI.Light.Elements.Common;
using JDI.Light.Extensions;
using JDI.Light.Interfaces.Common;
using JDI.Light.Interfaces.Composite;
using JDI.Light.Utils;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Composite
{
    public class Search : TextField, ISearch
    {
        protected Action<Search, string> FindAction = (s, text) =>
        {
            s.SearchField.Input(text);
            s.SearchButton.Click();
        };

        public Search() : this(null)
        {
        }

        public Search(By byLocator) : base(byLocator)
        {
        }

        private ITextField SearchField
        {
            get
            {
                var textFields = this.GetMembers(typeof(ITextField))
                    .Select(fi => (ITextField)fi.GetMemberValue(this)).Where(b => ((UIElement)b).Displayed).ToList();
                switch (textFields.Count)
                {
                    case 0:
                        throw Jdi.Assert.Exception($"Can't find any buttons on form '{ToString()}'.");
                    case 1:
                        return textFields[0];
                    default:
                        throw Jdi.Assert.Exception(
                            $"Form '{ToString()}' have more than 1 button. Use submit(entity, buttonName) for this case instead");
                }
            }
        }

        protected IButton SearchButton
        {
            get
            {
                var buttons = this.GetMembers(typeof(IButton))
                    .Select(fi => (IButton)fi.GetMemberValue(this)).Where(b => ((UIElement)b).Displayed).ToList();
                switch (buttons.Count)
                {
                    case 0:
                        throw Jdi.Assert.Exception($"Can't find any buttons on form '{ToString()}'.");
                    case 1:
                        return buttons[0];
                    default:
                        throw Jdi.Assert.Exception(
                            $"Form '{ToString()}' have more than 1 button. Use submit(entity, buttonName) for this case instead");
                }
            }
        }

        public void Find(string text)
        {
            Invoker.DoActionWithWait($"Search text '{text}'", () => FindAction(this, text));
        }
    }
}