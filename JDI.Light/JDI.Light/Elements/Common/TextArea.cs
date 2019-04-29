using System;
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

        public void SetLines(params string[] textLines)
        {
            CheckEnabled(true);
            Invoker.DoActionWithWait("Input several lines of text in textarea",
                () =>
                {
                    Clear();
                    Input(string.Join("\n", textLines));
                });
        }

        public void AddNewLine(string textLine)
        {
            CheckEnabled(true);
            Invoker.DoActionWithWait("Add text from new line in textarea",
                () => SendKeys("\n" + textLine));
        }

        public string[] GetLines()
        {
            return Invoker.DoActionWithResult("Get text as lines", () => Regex.Split(GetTextFunc(this), "\\r\\n"));
        }

        public int Rows() => Convert.ToInt32(GetAttribute("rows"));

        public int Cols() => Convert.ToInt32(GetAttribute("cols"));

        public int MinLength() => Convert.ToInt32(GetAttribute("minlength"));

        public int MaxLength() => Convert.ToInt32(GetAttribute("maxlength"));

        public override string GetText()
        {
            return GetTextFunc(this);
        }

        public override string GetValue()
        {
            return GetTextFunc(this);
        }
    }
}