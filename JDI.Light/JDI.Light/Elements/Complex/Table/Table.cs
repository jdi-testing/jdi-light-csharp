using System;
using System.Collections.Generic;
using System.Linq;
using JDI.Light.Asserts;
using JDI.Light.Elements.Base;
using JDI.Light.Factories;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Complex.Table
{
    public class Table : UIElement
    {
        public int FirstColumnIndex = -1;
        public int[] ColumnsMapping = { };
        public bool HeaderIsRow = true;

        public Table(By locator) : base(locator)
        {
            TableHeadersLocator = By.XPath(".//tr/th");
            TableBodyLocator = By.XPath(".//tbody");
            TableFooterLocator = By.XPath(".//tfoot");
            TableRowsLocator = By.XPath(".//tr");
            TableCellsLocator = By.XPath(".//td");
            TableRowLocator = By.XPath(".//tr[{0}]/td");
        }

        public By TableHeadersLocator { get; set; }
        public By TableBodyLocator { get; set; }
        public By TableFooterLocator { get; set; }
        public By TableRowsLocator { get; set; }
        public By TableCellsLocator { get; set; }
        public By TableRowLocator { get; set; }

        public List<UIElement> Headers => Body.FindElements(TableHeadersLocator)
            .Select(e => UIElementFactory.CreateInstance<UIElement>(TableHeadersLocator, Body, e)).ToList();
        public UIElement Body => UIElementFactory.CreateInstance<UIElement>(TableBodyLocator, this);
        public UIElement Footer => UIElementFactory.CreateInstance<UIElement>(TableFooterLocator, this);
        public List<UIElement> Rows => Body.FindElements(TableRowsLocator)
            .Select(e => UIElementFactory.CreateInstance<UIElement>(TableRowsLocator, Body, e)).ToList();

        public List<List<UIElement>> Cells => Rows.Select(r => r.FindElements(TableCellsLocator)
            .Select(e => UIElementFactory.CreateInstance<UIElement>(TableCellsLocator, r, e)).ToList()).ToList();

        public Line Row(params TableMatcher[] matchers)
        {
            var lines = TableMatcher.Table_Matcher.Invoke(this, matchers);
            if (lines == null || lines.Count.Equals(0))
            {
                return null;
            }
            var result = new List<string>();
            for (var i = 0; i < Headers.Count; i++)
            {
                result.Add(lines.ElementAt(i).Text);
            }
            return Line.InitLine(result, Headers.Select(h => h.Text).ToList());
        }

        public Line Row(int rowNum)
        {
            var headerValues = Headers.Select(h => h.Text).ToList();
            return new Line(WebRow(rowNum), headerValues);
        }

        public IEnumerable<IWebElement> WebRow(int rowNum)
        {
            if (rowNum < 1)
            {
                throw new ArgumentException($"Rows numeration starts from 1, but requested index is {rowNum}");
            }
            if (rowNum > Rows.Count)
            {
                throw new ArgumentException($"Table has {Rows.Count} rows, but requested index is {rowNum}");
            }
            return GetRow(rowNum);
        }

        public IEnumerable<IWebElement> GetRow(int rowNum)
        {
            var elements = GetRowByIndex(GetRowIndex(rowNum));
            //elements = elements.Where(el => el.Displayed).ToList();
            var result = new List<IWebElement>();
            if (FirstColumnIndex <= 1 && ColumnsMapping.Length <= 0) return elements;
            for (var i = 1; i <= Headers.Count; i++)
            {
                result.Add(elements.ElementAt(i - 1));
            }
            return elements;
        }

        public IEnumerable<IWebElement> GetRowByIndex(int rowNum)
        {
            return FindElements(FillByTemplate(TableRowLocator, rowNum));
        }

        public int GetRowIndex(int rowNum)
        {
            if (!HeaderIsRow) return HeaderIsRow ? rowNum + 1 : rowNum;
            var firstRow = GetRowByIndex(1).Select(c=> c.Text).ToList();
            HeaderIsRow = firstRow.Count == 0 || Headers.Select(h => h.Text).Union(firstRow).Any();
            return HeaderIsRow ? rowNum + 1 : rowNum;
        }

        public By FillByTemplate(By by, params object[] args)
        {
            var byLocator = GetByLocator(by);
            try
            {
                byLocator = string.Format(byLocator, args);
            }
            catch (FormatException)
            {
                throw new FormatException("Bad locator template '" + byLocator + "'. Args: " + string.Join("", args));
            }
            return By.XPath(byLocator);
        }

        public static string GetByLocator(By by)
        {
            var byAsString = by.ToString();
            var index = byAsString.IndexOf(": ", StringComparison.Ordinal) + 2;
            return byAsString.Substring(index);
        }

        public int TableSize => Rows.Count - 1;

        public bool IsTableEmpty => TableSize == 0;

        public new TableAssert Is()
        {
            return new TableAssert(this);
        }

        public new TableAssert AssertThat()
        {
            return Is();
        }
    }
}