using JDI.Light.Elements.Complex.Table;
using static JDI.Light.Elements.Complex.Table.TableMatcher;
using static JDI.Light.Jdi;

namespace JDI.Light.Asserts
{
    public class TableAssert
    {
        protected Table table;

        public TableAssert(Table table)
        {
            this.table = table;
        }

        public TableAssert HasRowWithValues(params TableMatcher[] matchers)
        {
            Assert.IsFalse(Table_Matcher.Invoke(table, matchers).Count.Equals(0),
                "The row does not contain the following values in these columns.");
            return this;
        }
    }
}
