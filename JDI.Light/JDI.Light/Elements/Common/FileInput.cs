using System;
using JDI.Light.Elements.Base;
using JDI.Light.Interfaces.Common;

namespace JDI.Light.Elements.Common
{
    public class FileInput : TextField, IFileInput
    {
        protected new Action<UIElement, string> SetValueAction = (el, val) => ((FileInput) el).Input(val);

        public FileInput() : this(null)
        {
        }

        public FileInput(By byLocator = null)
            : base(byLocator)
        {
        }
    }
}