using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Capstone.Web.Tests.PageObjects
{
    public class DetailPage
    {
        private IWebDriver webDriver;

        public DetailPage(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

    }
}
