using BoDi;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace FTADOTAutomation.Hooks.BrowserDriver
{
    [Binding]
    public class DriverManager :HookBase
    {
        public DriverManager(IObjectContainer oc, FeatureContext fc, IConfiguration config) : base(oc, fc, config)
        {

        }

        [BeforeScenario(Order = 2)]
        public static void BeforeScenario(FeatureContext fc, ObjectContainer oc)
        {
            var BrowserIndicator = new BrowserIndicator();
            if (!fc.FeatureInfo.Tags.Contains("NoBrowser"))
            {
                BrowserIndicator.isNowBrowserFeature = false;
                InvokeNewDriver(oc);
            }
            else
            {
                BrowserIndicator.isNowBrowserFeature = true;
            }
            oc.RegisterInstanceAs(BrowserIndicator);
        }

        private static void InvokeNewDriver(ObjectContainer oc)
        {
            oc.Resolve<BrowserSupport>().GetWebDriver();
        }

        public class BrowserIndicator
        {
            public bool isNowBrowserFeature = false;
        }

    }
}
