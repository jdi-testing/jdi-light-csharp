using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using JDI.Light.Enums;
using JDI.Light.Extensions;
using JDI.Light.Selenium.Elements.Base;
using JDI.Light.Selenium.Elements.Complex.Table.Interfaces;
using JDI.Light.Selenium.Elements.Composite;
using JDI.Light.Settings;
using OpenQA.Selenium;

namespace JDI.Light.Selenium.Elements.Complex.Table
{
    public class Table : CompositeUIElement, ITable
    {
        private readonly Columns _columns = new Columns();
        private readonly Rows _rows = new Rows();
        private IList<ICell> _allCells = new List<ICell>();
        private IList<string> _footer;

        public Table()
        {
            Columns.Table = this;
            Rows.Table = this;
        }

        private IList<ICell> AllCells
        {
            get
            {
                var result = new List<ICell>();
                var rows = Rows.Get();
                Columns.Headers.ForEach(columnName => Rows.Headers
                    .ForEach(rowName => result.Add(rows[rowName][columnName])));

                if (Cache)
                    AllCells = result;
                return result;
            }
            set => _allCells = value;
        }

        public bool Cache { set; get; }

        public By RootBy
        {
            set => Locator = value;
        }

        protected By CellLocatorBy { set; get; }

        public By ColumnBy
        {
            set => Columns.LineTemplate = value;
        }

        public By RowBy
        {
            set => Rows.LineTemplate = value;
        }

        public By FooterBy { get; set; } = By.XPath(".//tfoot/tr/th");

        public By ColumnHeaderBy
        {
            set => Columns.HeadersLocator = value;
        }

        public By RowHeaderBy
        {
            set => Rows.HeadersLocator = value;
        }

        public string[] ColumnHeaders
        {
            set
            {
                Columns.AddHeaders(value);
                Columns.HasHeader = true;
            }
        }

        public string[] RowHeaders
        {
            set
            {
                Rows.AddHeaders(value);
                Rows.HasHeader = true;
            }
        }

        public int ColumnFrom
        {
            set => Columns.StartIndex = value;
        }

        public int RowFrom
        {
            set => Rows.StartIndex = value;
        }

        public TableHeaderType HeaderType
        {
            set
            {
                switch (value)
                {
                    case TableHeaderType.AllHeaders:
                        Columns.HasHeader = true;
                        Rows.HasHeader = true;
                        break;
                    case TableHeaderType.NoHeaders:
                        Columns.HasHeader = false;
                        Rows.HasHeader = false;
                        break;
                    case TableHeaderType.ColumnsHeaders:
                        Columns.HasHeader = true;
                        Rows.HasHeader = false;
                        break;
                    case TableHeaderType.RowsHeaders:
                        Columns.HasHeader = false;
                        Rows.HasHeader = true;
                        break;
                }
            }
        }

        public Columns Columns
        {
            get => _columns;
            set => _columns.Update(value);
        }

        public Rows Rows
        {
            get => _rows;
            set => _rows.Update(value);
        }

        public Table Copy()
        {
            return Clone();
        }

        public Table Clone()
        {
            var newTable = new Table();
            newTable.Rows = Rows.Clone(new Rows(), newTable);
            newTable.Columns = Columns.Clone(new Columns(), newTable);
            newTable.Parent = Parent;
            return newTable;
        }

        public ITable UseCache()
        {
            Cache = true;
            return this;
        }

        public void Clean()
        {
            AllCells = new List<ICell>();
            Columns.Clean();
            Rows.Clean();
        }

        public void Clear()
        {
            Clean();
        }

        public Dictionary<string, ICell> Column(int colNum)
        {
            return Columns.GetColumn(colNum);
        }

        public Dictionary<string, ICell> Column(string colName)
        {
            return Columns.GetColumn(colName);
        }

        public Dictionary<string, ICell> Column(string value, Row row)
        {
            var columnCell = Cell(value, row);
            return columnCell != null ? Columns.GetColumn(columnCell.ColumnNum) : null;
        }

        public Dictionary<string, ICell> Row(string value, Column column)
        {
            var rowCell = Cell(value, column);
            return rowCell != null ? Rows.GetRow(rowCell.RowNum) : null;
        }

        public IList<string> ColumnValue(int colNum)
        {
            return Columns.GetColumnValue(colNum);
        }

        public IList<string> ColumnValue(string colName)
        {
            return Columns.GetColumnValue(colName);
        }

        public Dictionary<string, ICell> Row(int rowNum)
        {
            return Rows.GetRow(rowNum);
        }

        public Dictionary<string, ICell> Row(string rowName)
        {
            return Rows.GetRow(rowName);
        }

        public IList<string> RowValue(int rowNum)
        {
            return Rows.GetRowValue(rowNum);
        }

        public IList<string> RowValue(string rowName)
        {
            return Rows.GetRowValue(rowName);
        }

        public IList<string> Footer
        {
            get => new List<string>(_footer);
            set => _footer = new List<string>(value);
        }

        public Dictionary<string, SelectableElement> Header()
        {
            return Columns.Header();
        }

        public SelectableElement Header(string name)
        {
            return Columns.Header(name);
        }

        public IList<string> Headers => Columns.Headers;

        public ICell Cell(Column column, Row row)
        {
            var colIndex = column.Get(GetColumnIndex, num => num + Columns.StartIndex - 1);
            var rowIndex = (int) row.Get(GetRowIndex, num => num + Rows.StartIndex - 1);
            return AddCell(colIndex, rowIndex, column.Get(name => Columns.Headers.IndexOf(name) + 1, num => num),
                row.Get(name => Rows.Headers.IndexOf(name) + 1, num => num), column.Get(name => name, num => ""),
                row.Get(name => name, num => ""));
        }

        public ICell Cell(string columnName, string rowName)
        {
            return Cell(Complex.Table.Column.column(columnName), Complex.Table.Row.CreateRow(rowName));
        }

        public ICell Cell(int columnIndex, int rowIndex)
        {
            return Cell(Complex.Table.Column.column(columnIndex), Complex.Table.Row.CreateRow(rowIndex));
        }

        public IList<ICell> Cells(string value)
        {
            return new List<ICell>(GetCells().Where(cell => cell.Value.Equals(value)));
        }

        public ICell Cell(string value)
        {
            return Rows.Get().Select(row => row.Value.FirstOrDefault(pair => pair.Value.GetText.Equals(value)).Value)
                .FirstOrDefault(result => result != null);
        }

        public IList<ICell> GetCells()
        {
            var rows = Rows.Get();
            var result = (from columnName in Columns.Headers
                from rowName in Rows.Headers
                select rows[rowName][columnName]).ToList();
            if (Cache)
                AllCells = result;
            return result;
        }

        public Dictionary<string, Dictionary<string, ICell>> GetRows(params string[] colNameValues)
        {
            var result = new Dictionary<string, Dictionary<string, ICell>>();
            foreach (var row in Rows.Get())
            {
                var matches = true;
                foreach (var colNameValue in colNameValues)
                {
                    if (!colNameValue.Matches("[^=]+=[^=]*"))
                        throw JDISettings.Assert.Exception($"Wrong searchCriteria for Cells: {colNameValue}");
                    var splitted = colNameValue.Split(Convert.ToChar("="));
                    var colName = splitted[0];
                    var colValue = splitted[1];
                    var cell = row.Value[colName];
                    if (cell == null || !cell.Value.Equals(colValue))
                    {
                        matches = false;
                        break;
                    }
                }

                if (matches) result.Add(row.Key, row.Value);
            }

            return result;
        }

        public Dictionary<string, Dictionary<string, ICell>> GetColumns(params string[] rowNameValues)
            // TODO method name conflict
        {
            var result = new Dictionary<string, Dictionary<string, ICell>>();
            foreach (var column in Columns.Get())
            {
                var matches = true;
                foreach (var rowNameValue in rowNameValues)
                {
                    if (!rowNameValue.Matches("[^=]+=[^=]*"))
                        throw JDISettings.Assert.Exception($"Wrong searchCriteria for Cells: {rowNameValue}");
                    var splitted = rowNameValue.Split(Convert.ToChar("="));
                    var rowName = splitted[0];
                    var rowValue = splitted[1];
                    var cell = column.Value[rowName];
                    if (cell == null || !cell.Value.Equals(rowValue))
                    {
                        matches = false;
                        break;
                    }
                }

                if (matches) result.Add(column.Key, column.Value);
            }

            return result;
        }

        public bool WaitValue(string value, Row row)
        {
            return Timer.Wait(() => Column(value, row) != null);
        }

        public bool WaitValue(string value, Column column)
        {
            return Timer.Wait(() => Row(value, column) != null);
        }

        public bool Empty
        {
            get
            {
                WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;
                var rowsCount = Rows.GetCount(true);
                WebDriver.Manage().Timeouts().ImplicitWait =
                    TimeSpan.FromSeconds(JDISettings.Timeouts.CurrentTimeoutSec);
                return rowsCount == 0;
            }
        }

        public bool WaitHaveRows()
        {
            return WaitRows(1);
        }

        public bool WaitRows(int count)
        {
            return Timer.Wait(() => Rows.Count >= count);
        }

        public ICell Cell(string value, Column column)
        {
            var colIndex = column.Get(name => Columns.Headers.IndexOf(name) + 1, num => num);
            return Columns.GetColumn(colIndex).FirstOrDefault(pair => pair.Value.Value.Equals(value)).Value;
        }

        public ICell Cell(string value, Row row)
        {
            var rowNum = row.HasName ? Rows.Headers.IndexOf(row.Name) + 1 : row.Num;
            return Rows.GetRow(rowNum).FirstOrDefault(pair => pair.Value.Value.Equals(value)).Value;
        }

        public IList<ICell> CellsMatch(string regex)
        {
            // TODO: why Collection type is used here?
            return Matches(new Collection<ICell>(GetCells()), regex);
        }

        public ICell CellMatch(string regex)
        {
            return Rows.Get().Select(row => row.Value.FirstOrDefault(pair => pair.Value.GetText.Matches(regex)).Value)
                .FirstOrDefault(result => result != null);
        }

        public IList<ICell> CellsMatch(string regex, Column column)
        {
            var columnLine = Column(column);
            return columnLine.Where(v => v.Value.Value.Matches(regex)).Select(v => v.Value).ToList();
        }

        public IList<ICell> CellsMatch(string regex, Row row)
        {
            var columnLine = Row(row);
            return columnLine.Where(v => v.Value.Value.Matches(regex)).Select(v => v.Value).ToList();
        }

        public void SetTableSettings(TableSettings settings)
        {
            Rows.HasHeader = settings.RowHasHeaders;
            Rows.Headers = settings.RowHeaders;
            Rows.Count = settings.RowsCount;
            Columns.HasHeader = settings.ColumnHasHeaders;
            Columns.Headers = settings.ColumnHeaders;
            Columns.Count = settings.ColumnsCount;
        }

        public ITable SetUp(By root, By cell, By row, By column, By footer, int colStartIndex, int rowStartIndex)
        {
            CellLocatorBy = cell;
            Rows.LineTemplate = row;
            Columns.LineTemplate = column;
            FooterBy = footer;
            Columns.StartIndex = colStartIndex;
            Rows.StartIndex = rowStartIndex;
            return this;
        }

        public ITable UseCache(bool value)
        {
            Cache = value;
            return this;
        }

        private Dictionary<string, ICell> Column(Column column)
        {
            return column.Get(Column, Column);
        }

        private Dictionary<string, ICell> Row(Row row)
        {
            return row.Get(Row, Row);
        }

        private IList<string> GetAllEnumNames(Type headers)
        {
            return typeof(Enum).IsAssignableFrom(headers)
                ? Enum.GetNames(headers).ToList()
                : headers.GetFields().ToList().Select(el => el.GetValue(null).ToString()).ToList();
        }

        public ITable SetColumnsCount(int value)
        {
            Columns.Count = value;
            return this;
        }

        public ITable SetRowsCount(int value)
        {
            Rows.Count = value;
            return this;
        }

        protected IList<string> GetFooterAction()
        {
            return WebElement.FindElements(FooterBy).Select(e => e.Text).ToList();
        }

        public IList<string> FooterInstance() // TODO method name conflict
        {
            if (Footer != null)
                return Footer;
            //_footer = invoker.doJActionResult("Get Footer", this::getFooterAction); TODO
            if (Footer == null || Footer.Count == 0)
                return new List<string>();
            Columns.Count = Footer.Count;
            return Footer;
        }

        public ICell Cell(IWebElement webElement, Column column, Row row)
        {
            return AddCell(webElement, column.Get(name => Columns.Headers.IndexOf(name) + 1, num => num),
                row.Get(name => Rows.Headers.IndexOf(name) + 1, num => num), column.Get(name => name, num => ""),
                row.Get(name => name, num => ""));
        }

        private IList<ICell> Matches(Collection<ICell> list, string regex)
        {
            return new List<ICell>(list.Where(cell => cell.Value.Matches(regex)));
        }

        private int GetColumnIndex(string name)
        {
            int nameIndex;
            var headers = Columns.Headers;
            if (headers != null && headers.Contains(name))
                nameIndex = headers.IndexOf(name);
            else
                throw JDISettings.Assert.Exception("Can't Get Column: '" + name + "'. " + (headers == null
                                                ? "ColumnHeaders is Null"
                                                : "Available ColumnHeaders: " + headers.FormattedJoin(", ", "'{0}'") + ")"));
            return nameIndex + Columns.StartIndex;
        }

        private object GetRowIndex(string name)
        {
            int nameIndex;
            var headers = Rows.Headers;
            if (headers != null && headers.Contains(name))
                nameIndex = headers.IndexOf(name);
            else
                throw JDISettings.Assert.Exception(
                    $"Can't Get Row: {name}. Available RowHeaders: {Headers.FormattedJoin(", ", "'{0}'")}");
            return nameIndex + Rows.StartIndex;
        }

        private ICell AddCell(int colIndex, int rowIndex, int colNum, int rowNum, string colName, string rowName)
        {
            var cell = (Cell) AllCells.FirstOrDefault(c => c.ColumnNum == colNum && c.RowNum == rowNum);
            if (cell != null)
                return cell.UpdateData(colName, rowName);
            cell = new Cell(colNum, rowNum, colName, rowName, CellLocatorBy, this, colIndex, rowIndex);

            if (Cache)
                AllCells.Add(cell);
            return cell;
        }

        private ICell AddCell(IWebElement webElement, int colNum, int rowNum, string colName, string rowName)
        {
            var cell = (Cell) _allCells.FirstOrDefault(c => c.ColumnNum == colNum && c.RowNum == rowNum);
            if (cell != null)
            {
                cell.WebElement = webElement;
                return cell.UpdateData(colName, rowName);
            }

            cell = new Cell(colNum, rowNum, colName, rowName, CellLocatorBy, this, -1, webElement: webElement);

            if (Cache)
                AllCells.Add(cell);
            return cell;
        }
    }
}