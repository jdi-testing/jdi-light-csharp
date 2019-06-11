using System;
using static JDI.Light.Jdi;

namespace JDI.Light.Elements.Base
{
    public static class BaseValidation
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

        public static void ValidateDuration(int min, int max, Action action)
        {
            var start = DateTime.Now.Millisecond;
            try
            {
                action.Invoke();
            }
            finally
            {
                var passedTime = DateTime.Now.Millisecond - start;
                Assert.IsTrue(passedTime > min*1000-500);
                Assert.IsTrue(passedTime < max*1000+500);
            }
        }

        public static void DurationImmediately(Action action)
        {
            DurationMoreThan(0, action);
        }

        public static void DurationMoreThan(int duration, Action action)
        {
            ValidateDuration(duration, duration + 1, action);
        }

        public static void DurationLessThan(int duration, Action action)
        {
            ValidateDuration(duration - 1, duration, action);
        }
    }
}
