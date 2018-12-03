using System;
using System.Collections.Generic;
using JDI.Light.Elements.Base;
using JDI.Light.Elements.Common;
using JDI.Light.Elements.Complex;
using JDI.Light.Elements.Complex.Table;
using JDI.Light.Elements.Complex.Table.Interfaces;
using JDI.Light.Interfaces.Base;
using JDI.Light.Interfaces.Common;
using JDI.Light.Interfaces.Complex;

namespace JDI.Light.Settings
{
    public static class MapInterfaceToElement
    {
        public static readonly Dictionary<Type, Type> DefaultInterfacesMap = new Dictionary<Type, Type>
        {
            {typeof(IBaseElement), typeof(UIElement)},
            {typeof(IButton), typeof(Button)},
            {typeof(IClickable), typeof(Clickable)},
            {typeof(IComboBox), typeof(ComboBox)},
            {typeof(ISelector), typeof(Selector)},
            {typeof(IText), typeof(Text)},
            {typeof(IImage), typeof(Image)},
            {typeof(ITextArea), typeof(TextArea)},
            {typeof(ITextField), typeof(TextField)},
            {typeof(ILabel), typeof(Label)},
            {typeof(IDropDown), typeof(Dropdown)},
            {typeof(IDropList), typeof(DropList)},
            {typeof(ITable), typeof(Table)},
            {typeof(ICheckBox), typeof(CheckBox)},
            {typeof(IRadioButtons), typeof(RadioButtons)},
            {typeof(ICheckList), typeof(CheckList)},
            {typeof(ITabs), typeof(Tabs)},
            {typeof(IMenu), typeof(Menu)},
            {typeof(IFileInput), typeof(FileInput)},
            {typeof(IDatePicker), typeof(DatePicker)},
            {typeof(ILink), typeof(Link)}
        };

        public static Type ClassFromInterface(Type clazz)
        {
            return DefaultInterfacesMap[clazz];
        }
    }
}