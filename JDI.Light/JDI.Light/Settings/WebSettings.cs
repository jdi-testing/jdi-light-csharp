using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using JDI.Light.Interfaces;
using JDI.Light.Interfaces.Base;
using JDI.Light.Interfaces.Common;
using JDI.Light.Interfaces.Complex;
using JDI.Light.Logging;
using JDI.Light.Selenium.DriverFactory;
using JDI.Light.Selenium.Elements.Base;
using JDI.Light.Selenium.Elements.Common;
using JDI.Light.Selenium.Elements.Complex;
using JDI.Light.Selenium.Elements.Complex.Table;
using JDI.Light.Selenium.Elements.Complex.Table.Interfaces;
using OpenQA.Selenium;
using Image = JDI.Light.Selenium.Elements.Common.Image;

namespace JDI.Light.Settings
{
    public class WebSettings : JDI
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
        private static WebDriverFactory _webDriverFactory;

        public static bool GetLatestDriver = true;
        public static string Domain;
        public static bool HasDomain => Domain != null && Domain.Contains("://");
        public static IWebDriver WebDriver => WebDriverFactory.GetDriver();
        public static WebDriverFactory WebDriverFactory =>
            _webDriverFactory ?? (_webDriverFactory = new WebDriverFactory());
        public static IJavaScriptExecutor JsExecutor => DriverFactory.GetDriver() as IJavaScriptExecutor;

        public static void Init(ILogger logger, IAssert assert,
            WebTimeoutSettings timeouts = null, IDriverFactory<IWebDriver> driverFactory = null)
        {
            DriverFactory = driverFactory ?? new WebDriverFactory();
            Assert = assert;
            Timeouts = timeouts ?? new WebTimeoutSettings();
            Logger = logger ?? new ConsoleLogger();
            MapInterfaceToElement.Init(DefaultInterfacesMap);
        }

        public static void InitFromProperties(ILogger logger = null, IAssert assert = null,
            WebTimeoutSettings timeouts = null, IDriverFactory<IWebDriver> driverFactory = null)
        {
            Init(logger, assert, timeouts, driverFactory);
            JDI.InitFromProperties();
            FillFromSettings(p => Domain = p, "Domain");
            FillFromSettings(p => DriverFactory.DriverPath = p, "DriversFolder");
            FillFromSettings(p => GetLatestDriver = p.ToLower().Equals("true") || p.ToLower().Equals("1"), "GetLatest");
            FillFromSettings(p =>
            {
                p = p.ToLower();
                if (p.Equals("soft"))
                    p = "any,multiple";
                if (p.Equals("strict"))
                    p = "visible,single";
                if (p.Split(',').Length != 2) return;
                var parameters = p.Split(',').ToList();
                if (parameters.Contains("visible") || parameters.Contains("displayed"))
                    WebDriverFactory.ElementSearchCriteria = el => el.Displayed;
                if (parameters.Contains("any") || parameters.Contains("all"))
                    WebDriverFactory.ElementSearchCriteria = el => el != null;
                if (parameters.Contains("single") || parameters.Contains("displayed"))
                    WebDriverFactory.OnlyOneElementAllowedInSearch = true;
                if (parameters.Contains("multiple") || parameters.Contains("displayed"))
                    WebDriverFactory.OnlyOneElementAllowedInSearch = false;
            }, "SearchElementStrategy");

            FillFromSettings(p =>
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