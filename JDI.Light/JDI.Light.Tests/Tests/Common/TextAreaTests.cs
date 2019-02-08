using NUnit.Framework;

namespace JDI.Light.Tests.Tests.Common
{
    [TestFixture]
    public class TextAreaTests : TestBase
    {
        private const string ToAddText = "text123!@#$%^&*()";

        [SetUp]
        public void SetUp()
        {
            Jdi.Logger.Info("Navigating to Metals and Colors page.");
            TestSite.ContactFormPage.Open();
            TestSite.ContactFormPage.CheckTitle();
            Jdi.Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void GetLinesTest()
        {
            TestSite.ContactFormPage.DescriptionArea.Clear();
            TestSite.ContactFormPage.DescriptionArea.InputLines("line1", "line2", "line3");
            var lines = TestSite.ContactFormPage.DescriptionArea.GetLines();
            Jdi.Assert.CollectionEquals(lines, new[] { "line1", "line2", "line3" });
        }

        [Test]
        public void InputLinesTest()
        {
            TestSite.ContactFormPage.DescriptionArea.Clear();
            TestSite.ContactFormPage.DescriptionArea.InputLines("line1", "line2");
            Jdi.Assert.CollectionEquals(TestSite.ContactFormPage.DescriptionArea.GetLines(), new[] { "line1", "line2" });
        }

        [Test]
        public void AddNewLineTest()
        {
            TestSite.ContactFormPage.DescriptionArea.Clear();
            TestSite.ContactFormPage.DescriptionArea.InputLines("line1", "line2");
            TestSite.ContactFormPage.DescriptionArea.AddNewLine("line3");
            Jdi.Assert.CollectionEquals(TestSite.ContactFormPage.DescriptionArea.GetLines(), new[] { "line1", "line2", "line3" });
        }
    }
}