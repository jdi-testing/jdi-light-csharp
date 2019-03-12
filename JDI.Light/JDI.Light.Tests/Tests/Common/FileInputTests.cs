using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JDI.Light.Tests.UIObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using static JDI.Light.Jdi;

namespace JDI.Light.Tests.Tests.Common
{
    [TestFixture]
    class FileInputTests : TestBase
    {
        //Tests require administrator access

        [SetUp]
        public void SetUp()
        {
            Logger.Info("Navigating to HTML5 page.");
            TestSite.Html5Page.Open();
            TestSite.Html5Page.CheckTitle();
            Logger.Info("Setup method finished");
            Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
        }

        private string CreateFile(string filename)
        {
            string filepath = Path.Combine(Directory.GetCurrentDirectory(), filename);
            var sw = File.CreateText(filepath);
            sw.WriteLine("hello world");
            sw.Close();
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

        [Test]
        public void DownloadTest()
        {
            string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
            string downloadFolder = Path.Combine(userRoot, "Downloads");
            string fileToUpload = Path.Combine(downloadFolder, "jdi-logo.jpg");
            if (File.Exists(fileToUpload))
            {
                File.Delete(fileToUpload);
            }
            TestSite.Html5Page.FileDownload.Click();
            Thread.Sleep(5000);
            Jdi.Assert.IsTrue(File.Exists(fileToUpload));
        }
    }
}
