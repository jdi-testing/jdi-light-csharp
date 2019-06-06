using JDI.Light.Elements.Composite;
using JDI.Light.Extensions;
using JDI.Light.Tests.UIObjects;
using NUnit.Framework;
using static NUnit.Framework.Assert;

namespace JDI.Light.Tests.Tests.Composite.PseudoSiteTests
{
    public class AnnotationsWebPageTests : TestBase
    {
        [SetUp]
        public override void SetUpTest()
        {
            Jdi.Logger.Info("Test Base Set up started...");
            TestSite = Jdi.InitSite<TestSite>();
            Jdi.Logger.Info("Test Base Set up done.");
            Jdi.Logger.Info("Run test...");
        }

        [TestCaseSource(nameof(_annotationsWebPageData))]
        public void AnnotationsWebPageTest(string webPage, string expectedUrl, string expectedTitle)
        {
            if (!(TestSite.GetType().GetMember(webPage)[0].GetMemberValue(TestSite) is WebPage targetElement)) return;
            AreEqual(targetElement.Url, expectedUrl);
            AreEqual(targetElement.Title, expectedTitle);
        }

        private static object[] _annotationsWebPageData =
        {
            new object[] { nameof(TestSite.PageWithBoth), "https://epam.github.io/JDI/pagewithboth.com", "Page with both" },
            new object[] { nameof(TestSite.PageWithTitle), "https://epam.github.io/JDI/", "Page with Title" },
            new object[] { nameof(TestSite.PageWithUrl), "https://epam.github.io/JDI/pagewithurl.com", "" },
            new object[] { nameof(TestSite.SlashPageWithUrl), "https://epam.github.io/JDI/pagewithurl.com", "" },
            new object[] { nameof(TestSite.PageWithoutBoth), null, null }
        };

        [TearDown]
        public override void TestTearDown()
        {
            Jdi.Logger.Info("Run test tear down done.");
        }
    }
}
