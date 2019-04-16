﻿using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace JDI.Light.Tests.Tests.Simple
{
    [TestFixture]
    public class TextTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            Jdi.Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
            TestSite.HomePage.Open();
            Jdi.Logger.Info("Setup method finished");
        }

        private readonly string _expectedText = ("Lorem ipsum dolor sit amet, consectetur adipisicing elit,"
                                                 + " sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."
                                                 + " Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris"
                                                 + " nisi ut aliquip ex ea commodo consequat Duis aute irure dolor in"
                                                 + " reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur."
            ).ToUpper();

        private readonly string _regEx = ".* IPSUM DOLOR SIT AMET.*";
        private readonly string _contains = "ENIM AD MINIM VENIAM, QUIS NOSTRUD";

        [Test]
        public void GetTextTest()
        {
            Jdi.Assert.AreEquals(TestSite.HomePage.Text.Value, _expectedText);
        }

        [Test]
        public void GetValueTest()
        {
            Jdi.Assert.AreEquals(TestSite.HomePage.Text.Value, _expectedText);
        }

        [Test]
        public void SetAttributeTest()
        {
            var attributeName = "testAttr";
            var value = "testValue";
            TestSite.HomePage.Text.SetAttribute(attributeName, value);
            Jdi.Assert.AreEquals(TestSite.HomePage.Text.GetAttribute(attributeName), value);
        }

        [Test]
        public void WaitMatchTest()
        {
            Jdi.Assert.AreEquals(TestSite.HomePage.Text.WaitMatchText(_regEx), _expectedText);
        }

        [Test]
        public void WaitMatchTextParallelTest()
        {
            TestSite.SupportPage.Open();
            var actualResultTask = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(200);
                return TestSite.HomePage.Text.WaitMatchText(_regEx);
            });
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(100);
                TestSite.HomePage.Open();
            });
            Jdi.Assert.AreEquals(actualResultTask.Result, _expectedText);
        }

        [Test]
        public void WaitText()
        {
            Jdi.Assert.AreEquals(TestSite.HomePage.Text.WaitText(_contains), _expectedText);
        }

        [Test]
        public void WaitTextParallelTest()
        {
            TestSite.SupportPage.Open();
            var actualResultTask = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(200);
                return TestSite.HomePage.Text.WaitText(_contains);
            });
            Task.Run(() =>
            {
                Thread.Sleep(100);
                TestSite.HomePage.Open();
            });
            Jdi.Assert.AreEquals(actualResultTask.Result, _expectedText);
        }
    }
}