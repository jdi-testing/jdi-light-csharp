using JDI.Light.Interfaces.Common;
using OpenQA.Selenium;

namespace JDI.Light.Selenium.Elements.Common
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
            Actions.InputLines(ClearAction, InputAction, textLines);
        }

        public void AddNewLine(string textLine)
        {
            Actions.AddNewLine(textLine, InputAction);
        }

        public string[] GetLines()
        {
            return Actions.GetLines(GetTextFunc);
        }
    }
}