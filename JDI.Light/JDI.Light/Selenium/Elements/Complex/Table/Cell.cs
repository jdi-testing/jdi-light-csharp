using System;
using System.Linq;
using JDI.Light.Extensions;
using JDI.Light.Selenium.DriverFactory;
using JDI.Light.Selenium.Elements.Base;
using JDI.Light.Selenium.Elements.Complex.Table.Interfaces;
using JDI.Light.Settings;
using OpenQA.Selenium;

namespace JDI.Light.Selenium.Elements.Complex.Table
{
    public class Cell : SelectableElement, ICell
    {
        private readonly By _cellLocatorTemplate = By.XPath(".//tr[{1}]/td[{0}]");
        
        public Cell(int columnNum, int rowNum, string colName, string rowName,
            By cellLocatorTemplate, Table table, int columnIndex = -1, int rowIndex = -1, IWebElement webElement = null)
        {
            if (columnIndex > 0)
                ColumnIndex = table.Rows.HasHeader && table.Rows.LineTemplate == null
                    ? ColumnIndex + 1
                    : ColumnIndex;
            WebElement = webElement;
            RowIndex = rowIndex;
            ColumnNum = columnNum;
            RowNum = rowNum;
            _columnName = colName;
            _rowName = rowName;
            if (cellLocatorTemplate != null)
                _cellLocatorTemplate = cellLocatorTemplate;
            Table = table;
            ClickAction = c => ((Cell) c).Get().Click();
            GetValueFunc = w => TextAction(this);
        }

        public int RowIndex { set; get; }
        public int ColumnIndex { set; get; }
        public Table Table { get; set; }
        private string _columnName { get; set; }
        private string _rowName { get; set; }
        protected Func<Cell, string> TextAction => c => Get().GetText;

        protected new Func<Cell, bool> SelectedAction => c => Get().Selected;
        public int ColumnNum { get; set; }
        public int RowNum { get; set; }

        public string ColumnName => !string.IsNullOrEmpty(_columnName)
            ? _columnName
            : Table.Columns.Headers[ColumnNum - 1];

        public string RowName => !string.IsNullOrEmpty(_rowName)
            ? _rowName
            : Table.Rows.Headers[RowNum - 1];

        public SelectableElement Get()
        {
            if (WebElement != null)
            {
                var se = new SelectableElement {WebElement = WebElement};
                return se;
            }
            return new SelectableElement(_cellLocatorTemplate.FillByTemplate(ColumnIndex, RowIndex));
        }

        public T Get<T>(Type clazz) where T : UIElement
        {
            T instance;
            try
            {
                instance = (T) Activator.CreateInstance(clazz.IsInterface
                    ? MapInterfaceToElement.ClassFromInterface(clazz)
                    : clazz);
            }
            catch
            {
                throw JDISettings.Asserter.Exception("Can't get Cell from interface/class: " +
                                            clazz.ToString().Split("\\.").Last());
            }

            return Get(instance);
        }

        public T Get<T>(T cell) where T : UIElement
        {
            var locator = cell.Locator;
            if (locator == null || locator.ToString().Equals(""))
                locator = _cellLocatorTemplate;
            if (!locator.ToString().Contains("{0}") || !locator.ToString().Contains("{1}"))
                throw JDISettings.Asserter.Exception($"Can't create cell with locator template {cell.Locator}. " +
                                                     "Template for Cell should contains '{0}' - for column and '{1}' - for row indexes.");
            cell.Locator = locator.FillByTemplate(RowIndex, ColumnIndex);
            cell.Parent = Table;
            return cell;
        }

        public Cell UpdateData(string colName, string rowName)
        {
            if (string.IsNullOrEmpty(_columnName) && !string.IsNullOrEmpty(colName))
                _columnName = colName;
            if (string.IsNullOrEmpty(_rowName) && !string.IsNullOrEmpty(rowName))
                _rowName = rowName;
            return this;
        }
    }
}