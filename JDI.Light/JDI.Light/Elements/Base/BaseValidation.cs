using static JDI.Light.Jdi;

namespace JDI.Light.Elements.Base
{
    public class BaseValidation
    {
        public static void BaseElementValidation(UIElement element)
        {
            Assert.IsTrue(element.Enabled);
            Assert.IsTrue(element.Displayed);
            Assert.IsFalse(element.Disabled);
            Assert.IsFalse(element.Hidden);
            var location = element.Location;
            Assert.IsTrue(location.X > 0 && location.Y > 0, "Location: " + location);
            var size = element.Size;
            Assert.IsTrue(size.Height > 0 && size.Width > 0, "Size: " + location);
            element.SetAttribute("test-jdi", "test-value");
            Assert.AreEquals(element.GetAttribute("test-jdi"), "test-value");
            element.Highlight("blue");
            element.Highlight();
            element.Show();
        }
    }
}
