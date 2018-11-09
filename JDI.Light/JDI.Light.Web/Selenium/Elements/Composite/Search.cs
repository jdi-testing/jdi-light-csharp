using System;
using System.Collections.Generic;
using JDI.Core;
using JDI.Core.Interfaces.Common;
using JDI.Core.Interfaces.Complex;
using JDI.Core.Settings;
using JDI.Core.Utils;
using JDI.Web.Selenium.DriverFactory;
using JDI.Web.Selenium.Elements.Base;
using JDI.Web.Selenium.Elements.Common;
using JDI.Web.Selenium.Elements.Complex;
using OpenQA.Selenium;

namespace JDI.Web.Selenium.Elements.Composite
{
    public class Search : TextField, ISearch
    {
        protected Clickable Select;
        private readonly TextList _suggestions;

        public Search() : this(null) { }

        public Search(By byLocator = null, By selectLocator = null, By suggestionsListLocator = null) : base(byLocator)
        {
            Select = new Clickable(selectLocator);
            _suggestions = new TextList(suggestionsListLocator);
        }

        protected Action<Search, string> FindAction = (s, text) =>
        {
            s.SearchField.NewInput(text);
            s.SearchButton.Click();
        };

        protected Action<Search, string, string> ChooseSuggestionNameAction = (s, text, selectValue) =>
        {
            s.SearchField.Input(text);
            s.GetElement(selectValue).Click();
        };

        protected Action<Search, string, int> ChooseSuggestionIndexAction = (s, text, selectIndex) =>
        {
            s.SearchField.Input(text);
            s.Suggestions.TextElements[selectIndex].Click();
        };

        protected Func<Search, string, IList<string>> GetSuggesionsAction = (s, text) =>
        {
            s.SearchField.Input(text);
            return s.Suggestions.Texts;
        };

        public void Find(string text)
        {
            Invoker.DoJAction($"Search text '{text}'", s => FindAction(this, text)) ;
        }

        public void ChooseSuggestion(string text, string selectValue)
        {
            Invoker.DoJAction($"Search for text '{text}' and choose suggestion '{selectValue}'",
                s => ChooseSuggestionNameAction(this, text, selectValue));
        }

        public void ChooseSuggestion(string text, int selectIndex)
        {
            Invoker.DoJAction($"Search for text '{text}' and choose suggestion '{selectIndex}'",
                s => ChooseSuggestionIndexAction(this, text, selectIndex));
        }

        public IList<string> GetSuggesions(string text)
        {
            return Invoker.DoJActionResult($"Get all suggestions for input '{text}'",
                s => GetSuggesionsAction(this, text));
        }

        private TextList Suggestions
        {
            get {
                if (_suggestions != null)
                    return _suggestions;
                throw JDISettings.Exception("Suggestions list locator not specified for search. Use accordance constructor");
            }
        }

        private Clickable GetElement(string name)
        {
            if (Select != null)
                return Copy(Select, Locator.FillByTemplate(name));
            throw JDISettings.Exception("Select locator not specified for search. Use accordance constructor");
        }

        private ITextField SearchField
        {
            get
            {
                var fields = this.GetFields(typeof(ITextField));
                switch (fields.Count)
                {
                    case 0:
                        throw JDISettings.Exception($"Can't find any buttons on form '{ToString()}'.");
                    case 1:
                        return (ITextField) fields[0].GetValue(this);
                    default:
                        throw JDISettings.Exception($"Form '{ToString()}' have more than 1 button. Use submit(entity, buttonName) for this case instead");
                }
            }
        }

        protected IButton SearchButton
        {
            get
            {
                var fields = this.GetFields(typeof(IButton));
                switch (fields.Count)
                {
                    case 0:
                        throw JDISettings.Exception($"Can't find any buttons on form '{ToString()}'.");
                    case 1:
                        return (IButton) fields[0].GetValue(this);
                    default:
                        throw JDISettings.Exception(
                            $"Form '{ToString()}' have more than 1 button. Use submit(entity, buttonName) for this case instead");
                }
            }
        }
    }
}