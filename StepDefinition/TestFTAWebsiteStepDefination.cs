using AventStack.ExtentReports.Gherkin.Model;
using OpenQA.Selenium;

using TechTalk.SpecFlow;
using System;
using AventStack.ExtentReports;

namespace FTADOTAutomation.Steps
{
    [Binding]
    public sealed class TestFTAWebsiteStepDefination
    {
        IWebDriver driver;
        public ExtentReports Exreport;
        public ScenarioContext SC;

        public TestFTAWebsiteStepDefination(IWebDriver driver, ExtentReports exreport,ScenarioContext sc)
        {
            this.driver = driver;
            Exreport = exreport;
            SC = sc;

        }

        [When(@"User launches FTA website")]
        public void WhenUserLaunchesFTAWebsite()
        {
            driver.Url = "https://www.transit.dot.gov/";
        }


        [When(@"Clicks on About link")]
        public void WhenClicksOnAboutLink()
        {
            driver.FindElement(By.XPath("//a[text()='About']")).Click();
            
        }

        [When(@"Verify about page")]
        public void WhenVerifyAboutPage()
        {
            if(driver.FindElement(By.XPath("//a[text()='About FTA']")).Displayed)
            {
                Console.WriteLine("About Page displayed");
            }
        }


        [When(@"Clicks on Funding link")]
        public void WhenClicksOnFundingLink()
        {
            driver.FindElement(By.XPath("//a[text()='Funding']")).Click();
        }


        [When(@"Verify Funding page")]
        public void WhenVerifyFundingPage()
        {
            if (driver.FindElement(By.XPath("//a[text()='Funding']")).Displayed)
            {
                Console.WriteLine("Funding Page displayed");                
            }
        }


        [When(@"Clicks on regulation and programs link")]
        public void WhenClicksOnRegulationAndProgramsLink()
        {
            driver.FindElement(By.XPath("//a[text()='Regulations & Programs']")).Click();
        }

        [When(@"Verify Regulation Programs page")]
        public void WhenVerifyRegulationProgramsPage()
        {
            if (driver.FindElement(By.XPath("//a[text()='Regulations & Programs']")).Displayed)
            {
                Console.WriteLine("Regulations & Programs Page displayed");                                  
            }
        }




    }
}
