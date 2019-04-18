using JDI.Light.Interfaces.Common;
using NUnit.Framework;

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
            _height.SetNumber("2.1");
        }

        //[Test]
        public void GetLabelTextTest()
        {
            // not implemented
        }

        [Test]
        public void GetNumberTest()
        {
            Jdi.Assert.AreEquals("2.1", _height.Value());
        }

        [Test]
        public void MinTest()
        {
            Jdi.Assert.AreEquals("0.3", _height.Min());
        }

        [Test]
        public void MaxTest()
        {
            Jdi.Assert.AreEquals("2.5", _height.Max());
        }

        [Test]
        public void PlaceHolderTest()
        {
            Jdi.Assert.AreEquals("20 cm increments. Range [0.3,2.5]", _height.Placeholder());
        }

        [Test]
        public void SetNumberTest()
        {
            _height.SetNumber("1.4");
            Jdi.Assert.AreEquals("1.4", _height.Value());
        }
    }
}