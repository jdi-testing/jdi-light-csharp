using System;

namespace JDI.Core.Interfaces.Complex.Tables
{
    public abstract class RowColumn
    {
        private int num;
        private string name;

        public RowColumn(int num)
        {
            this.num = num;
        }

        public RowColumn(string name)
        {
            this.name = name;
        }

        public bool HasName()
        {
            return name != null && !name.Equals("");
        }

        public int GetNum()
        {
            return num;
        }

        public string GetName()
        {
            return name;
        }

        public T Get<T>(Func<RowColumn, T> action)
        {
            return action.Invoke(this);
        }

        public T Get<T>(Func<string, T> nameAction, Func<int, T> numAction)
        {
            return HasName() ? nameAction.Invoke(name) : numAction.Invoke(num);
        }
        
        
        public override string ToString()
        {
            return HasName() ? name : num + "";
        }
    }
}
