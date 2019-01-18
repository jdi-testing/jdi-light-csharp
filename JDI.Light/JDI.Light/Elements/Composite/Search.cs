using System;
using System.Collections.Generic;
using System.Linq;
using JDI.Light.Elements.Base;
using JDI.Light.Elements.Common;
using JDI.Light.Interfaces.Common;
using JDI.Light.Interfaces.Composite;
using JDI.Light.Utils;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Composite
{
    public class Search : TextField, ISearch
    {
        private readonly TextList _suggestions;

        protected Action<Search, string, int> ChooseSuggestionIndexAction = (s, text, selectIndex) =>
        {
            s.SearchField.Input(text);
            s.Suggestions.TextElements[selectIndex].Click();
        };

        protected Action<Search, string> FindAction = (s, text) =>
        {
            s.SearchField.NewInput(text);
            s.SearchButton.Click();
        };

        protected Func<Search, string, IList<string>> GetSuggestionsAction = (s, text) =>
        {
            s.SearchField.Input(text);
            return s.Suggestions.Texts;
        };

        protected UIElement Select;

        public Search() : this(null)
        {
        }

        public Search(By byLocator = null, By selectLocator = null, By suggestionsListLocator = null) : base(byLocator)
        {
            Select = new UIElement(selectLocator);
            _suggestions = new TextList(suggestionsListLocator);
        }

        private TextList Suggestions
        {
            get
            {
                if (_suggestions != null)
                    return _suggestions;
                throw Jdi.Assert.Exception(
                    "Suggestions list locator not specified for search. Use accordance constructor");
            }
        }

        private ITextField SearchField
        {
            get
            {
                var fields = this.GetFields(typeof(ITextField))
                    .Select(fi => (ITextField)fi.GetValue(this)).Where(b => ((UIElement)b).Displayed).ToList();
                switch (fields.Count)
                {
                    case 0:
                        throw Jdi.Assert.Exception($"Can't find any buttons on form '{ToString()}'.");
                    case 1:
                        return fields[0];
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
                var fields = this.GetFields(typeof(IButton))
                    .Select(fi => (IButton)fi.GetValue(this)).Where(b => ((UIElement)b).Displayed).ToList();
                switch (fields.Count)
                {
                    case 0:
                        throw Jdi.Assert.Exception($"Can't find any buttons on form '{ToString()}'.");
                    case 1:
                        return fields[0];
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

        public void ChooseSuggestion(string text, int selectIndex)
        {
            Invoker.DoActionWithWait($"Search for text '{text}' and choose suggestion '{selectIndex}'",
                () => ChooseSuggestionIndexAction(this, text, selectIndex));
        }

        public IList<string> GetSuggestions(string text)
        {
            return Invoker.DoActionWithResult($"Get all suggestions for input '{text}'",
                () => GetSuggestionsAction(this, text));
        }
    }
}