using OpenQA.Selenium;
using System;
using System.IO;
using System.Text;
using TechTalk.SpecFlow;
using FTADOTAutomation.Driver;
using System.Drawing;
using System.Drawing.Imaging;
using BoDi;
using FTADOTAutomation.Hooks.BrowserDriver;

namespace FTADOTAutomation
{
    public class ScreenShot
    {
        IWebDriver Driver;

        private readonly ObjectContainer objectContainer;
        public ScreenShot(IWebDriver driver)
        {
            Driver = driver;
        }

        public string TakeScreenShotAsBase64()
        {

            ITakesScreenshot takesScreenshot = Driver as ITakesScreenshot;
            var screenshot = takesScreenshot.GetScreenshot();

            return screenshot.AsBase64EncodedString;

        }

       
       
    }
}
