using JDI.Light.Matchers.StringMatchers;
using NUnit.Framework;
using Is = JDI.Light.Matchers.Is;

namespace JDI.Light.Tests.Tests.Simple
{
    [TestFixture]
    public class LinkTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            TestSite.Html5Page.Open();
            TestSite.Html5Page.CheckTitle();
        }

        private readonly string Text = "Github JDI";

        [Test]
        public void GetTextTest()
        {
            Assert.AreEqual(TestSite.Html5Page.GithubLink.GetText(), Text);
        }

        [Test]
        public void GetValueTest()
        {
            Assert.AreEqual(TestSite.Html5Page.GithubLink.GetValue(), Text);
        }

        [Test]
        public void GetRefTest()
        {
            Assert.AreEqual(TestSite.Html5Page.GithubLink.Ref(), "https://github.com/jdi-testing");
        }

        [Test]
        public void GetUrlTest()
        {
            Assert.AreEqual(TestSite.Html5Page.GithubLink.Url(), "https://epam.github.io/JDI/html5.html");
        }

        [Test]
        public void GetAltTest()
        {
            Assert.AreEqual(TestSite.Html5Page.GithubLink.Alt(), "Github JDI Link");
        }

        [Test]
        public void ClickTest()
        {
            TestSite.Html5Page.GithubLink.Click();
            Assert.AreEqual(TestSite.Html5Page.GithubLink.Url(), "https://github.com/jdi-testing");
            TestSite.Html5Page.Open();
        }

        [Test]
        public void IsValidationTest()
        {
            TestSite.Html5Page.GithubLink.Is().Text(Is.EqualTo(Text))
                .Text(Is.EqualToIgnoringCase("Github jdi"))
                .Enabled();
        }

        [Test]
        public void LinkValidationTest()
        {
            TestSite.Html5Page.GithubLink.Is().Ref(ContainsStringMatcher.ContainsString("github"))
                .Alt(ContainsStringMatcher.ContainsString("JDI"));
        }

        [Test]
        public void AssertValidationTest()
        {
            TestSite.Html5Page.GithubLink.AssertThat().Text(Is.EqualTo(Text));
        }
    }
}