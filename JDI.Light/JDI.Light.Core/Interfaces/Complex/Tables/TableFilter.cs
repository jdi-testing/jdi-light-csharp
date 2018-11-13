using System;
using System.Text.RegularExpressions;
using JDI.Web.Selenium.Elements.Composite;

namespace JDI.Core.Interfaces.Complex.Tables
{
    public class TableFilter
    {
        public string name;
        public CheckPageTypes type;
        public string value;

        public TableFilter(string template)
        {
            string[] split;

            if (Regex.IsMatch(template, "[^=]+\\*=[^=]*"))
            {
                split = template.Split(new[] {"\\*="}, StringSplitOptions.None);
                name = split[0];
                value = split[1];
                type = CheckPageTypes.Match;
                return;
            }

            if (Regex.IsMatch(template, "[^=]+~=[^=]*"))
            {
                split = template.Split(new[] {"~="}, StringSplitOptions.None);
                name = split[0];
                value = split[1];
                type = CheckPageTypes.Contains;
                return;
            }

            if (Regex.IsMatch(template, "[^=] +=[^=] * "))
            {
                split = template.Split(new[] {"="}, StringSplitOptions.None);
                name = split[0];
                value = split[1];
                type = CheckPageTypes.Equal;
                return;
            }

            throw new ArgumentException("Wrong searchCriteria for Cells: " + template);
        }
    }
}