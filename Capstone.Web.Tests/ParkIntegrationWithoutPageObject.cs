using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;

namespace Capstone.Web.Tests
{
    [TestClass]
    public class ParkIntegrationWithoutPageObject
    {
        private static IWebDriver webDriver;

        [TestInitialize]
        public void TestInit()
        {
            webDriver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            webDriver.Navigate().GoToUrl(Helper.URL);
        }

        [TestCleanup]
        public void CleanUp()
        {
            webDriver.Close();
        }

        [TestMethod]
        public void ElementsExistOnPage()
        {
            IWebElement header = webDriver.FindElement(By.TagName("head"));
            IWebElement footer = webDriver.FindElement(By.TagName("footer"));
            IWebElement grandCanyonImg = webDriver.FindElement(By.Id("GCNP"));
            Assert.IsNotNull(header);
            Assert.IsNotNull(footer);
            Assert.IsTrue(grandCanyonImg.Displayed);
        }

        [TestMethod]
        public void DetailNavigationLinkWorksCorrectly()
        {
            IWebElement detailPageLink = webDriver.FindElement(By.Id("GCNP"));
            detailPageLink.Click();
            Assert.IsTrue(webDriver.Url.Contains("/Home/Detail/GCNP"));

        }

        [TestMethod]
        public void SurveyNavigationLinkWorksCorrectly()
        {
            IWebElement inputPageLink = webDriver.FindElement(By.LinkText("Survey"));
            inputPageLink.Click();
            Assert.IsTrue(webDriver.Title.Contains("Survey"));
        }
    }
}
