using System;
using JDI.Core.Interfaces.Base;
using JDI.Web.Selenium.Base;
using JDI.Web.Selenium.Elements.Base;

namespace JDI.Web.Selenium.Elements.Complex.Table.Interfaces
{
    public interface ICell : ISelect
    {
        int ColumnNum { get; }
        int RowNum { get; }
        string ColumnName { get; }
        string RowName { get; }
        SelectableElement Get();
        T Get<T>(T element) where T : WebBaseElement;
        T Get<T>(Type clazz) where T : WebBaseElement;
    }
}