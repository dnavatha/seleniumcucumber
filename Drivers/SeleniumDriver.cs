using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace FTADOTAutomation.Drivers
{
    public class SeleniumDriver
    {
        private IWebDriver driver;

        private readonly ScenarioContext _scenarioContext;

        public SeleniumDriver(ScenarioContext scenarioContext)
        {
             _scenarioContext = scenarioContext;
        }
        
        public IWebDriver Setup()
        {
            var chromedriverOptions = new ChromeOptions();

            driver = new RemoteWebDriver(new Uri("https://www.transit.dot.gov/about-fta"), chromedriverOptions.ToCapabilities());

            _scenarioContext.Set(driver, "WebDriver");

            driver.Manage().Window.Maximize();

            return driver;

        }

    }
}
