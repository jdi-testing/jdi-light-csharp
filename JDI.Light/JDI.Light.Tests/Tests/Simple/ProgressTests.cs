using JDI.Light.Matchers.IntegerMatchers;
using NUnit.Framework;
using Is = JDI.Light.Matchers.Is;

namespace JDI.Light.Tests.Tests.Simple
{
    [TestFixture]
    public class ProgressTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            TestSite.Html5Page.Open();
            TestSite.Html5Page.CheckOpened();
        }

        [Test]
        public void GetValueTest()
        {
            Assert.AreEqual(TestSite.Html5Page.Progress.Value(), "70");
        }

        [Test]
        public void MaxTest()
        {
            Assert.AreEqual(TestSite.Html5Page.Progress.Max(), "100");
        }

        [Test]
        public void IsValidationTest()
        {
            TestSite.Html5Page.Progress.AssertThat().MaxValue(Is.EqualTo(100))
                .Value(Is.EqualTo(70));
            TestSite.Html5Page.Progress.Is().Value(Is.GreaterThanOrEqualTo(10))
                .Value(Is.LessThanOrEqualTo(100))
                .Enabled();
        }

        [Test]
        public void AssertValidationTest()
        {
            TestSite.Html5Page.Progress.AssertThat().Value(GreaterThanMatcher.GreaterThan(0))
                .Value(LessThanMatcher.LessThan(200))
                .Value(EqualToMatcher.EqualTo(70));
        }
    }
}