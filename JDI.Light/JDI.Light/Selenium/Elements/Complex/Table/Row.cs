using System;

namespace JDI.Core.Selenium.Elements.Complex.Table
{
    public class Row : RowColumn
    {
        public Row(int num = -1, string name = null) : base(num, name)
        {
        }

        public static Row CreateRow(int num)
        {
            return new Row(num);
        }

        public static Row CreateRow(string name)
        {
            return new Row(name: name);
        }

        public static Row CreateRow(Enum name)
        {
            return CreateRow(name.ToString());
        }
    }
}