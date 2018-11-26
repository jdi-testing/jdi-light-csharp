using System;
using JDI.Light.Enums;

namespace JDI.Light.Attributes.JAttributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    public class JTableAttribute : Attribute
    {
        public FindByAttribute Cell = null;
        public int ColStartIndex = -1;
        public FindByAttribute Column = null;
        public FindByAttribute Footer = null;
        public string[] Header = null;

        public TableHeaderType HeaderType = TableHeaderType.ColumnsHeaders;

        public int Height = -1;
        public FindByAttribute Root = null;
        public FindByAttribute Row = null;
        public string[] RowsHeader = null;

        public int RowStartIndex = -1;
        public string Size = "";
        public bool UseCache = true;
        public int Width = -1;
    }
}