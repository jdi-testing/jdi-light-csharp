using System;
using System.IO;
using System.Threading;
using NUnit.Framework;
using NUnit.Framework.Internal;
using static JDI.Light.Asserts.FileAssert;
using static JDI.Light.Matchers.LongMatchers.GreaterThanMatcher;
using static JDI.Light.Matchers.LongMatchers.IsMatcher;
using static JDI.Light.Matchers.StringMatchers.ContainsStringMatcher;

namespace JDI.Light.Tests.Tests.Simple
{
    [TestFixture]
    internal class FileInputTests : TestBase
    {
        //Tests require administrator access

        [SetUp]
        public void SetUp()
        {
            Jdi.Logger.Info("Navigating to HTML5 page.");
            TestSite.Html5Page.Open();
            TestSite.Html5Page.CheckTitle();
            Jdi.Logger.Info("Setup method finished");
            Jdi.Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
        }

        private static string CreateFile(string filename)
        {
            string filepath = Path.Combine(Directory.GetCurrentDirectory(), filename);
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
            string filename = "test.txt";
            TestSite.Html5Page.FileInput.SelectFile(CreateFile(filename));
            var uploadedFile = TestSite.Html5Page.FileInput.GetAttribute("value");
            Jdi.Assert.Contains(uploadedFile, filename);
        }

        private static void CreateTextFile(string fileName)
        {
            File.WriteAllText(Path.Combine(Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"), "Downloads"),
            fileName), "Earth provides enough to satisfy every man's needs, but not every man's greed");
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
            Thread.Sleep(5000);
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
    }
}
