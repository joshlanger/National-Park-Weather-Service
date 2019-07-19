using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Web.Tests.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Capstone.Web.Tests
{
    
    public class ParkPage
    {
        private IWebDriver webDriver;

        public ParkPage(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        public DetailPage ClickGrandCanyon()
        {
            IWebElement grandCanyonLink = webDriver.FindElement(By.Id("GCNP"));
            grandCanyonLink.Click();
            return new DetailPage(webDriver);
        }
    }
}
