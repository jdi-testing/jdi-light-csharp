namespace JDI.Light.Elements.Complex.Table
{
    public class Column : NameNum
    {
        public static Column InColumn(string value)
        {
            return ColumnByValue(value);
        }

        public static Column InColumn(int num)
        {
            return ColumnByNum(num);
        }

        public static Column ColumnByValue(string value)
        {
            Column column = new Column();
            return (Column)column.Set(column, s => s.Name = value);
        }

        public static Column ColumnByNum(int num)
        {
            Column column = new Column();
            return (Column)column.Set(column, s => s.Num = num);
        }
    }
}
