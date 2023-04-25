using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;

using BoDi;
using OpenQA.Selenium.Chrome;
using Microsoft.Extensions.Configuration;
using AventStack.ExtentReports.Reporter;

namespace FTADOTAutomation.Hooks
{
    [Binding]
    public class HookBase 
    {
        protected IObjectContainer ObjectContainer;
        protected FeatureContext FeatureContext;
        protected IConfiguration Configuration;



        public HookBase(IObjectContainer objectContainer, FeatureContext fc, IConfiguration config)
        {
            FeatureContext = fc;
            ObjectContainer = objectContainer;
            Configuration = config;
        }

           



    }
}
