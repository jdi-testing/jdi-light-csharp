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

        // Element Actions
        public bool IsDisplayed(Func<UIElement, bool> isDisplayed)
        {
            return Invoker.DoJActionResult("Is element displayed", isDisplayed);
        }

        public bool IsHidden(Func<UIElement, bool> isHidden)
        {
            return Invoker.DoJActionResult("Is element hidden", isHidden);
        }

        public void WaitDisplayed(Func<UIElement, bool> isDisplayed)
        {
            Invoker.DoJActionResult("Wait element displayed", isDisplayed);
        }

        public void WaitVanished(Func<UIElement, bool> isVanished)
        {
            Invoker.DoJActionResult("Wait element vanished", isVanished);
        }

        // Value Actions
        public string GetValue(Func<UIElement, string> getValueFunc)
        {
            return Invoker.DoJActionResult("Get value", getValueFunc);
        }

        public void SetValue(string value, Action<UIElement, string> setValueAction)
        {
            Invoker.DoJAction("Get value", el => setValueAction(el, value));
        }

        // Click Action
        public void Click(Action<UIElement> clickAction)
        {
            Invoker.DoJAction("Click on Element", clickAction);
        }

        // Text Actions
        public string GetText(Func<UIElement, string> getTextAction)
        {
            return Invoker.DoJActionResult("Get text", getTextAction);
        }

        public string WaitText(string text, Func<UIElement, string> getTextAction)
        {
            Func<UIElement, Func<string>> textAction = el => () => getTextAction(el);
            return Invoker.DoJActionResult($"Wait text contains '{text}'",
                el => textAction(el).GetByCondition(t => t.Contains(text)));
        }

        public string WaitMatchText(string regEx, Func<UIElement, string> getTextAction)
        {
            Func<UIElement, Func<string>> textAction = el => () => getTextAction(el);
            return Invoker.DoJActionResult($"Wait text match regex '{regEx}'",
                el => textAction(el).GetByCondition(t => t.Matches(regEx)));
        }

        // Check/Select Actions
        public bool Selected(Func<UIElement, bool> isSelectedAction)
        {
            return Invoker.DoJActionResult("Is Selected", isSelectedAction);
        }

        public void Check(Action<UIElement> checkAction)
        {
            Invoker.DoJAction("Check Checkbox", checkAction);
        }

        public void Uncheck(Action<UIElement> uncheckAction)
        {
            Invoker.DoJAction("Uncheck Checkbox", uncheckAction);
        }

        public bool IsChecked(Func<UIElement, bool> isCheckedAction)
        {
            return Invoker.DoJActionResult("IsChecked",
                isCheckedAction,
                result => "Checkbox is " + (result ? "checked" : "unchecked"));
        }

        // Input Actions
        public void InputLines(Action<UIElement> clearAction, Action<UIElement, string> inputAction,
            params string[] textLines)
        {
            Invoker.DoJAction("Input several lines of text in textarea",
                el =>
                {
                    clearAction(el);
                    inputAction(el, string.Join("\n", textLines));
                });
        }

        public void AddNewLine(string textLine, Action<UIElement, string> inputAction)
        {
            Invoker.DoJAction("Add text from new line in textarea",
                el => inputAction(el, "\n" + textLine));
        }

        public string[] GetLines(Func<UIElement, string> getTextAction)
        {
            return Invoker.DoJActionResult("Get text as lines", el => Regex.Split(getTextAction(el), "\\\\n"));
        }

        public void Input(string text, Action<UIElement, string> inputAction)
        {
            Invoker.DoJAction("Input text '" + text + "' in text field",
                el => inputAction(el, text));
        }

        public void Clear(Action<UIElement> clearAction)
        {
            Invoker.DoJAction("Clear text field", clearAction);
        }

        public void Focus(Action<UIElement> focusAction)
        {
            Invoker.DoJAction("Focus on text field", focusAction);
        }

        // Selector
        public void Select(string name, Action<UIElement, string> selectAction)
        {
            Invoker.DoJAction($"Select '{name}'", el => selectAction(el, name));
        }

        public void Select(int index, Action<UIElement, int> selectByIndexAction)
        {
            Invoker.DoJAction($"Select '{index}'", el => selectByIndexAction(el, index));
        }

        public void Hover(string name, Action<UIElement, string> hoverAction)
        {
            Invoker.DoJAction($"Hover '{name}'", el => hoverAction(el, name));
        }

        public bool Selected(string name, Func<UIElement, string, bool> isSelectedAction)
        {
            return Invoker.DoJActionResult($"Wait is '{name}' selected", el => isSelectedAction(el, name));
        }

        public string Selected(Func<UIElement, string> isSelectedAction)
        {
            return Invoker.DoJActionResult("Get Selected element name", isSelectedAction);
        }

        public int SelectedIndex(Func<UIElement, int> isSelectedAction)
        {
            return Invoker.DoJActionResult("Get Selected element index", isSelectedAction);
        }

        //MultiSelector
        public void Select(Action<UIElement, IList<string>> selectListAction, params string[] names)
        {
            Invoker.DoJAction($"Select '{names.FormattedJoin()}'", el => selectListAction(el, names));
        }

        public void Select(Action<UIElement, IList<int>> selectListAction, int[] indexes)
        {
            var listIndexes = indexes.Select(i => i.ToString()).ToList();
            Invoker.DoJAction($"Select '{listIndexes.FormattedJoin()}'", el => selectListAction(el, indexes));
        }

        public List<string> AreSelected(Func<UIElement, IList<string>> getNames,
            Func<UIElement, string, bool> waitSelectedAction)
        {
            return Invoker.DoJActionResult("Are selected", el =>
                getNames(el).Where(name => waitSelectedAction(el, name)).ToList());
        }

        public void WaitSelected(Func<UIElement, string, bool> waitSelectedAction, params string[] names)
        {
            var result = Invoker.DoJActionResult($"Are deselected '{names.FormattedJoin()}'",
                el => names.All(name => waitSelectedAction(el, name)));
            JDI.Assert.IsTrue(result);
        }

        public List<string> AreDeselected(Func<UIElement, IList<string>> getNames,
            Func<string, bool> waitSelectedAction)
        {
            return Invoker.DoJActionResult("Are deselected", el =>
                getNames(el).Where(name => !waitSelectedAction.Invoke(name)).ToList());
        }

        public void Expand(Action<UIElement> expandAction)
        {
            Invoker.DoJAction("Expand Element", expandAction);
        }

        public void WaitDeselected(Func<UIElement, string, bool> waitSelectedAction, params string[] names)
        {
            var result = Invoker.DoJActionResult($"Are deselected '{names.FormattedJoin()}'",
                el => names.All(name => !waitSelectedAction(el, name)));
            JDI.Assert.IsTrue(result);
        }
    }
}