using System;
using System.Collections.Generic;
using System.Linq;
using JDI.Light.Asserts;
using JDI.Light.Elements.Base;
using JDI.Light.Factories;
using OpenQA.Selenium;
using static System.String;

namespace JDI.Light.Elements.Complex.Table
{
    public class Table : UIElement
    {
        public int FirstColumnIndex { get; set; } = -1;
        public int[] ColumnsMapping { get; set; } = { };
        public bool HeaderIsRow { get; set; } = true;

        public Table(By locator) : base(locator)
        {
            TableHeadersLocator = By.XPath(".//tr/th");
            TableBodyLocator = By.XPath(".//tbody");
            TableFooterLocator = By.XPath(".//tfoot");
            TableRowsLocator = By.XPath(".//tr");
            TableCellsLocator = By.XPath(".//td");
            TableRowLocator = By.XPath(".//tr[{0}]/td");
            CellLocator = By.XPath(".//tr[{1}]/td[{0}]");
            ColumnLocator = By.XPath(".//tr/td[{0}]");
        }

        public By TableHeadersLocator { get; set; }
        public By TableBodyLocator { get; set; }
        public By TableFooterLocator { get; set; }
        public By TableRowsLocator { get; set; }
        public By TableCellsLocator { get; set; }
        public By TableRowLocator { get; set; }
        public By CellLocator { get; set; }
        public By ColumnLocator { get; set; }

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

        public string Cell(int colNum, int rowNum)
        {
            return WebCell(colNum, rowNum).Text;
        }

        public Line Column(int colNum)
        {
            var headerValues = Headers.Select(h => h.Text).ToList();
            return new Line(WebColumn(colNum).Select(c => c.Text).ToList(), headerValues);
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

        public IWebElement WebCell(int colNum, int rowNum)
        {
            return GetCell(colNum, rowNum);
        }

        public IEnumerable<IWebElement> WebColumn(int colNum)
        {
            if (colNum < 1)
            {
                throw new ArgumentException($"Columns numeration starts from 1 (but requested index is {colNum})");
            }
            return GetColumn(colNum);
        }

        public IEnumerable<IWebElement> GetRow(int rowNum)
        {
            var elements = GetRowByIndex(GetRowIndex(rowNum));
            var result = new List<IWebElement>();
            if (FirstColumnIndex <= 1 || ColumnsMapping.Length <= 0) { return elements; }
            for (var i = 1; i <= Headers.Count; i++)
            {
                result.Add(elements.ElementAt(i - 1));
            }
            return result;
        }

        public IEnumerable<IWebElement> GetRowByIndex(int rowNum)
        {
            return FindElements(FillByTemplate(TableRowLocator, rowNum));
        }

        public IWebElement GetCell(int colNum, int rowNum)
        {
            return FindElement(FillByTemplate(CellLocator, GetColumnIndex(colNum), GetRowIndex(rowNum), this));
        }

        public IEnumerable<IWebElement> GetColumn(int colNum)
        {
            return FindElements(FillByTemplate(ColumnLocator, GetColumnIndex(colNum), this));
        }

        public int GetRowIndex(int rowNum)
        {
            if (!HeaderIsRow) { return HeaderIsRow ? rowNum + 1 : rowNum; }
            var firstRow = GetRowByIndex(1).Select(c => c.Text).ToList();
            HeaderIsRow = firstRow.Count == 0 || Headers.Select(h => h.Text).Union(firstRow).Any();
            return HeaderIsRow ? rowNum + 1 : rowNum;
        }

        private int GetColumnIndex(int index)
        {
            if (FirstColumnIndex > 1)
            {
                return index + FirstColumnIndex - 1;
            }
            return ColumnsMapping.Length > 0 ? ColumnsMapping[index - 1] : index;
        }

        public static By FillByTemplate(By by, params object[] args)
        {
            var byLocator = GetByLocator(by);
            try
            {
                byLocator = Format(byLocator, args);
            }
            catch (FormatException)
            {
                throw new FormatException("Bad locator template '" + byLocator + "'. Args: " + Join("", args));
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