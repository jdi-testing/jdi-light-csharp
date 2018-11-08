using System;

namespace JDI.Web.Attributes.Objects
{
    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    public class JTableAttribute : Attribute
    {
        public FindByAttribute Root = null;
        public string[] Header = null;
        public string[] RowsHeader = null;

        public FindByAttribute Cell = null;
        public FindByAttribute Row = null;
        public FindByAttribute Column = null;
        public FindByAttribute Footer = null;

        public int Height = -1;
        public int Width = -1;
        public string Size = "";

        public int RowStartIndex = -1;
        public int ColStartIndex = -1;

        public TableHeaderTypes HeaderType = TableHeaderTypes.ColumnsHeaders;
        public bool UseCache = true;

    }
}
