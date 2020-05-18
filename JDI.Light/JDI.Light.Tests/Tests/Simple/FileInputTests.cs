using System;
using System.IO;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using static System.Threading.Thread;
using static JDI.Light.Asserts.FileAssert;
using static JDI.Light.Elements.Base.BaseValidation;
using static JDI.Light.Jdi;
using static JDI.Light.Matchers.LongMatchers.GreaterThanMatcher;
using static JDI.Light.Matchers.LongMatchers.IsMatcher;
using static JDI.Light.Matchers.StringMatchers.ContainsStringMatcher;
using static JDI.Light.Matchers.StringMatchers.EqualToMatcher;
using static NUnit.Framework.Assert;

namespace JDI.Light.Tests.Tests.Simple
{
    [TestFixture]
    internal class FileInputTests : TestBase
    {
        //Tests require administrator access

        private readonly string _fileName = "test.txt";

        [SetUp]
        public void SetUp()
        {
            Logger.Info("Navigating to HTML5 page.");
            TestSite.Html5Page.Open();
            TestSite.Html5Page.CheckTitle();
            Logger.Info("Setup method finished");
            Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
        }

        private static string CreateFile(string filename)
        {
            var locationDir = Assembly.GetExecutingAssembly().Location;
            var locationArr = locationDir.Split('\\');
            var locationArr1 = locationArr.Take(locationArr.Count() - 1).ToArray();
            var filepath = Path.Combine(string.Join("\\", locationArr1), filename);
            using (var sw = File.CreateText(filepath))
            {
                sw.WriteLine("hello world");
                sw.Close();
            }
            return filepath;
        }

        [Test]
        public void FileInputTest()
        {
            TestSite.Html5Page.FileInput.SelectFile(CreateFile(_fileName));
            var uploadedFile = TestSite.Html5Page.FileInput.GetAttribute("value");
            Jdi.Assert.Contains(uploadedFile, _fileName);
        }

        [Test]
        public void DisabledUploadTest()
        {
            Sleep(2000);
            try
            {
                TestSite.Html5Page.DisabledFileInput.SelectFile(CreateFile(_fileName));
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }
            Sleep(2000);
            TestSite.Html5Page.DisabledFileInput.Is.Text(EqualTo(""));
        }

        private static void CreateTextFile(string fileName)
        {
            File.WriteAllText(Path.Combine(Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"), "Downloads"),
            fileName), "Earth provides enough to satisfy every man's needs, but not every man's greed");
        }

        [Test]
        public void LabelTest()
        {
            AreEqual(TestSite.Html5Page.FileInput.LabelText(), "Profile picture:");
            TestSite.Html5Page.FileInput.Label().Is.Text(ContainsString("picture"));
        }

        [Test]
        public void DownloadTest()
        {
            CleanupDownloads();
            var userRoot = Environment.GetEnvironmentVariable("USERPROFILE");
            var downloadFolder = Path.Combine(userRoot, "Downloads");
            var fileToUpload = Path.Combine(downloadFolder, "jdi-logo.jpg");
            if (File.Exists(fileToUpload))
            {
                File.Delete(fileToUpload);
            }
            TestSite.Html5Page.FileDownload.Click();
            Sleep(5000);
            AssertThatFile(fileToUpload).IsDownloaded().HasSize(Is(32225));
            AssertThatFile(fileToUpload).HasSize(GreaterThan(100));
        }

        [Test]
        public void AssertFileTest()
        {
            CleanupDownloads();
            var fileName = "gandhi.txt";
            CreateTextFile("gandhi.txt");
            AssertThatFile(fileName).Text(ContainsString("enough to satisfy"));
        }

        [Test]
        public void BaseValidationTest()
        {
            BaseElementValidation(TestSite.Html5Page.FileInput);
        }
    }
}
