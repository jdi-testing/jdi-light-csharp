using System;
using Epam.JDI.Core.Interfaces.Base;

namespace JDI_Core.Interfaces.Complex.Tables
{
    public interface ICell : ISelect
    {
        T Get<T>(Func<T> clazz);
        T Get<T>(T element);

        int ColumnNum();

        int RowNum();

        string ColumnName();

        string RowName();
    }
}
