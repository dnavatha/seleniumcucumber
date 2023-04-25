using BoDi;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace FTADOTAutomation.Hooks.BrowserDriver
{
    public class BrowserSupport
    {

        private readonly ScenarioContext scenarioContext = ScenarioContext.Current;
        private IObjectContainer _browserObjectContainer;
        private IWebDriver _browserCreated;


        public BrowserSupport()
        {
            _browserObjectContainer = scenarioContext.ScenarioContainer;
        }

        public IWebDriver GetWebDriver()
        {
            if(_browserCreated == null)
            {
                _browserCreated = _browserObjectContainer.Resolve<DriverInstance>().GetWebDriver();
            }
            return _browserCreated;
        }
        
    }
}
