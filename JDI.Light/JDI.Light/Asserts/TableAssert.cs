using JDI.Light.Elements.Complex.Table;
using NHamcrest;
using static JDI.Light.Elements.Complex.Table.TableMatcher;
using static NHamcrest.XUnit.Assert;

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
            That(Table_Matcher.Invoke(table, matchers).Count, Is.Not(0));
            return this;
        }
    }
}
