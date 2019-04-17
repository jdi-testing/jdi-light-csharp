using System;
using System.Collections.Generic;
using JDI.Light.Elements.Base;
using JDI.Light.Elements.Common;
using JDI.Light.Elements.Complex;
using JDI.Light.Interfaces.Base;
using JDI.Light.Interfaces.Common;
using JDI.Light.Interfaces.Complex;
using OpenQA.Selenium;

namespace JDI.Light.Settings
{
    public static class MapInterfaceToElement
    {
        public static readonly Dictionary<Type, Type> DefaultInterfacesMap = new Dictionary<Type, Type>
        {
            {typeof(IWebElement), typeof(UIElement)},
            {typeof(IBaseElement), typeof(UIElement)},
            {typeof(IBaseUIElement), typeof(UIElement)},
            {typeof(IButton), typeof(Button)},
            {typeof(ITextElement), typeof(TextElement)},
            {typeof(IImage), typeof(Image)},
            {typeof(ITextArea), typeof(TextArea)},
            {typeof(ITextField), typeof(TextField)},
            {typeof(ILabel), typeof(Label)},
            {typeof(ICheckBox), typeof(CheckBox)},
            {typeof(IDatePicker), typeof(DatePicker)},
            {typeof(ILink), typeof(Link)},
            {typeof(ICheckList), typeof(CheckList) },
            {typeof(IRadioButtons), typeof(RadioButton) },
            {typeof(IDropDown), typeof(DropDown) },
            {typeof(IDataList), typeof(DataList) },
            {typeof(IDateTimeSelector), typeof(DateTimeSelector) },
            {typeof(IRange), typeof(Range) },
            {typeof(IProgressBar), typeof(ProgressBar) }
        };

        public static Type ClassFromInterface(Type clazz)
        {
            return DefaultInterfacesMap[clazz];
        }
    }
}