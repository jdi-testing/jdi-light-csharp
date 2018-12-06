using System.Text.RegularExpressions;
using JDI.Light.Interfaces.Common;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Common
{
    public class TextArea : TextField, ITextArea
    {
        public TextArea() : this(null)
        {
        }

        public TextArea(By byLocator = null)
            : base(byLocator)
        {
        }

        public void InputLines(params string[] textLines)
        {
            Invoker.DoActionWithWait("Input several lines of text in textarea",
                () =>
                {
                    Clear();
                    Input(string.Join("\n", textLines));
                });
        }

        public void AddNewLine(string textLine)
        {
            Invoker.DoActionWithWait("Add text from new line in textarea",
                () => Input("\n" + textLine));
        }

        public string[] GetLines()
        {
            return Invoker.DoActionWithResult("Get text as lines", () => Regex.Split(GetTextFunc(this), "\\\\n"));
        }
    }
}