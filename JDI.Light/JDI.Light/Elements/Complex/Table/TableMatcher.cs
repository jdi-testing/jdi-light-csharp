using System;
using System.Collections.ObjectModel;
using System.Linq;
using OpenQA.Selenium;
using static System.String;

namespace JDI.Light.Elements.Complex.Table
{
    public class TableMatcher
    {
        private readonly string _locator;
        private readonly Column _column;
        private readonly string _name;

        private TableMatcher(string locator, Column column, string name)
        {
            _locator = locator;
            _column = column;
            _name = name;
        }

        public static Func<Table, TableMatcher[], ReadOnlyCollection<IWebElement>> Table_Matcher = (table, matchers) =>
        {
            string locator = $"./{Join("", matchers.Select(m => m.GetLocator(table) + "/.."))}/td";
            return table.FindElements(By.XPath(locator));
        };

        public static TableMatcher HasValue(string value, Column column)
        {
            return new TableMatcher("/td[{0}][text()='" + value + "']", column, "has '" + value + "'");
        }
        public static TableMatcher ContainsValue(string value, Column column)
        {
            return new TableMatcher("/td[{0}][contains(text(),'" + value + "')]", column, "contains '" + value + "'");
        }

        public string GetLocator(Table table)
        {
            return Format(_locator, _column.GetIndex(table.Headers));
        }

        public override string ToString()
        {
            return _name;
        }
    }
}
