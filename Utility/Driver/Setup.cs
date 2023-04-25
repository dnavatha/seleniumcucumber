using System;
using System.Collections.Generic;
using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using TechTalk.SpecFlow;
using FTADOTAutomation.Helpers;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using System.Linq;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium.Edge;

namespace FTADOTAutomation.Driver
{
    [Binding]
    public class Setup
    {
        private static IObjectContainer _objectContainer;
        public static IWebDriver driver = null;
        public static IWebDriver driverTemp;
        public static String SourceDir = Environment.GetEnvironmentVariable("SourceDir/../net5.0/") ?? "";
        public static Dictionary<int, int> TestCaseOutcome = new Dictionary<int, int> ();
        public static bool isNoBrowserFeature = false;

        public Setup( )
        {
            driver = UserActions.WebDriver;
        }

                
            
        
    }
}
