using System.Collections.Generic;
using JDI.Light.Elements.Base;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Composite
{
    public class Table : CompositeUIElement
    {
        public Table(By locator) : base(locator)
        {
        }

        public By TableHeaderLocator { get; set; }
        public By TableBodeLocator { get; set; }
        public By TableFooterLocator { get; set; }
        public By TableRowsLocator { get; set; }
        public By TableColumnsLocator { get; set; }
        
        public CompositeUIElement Header => new CompositeUIElement(TableHeaderLocator);
        public CompositeUIElement Body => new CompositeUIElement(TableBodeLocator);
        public CompositeUIElement Footer => new CompositeUIElement(TableFooterLocator);
        public List<CompositeUIElement> Rows => new List<CompositeUIElement>();
        public List<CompositeUIElement> Columns => new List<CompositeUIElement>();


    }
}