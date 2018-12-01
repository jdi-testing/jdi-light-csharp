using System;

namespace JDI.Light.Elements.Complex.Table
{
    public class RowColumn
    {
        public RowColumn(int num, string name)
        {
            Num = num;
            Name = name;
        }

        public int Num { get; set; }
        public string Name { get; set; }

        public bool HasName => !string.IsNullOrEmpty(Name);

        public T Get<T>(Func<RowColumn, T> action)
        {
            return action.Invoke(this);
        }

        public T Get<T>(Func<string, T> nameAction, Func<int, T> numAction)
        {
            return HasName ? nameAction.Invoke(Name) : numAction.Invoke(Num);
        }
    }
}