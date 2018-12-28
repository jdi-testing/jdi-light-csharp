using System.Linq;
using JDI.Light.Elements.Base;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Composite
{
    public class Table : CompositeUIElement
    {
        public Table(By locator) : base(locator)
        {
            TableHeadersLocator = By.XPath(".//thead/tr/th");
            TableBodyLocator = By.XPath(".//tbody");
            TableFooterLocator = By.XPath(".//tfoot");
            TableRowsLocator = By.XPath(".//tr");
            TableCellsLocator = By.XPath(".//td");
        }

        public By TableHeadersLocator { get; set; }
        public By TableBodyLocator { get; set; }
        public By TableFooterLocator { get; set; }
        public By TableRowsLocator { get; set; }
        public By TableCellsLocator { get; set; }

        public CompositeUIElement Headers => new CompositeUIElement(TableHeadersLocator) { Parent = this };
        public CompositeUIElement Body => new CompositeUIElement(TableBodyLocator) { Parent = this };
        public CompositeUIElement Footer => new CompositeUIElement(TableFooterLocator) { Parent = this };
        public CompositeUIElement[] Rows => Body.FindElements(TableRowsLocator)
            .Select(e => new CompositeUIElement(TableRowsLocator)
            {
                WebElement = e,
                Parent = Body
            }).ToArray();

        public CompositeUIElement[][] Cells => Rows.Select(r => r.FindElements(TableCellsLocator)
            .Select(e => new CompositeUIElement(TableCellsLocator)
            {
                WebElement = e,
                Parent = r
            }).ToArray()).ToArray();
    }
}