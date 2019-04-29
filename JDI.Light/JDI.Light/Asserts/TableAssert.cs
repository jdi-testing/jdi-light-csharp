using System.Collections.Generic;
using System.Linq;
using JDI.Light.Asserts.Generic;
using JDI.Light.Elements.Complex.Table;
using JDI.Light.Matchers;
using static JDI.Light.Elements.Complex.Table.TableMatcher;
using static JDI.Light.Jdi;

namespace JDI.Light.Asserts
{
    public class TableAssert : IsAssert<TableAssert>
    {
        protected Table Table { get; }

        public TableAssert(Table table) : base(table)
        {
            Table = table;
        }

        public TableAssert Empty()
        {
            Assert.IsTrue(IsTableEmpty, "The table is not empty");
            return this;
        }

        public TableAssert NotEmpty()
        {
            Assert.IsFalse(IsTableEmpty, "The table is empty");
            return this;
        }

        public TableAssert Size(Matcher<int> condition)
        {
            Assert.IsTrue(condition.IsMatch(TableSize),
                $"The table size {TableSize} is not {condition.ActionName} {condition.RightValue}");
            return this;
        }

        public TableAssert Size(int expectedSize)
        {
            Assert.AreEquals(expectedSize, TableSize);
            return this;
        }

        public TableAssert HasColumn(string column)
        {
            Assert.IsTrue(Table.Headers.Any(header => header.Text == column),
                $"the table doen not have {column} column");
            return this;
        }

        public TableAssert HasColumns(IEnumerable<string> columns)
        {
            foreach (var column in columns)
            {
                HasColumn(column);
            }
            return this;
        }

        public TableAssert Columns(Matcher<IEnumerable<string>> condition)
        {
            var tableHeaders = Table.Headers.Select(header => header.Text).ToArray();
            Assert.IsTrue(condition.IsMatch(tableHeaders),
                $"The table columns {string.Join(",", tableHeaders)} are not {condition.ActionName} {string.Join(",", condition.RightValue)}");
            return this;
        }

        public TableAssert RowsWithValues(int count, params TableMatcher[] matchers)
        {
            Assert.IsTrue(Table_Matcher.Invoke(Table, matchers).Count >= Table.Headers.Count * count,
                $"The table has less than {count} rows containing the following values in these columns.");
            return this;
        }

        public TableAssert HasRowWithValues(params TableMatcher[] matchers)
        {
            Assert.IsFalse(Table_Matcher.Invoke(Table, matchers).Count.Equals(0),
                "The row does not contain the following values in these columns.");
            return this;
        }

        //todo remove after implementation in Table class
        protected int TableSize => Table.Rows.Count - 1;

        protected bool IsTableEmpty => TableSize == 0;
    }
}