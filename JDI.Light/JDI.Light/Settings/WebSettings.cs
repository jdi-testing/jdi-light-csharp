using System;
using System.Collections.Generic;

namespace JDI.Light.Settings
{
    public class WebSettings
    {
        public static List<string> SmartSearchLocators = new List<string>();

        public static Func<string, string> SmartSearchName = (smartSearchName) =>
            smartSearchName.Substring(0, smartSearchName.IndexOf("-")).Trim();
    }
}
