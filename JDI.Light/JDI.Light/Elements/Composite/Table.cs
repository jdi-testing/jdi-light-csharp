using System.Linq;
using JDI.Light.Elements.Base;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Composite
{
    public class Table : UIElement
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

        public UIElement Headers => new UIElement(TableHeadersLocator) { Parent = this };
        public UIElement Body => new UIElement(TableBodyLocator) { Parent = this };
        public UIElement Footer => new UIElement(TableFooterLocator) { Parent = this };
        public UIElement[] Rows => Body.FindElements(TableRowsLocator)
            .Select(e => new UIElement(TableRowsLocator)
            {
                WebElement = e,
                Parent = Body
            }).ToArray();

        public UIElement[][] Cells => Rows.Select(r => r.FindElements(TableCellsLocator)
            .Select(e => new UIElement(TableCellsLocator)
            {
                WebElement = e,
                Parent = r
            }).ToArray()).ToArray();
    }
}