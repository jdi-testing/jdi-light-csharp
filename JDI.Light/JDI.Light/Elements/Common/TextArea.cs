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
            Invoker.DoAction("Input several lines of text in textarea",
                el =>
                {
                    ClearAction(this);
                    InputAction(this, string.Join("\n", textLines));
                });
        }

        public void AddNewLine(string textLine)
        {
            Invoker.DoAction("Add text from new line in textarea",
                el => InputAction(this, "\n" + textLine));
        }

        public string[] GetLines()
        {
            return Invoker.DoActionResultWithResult("Get text as lines", el => Regex.Split(GetTextFunc(this), "\\\\n"));
        }
    }
}