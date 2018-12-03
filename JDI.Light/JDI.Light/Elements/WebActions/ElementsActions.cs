using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using JDI.Light.Elements.Base;
using JDI.Light.Extensions;

namespace JDI.Light.Elements.WebActions
{
    public class ElementsActions
    {
        public ElementsActions(ActionInvoker<UIElement> invoker)
        {
            Invoker = invoker;
        }

        public ActionInvoker<UIElement> Invoker { get; set; }

        // Value Actions
        public string GetValue(Func<UIElement, string> getValueFunc)
        {
            return Invoker.DoActionResultWithResult("Get value", getValueFunc);
        }

        public void SetValue(string value, Action<UIElement, string> setValueAction)
        {
            Invoker.DoAction("Get value", el => setValueAction(el, value));
        }

        // Text Actions
        public string GetText(Func<UIElement, string> getTextAction)
        {
            return Invoker.DoActionResultWithResult("Get text", getTextAction);
        }

        public string WaitText(string text, Func<UIElement, string> getTextAction)
        {
            Func<UIElement, Func<string>> textAction = el => () => getTextAction(el);
            return Invoker.DoActionResultWithResult($"Wait text contains '{text}'",
                el => textAction(el).GetByCondition(t => t.Contains(text)));
        }

        public string WaitMatchText(string regEx, Func<UIElement, string> getTextAction)
        {
            Func<UIElement, Func<string>> textAction = el => () => getTextAction(el);
            return Invoker.DoActionResultWithResult($"Wait text match regex '{regEx}'",
                el => textAction(el).GetByCondition(t => t.Matches(regEx)));
        }

        // Selector
        public void Select(string name, Action<UIElement, string> selectAction)
        {
            Invoker.DoAction($"Select '{name}'", el => selectAction(el, name));
        }

        public void Select(int index, Action<UIElement, int> selectByIndexAction)
        {
            Invoker.DoAction($"Select '{index}'", el => selectByIndexAction(el, index));
        }

        public void Hover(string name, Action<UIElement, string> hoverAction)
        {
            Invoker.DoAction($"Hover '{name}'", el => hoverAction(el, name));
        }

        public bool Selected(string name, Func<UIElement, string, bool> isSelectedAction)
        {
            return Invoker.DoActionResultWithResult($"Wait is '{name}' selected", el => isSelectedAction(el, name));
        }

        public string Selected(Func<UIElement, string> isSelectedAction)
        {
            return Invoker.DoActionResultWithResult("Get Selected element name", isSelectedAction);
        }

        public int SelectedIndex(Func<UIElement, int> isSelectedAction)
        {
            return Invoker.DoActionResultWithResult("Get Selected element index", isSelectedAction);
        }

        //MultiSelector
        public void Select(Action<UIElement, IList<string>> selectListAction, params string[] names)
        {
            Invoker.DoAction($"Select '{names.FormattedJoin()}'", el => selectListAction(el, names));
        }

        public void Select(Action<UIElement, IList<int>> selectListAction, int[] indexes)
        {
            var listIndexes = indexes.Select(i => i.ToString()).ToList();
            Invoker.DoAction($"Select '{listIndexes.FormattedJoin()}'", el => selectListAction(el, indexes));
        }

        public List<string> AreSelected(Func<UIElement, IList<string>> getNames,
            Func<UIElement, string, bool> waitSelectedAction)
        {
            return Invoker.DoActionResultWithResult("Are selected", el =>
                getNames(el).Where(name => waitSelectedAction(el, name)).ToList());
        }

        public void WaitSelected(Func<UIElement, string, bool> waitSelectedAction, params string[] names)
        {
            var result = Invoker.DoActionResultWithResult($"Are deselected '{names.FormattedJoin()}'",
                el => names.All(name => waitSelectedAction(el, name)));
            JDI.Assert.IsTrue(result);
        }

        public List<string> AreDeselected(Func<UIElement, IList<string>> getNames,
            Func<string, bool> waitSelectedAction)
        {
            return Invoker.DoActionResultWithResult("Are deselected", el =>
                getNames(el).Where(name => !waitSelectedAction.Invoke(name)).ToList());
        }

        public void Expand(Action<UIElement> expandAction)
        {
            Invoker.DoAction("Expand Element", expandAction);
        }

        public void WaitDeselected(Func<UIElement, string, bool> waitSelectedAction, params string[] names)
        {
            var result = Invoker.DoActionResultWithResult($"Are deselected '{names.FormattedJoin()}'",
                el => names.All(name => !waitSelectedAction(el, name)));
            JDI.Assert.IsTrue(result);
        }
    }
}