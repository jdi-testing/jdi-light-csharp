using System;
using System.Collections.Generic;
using System.Drawing;
using JDI.Light.Interfaces.Base;
using JDI.Light.Interfaces.Common;
using JDI.Light.Interfaces.Complex;
using JDI.Light.Selenium.DriverFactory;
using JDI.Light.Selenium.Elements.Base;
using JDI.Light.Selenium.Elements.Common;
using JDI.Light.Selenium.Elements.Complex;
using JDI.Light.Selenium.Elements.Complex.Table;
using JDI.Light.Selenium.Elements.Complex.Table.Interfaces;
using Image = JDI.Light.Selenium.Elements.Common.Image;
using static JDI.Light.Utils.ExceptionUtils;

namespace JDI.Light.Settings
{
    public class WebSettings
    {
        private static readonly Dictionary<Type, Type> DefaultInterfacesMap = new Dictionary<Type, Type>
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

        public static bool GetLatestDriver = true;
        public static string Domain;
        public static bool HasDomain => Domain != null && Domain.Contains("://");

        public static void Init()
        {
            MapInterfaceToElement.Init(DefaultInterfacesMap);

            GetFromPropertiesAvoidExceptions(p => Domain = p, "Domain");
            GetFromPropertiesAvoidExceptions(p => GetLatestDriver = p.ToLower().Equals("true") || p.ToLower().Equals("1"), "GetLatest");
            GetFromPropertiesAvoidExceptions(p =>
            {
                string[] split = null;
                if (p.Split(',').Length == 2)
                    split = p.Split(',');
                if (p.ToLower().Split('x').Length == 2)
                    split = p.ToLower().Split('x');
                if (split != null)
                    WebDriverFactory.BrowserSize = new Size(int.Parse(split[0]), int.Parse(split[1]));
            }, "BrowserSize");
        }
    }
}