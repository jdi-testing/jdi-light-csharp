using System;
using JDI.Light.Interfaces.Base;
using JDI.Light.Selenium.Elements.Base;

namespace JDI.Light.Selenium.Elements.Complex.Table.Interfaces
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