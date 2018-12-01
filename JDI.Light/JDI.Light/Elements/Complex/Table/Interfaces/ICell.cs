using System;
using JDI.Light.Elements.Base;
using JDI.Light.Interfaces.Base;

namespace JDI.Light.Elements.Complex.Table.Interfaces
{
    public interface ICell : ISelect
    {
        int ColumnNum { get; }
        int RowNum { get; }
        string ColumnName { get; }
        string RowName { get; }
        SelectableElement Get();
        T Get<T>(T element) where T : UIElement;
        T Get<T>(Type clazz) where T : UIElement;
    }
}