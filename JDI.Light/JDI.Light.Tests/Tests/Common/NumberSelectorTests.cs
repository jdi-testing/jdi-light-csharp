using JDI.Light.Interfaces.Common;
using JDI.Light.Matchers.StringMatchers;
using NUnit.Framework;
using Is = JDI.Light.Matchers.Is;

namespace JDI.Light.Tests.Tests.Common
{
    [TestFixture]
    public class NumberSelectorTests : TestBase
    {
        private INumberSelector _height;

        [SetUp]
        public void SetUp()
        {
            TestSite.Html5Page.Open();
            TestSite.Html5Page.CheckOpened();
            _height = TestSite.Html5Page.Height;
            Assert.DoesNotThrow(() => _height.SetNumber(2.1, true));
        }

        //[Test]
        public void GetLabelTextTest()
        {
            // not implemented
        }

        [Test]
        public void GetNumberTest()
        {
            Jdi.Assert.AreEquals(2.1, _height.Value);
        }

        [Test]
        public void MinTest()
        {
            Jdi.Assert.AreEquals(0.3, _height.Min);
        }

        [Test]
        public void MaxTest()
        {
            Jdi.Assert.AreEquals(2.5, _height.Max);
        }

        [Test]
        public void StepTest()
        {
            Jdi.Assert.AreEquals(0.2, _height.Step);
        }

        [Test]
        public void PlaceHolderTest()
        {
            Jdi.Assert.AreEquals("20 cm increments. Range [0.3,2.5]", _height.Placeholder);
        }

        [Test]
        public void SetNumberTest()
        {
            _height.SetNumber(1.4);
            Jdi.Assert.AreEquals(1.4, _height.Value);
        }

        [Test]
        public void IsValidationTest()
        {
            _height.AssertThat().MinValue(Is.EqualTo(0.3))
                .MaxValue(Is.EqualTo(2.5))
                .StepValue((Is.EqualTo(0.2)));
            _height.Is().Placeholder(ContainsStringMatcher.ContainsString("20 cm increments"))
                .Number(Is.GreaterThanOrEqualTo(0.3))
                .Number(Is.LessThanOrEqualTo(2.5));
            _height.AssertThat().Number(Is.EqualTo(2.1));
            _height.Is().Enabled();
        }

        [Test]
        public void AssertValidationTest()
        {
            _height.AssertThat().Number(Is.GreaterThan(0.0))
                .Number(Is.LessThan(3.0))
                .Number(Is.EqualTo(2.1));
        }
    }
}